
# Test commands (`/test`)

## Echo
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/test/echo[0;31m/<path-content>[0m
```
::: code-group
```typescript [Argument schema]
{
    /* The string to echo back */
    input: string
}
```
```typescript [Response schema]
{
    /* Whatever is included in the <path-content> of the command */
    /* This is to test that command parsing functions correctly. */
    pathMessage: number,
    /* Whatever is contained in the `input` of the argument payload. */
    payloadMessage: string,
}
```
```json [Example arguments]
{
    "input": "hello"
}
```
```json [Example response]
{
    "pathMessage": "",
    "payloadMessage": "hello",
    "status": 200,
    "messageIdentifier": null
}
```
:::

This command simply returns the information you give it back to you.

Unlike most other commands, this does not actually sync with the emulator and so will work regardless of if the emulator's main thread is actually responding or not.

This command is provided mostly for the purpose of testing that the server is functioning correctly.

The path argument can either be present or not present, i.e. `/test/echo` and `/test/echo/foobar` both trigger the command.
If the path argument is provided, it's included in the response as `pathMessage`.


## Wait
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/test/wait
```
::: code-group
```typescript [Argument schema]
{
    /* The time in milliseconds to wait for */
    time: number
}
```
```typescript [Response schema]
{
    /* The time the waiting started as an ISO datetime */
    timeStarted: string,
    /* The time the waiting finished as an ISO datetime */
    timeStarted: string,
}
```
```json [Example arguments]
{
    "time": 20000
}
```
```json [Example response]
{
    "timeStarted": "2025-02-01T16:24:47.4625657Z",
    "timeStopped": "2025-02-01T16:25:07.4633451Z",
    "status": 200,
    "messageIdentifier": null
}
```
:::

This command simply stalls for the amount of time requested.

This does not affect the emulator in any way because it is run on the worker thread associated with the command.

The point of this is to test that commands can run in parallel, and that locking up a server thread does not lock up the emulator.
