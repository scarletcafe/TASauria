
# Meta commands (`/meta`)

## Ping
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/meta/ping
```
::: code-group
```typescript [Argument schema]
{
    /* none */
}
```
```typescript [Response schema]
{
    /* Always true, to indicate the ping was received */
    pong: true
}
```
```json [Example arguments]
{}
```
```json [Example response]
{
    "pong": true,
    "status": 200,
    "messageIdentifier": null
}
```
:::

Returns a simple pong message to the command.

This is mainly for the benefit of WebSocket clients and for the initialization phase of all clients in order to test that the connection to the server is actually functional.


## Batch
::: danger SECURITY
This command does not have any of its own security requirements, but it still imposes the security requirements of the commands it invokes.

If **ANY** of the commands specified do not resolve (e.g. because of an invalid path), or any of them fail their respective security check, in order to make the operation atomic **NONE** of the commands will be run.

That means, for instance, if you invoke both `/joypad/set` (which has its permission granted by default) and `/memory/writerange` (which does **NOT** have its permission granted by default) within the same batch, if the user does not have '**Allow writing memory**' enabled in the TASauria plugin security settings, neither the memory write nor the joypad update will occur.
:::
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/meta/batch
```
::: code-group
```typescript [Argument schema]
{
    commands: {
        command: string,
        [argument_name: string]: any
    }[]
}
```
```typescript [Response schema]
{
    /* The output of the respective commands */
    output: {
        [output_name: string]: any,
        status: number
    }[]
}
```
```json [Example arguments]
{
    "commands": [
        {"command": "/client/version"},
        {"command": "/client/framestatus"}
    ]
}
```
```json [Example response]
{
    "output": [
        {
            "stableVersion": "2.10",
            "releaseDate": "January 7, 2025",
            "gitBranch": "HEAD",
            "gitHash": "dd232820493c05296c304b64bf09c57ff1e4812f",
            "gitRevision": "22067",
            "isDevelopmentVersion": false,
            "customBuildString": null,
            "status": 200,
            "messageIdentifier": null
        },
        {
            "cycleCount": 0,
            "frameCount": 0,
            "lagCount": 0,
            "lagged": false,
            "status": 200
        }
    ],
    "status": 200,
    "messageIdentifier": null
}
```
:::

Allows multiple commands to be executed within the same request.

The commands are executed on the main thread, back to back, in the order they are provided, without yielding.

The utility of this is that you can guarantee the emulator does not advance on its own between executing these commands.

For instance, if you run `/client/framestatus`, and then some N number of memory reads via `/memory/readrange`, you can guarantee that all of those memory reads come from the exact frame reported.

In comparison, if the commands were executed separately, the memory reads may occur on different frames from both the frame status check and each other, making it difficult to guarantee much about the data you are receiving.

This method only allows execution of commands in the case where all the arguments to the respective commands are known at the time of request.
It isn't possible to pivot or otherwise script the values you handle from within the command itself.

If you need to do calculations based on the data you retrieve, e.g. to set the joypad input of a frame based on what is in memory, you can pause the emulator using `/client/pause`, call the commands separately, and then advance manually with `/client/frameadvance`.
