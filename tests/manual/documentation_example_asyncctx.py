# -*- coding: utf-8 -*-

"""

tests.manual.documentation_example_asyncctx
>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

This is a verbatim copy of one of the "basic examples" in the documentation.

It's in tests because, if it doesn't work, it's going to confuse a lot of new users who might try it.

:copyright: (c) 2025-present Devon (scarlet.cafe) R
:license: MIT, see LICENSE for more details.

"""

import asyncio

from tasauria import TASauria


async def main():
    async with TASauria("ws://127.0.0.1:20251/websocket") as emu:
        frame_status = await emu.get_frame_status()
        print(frame_status)
        # => FrameStatus(cycle_count=0, frame_count=1009, lag_count=39, is_lagged=False)


asyncio.run(main())
