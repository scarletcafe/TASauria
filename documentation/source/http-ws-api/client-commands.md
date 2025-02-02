
# Client commands (`/client`)


## Board name
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/client/boardname
```
::: code-group
```typescript [Argument schema]
{
    /* none */
}
```
```typescript [Response schema]
{
    /* The board name */
    boardName: string,
}
```
```json [Example arguments]
{}
```
```json [Example response]
{
    "boardName": "NROM",
    "status": 200,
    "messageIdentifier": null
}
```
:::

Returns the currently loaded board name.
If there is no core loaded, or the current core does not have different board types, `boardName` will be an empty string `""`.

This is equivalent to the Lua `emu.getboardname()`.


## Cycle count
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/client/cyclecount
```
::: code-group
```typescript [Argument schema]
{
    /* none */
}
```
```typescript [Response schema]
{
    /* The total executed cycles at the time of the command execution */
    cycleCount: number,
}
```
```json [Example arguments]
{}
```
```json [Example response]
{
    "cycleCount": 100,
    "status": 200,
    "messageIdentifier": null
}
```
:::

Returns the total number of executed cycles.

If no core is loaded, `cycleCount` will be `0`.

If the loaded core doesn't support cycle counting, `cycleCount` will be `0`.

This is equivalent to the Lua `emu.totalexecutedcycles()`.


## Display type
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/client/displaytype
```
::: code-group
```typescript [Argument schema]
{
    /* none */
}
```
```typescript [Response schema]
{
    /* The display type, e.g. PAL or NTSC */
    displayType: string,
}
```
```json [Example arguments]
{}
```
```json [Example response]
{
    "displayType": "PAL",
    "status": 200,
    "messageIdentifier": null
}
```
:::

Returns the display type determined by the current ROM and core.
If no core is currently loaded, `displayType` will be an empty string `""`.

This is equivalent to the Lua `emu.getdisplaytype()`.


## Frame advance
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/client/frameadvance
```
::: code-group
```typescript [Argument schema]
{
    /* Whether the emulator should be unpaused after this frame has advanced. */
    /* If this is false, then the emulator will be paused after this frame, regardless of whether it was paused or not before. */
    /* If this is true, then the emulator will be unpaused after this frame, regardless of whether it was paused or not before. */
    /* If this is null, then it will leave the emulator in whatever pause state it was previously in. */
    unpause: boolean?
}
```
```typescript [Response schema]
{
    /* The frame the emulator was on BEFORE the frame was advanced */
    frameCount: number,
}
```
```json [Example arguments]
{
    "unpause": false
}
```
```json [Example response]
{
    "frameCount": 100,
    "status": 200,
    "messageIdentifier": null
}
```
:::

Advances the active emulation core by 1 frame.

This is equivalent to the Lua `emu.frameadvance()`.


## Frame count
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/client/framecount
```
::: code-group
```typescript [Argument schema]
{
    /* none */
}
```
```typescript [Response schema]
{
    /* The frame the emulator was on at the time of the command execution */
    frameCount: number,
}
```
```json [Example arguments]
{}
```
```json [Example response]
{
    "frameCount": 100,
    "status": 200,
    "messageIdentifier": null
}
```
:::

Returns the current frame in the core.
If no core is loaded, `frameCount` will be `0`.

This is equivalent to the Lua `emu.framecount()`.


## Game info
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/client/game
```
::: code-group
```typescript [Argument schema]
{
    /* none */
}
```
```typescript [Response schema]
{
    /* Whether a game is currently loaded or not. */
    loaded: boolean,
    /* The name of the game that is loaded. */
    name: string,
    /* The system the game is for. */
    system: string,
    /* The region the game is for. */
    region: string,
    /* The board type the game uses. */
    boardType: string,
    /* The unique game hash */
    hash: string,
    /* Whether this ROM itself is in the database or not */
    inDatabase: boolean,
    /* The status of this ROM in the BizHawk database */
    /* Can be any of:          */
    /* - "GoodDump"            */
    /* - "BadDump"             */
    /* - "Homebrew"            */
    /* - "TranslatedRom"       */
    /* - "Hack"                */
    /* - "Unknown"             */
    /* - "Bios"                */
    /* - "Overdump"            */
    /* - "NotInDatabase"       */
    /* - "Imperfect"           */
    /* - "Unimplemented"       */
    /* - "NotWorking"          */
    databaseStatus: string,
    /* Whether this game dump is considered 'bad' or not in the database */
    databaseStatusBad: boolean,
    /* The options associated with the current game. */
    /* These are highly-core specific and you will need to test with your own game if you need to pull something from this. */
    gameOptions: {
        [name: string]: string
    }
}
```
```json [Example arguments]
{}
```
```json [Example response]
{
    "loaded": true,
    "name": "Super Mario Bros.",
    "system": "NES",
    "region": "",
    "boardType": "NROM",
    "hash": "EA343F4E445A9050D4B4FBAC2C77D0693B1D0922",
    "inDatabase": true,
    "databaseStatus": "GoodDump",
    "databaseStatusBad": false,
    "gameOptions": {},
    "status": 200,
    "messageIdentifier": null
}
```
:::

