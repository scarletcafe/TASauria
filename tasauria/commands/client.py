# -*- coding: utf-8 -*-

"""
tasauria.commands.client
~~~~~~~~~~~~~~~~~~~~~~~~~

Client-related commands.

:copyright: (c) 2025-present Devon (scarlet.cafe) R
:license: MIT, see LICENSE for more details.

"""

import dataclasses
import typing

from tasauria.commands import Command, NoArguments


class ClientFrameStatusServerOutput(typing.TypedDict):
    cycleCount: int
    frameCount: int
    lagCount: int
    lagged: bool


@dataclasses.dataclass
class FrameStatus:
    cycle_count: int
    frame_count: int
    lag_count: int
    is_lagged: bool


class ClientFrameStatusCommand(Command[NoArguments, NoArguments, ClientFrameStatusServerOutput, FrameStatus]):

    @staticmethod
    def marshal_input(
        **kwargs: typing.Any,
    ) -> typing.Tuple[str, NoArguments]:
        return (
            "/client/framestatus",
            {}
        )

    @staticmethod
    def demarshal_output(
        payload: ClientFrameStatusServerOutput,
        **kwargs: typing.Any
    ) -> FrameStatus:
        return FrameStatus(
            cycle_count=payload["cycleCount"],
            frame_count=payload["frameCount"],
            lag_count=payload["lagCount"],
            is_lagged=payload["lagged"]
        )


class ClientFrameAdvanceInput(typing.TypedDict):
    unpause: bool


class ClientFrameAdvanceCommand(Command[ClientFrameAdvanceInput, ClientFrameAdvanceInput, ClientFrameStatusServerOutput, FrameStatus]):

    @staticmethod
    def marshal_input(
        **kwargs: typing.Any,
    ) -> typing.Tuple[str, ClientFrameAdvanceInput]:
        return (
            "/client/frameadvance",
            {
                "unpause": kwargs.get("unpause", None)
            }
        )

    @staticmethod
    def demarshal_output(
        payload: ClientFrameStatusServerOutput,
        **kwargs: typing.Any
    ) -> FrameStatus:
        return FrameStatus(
            cycle_count=payload["cycleCount"],
            frame_count=payload["frameCount"],
            lag_count=payload["lagCount"],
            is_lagged=payload["lagged"]
        )
