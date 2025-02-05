# -*- coding: utf-8 -*-

"""
tasauria.client
~~~~~~~~~~~~~~~~

Implementation of the client

:copyright: (c) 2025-present Devon (scarlet.cafe) R
:license: MIT, see LICENSE for more details.

"""

import typing
from types import TracebackType

import yarl

from tasauria.adapters.async_ import AsyncAdapter, HTTPAdapter, WebSocketAdapter
from tasauria.commands import Command, PythonCommandInput, ServerCommandInput, ServerCommandOutput, PythonCommandOutput
from tasauria.commands.client import ClientFrameStatusCommand, ClientFrameAdvanceCommand, FrameStatus
from tasauria.commands.joypad import JoypadGetCommand
from tasauria.exceptions import AdapterDisconnected
from tasauria.types import BizHawkInput


class TASauria:
    def __init__(
        self,
        url: typing.Optional[typing.Union[yarl.URL, str]] = None,
    ):
        if url is None:
            url = "ws://127.0.0.1:20251/websocket"

        if not isinstance(url, yarl.URL):
            url = yarl.URL(url)

        if url.scheme.lower() in ('ws', 'wss'):
            self.adapter_type: typing.Type[AsyncAdapter] = WebSocketAdapter
        else:
            self.adapter_type: typing.Type[AsyncAdapter] = HTTPAdapter

        self.url = url
        self.adapter: typing.Optional[AsyncAdapter] = None

    async def __aenter__(self):
        await self.connect()
        return self

    async def __aexit__(
        self,
        _exception_type: typing.Type[BaseException],
        _exception_value: BaseException,
        _exception_traceback: TracebackType,
    ):
        return False

    async def _execute_command(
        self,
        command: typing.Type[Command[PythonCommandInput, ServerCommandInput, ServerCommandOutput, PythonCommandOutput]],
        **kwargs: typing.Any
    ) -> PythonCommandOutput:
        if self.adapter is None:
            raise AdapterDisconnected()

        return await self.adapter.execute_command(command, **kwargs)

    async def connect(self) -> None:
        if self.adapter is None:
            self.adapter = await self.adapter_type.connect(self.url)

    async def close(
        self
    ) -> None:
        if self.adapter is not None:
            await self.adapter.stop()

    # -- Commands --
    async def get_frame_status(
        self
    ) -> FrameStatus:
        return await self._execute_command(
            ClientFrameStatusCommand
        )

    async def frame_advance(
        self,
        unpause: bool = False
    ) -> FrameStatus:
        return await self._execute_command(
            ClientFrameAdvanceCommand,
            unpause=unpause
        )

    async def get_joypad(
        self,
        controller: typing.Optional[int] = None,
        with_movie: bool = False,
        immediate: bool = False,
    ) -> BizHawkInput:
        return await self._execute_command(
            JoypadGetCommand,
            controller=controller,
            with_movie=with_movie,
            immediate=immediate
        )