Returns information about the currently loaded game.

If no game or core is loaded, `loaded` will be `false` and most of the fields will be either `null` or some default value.

This is equivalent to these Lua functions:
- `gameinfo.getboardtype()`,
- `gameinfo.getoptions()`,
- `gameinfo.getromhash()`,
- `gameinfo.getromname()`,
- `gameinfo.getstatus()`,
- `gameinfo.indatabase()`,
- `gameinfo.isstatusbad()`


## Lag count
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/client/lagcount
```
::: code-group
```typescript [Argument schema]
{
    /* none */
}
```
```typescript [Response schema]
{
    /* The amount of lag frames elapsed since the core booted. */
    lagCount: number,
}
```
```json [Example arguments]
{}
```
```json [Example response]
{
    "lagCount": 37,
    "status": 200,
    "messageIdentifier": null
}
```
:::

Returns the amount of lagged frames so far.
If no core is loaded, `lagCount` will be `0`.

This is equivalent to the Lua `emu.lagcount()`.


## Is lagged
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/client/lagged
```
::: code-group
```typescript [Argument schema]
{
    /* none */
}
```
```typescript [Response schema]
{
    /* Whether the frame that the command was executed was a lag frame. */
    lagged: boolean,
}
```
```json [Example arguments]
{}
```
```json [Example response]
{
    "lagged": false,
    "status": 200,
    "messageIdentifier": null
}
```
:::

Returns whether the current frame is a lag frame or not.
If no core is loaded, `lagged` will be `false`.

This is equivalent to the Lua `emu.islagged()`.


## Pause
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/client/pause
```
::: code-group
```typescript [Argument schema]
{
    /* Optionally sets the current pause state. */
    /* If this is true, the emulator will pause. */
    /* If this is false, the emulator will unpause. */
    /* If this is null, the pause state will not change (e.g. if you only want to check the pause state) */
    set: boolean?
}
```
```typescript [Response schema]
{
    /* Whether the emulator was paused before this action. */
    paused: boolean,
}
```
```json [Example arguments]
{
    "set": true
}
```
```json [Example response]
{
    "paused": false,
    "status": 200,
    "messageIdentifier": null
}
```
:::

Returns the current pause state, and optionally sets a new one.

If `set` is not provided, then the pause state is left unchanged.
You can use this with e.g. HTTP `GET` to only query the pause state.

If `set` IS provided, the emulator is either paused or unpaused respectively.

The `paused` field in the response payload corresponds to whether the emulator was paused BEFORE it was set - e.g. a `"set": true` response may get a `"paused": false` response if the emulator was unpaused up until that point.

This is equivalent to the Lua `client.ispaused()`, `client.pause()`, and `client.unpause()`.


## Pause AV
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/client/pauseav
```
::: code-group
```typescript [Argument schema]
{
    /* Optionally sets the current A/V pause state. */
    /* If this is true, the A/V will pause. */
    /* If this is false, the A/V will unpause. */
    /* If this is null, the A/V pause state will not change (e.g. if you only want to check the pause state) */
    set: boolean?
}
```
```typescript [Response schema]
{
    /* Whether the A/V was paused before this action. */
    paused: boolean,
}
```
```json [Example arguments]
{
    "set": true
}
```
```json [Example response]
{
    "paused": false,
    "status": 200,
    "messageIdentifier": null
}
```
:::

