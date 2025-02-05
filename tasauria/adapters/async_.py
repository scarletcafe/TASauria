# -*- coding: utf-8 -*-

"""
tasauria.adapters.async_
~~~~~~~~~~~~~~~~~~~~~~~~~

Async (asyncio) implementations of TASauria adapters

:copyright: (c) 2025-present Devon (scarlet.cafe) R
:license: MIT, see LICENSE for more details.

"""

import asyncio
import typing

import aiohttp
import yarl

from tasauria.commands import Command, PythonCommandInput, ServerCommandInput, ServerCommandOutput, PythonCommandOutput
from tasauria.exceptions import AdapterDisconnected


AsyncAdapterOrSubclass = typing.TypeVar("AsyncAdapterOrSubclass", bound="AsyncAdapter")


class AsyncAdapter:
    """
    Abstract base class for asynchronous TASauria adapters
    """

    @classmethod
    async def connect(
        cls: typing.Type[AsyncAdapterOrSubclass],
        url: typing.Union[yarl.URL, str]
    ) -> AsyncAdapterOrSubclass:
        """
        Creates an adapter of this kind by connecting to a URL.
        """

        raise NotImplementedError()

    async def stop(
        self
    ) -> None:
        """
        Performs any cleanup that's required to 'stop' this adapter.
        """

        raise NotImplementedError()

    async def execute_command(
        self,
        command: typing.Type[Command[PythonCommandInput, ServerCommandInput, ServerCommandOutput, PythonCommandOutput]],
        **kwargs: PythonCommandInput
    ) -> PythonCommandOutput:
        """
        Uses this adapter's implementation to marshal, send, await receipt, and unmarshal a command and its response.
        """

        raise NotImplementedError()


class HTTPAdapter(AsyncAdapter):
    def __init__(
        self,
        session: aiohttp.ClientSession,
        url: yarl.URL,
    ):
        self._session: aiohttp.ClientSession = session
        self._url = url
        self._sequence_1: int = 0
        self._sequence_2: int = 0

    def next_sequence_id(
        self
    ) -> typing.Tuple[int, int]:
        """
        Generates a sequenced message ID to use for tracking simultaneous requests.

        The way we do this is way more conservative than is actually necessary, but it should be pretty failure-proof.
        """

        sequence_id = (self._sequence_2, self._sequence_1)

        self._sequence_1 += 1
        if self._sequence_1 >= 8_000_000:
            self._sequence_2 += 1
            self._sequence_1 -= 8_000_000

        return sequence_id

    @classmethod
    async def connect(
        cls,
        url: typing.Union[yarl.URL, str]
    ) -> "HTTPAdapter":
        session = aiohttp.ClientSession()

        if not isinstance(url, yarl.URL):
            url = yarl.URL(url)

        return cls(
            session,
            url
        )

    async def stop(
        self
    ) -> None:
        await self._session.close()

    async def execute_command(
        self,
        command: typing.Type[Command[PythonCommandInput, ServerCommandInput, ServerCommandOutput, PythonCommandOutput]],
        **kwargs: typing.Any
    ) -> PythonCommandOutput:
        input_command, input_payload = command.marshal_input(**kwargs)
        sequence_id = self.next_sequence_id()

        async with self._session.post(
            self._url.with_path(input_command),
            json={
                "messageIdentifier": sequence_id,
                **input_payload
            }
        ) as request:
            response = await request.json()

        return command.demarshal_output(response, **kwargs)


class WebSocketAdapter(AsyncAdapter):
    def __init__(
        self,
        session: aiohttp.ClientSession,
        socket: aiohttp.ClientWebSocketResponse
    ):
        self._session: aiohttp.ClientSession = session
        self._socket: aiohttp.ClientWebSocketResponse = socket
        self._sequence_1: int = 0
        self._sequence_2: int = 0
        self._response_listeners: typing.Dict[
            typing.Tuple[int, int],
            asyncio.Future[typing.Dict[str, typing.Any]]
        ] = {}
        self._closed: asyncio.Event = asyncio.Event()
        self._task = asyncio.create_task(self.loop())

    def next_sequence_id(
        self
    ) -> typing.Tuple[int, int]:
        """
        Generates a sequenced message ID to use for tracking simultaneous requests.

        The way we do this is way more conservative than is actually necessary, but it should be pretty failure-proof.
        """

        sequence_id = (self._sequence_2, self._sequence_1)

        self._sequence_1 += 1
        if self._sequence_1 >= 8_000_000:
            self._sequence_2 += 1
            self._sequence_1 -= 8_000_000

        return sequence_id

    async def loop(
        self
    ):
        cancel = asyncio.create_task(self._closed.wait())
        heartbeat_task = asyncio.create_task(asyncio.sleep(5))
        receive_future = asyncio.create_task(self._socket.receive_json())

        while not self._closed.is_set():
            done, _ = await asyncio.wait((
                receive_future,
                heartbeat_task,
                cancel
            ), return_when=asyncio.FIRST_COMPLETED)

            if heartbeat_task in done:
                heartbeat_task = asyncio.create_task(asyncio.sleep(5))
                await self._socket.send_json({
                    "command": "/ping",
                    "messageIdentifier": self.next_sequence_id(),
                })

            if receive_future in done:
                try:
                    response: typing.Dict[str, typing.Any] = await receive_future  # type: ignore
                except aiohttp.WebSocketError:
                    break

                receive_future = asyncio.create_task(self._socket.receive_json())

                if "messageIdentifier" in response:
                    message_id = response["messageIdentifier"]

                    if isinstance(message_id, list):
                        message_id: typing.Tuple[int, ...] = tuple(message_id)

                    if message_id in self._response_listeners:
                        self._response_listeners[message_id].set_result(response)

            if cancel in done:
                break

        await self._socket.close()
        await self._session.close()

    @classmethod
    async def connect(
        cls,
        url: typing.Union[yarl.URL, str]
    ) -> "WebSocketAdapter":
        session = aiohttp.ClientSession()
        socket = await session.ws_connect(url)

        return cls(
            session,
            socket
        )

    async def stop(
        self
    ) -> None:
        self._closed.set()

    async def execute_command(
        self,
        command: typing.Type[Command[PythonCommandInput, ServerCommandInput, ServerCommandOutput, PythonCommandOutput]],
        **kwargs: typing.Any
    ) -> PythonCommandOutput:
        if self._closed.is_set():
            raise AdapterDisconnected()

        input_command, input_payload = command.marshal_input(**kwargs)
        sequence_id = self.next_sequence_id()

        # Create listener for this sequence ID in advance so we have no chance of missing it
        future: asyncio.Future[typing.Dict[str, typing.Any]] = asyncio.Future()
        self._response_listeners[sequence_id] = future

        await self._socket.send_json({
            "command": input_command,
            "messageIdentifier": sequence_id,
            **input_payload
        })

        try:
            task = asyncio.create_task(asyncio.wait_for(future, 15.0))

            done, pending = await asyncio.wait((
                task,
                asyncio.create_task(self._closed.wait())
            ), return_when=asyncio.FIRST_COMPLETED)

            for pending_task in pending:
                pending_task.cancel()

            if task in done:
                response: ServerCommandOutput = typing.cast(
                    ServerCommandOutput,
                    await task
                )
            else:
                raise AdapterDisconnected()
        finally:
            self._response_listeners.pop(sequence_id, None)

        return command.demarshal_output(response, **kwargs)
