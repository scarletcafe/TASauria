
# Joypad commands (`/joypad`)


## Get joypad state
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/joypad/get
```
::: code-group
```typescript [Argument schema]
{
    /* The controller to read from. */
    /* If this is null, the state of all controllers and system buttons is returned. */
    controller?: int,
}
```
```typescript [Response schema]
{
    /* The currently loaded system ID */
    system: string,
    /* The currently loaded board type */
    boardType: string,
    /* The state of the joypad and (optionally) system buttons. */
    /* Buttons are represented with booleans, and analog inputs with integers. */
    state: {
        [name: string]: boolean | number,
    }
}
```
```json [Example arguments]
{
    "controller": 1,
}
```
```json [Example response]
{
    "system": "N64",
    "boardType": "",
    "state": {
        "A Up": false,
        "A Down": false,
        "A Left": false,
        "A Right": false,
        "DPad U": false,
        "DPad D": false,
        "DPad L": false,
        "DPad R": false,
        "Start": false,
        "Z": false,
        "B": false,
        "A": false,
        "C Up": false,
        "C Down": false,
        "C Left": false,
        "C Right": false,
        "L": false,
        "R": false,
        "X Axis": 0,
        "Y Axis": 0
    },
    "status": 200,
    "messageIdentifier": null
}
```
:::

Reads the current joypad state as processed by the emulator.

This may be the user's own input via their controller or the virtual pad, or it may be inputs read from a movie if one is playing.

The system ID and board type are included in this payload to help interpret the input values returned.

If no core is loaded the `state` will be an empty object `{}`.

This is equivalent to the Lua `joypad.get()`.


## Get immediate joypad state
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/joypad/getimmediate
```
::: code-group
```typescript [Argument schema]
{
    /* The controller to read from. */
    /* If this is null, the state of all controllers and system buttons is returned. */
    controller?: int,
}
```
```typescript [Response schema]
{
    /* The currently loaded system ID */
    system: string,
    /* The currently loaded board type */
    boardType: string,
    /* The state of the joypad and (optionally) system buttons. */
    /* Buttons are represented with booleans, and analog inputs with integers. */
    state: {
        [name: string]: boolean | number,
    }
}
```
```json [Example arguments]
{
    "controller": 1,
}
```
```json [Example response]
{
    "system": "N64",
    "boardType": "",
    "state": {
        "A Up": false,
        "A Down": false,
        "A Left": false,
        "A Right": false,
        "DPad U": false,
        "DPad D": false,
        "DPad L": false,
        "DPad R": false,
        "Start": false,
        "Z": false,
        "B": false,
        "A": false,
        "C Up": false,
        "C Down": false,
        "C Left": false,
        "C Right": false,
        "L": false,
        "R": false,
        "X Axis": 0,
        "Y Axis": 0
    },
    "status": 200,
    "messageIdentifier": null
}
```
:::

Reads the immediate joypad state as reported by the emulator.

This is always the input provided by the user.

The system ID and board type are included in this payload to help interpret the input values returned.

If no core is loaded the `state` will be an empty object `{}`.

This is equivalent to the Lua `joypad.getimmediate()`.


