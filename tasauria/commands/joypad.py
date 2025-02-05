# -*- coding: utf-8 -*-

"""
tasauria.commands.joypad
~~~~~~~~~~~~~~~~~~~~~~~~~

Joypad-related commands.

:copyright: (c) 2025-present Devon (scarlet.cafe) R
:license: MIT, see LICENSE for more details.

"""

import typing

from tasauria.commands import Command
from tasauria.systems import parse_controller_input, parse_system_input
from tasauria.types import BizHawkInput, TASauriaJoypadPayload


class JoypadGetInput(typing.TypedDict):
    controller: typing.Optional[int]
    immediate: bool
    with_movie: bool


class JoypadGetServerInput(typing.TypedDict):
    controller: typing.Optional[int]


class JoypadGetCommand(Command[JoypadGetInput, JoypadGetServerInput, TASauriaJoypadPayload, BizHawkInput]):

    @staticmethod
    def marshal_input(
        **kwargs: typing.Any,
    ) -> typing.Tuple[str, JoypadGetServerInput]:
        command: str = "/joypad/get"

        if kwargs.get("with_movie", False):
            command = "/joypad/getwithmovie"

        if kwargs.get("immediate", False):
            command = "/joypad/getimmediate"

        return (
            command,
            {
                "controller": kwargs.get("controller", None)
            }
        )

    @staticmethod
    def demarshal_output(
        payload: TASauriaJoypadPayload,
        **kwargs: typing.Any
    ) -> BizHawkInput:
        if kwargs.get("controller", None) is not None:
            return parse_controller_input(payload)
        else:
            return parse_system_input(payload)
