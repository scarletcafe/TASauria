# -*- coding: utf-8 -*-

"""

tasauria.__main__
==================

Script that is run when using `python -m tasauria`.

This drops you into an IPython shell where `emu` is an already-connected TASauria instance.


:copyright: (c) 2025-present Devon (scarlet.cafe) R
:license: MIT, see LICENSE for more details.

"""

import logging
import sys
import typing

import click
import IPython
from traitlets.config import Config


STARTUP_SCRIPT = """

import click
from tasauria import TASauria

click.secho('Attempting to connect on: ', fg='green', nl=False)
click.secho(TASAURIA_URL, fg='blue', nl=False)
click.secho(' ...', fg='green')

try:
    emu = TASauria(TASAURIA_URL)
    await emu.connect()
except Exception as error:
    raise RuntimeError("Failed to connect to TASauria server") from error
else:
    click.secho('Successfully connected, access emulator using ', fg='green', nl=False)
    click.secho('`', fg='black', nl=False)
    click.secho('emu', fg='white', nl=False)
    click.secho('`', fg='black')

VERSION_INFO = await emu.get_version_info()
GAME_INFO = await emu.get_game_info()

if VERSION_INFO.is_development_version:
    BIZHAWK_VERSION = f'BizHawk {VERSION_INFO.git_hash} (dev)'
else:
    BIZHAWK_VERSION = f'BizHawk {VERSION_INFO.stable_version}'

if GAME_INFO.loaded:
    click.secho(f'{BIZHAWK_VERSION} playing ', fg='green', nl=False)
    click.secho(GAME_INFO.name, fg='blue', nl=False)
    click.secho(f" ({GAME_INFO.hash})", fg='cyan', nl=False)
    click.secho(' on ', fg='green', nl=False)
    click.secho(GAME_INFO.system, fg='blue')
else:
    click.secho("No game loaded", fg='green')

""".strip().split("\n\n")


LOG_FORMAT: logging.Formatter = logging.Formatter('%(asctime)s:%(levelname)s:%(name)s: %(message)s')
LOG_STREAM: logging.Handler = logging.StreamHandler(stream=sys.stdout)
LOG_STREAM.setFormatter(LOG_FORMAT)


@click.command()
@click.option('--log-level', '-v', default='INFO')
@click.option('--log-file', '-l', default=None)
@click.option('--port', '-p', default=20251)
@click.option('--url', '-u', default=None)
def entrypoint(
    log_level: str,
    log_file: typing.Optional[str] = None,
    port: int = 20251,
    url: typing.Optional[str] = None,
):

    logger = logging.getLogger()
    logger.setLevel(getattr(logging, log_level))
    logger.addHandler(LOG_STREAM)

    if log_file:
        log_file_handler: logging.Handler = logging.FileHandler(filename=log_file, encoding='utf-8', mode='a')
        log_file_handler.setFormatter(LOG_FORMAT)
        logger.addHandler(log_file_handler)

    if url is None:
        url = f"http://127.0.0.1:{port}/"

    config = Config()
    config.InteractiveShellApp.exec_lines = [
        f"TASAURIA_URL = {url!r}",
        *STARTUP_SCRIPT
    ]

    IPython.start_ipython(
        using='asyncio',
        config=config
    )


if __name__ == '__main__':
    entrypoint()
