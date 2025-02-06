# -*- coding: utf-8 -*-

"""

examples.cookbook.n64_spin_control_stick
=========================================

This script gets the currently running game, checks if it's N64,
and then if it is, spins the control stick indefinitely.

:copyright: (c) 2025-present Devon (scarlet.cafe) R
:license: MIT, see LICENSE for more details.

"""

import asyncio
import logging
import math
import sys
import typing

import click

from tasauria import TASauria
from tasauria.systems.n64 import Nintendo64Controller


async def run_async():
    async with TASauria() as emu:
        game_info = await emu.get_game_info()
        print(game_info)

        assert game_info.system == 'N64'

        angle = 0.0
        while True:
            stick_x = math.sin(angle)
            stick_y = math.cos(angle)

            await emu.set_joypad(
                state=Nintendo64Controller(
                    x_axis=int(stick_x * 127.0),
                    y_axis=int(stick_y * 127.0)
                ),
                controller=1
            )
            await emu.frame_advance()

            angle += math.radians(1.0)


LOG_FORMAT: logging.Formatter = logging.Formatter('%(asctime)s:%(levelname)s:%(name)s: %(message)s')
LOG_STREAM: logging.Handler = logging.StreamHandler(stream=sys.stdout)
LOG_STREAM.setFormatter(LOG_FORMAT)


@click.command()
@click.option('--log-level', '-v', default='INFO')
@click.option('--log-file', '-l', default=None)
def entrypoint(
    log_level: str,
    log_file: typing.Optional[str] = None,
):

    logger = logging.getLogger()
    logger.setLevel(getattr(logging, log_level))
    logger.addHandler(LOG_STREAM)

    if log_file:
        log_file_handler: logging.Handler = logging.FileHandler(filename=log_file, encoding='utf-8', mode='a')
        log_file_handler.setFormatter(LOG_FORMAT)
        logger.addHandler(log_file_handler)

    asyncio.run(run_async())


if __name__ == '__main__':
    entrypoint()
