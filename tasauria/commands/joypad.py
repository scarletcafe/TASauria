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
from tasauria.systems import parse_system_input
from tasauria.types import BizHawkInput, TASauriaJoypadPayload


class JoypadGetInput(typing.TypedDict):
    controller: typing.Optional[int]


class JoypadGetCommand(Command[JoypadGetInput, JoypadGetInput, TASauriaJoypadPayload, BizHawkInput]):

    @staticmethod
    def marshal_input(
        **kwargs: typing.Any,
    ) -> typing.Tuple[str, JoypadGetInput]:

        return (
            "/joypad/get",
            {
                "controller": kwargs.get("controller", None)
            }
        )

    @staticmethod
    def demarshal_output(
        payload: TASauriaJoypadPayload
    ) -> BizHawkInput:
        return parse_system_input(payload)
