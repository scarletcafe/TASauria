# -*- coding: utf-8 -*-

"""

tests.manual.frame_advance_speed
>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

This manual test simply loops frame advances and sees how fast it can go.

This can be used to test performance regressions.

:copyright: (c) 2025-present Devon (scarlet.cafe) R
:license: MIT, see LICENSE for more details.

"""

import asyncio
import logging
import statistics
import sys
import time
import typing

import click

from tasauria import TASauria


LOG_FORMAT: logging.Formatter = logging.Formatter('%(asctime)s:%(levelname)s:%(name)s: %(message)s')
LOG_STREAM: logging.Handler = logging.StreamHandler(stream=sys.stdout)
LOG_STREAM.setFormatter(LOG_FORMAT)


async def run_async():
    last_reading: typing.Optional[float] = None
    readings: typing.List[float] = []

    async with TASauria() as emu:
        while True:
            frame_state = await emu.frame_advance()
            current_reading = time.perf_counter()

            if last_reading is None:
                print(f"Frame {frame_state.frame_count} (no readings..)")
            else:
                time_elapsed = current_reading - last_reading
                framerate = 1.0 / time_elapsed

                readings.append(framerate)
                if len(readings) > 20:
                    readings.pop(0)

                if len(readings) >= 2:
                    print(
                        f"Frame {frame_state.frame_count} ({framerate:.1f} fps)"
                        f" [{statistics.mean(readings):.2f} \N{PLUS-MINUS SIGN}"
                        f" {statistics.stdev(readings):.2f} of {len(readings)} readings]"
                    )
                else:
                    print(
                        f"Frame {frame_state.frame_count} ({framerate:.1f} fps)"
                    )

            last_reading = current_reading


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
