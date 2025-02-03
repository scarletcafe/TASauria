
# Movie commands (`/movie`)

## Movie info
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/movie/info
```
::: code-group
```typescript [Argument schema]
{
    /* none */
}
```
```typescript [Response schema]
{
    /* Whether a movie is currently loaded or not. */
    loaded: boolean,
    /* The mode the movie is in. */
    /* Can be any of:  */
    /* - "Inactive"    */
    /* - "Play"        */
    /* - "Record"      */
    /* - "Finished"    */
    mode: string,
    /* Whether the movie is in read-only mode or not. */
    readOnly: boolean,
    /* The filename of the movie. */
    /* Unlike the Lua API, this does not contain the full path. */
    fileName: string,
    /* Length of the movie in frames. */
    length: number,
    /* Framerate associated with the movie. */
    frameRate: number,
    /* Amount of rerecords associated with this movie so far. */
    rerecords: number
}
```
```json [Example arguments]
{}
```
```json [Example response]
{
    "loaded": true,
    "mode": "Play",
    "readOnly": true,
    "fileName": "malleoz-papermario-allcards.bk2",
    "length": 542030,
    "frameRate": 60.0,
    "rerecords": 135527,
    "status": 200,
    "messageIdentifier": null
}
```
:::

Returns information about the currently loaded movie.

If no movie or core is loaded, `loaded` will be `false` and most of the fields will be at their default values.

This is equivalent to these Lua functions:
- `movie.filename()` (except that the full file path is not given),
- `movie.getfps()`,
- `movie.getreadonly()`,
- `movie.getrerecordcount()`,
- `movie.isloaded()`,
- `movie.length()`,
- `movie.mode()` (except the modes are not capitalized)


## Movie comments
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/movie/comments
```
::: code-group
```typescript [Argument schema]
{
    /* none */
}
```
```typescript [Response schema]
{
    /* Any comments included with the movie */
    comments: string[],
}
```
```json [Example arguments]
{}
```
```json [Example response]
{
    "comments": [],
    "status": 200,
    "messageIdentifier": null
}
```
:::

Returns any comments stored with the current movie.

If no movie or core is loaded, `comments` will be an empty array `[]`.

This is equivalent to the Lua function `movie.getcomments()`.


