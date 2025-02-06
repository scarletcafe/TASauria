# -*- coding: utf-8 -*-

"""

tests.manual.documentation_basic_example
>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

This is a verbatim copy of one of the "basic examples" in the documentation.

It's in tests because, if it doesn't work, it's going to confuse a lot of new users who might try it.

:copyright: (c) 2025-present Devon (scarlet.cafe) R
:license: MIT, see LICENSE for more details.

"""

import asyncio

from tasauria import TASauria


async def main():
    emu = TASauria()
    await emu.connect()

    frame_status = await emu.get_frame_status()
    print(frame_status)

    await emu.close()


asyncio.run(main())
