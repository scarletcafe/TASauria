
# HTTP/WS API general conventions

::: warning
If you want to use TASauria from Python, you should be reading the Python API documentation.

This section of the documentation is mostly intended for:
- People who want to write other (e.g. non-Python) wrappers for the TASauria server
- People who want to interact from TASauria without any wrappers, e.g. from a scripting language where one does not exist
- People who need to test TASauria functionality
- People developing or debugging TASauria itself

If this is not you, then you can safely ignore this part of the documentation and return to the Python section.
:::

The TASauria plugin exposes a standard HTTP and WS-capable server during normal operation.
In order to interact with the server, you should first check that it is running by opening its external tool window in BizHawk.

This window also contains server-related settings, such as the host and port that the server runs on.
By default, TASauria hosts on the IPv4 lookback (i.e. `localhost`/`127.0.0.1`), and on port `20251`, but this can be changed.

For the duration of this page, we will assume the server is hosted using its default settings.

To test that the server is running, you can perform either an HTTP `GET` or HTTP `POST` request against `/test/echo`:

```ansi
 [0;32mGET[0m   [0;30mhttp://127.0.0.1:20251[0m/test/echo
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/test/echo
```

You should get a response like this:
```json
{
    "pathMessage": "",
    "payloadMessage": "",
    "status": 200,
    "messageIdentifier": null
}
```

## Commands and invocation

The TASauria server has many commands that can be used to interact with the emulator.

Commands take the form of an HTTP path, and they usually look like `/<command family>/<action>`.

For instance, there is `/memory/readrange` for reading memory, or `/client/paused` for querying and setting the pause state.

All commands return JSON payloads. Some commands accept arguments that determine what the command does or what information it returns.

There are three ways to trigger a command:

### HTTP POST

You can HTTP `POST` to the server to execute a command. The command is specified by the path given.

The payload of the request can either be a valid JSON object of the arguments, or be completely empty (which is interpreted the same as `{}`).

The returned object will be whatever the response from the command was, if it was successful.

If the command errors, the returned object will contain an `error` field of type `string`.

Example:
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/memory/readinteger
```
Payload:
```json
{
    "address": 1024,
    "size": 4,
    "domain": "RDRAM"
}
```
Response:
```json
{
    "data": 25000,
    "status": 200,
    "messageIdentifier": null
}
```

### WebSocket

TASauria exposes a WebSocket server under the path `/websocket`.

To execute commands, you send a JSON object sending both a `command` to execute and any arguments for the command.

Example:

```ansi
[0;33mWS[0m   [0;30mws://127.0.0.1:20251[0m/websocket
```
Sent payload:
```json
{
    "command": "/memory/readinteger",
    "address": 1024,
    "size": 4,
    "domain": "RDRAM"
}
```
Received payload:
```json
{
    "data": 25000,
    "status": 200,
    "messageIdentifier": null
}
```

You may have noticed the `status` and `messageIdentifier` fields returned on the response.
Because WebSocket messages don't have a status code, the status code is always included in the response payload.

The `messageIdentifier` field will contain whatever is inside the `messageIdentifier` argument sent alongside the command, or `null` if one was not set.

This can be a number, string, or even an object or array.

It's recommended that when you send a request, you generate a new identifier and include it with the payload.
This allows you to then associate the response you receive with the request that caused it, which can allow your client code to wait on multiple commands simultaneously.

Example:
```ansi
[0;33mWS[0m   [0;30mws://127.0.0.1:20251[0m/websocket
```
Sent payload:
```json
{
    "command": "/client/paused",
    "messageIdentifier": [120, 78001]
}
```
Received payload:
```json
{
    "paused": true,
    "status": 200,
    "messageIdentifier": [120, 78001]
}
```


### HTTP GET

You can HTTP `GET` to the server to execute a command. The command is specified by the path given.

HTTP `GET` requests don't contain payloads, and so this method is best for commands that don't need arguments (such as if you are querying something about the emulator).

However, you can include arguments as query parameters if they are top-level, and are of type `bool`, `int`, `float`, or `string`.

The returned object will be whatever the response from the command was, if it was successful.

If the command errors, the returned object will contain an `error` field of type `string`.

Example:
```ansi
[0;32mGET[0m   [0;30mhttp://127.0.0.1:20251[0m/memory/readinteger[0;30m?address=1024&size=4&domain=RDRAM[0m
```
Response:
```json
{
    "data": 25000,
    "status": 200,
    "messageIdentifier": null
}
```

Throughout the documentation, the command reference will be written with the assumption that you are using the HTTP `POST` interface.

All commands are possible to execute via either HTTP `POST` or the WebSocket interface.

Most commands are possible to execute via HTTP `GET`, however certain ones that require providing buffers won't work because of the limitations of query strings.
