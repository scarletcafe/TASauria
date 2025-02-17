
# A/V commands (`/av`)


## Pause A/V
::: danger SECURITY
Setting the A/V pause state requires '**Allow A/V control**' to be enabled in the TASauria plugin security settings.
This permission is usually **DISABLED** by default, and must be enabled by the user to be accessible.
:::
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/av/pause
```
::: code-group
```typescript [Argument schema]
{
    /* Optionally sets the current A/V pause state. */
    /* If this is true, the A/V will pause. */
    /* If this is false, the A/V will unpause. */
    /* If this is null, the A/V pause state will not change (e.g. if you only want to check the pause state) */
    set?: boolean
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


## Screenshot
::: danger SECURITY
Setting the A/V pause state requires '**Allow A/V control**' to be enabled in the TASauria plugin security settings.
This permission is usually **DISABLED** by default, and must be enabled by the user to be accessible.
:::
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/av/screenshot
```
::: code-group
```typescript [Argument schema]
{
    /* Whether to include the OSD (On Screen Display) in the screenshot. */
    /* If this is null, the OSD will not be included. */
    includeOSD?: boolean,
}
```
```typescript [Response schema]
{
    /* The binary data of the screenshot encoded as a Base64-encoded string. */
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

Creates a screenshot and returns the image (usually PNG) as a data buffer.

The `data` field contains a Base64-encoded string that represents the raw binary data of the image.
This uses the RFC 4648 'standard' Base64 alphabet (i.e., it uses `+` and `/` as opposed to the URL-safe `-` and `_`), and the padding is preserved (the trailing `=`s are not removed).

In practice the plugin saves to a temporary file and then returns the content of this file as the response.

If no core is loaded the screenshot will be from NullHawk (i.e. completely black).

This is equivalent to the Lua `client.screenshot()` and `client.setscreenshotosd()`.