## Movie read/write or read-only mode
::: danger SECURITY
Setting the read/write mode requires '**Allow movie management**' to be enabled in the TASauria plugin security settings.
This permission is usually **DISABLED** by default, and must be enabled by the user to be accessible.
:::
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/movie/readonly
```
::: code-group
```typescript [Argument schema]
{
    /* Optionally sets whether the movie is read-only or not */
    /* If this is true, the movie is set to read-only mode. */
    /* If this is false, the move is set to read/write mode. */
    /* If this is null, the movie read-only state will not change (e.g. if you only want to check if the movie is read-only) */
    set?: boolean
}
```
```typescript [Response schema]
{
    /* Whether the movie is set to read-only. */
    readOnly: boolean,
}
```
```json [Example arguments]
{
    "set": false
}
```
```json [Example response]
{
    "readOnly": true,
    "status": 200,
    "messageIdentifier": null
}
```
:::

Returns the current movie read-only state, and optionally sets a new one.

If `set` is not provided, then the read-only state is left unchanged.
You can use this with e.g. HTTP `GET` to only query the read-only state.

If `set` IS provided, the movie is set to either read-only or read/write respectively.

The `readOnly` field in the response payload corresponds to whether the movie was read-only BEFORE it was set - e.g. a `"set": false` response may get a `"readOnly": true` response if the movie was read-only up until that point.

This is equivalent to the Lua `movie.getreadonly()` and `movie.setreadonly()`.


## Get movie
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/movie/get
```
::: code-group
```typescript [Argument schema]
{
    /* none */
}
```
```typescript [Response schema]
{
    /* The binary data of the movie encoded as a Base64-encoded string. */
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

Retrieves the data of the movie file as it is currently on disk. This does NOT save any current changes to the movie.

The `data` field contains a Base64-encoded string that represents the raw binary data of the movie file.
This uses the RFC 4648 'standard' Base64 alphabet (i.e., it uses `+` and `/` as opposed to the URL-safe `-` and `_`), and the padding is preserved (the trailing `=`s are not removed).

If no core is loaded the movie returned will be a completely empty buffer.

There is no equivalent in Lua for this.


## Save movie
::: danger SECURITY
This command requires '**Allow movie management**' to be enabled in the TASauria plugin security settings.
This permission is usually **DISABLED** by default, and must be enabled by the user to be accessible.
:::
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/movie/save
```
::: code-group
```typescript [Argument schema]
{
    /* none */
}
```
```typescript [Response schema]
{
    /* The binary data of the movie encoded as a Base64-encoded string. */
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

Saves the movie file and returns the content as a data buffer.

This will save the movie to whatever location it was opened from. If the user loaded the movie from a file, it will overwrite that file.

The `data` field contains a Base64-encoded string that represents the raw binary data of the movie file.
This uses the RFC 4648 'standard' Base64 alphabet (i.e., it uses `+` and `/` as opposed to the URL-safe `-` and `_`), and the padding is preserved (the trailing `=`s are not removed).

If no core is loaded no saving occurs and the movie returned will be a completely empty buffer.

This is equivalent to the Lua `movie.save()` with no arguments.


## Fork movie
::: danger SECURITY
This command requires '**Allow movie management**' to be enabled in the TASauria plugin security settings.
This permission is usually **DISABLED** by default, and must be enabled by the user to be accessible.
:::
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/movie/fork
```
::: code-group
```typescript [Argument schema]
{
    /* none */
}
```
```typescript [Response schema]
{
    /* The binary data of the movie encoded as a Base64-encoded string. */
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

Saves the movie file to a new file and returns the content as a data buffer.

For security reasons, TASauria does not provide a method to perform a direct "Save as" on a loaded movie, as that would allow manipulation of the host filesystem.

Instead, this performs "Save as" onto a temporary file, and then returns the content of that file.
This way, it is possible to save and retrieve current changes to the movie *without* overwriting the original file if the user was the one who initially opened it.

The temporary file is not deleted after this operation completes, and BizHawk updates the current movie path to the new location.
You can therefore continue to make further changes to the movie and then use `/movie/save` to update this forked movie.

The `data` field contains a Base64-encoded string that represents the raw binary data of the movie file.
This uses the RFC 4648 'standard' Base64 alphabet (i.e., it uses `+` and `/` as opposed to the URL-safe `-` and `_`), and the padding is preserved (the trailing `=`s are not removed).

If no core is loaded no saving occurs and the movie returned will be a completely empty buffer.

This can be used to achieve similar means as the Lua `movie.save()` with arguments.


## Stop movie
::: danger SECURITY
This command requires '**Allow movie management**' to be enabled in the TASauria plugin security settings.
This permission is usually **DISABLED** by default, and must be enabled by the user to be accessible.
:::
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/movie/stop
```
::: code-group
```typescript [Argument schema]
{
    /* Whether to save the movie on stopping */
    /* If true, the movie will be saved and then stopped. */
    /* If false, the movie will be stopped without saving. */
    /* If null, defaults to saving the movie and then stopping. */
    save?: boolean
}
```
```typescript [Response schema]
{
    /* The binary data of the movie encoded as a Base64-encoded string. */
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

Stops the movie, optionally saves, and then returns the content as a data buffer.

Unless `save` is explicitly `false`, this will save the movie to whatever location it was opened from. If the user loaded the movie from a file, it will overwrite that file.

The `data` field contains a Base64-encoded string that represents the raw binary data of the movie file.
This uses the RFC 4648 'standard' Base64 alphabet (i.e., it uses `+` and `/` as opposed to the URL-safe `-` and `_`), and the padding is preserved (the trailing `=`s are not removed).

If no movie is loaded no saving occurs and the movie returned will be a completely empty buffer.

This is equivalent to the Lua `movie.stop()`.


## New movie
::: danger SECURITY
This command requires '**Allow movie management**' to be enabled in the TASauria plugin security settings.
This permission is usually **DISABLED** by default, and must be enabled by the user to be accessible.
:::
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/movie/new
```
::: code-group
```typescript [Argument schema]
{
    /* Whether to start this movie from a save state. */
    startFromSaveState: boolean,
    /* Whether to start this movie from the current SaveRAM state. */
    startFromSaveRAM: boolean,
    /* The author name to associate with the movie. */
    /* If this is null, the default author name saved in the config is used. */
    author?: string,
}
```
```typescript [Response schema]
{
    /* The name of the author used. */
    author: string,
}
```
```json [Example arguments]
{
    "startFromSaveState": false,
    "startFromSaveRAM": false,
    "author": null
}
```
```json [Example response]
{
    "author": "default user",
    "status": 200,
    "messageIdentifier": null
}
```
:::

Creates and starts a new movie.

For security reasons, TASauria does not provide a method to specify the location of the new movie, as that would allow manipulation of the host filesystem.

Instead, this creates the new movie as a temporary file on the host.
You can proceed to record inputs and so on into it, and then perform `/movie/save` to get a copy of the movie file as a buffer.

If no core is loaded, a NullHawk movie will be created. This is usually impossible and rather amusing, but it's impossible to advance frames so nothing of real note happens.

There is no equivalent in Lua for this.
