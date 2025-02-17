
# First steps

This section assumes you have completed the previous section on [Getting Started](../getting-started).

If you haven't done so already, you should open up EmuHawk, open the TASauria window from `Tools` > `External Tool` > `TASauria` selection, and accept the security warning if it appears.

From the TASauria window, you can start and stop the server. You can also check the box to start the server automatically upon opening the window if you end up using TASauria frequently (remember to `Save current settings as default` or else the change won't persist).

Once the server is running, it's possible to connect to it from Python.

The `tasauria` Python library uses `asyncio`, a built-in module for asynchronous programming. If you've never worked with asyncio before, you may want to read about it a little bit on the [Python documentation](https://docs.python.org/3/library/asyncio.html).

Here's a basic script to give you an idea of what typical usage of `tasauria` looks like.

```python
import asyncio

from tasauria import TASauria


async def main():
    emu = TASauria()
    await emu.connect()

    frame_status = await emu.get_frame_status()
    print(frame_status)
    # => FrameStatus(cycle_count=0, frame_count=1009, lag_count=39, is_lagged=False)

    await emu.close()


asyncio.run(main())
```

The `TASauria` class is the main way of interacting with the plugin. If you give it no arguments, it defaults to trying to connect to the WebSocket exposed by the plugin server on the current machine.

If you're connecting to the server on another machine, you can pass a URL into the constructor to point it somewhere else:

```python
emu = TASauria("ws://192.168.1.5:20251/websocket")
```

TASauria supports connecting either by HTTP or WebSocket. It is recommended to use WebSocket by default as it's easier to do multiple commands at once, and it reduces the time taken to execute the command by reusing an existing connection. However, if you want to use HTTP anyway you can simply pass a URL using HTTP, and it will automatically use HTTP instead:

```python
emu = TASauria("http://192.168.1.5:20251/")
```

Regardless of which method you use, you must then `.connect()` to the server to set up the necessary resources in Python:

```python
await emu.connect()
```

Once you have connected, it's possible to execute any command using the `emu` instance.
The `tasauria` library uses strict [Pyright](https://microsoft.github.io/pyright/) type hinting, so if you write your script in an IDE that supports it, such as [Visual Studio Code](https://code.visualstudio.com/) with the [Python language support](https://marketplace.visualstudio.com/items?itemName=ms-python.python) installed, you should get autocomplete and correct type information to assist your development.

After you've done sending your commands, you should `.close()` the connection gracefully to clean everything up before stopping:

```python
await emu.close()
```

The `TASauria` class supports being used as an asynchronous context manager, which allows connection and clean-up to be done without having to manually call connect and close. Here's the same script from before, but with an explicit host and using `async with` to handle the lifecycle instead:

```python
import asyncio

from tasauria import TASauria


async def main():
    async with TASauria("ws://127.0.0.1:20251/websocket") as emu:
        frame_status = await emu.get_frame_status()
        print(frame_status)
        # => FrameStatus(cycle_count=0, frame_count=1009, lag_count=39, is_lagged=False)


asyncio.run(main())
```