Returns the current A/V pause state, and optionally sets a new one.

If `set` is not provided, then the A/V pause state is left unchanged.
You can use this with e.g. HTTP `GET` to only query the pause state.

If `set` IS provided, the A/V is either paused or unpaused respectively.

The `paused` field in the response payload corresponds to whether the A/V was paused BEFORE it was set - e.g. a `"set": true` response may get a `"paused": false` response if the A/V was unpaused up until that point.

You should be warned that pausing/unpausing A/V is somewhat crash-prone when not using one of the raw (e.g. PNG or AVI) output methods.
Consider doing this another way.

This is equivalent to the Lua `client.pause_av()` and `client.unpause_av()`.


## Seek
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/client/seek
```
::: code-group
```typescript [Argument schema]
{
    /* Optionally sets a frame to seek to. */
    /* If this is present, the emulator will begin seeking to this frame. */
    /* If this is null, the seek state will not change (e.g. if you only want to check the seek state) */
    frame: boolean?
}
```
```typescript [Response schema]
{
    /* Whether the emulator was seeking before this action. */
    seeking: boolean,
}
```
```json [Example arguments]
{
    "frame": 1050,
}
```
```json [Example response]
{
    "seeking": false,
    "status": 200,
    "messageIdentifier": null
}
```
:::

Returns the current seek state, and optionally sets a new seek target.

This is equivalent to the Lua `client.isseeking()` and `client.seekframe()`.


## System ID
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/client/systemid
```
::: code-group
```typescript [Argument schema]
{
    /* none */
}
```
```typescript [Response schema]
{
    /* The system ID represented by the current core */
    systemID: string,
}
```
```json [Example arguments]
{}
```
```json [Example response]
{
    "systemID": "N64",
    "status": 200,
    "messageIdentifier": null
}
```
:::

Returns the system ID of the currently loaded core.
If there is no core loaded, `systemID` will be the string `"NULL"`

This is equivalent to the Lua `emu.getdisplaytype()`.


## Turbo
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/client/turbo
```
::: code-group
```typescript [Argument schema]
{
    /* none */
}
```
```typescript [Response schema]
{
    /* Whether the emulator was turboing when the command was executed. */
    turboing: boolean,
}
```
```json [Example arguments]
{}
```
```json [Example response]
{
    "turboing": false,
    "status": 200,
    "messageIdentifier": null
}
```
:::

Returns the current turbo state.

This is equivalent to the Lua `client.isturbo()`.


## Version
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/client/version
```
::: code-group
```typescript [Argument schema]
{
    /* none */
}
```
```typescript [Response schema]
{
    /* The stable version number associated with this build. */
    /* If this is a development build, this is the last release version before it. */
    stableVersion: string,
    /* The release date of the last stable build in natural English. */
    releaseDate: string,
    /* The git branch used for this build. */
    gitBranch: string,
    /* The git hash from which this build was made. */
    gitHash: string,
    /* The git revision (commit number) from which this build was made. */
    gitRevision: string,
    /* Whether this is a development version or not. */
    isDevelopmentVersion: boolean,
    /* A custom build string, if applicable. */
    customBuildString: string?
}
```
```json [Example arguments]
{}
```
```json [Example response]
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
}
```
:::

Returns information about the version of BizHawk the plugin is running in.

This command reads the information from the assembly, and so it does not interrupt the main thread at all, and will still function even if the emulator is locked up.

This is equivalent to the Lua `client.getversion()`.