## Get with movie inputs
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/joypad/getwithmovie
```
::: code-group
```typescript [Argument schema]
{
    /* The controller to read from. */
    /* If this is null, the state of all controllers and system buttons is returned. */
    controller?: int,
}
```
```typescript [Response schema]
{
    /* The currently loaded system ID */
    system: string,
    /* The currently loaded board type */
    boardType: string,
    /* The state of the joypad and (optionally) system buttons. */
    /* Buttons are represented with booleans, and analog inputs with integers. */
    state: {
        [name: string]: boolean | number,
    }
}
```
```json [Example arguments]
{
    "controller": 1,
}
```
```json [Example response]
{
    "system": "N64",
    "boardType": "",
    "state": {
        "A Up": false,
        "A Down": false,
        "A Left": false,
        "A Right": false,
        "DPad U": false,
        "DPad D": false,
        "DPad L": false,
        "DPad R": false,
        "Start": false,
        "Z": false,
        "B": false,
        "A": false,
        "C Up": false,
        "C Down": false,
        "C Left": false,
        "C Right": false,
        "L": false,
        "R": false,
        "X Axis": 0,
        "Y Axis": 0
    },
    "status": 200,
    "messageIdentifier": null
}
```
:::

Reads the joypad state, including any inputs controlled by the current movie.

The system ID and board type are included in this payload to help interpret the input values returned.

If no core is loaded the `state` will be an empty object `{}`.

This is equivalent to the Lua `joypad.getwithmovie()`.


## Set button
::: warning SECURITY
This command requires '**Allow joypad/system input**' to be enabled in the TASauria plugin security settings.
This permission is usually enabled by default.
:::
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/joypad/setbutton
```
::: code-group
```typescript [Argument schema]
{
    /* The controller to set the button of. */
    /* If this is null, the name must include a controller prefix or be for a system button. */
    controller?: int,
    /* The button to set the state of. */
    name: string,
    /* The button value to set. true is pressed, false is released. */
    value: boolean,
}
```
```typescript [Response schema]
{
    /* The state of the button before this action was performed. */
    value: boolean
}
```
```json [Example arguments]
{
    "controller": 1,
    "name": "A",
    "value": true
}
```
```json [Example response]
{
    "value": false,
    "status": 200,
    "messageIdentifier": null
}
```
:::

Sets whether a given button is pressed or not on this frame.

If the button does not exist, or no core is loaded, this command returns an error.

This is equivalent to the Lua `joypad.set()`.



## Set analog
::: warning SECURITY
This command requires '**Allow joypad/system input**' to be enabled in the TASauria plugin security settings.
This permission is usually enabled by default.
:::
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/joypad/setanalog
```
::: code-group
```typescript [Argument schema]
{
    /* The controller to set the analog input value of. */
    /* If this is null, the name must include a controller prefix or be for a system input. */
    controller?: int,
    /* The analog input to set the state of. */
    name: string,
    /* The analog value to set. */
    value: int,
}
```
```typescript [Response schema]
{
    /* The state of the analog input before this action was performed. */
    value: int
}
```
```json [Example arguments]
{
    "controller": 1,
    "name": "X Axis",
    "value": 127,
}
```
```json [Example response]
{
    "value": 0,
    "status": 200,
    "messageIdentifier": null
}
```
:::

Sets a given analog input on this frame.

If the input does not exist, or no core is loaded, this command returns an error.

This is equivalent to the Lua `joypad.setanalog()`.



## Set joypad state
::: warning SECURITY
This command requires '**Allow joypad/system input**' to be enabled in the TASauria plugin security settings.
This permission is usually enabled by default.
:::
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/joypad/set
```
::: code-group
```typescript [Argument schema]
{
    /* The controller to write to. */
    /* If this is null, the names must include controller prefixes or be for a system input. */
    controller?: int,
    /* The state of the joypad and (optionally) system buttons to set. */
    /* Buttons are represented with booleans, and analog inputs with integers. */
    state: {
        [name: string]: boolean | number,
    }
}
```
```typescript [Response schema]
{
    /* The currently loaded system ID */
    system: string,
    /* The currently loaded board type */
    boardType: string,
    /* The state of the joypad and (optionally) system buttons before this action was performed. */
    /* Buttons are represented with booleans, and analog inputs with integers. */
    state: {
        [name: string]: boolean | number,
    }
}
```
```json [Example arguments]
{
    "controller": 1,
    "state": {
        "A": true,
        "Z": true,
        "X Axis": 127
    }
}
```
```json [Example response]
{
    "system": "N64",
    "boardType": "",
    "state": {
        "A Up": false,
        "A Down": false,
        "A Left": false,
        "A Right": false,
        "DPad U": false,
        "DPad D": false,
        "DPad L": false,
        "DPad R": false,
        "Start": false,
        "Z": false,
        "B": false,
        "A": false,
        "C Up": false,
        "C Down": false,
        "C Left": false,
        "C Right": false,
        "L": false,
        "R": false,
        "X Axis": 0,
        "Y Axis": 0
    },
    "status": 200,
    "messageIdentifier": null
}
```
:::

Writes various inputs of the controller state at once.

You do not need to provide all the inputs a controller has.

If inputs are provided that don't actually exist, they will be ignored.

This is equivalent to the Lua `joypad.set()` and `joypad.setanalog()`.
