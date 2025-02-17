
# Save state commands (`/savestate`)


## Save to slot
::: danger SECURITY
This command requires '**Allow saving and loading save states**' to be enabled in the TASauria plugin security settings.
This permission is usually **DISABLED** by default, and must be enabled by the user to be accessible.
:::
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/savestate/saveslot
```
::: code-group
```typescript [Argument schema]
{
    /* The slot number to save to. */
    slot: number,
    /* Whether to suppress the On-Screen Display message when saving to this slot. */
    /* If this is null, the OSD will not be suppressed. */
    suppressOSD?: boolean,
}
```
```typescript [Response schema]
{
    /* Whether the save was successful or not. */
    success: boolean,
}
```
```json [Example arguments]
{
    "slot": 2,
    "suppressOSD": false,
}
```
```json [Example response]
{
    "success": true,
    "status": 200,
    "messageIdentifier": null
}
```
:::

Creates a save state in the requested slot.

This obeys the current context, so e.g. a save state made during a movie will be associated with that movie.

If no core is loaded this command will silently fail.

This is equivalent to the Lua `savestate.saveslot()`.


## Save
::: danger SECURITY
This command requires '**Allow saving and loading save states**' to be enabled in the TASauria plugin security settings.
This permission is usually **DISABLED** by default, and must be enabled by the user to be accessible.
:::
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/savestate/save
```
::: code-group
```typescript [Argument schema]
{
    /* none */
}
```
```typescript [Response schema]
{
    /* The binary data of the save state encoded as a Base64-encoded string. */
    data: string,
}
```
```json [Example arguments]
{}
```
```json [Example response]
{
    "data": /* .. way too long to include */,
    "status": 200,
    "messageIdentifier": null
}
```
:::

Creates a save state and returns the state as a data buffer.

The `data` field contains a Base64-encoded string that represents the raw binary data of the save state.
This uses the RFC 4648 'standard' Base64 alphabet (i.e., it uses `+` and `/` as opposed to the URL-safe `-` and `_`), and the padding is preserved (the trailing `=`s are not removed).

Arbitrary save state saving is implemented in this way so that if the script (accessing the API) and the host (running the emulator) are running on different machines there is no path confusion, and also as a security measure.

In practice the plugin saves to a temporary file and then returns the content of this file as the response.

If no core is loaded the save state returned will be a completely empty buffer.

This is equivalent to the Lua `savestate.save()`.


## Load from slot
::: danger SECURITY
This command requires '**Allow saving and loading save states**' to be enabled in the TASauria plugin security settings.
This permission is usually **DISABLED** by default, and must be enabled by the user to be accessible.
:::
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/savestate/loadslot
```
::: code-group
```typescript [Argument schema]
{
    /* The slot number to load from. */
    slot: number,
    /* Whether to suppress the On-Screen Display message when loading from. this slot. */
    /* If this is null, the OSD will not be suppressed. */
    suppressOSD?: boolean,
}
```
```typescript [Response schema]
{
    /* Whether the load was successful or not. */
    success: boolean,
}
```
```json [Example arguments]
{
    "slot": 2,
    "suppressOSD": false,
}
```
```json [Example response]
{
    "success": true,
    "status": 200,
    "messageIdentifier": null
}
```
:::

Loads a save state from the requested slot.

This obeys the current context, so e.g. if a movie is playing this will load save state slots associated with that movie.

If no core is loaded this command will fail with `success` being `false`.

This is equivalent to the Lua `savestate.loadslot()`.


## Load
::: danger SECURITY
This command requires '**Allow saving and loading save states**' to be enabled in the TASauria plugin security settings.
This permission is usually **DISABLED** by default, and must be enabled by the user to be accessible.
:::
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/savestate/load
```
::: code-group
```typescript [Argument schema]
{
    /* The binary data of the save state encoded as a Base64-encoded string. */
    data: string,
}
```
```typescript [Response schema]
{
    /* Whether the load was successful or not. */
    success: boolean,
}
```
```json [Example arguments]
{
    "data": /* .. way too long to include */,
}
```
```json [Example response]
{
    "success": true,
    "status": 200,
    "messageIdentifier": null
}
```
:::

Loads a save state from the data buffer provided.

The `data` field should contain a Base64-encoded string that represents the raw binary data of the save state.
This should use the RFC 4648 'standard' Base64 alphabet (i.e., using `+` and `/` as opposed to the URL-safe `-` and `_`), with the padding preserved (the trailing `=`s are not removed).

Arbitrary save state loading is implemented in this way so that if the script (accessing the API) and the host (running the emulator) are running on different machines there is no path confusion, and also as a security measure.

In practice the plugin writes the content of this buffer to a temporary file and then loads the save state from that.

If no core is loaded the loading will always fail with `success` being `false`.

This is equivalent to the Lua `savestate.load()`.
