
# Client commands (`/client`)


## Close ROM
::: danger SECURITY
Setting the A/V pause state requires '**Allow loading and closing ROMs**' to be enabled in the TASauria plugin security settings.
This permission is usually **DISABLED** by default, and must be enabled by the user to be accessible.
:::
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/client/closerom
```
::: code-group
```typescript [Argument schema]
{
    /* none */
}
```
```typescript [Response schema]
{
    /* Whether the command succeeded. This should always be true. */
    success: boolean,
}
```
```json [Example arguments]
{}
```
```json [Example response]
{
    "success": true,
    "status": 200,
    "messageIdentifier": null
}
```
:::

Closes the currently open ROM.

This is equivalent to the Lua `client.closerom()`.


## Frame advance
::: warning SECURITY
This command requires '**Allow client control**' to be enabled in the TASauria plugin security settings.
This permission is usually enabled by default.
:::
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
    unpause?: boolean
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


## Frame status
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/client/framestatus
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
    /* The frame the emulator was on at the time of the command execution */
    frameCount: number,
    /* The amount of lag frames elapsed since the core booted. */
    lagCount: number,
    /* Whether the frame that the command was executed was a lag frame. */
    lagged: boolean,
}
```
```json [Example arguments]
{}
```
```json [Example response]
{
    "cycleCount": 5804814,
    "frameCount": 195,
    "lagCount": 6,
    "lagged": false,
    "status": 200,
    "messageIdentifier": null
}
```
:::

Returns information about the current frame status of the core.

If no core is loaded or if the core doesn't support the counting type, some fields will be the default `0` or `false`.

This is equivalent to these Lua functions:
- `emu.totalexecutedcycles()`,
- `emu.framecount()`,
- `emu.lagcount()`,
- `emu.islagged()`


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
    /* The board type the game uses. */
    boardType: string,
    /* The region the game is for. */
    region: string,
    /* The display type the game uses. */
    displayType: string,
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
    "boardType": "NROM",
    "region": "",
    "displayType": "NTSC",
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
- `gameinfo.getboardtype()` or `emu.getboardname()`,
- `gameinfo.getoptions()`,
- `gameinfo.getromhash()`,
- `gameinfo.getromname()`,
- `gameinfo.getstatus()`,
- `gameinfo.indatabase()`,
- `gameinfo.isstatusbad()`,
- `emu.getdisplaytype()`,
- `emu.getsystemid()`



## Load ROM
::: danger SECURITY
Setting the A/V pause state requires '**Allow loading and closing ROMs**' to be enabled in the TASauria plugin security settings.
This permission is usually **DISABLED** by default, and must be enabled by the user to be accessible.
:::
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/client/loadrom
```
::: code-group
```typescript [Argument schema]
{
    /* The binary data of the ROM encoded as a Base64-encoded string. */
    data: string,
}
```
```typescript [Response schema]
{
    /* Whether the command succeeded. This could be false if the ROM is of an unknown type or so on. */
    success: boolean,
}
```
```json [Example arguments]
{}
```
```json [Example response]
{
    "success": true,
    "status": 200,
    "messageIdentifier": null
}
```
:::

Loads the ROM provided.

The `data` field should contain a Base64-encoded string that represents the raw binary data of the ROM.
This should use the RFC 4648 'standard' Base64 alphabet (i.e., using `+` and `/` as opposed to the URL-safe `-` and `_`), with the padding preserved (the trailing `=`s are not removed).

This is equivalent to the Lua `client.openrom()`.


## Pause
::: warning SECURITY
Setting the pause state requires '**Allow client control**' to be enabled in the TASauria plugin security settings.
This permission is usually enabled by default.
:::
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
    set?: boolean
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


## Reboot core
::: warning SECURITY
This command requires '**Allow client control**' to be enabled in the TASauria plugin security settings.
This permission is usually enabled by default.
:::
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/client/rebootcore
```
::: code-group
```typescript [Argument schema]
{
    /* none */
}
```
```typescript [Response schema]
{
    /* Whether the command succeeded. This should always be true. */
    success: boolean,
}
```
```json [Example arguments]
{}
```
```json [Example response]
{
    "success": true,
    "status": 200,
    "messageIdentifier": null
}
```
:::

Reboots the current core.

This is equivalent to the Lua `client.reboot_core()`.


## Seek
::: warning SECURITY
Setting a seek target requires '**Allow client control**' to be enabled in the TASauria plugin security settings.
This permission is usually enabled by default.
:::
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/client/seek
```
::: code-group
```typescript [Argument schema]
{
    /* Optionally sets a frame to seek to. */
    /* If this is present, the emulator will begin seeking to this frame. */
    /* If this is null, the seek state will not change (e.g. if you only want to check the seek state) */
    frame?: boolean
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


## Speed
::: warning SECURITY
Setting a speed requires '**Allow client control**' to be enabled in the TASauria plugin security settings.
This permission is usually enabled by default.
:::
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/client/speed
```
::: code-group
```typescript [Argument schema]
{
    /* Optionally sets a speed for the emulator to change to. */
    /* This is a percentage, so 100 is normal speed. */
    percentage?: number
}
```
```typescript [Response schema]
{
    /* The speed the emulator was running at before this action. */
    percentage: number,
}
```
```json [Example arguments]
{
    "percentage": 50,
}
```
```json [Example response]
{
    "percentage": 100,
    "status": 200,
    "messageIdentifier": null
}
```
:::

Returns the current emulation speed, and optionally sets a new target speed.

The `percentage` provided is merely a target, i.e., this sets the frame limiter.

If BizHawk completes its frame faster than required for the target speed, it will lock the main thread in order to slow down.
This means that if a low target speed is set, commands will take longer to execute and the client will be less responsive for the user.

In order to avoid scripts accidentally enforcing painfully slow target speeds, TASauria will clamp the provided percentage between 5% and 5000%.

If you need speeds slower than this, you should consider pausing the emulator instead and advancing frames manually at the desired pace.

It's still possible for the user to inadvertently slow TASauria down by manually setting a lower speed.

This is equivalent to the Lua `client.speedmode()`.


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
    customBuildString?: string
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
