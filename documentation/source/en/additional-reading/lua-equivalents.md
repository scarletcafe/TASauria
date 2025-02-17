
# Lua equivalents

This page describes what equivalents to the Lua API provided by BizHawk exist.

You can use this page to assist in porting Lua scripts to TASauria.

## Modules not provided

Some modules provided by BizHawk are not replicated within TASauria.
Most of these modules are only provided in Lua to compensate for missing or ambiguous functionality, and these limitations become irrelevant from a Python perspective.

Nevertheless, for the benefit of porting, this section will describe the modules and how to replicate their behaviour in Python.

### ⚙️ `bit` module

This module provides bitwise operations. Some bitwise operators were introduced in newer Lua versions, leaving some of these functions deprecated even on the Lua side.

#### `bit.arshift`

Python's built in right shift (`>>`) is technically already an arithmetic right shift, however, because Python uses dynamically sized integers, it may not produce the equivalent results depending on the size and sign of the integer.

Usually the following is sufficient, assuming a 32-bit unsigned integer:

```python
(val & 0xFFFFFFFF) >> amt
```

#### `bit.band`

Bitwise AND, Python supports this natively:

```python
val & amt
```

#### `bit.band`

Bitwise NOT, Python supports this natively, however, it will produce signed numbers:

```python
~val
```

To get an unsigned 32-bit value, perform an AND afterwards:

```python
~val & 0xFFFFFFFF
```

#### `bit.bor`

Bitwise OR, Python supports this natively:

```python
val | amt
```

#### `bit.bxor`

Bitwise XOR, Python supports this natively:

```python
val ^ amt
```

#### `bit.byteswap_16`

Python doesn't have a native byte swap, but it can be achieved trivially with the `struct` built-in module:

```python
struct.unpack(">H", struct.pack("<H", val))[0]
```

#### `bit.byteswap_32`

Python doesn't have a native byte swap, but it can be achieved trivially with the `struct` built-in module:

```python
struct.unpack(">I", struct.pack("<I", val))[0]
```

#### `bit.byteswap_64`

Python doesn't have a native byte swap, but it can be achieved trivially with the `struct` built-in module:

```python
struct.unpack(">Q", struct.pack("<Q", val))[0]
```

#### `bit.check`

Checks for a bit in the given position, can be achieved by combining a left shift and bitwise AND:

```python
num & (1 << pos) != 0
```

#### `bit.clear`

Clears the bit in the given position, can be achieved via bitwise NOT and bitwise AND.

```python
num & ~(1 << pos)
```

#### `bit.lshift`

Python's built-in left shift is arithmetic but can be made logical using bitwise AND.

```python
(val << amt) & 0xFFFFFFFF
```

#### `bit.rol`

Bitwise left rotation, can be implemented by handling both sides of the boundary and using bitwise AND to fit it to size:

```python
((val << amt) | (val >> (32 - amt))) & 0xFFFFFFFF
```

#### `bit.ror`

Bitwise right rotation, can be implemented by handling both sides of the boundary and using bitwise AND to fit it to size:

```python
((val >> amt) | (val << (32 - amt))) & 0xFFFFFFFF
```

#### `bit.rshift`

Right logical shift, can be implemented like so:

```python
(val % 0x100000000) >> amt
```

#### `bit.set`

Set the bit in the given position, can be achieved via bitwise OR.

```python
num | (1 << pos)
```

### ⚙️ `bizstring` module

This module provides mostly formatting-related operations.

Usually the examples provided use the same names as the arguments shown in the BizHawk documentation.
However, the name `str` is often used within the documentation for this module, and `str` is a built-in type in Python, and `string` is a built-in module.

Because of this, the variable names used here may instead be longer variants (e.g. `string_1`) to prevent any confusion.

#### `bizstring.binary`

This converts a number to its binary representation. In Python, you can do this with format strings:
```python
f"{num:b}"
```

#### `bizstring.contains`

Returns whether a string is contained within another. In Python, this is a feature built into the syntax:

```python
string_2 in string_1
```

#### `bizstring.endswith`

Returns whether a string ends with another string. In Python, this is a method on the `str` type:

```python
string_1.endswith(string_2)
```

#### `bizstring.hex`

This converts a number to its hexadecimal representation, padded to at least 2 zeroes. In Python, you can do this with format strings:

```python
f"{num:02X}"
```

#### `bizstring.octal`

This converts a number to its octal representation, padded to at least 2 zeroes. In Python, you can do this with format strings:

```python
f"{num:02o}"
```

#### `bizstring.pad_end`

Pads the end of a string with a given character until the string is at least a given length.

In Python, this can be done with format strings:

```python
# The length and pad_char are part of the format string
# The below example left pads to a length of 10 using an underscore.
#           pad_char
#           |
#           v ||- length
f"{string_1:_<10}"
```

#### `bizstring.pad_start`

Pads the start of a string with a given character until the string is at least a given length.

In Python, this can be done with format strings:

```python
# The length and pad_char are part of the format string
# The below example right pads to a length of 10 using an underscore.
#           pad_char
#           |
#           v ||- length
f"{string_1:_>10}"
```

#### `bizstring.remove`

Removes a character span from a string. In Python, this can be done by appending two string slices:

```python
string_1[:position] + string_1[position + count:]
```

#### `bizstring.split`

Splits a string into an array using a separator. In Python, this is a method on the `str` type:

```python
string_1.split(separator)
```

#### `bizstring.startswith`

Returns whether a string starts with another string. In Python, this is a method on the `str` type:

```python
string_1.startswith(string_2)
```

#### `bizstring.substring`

Returns a substring comprising a range of the source string. In Python, this can be done with a string slice:

```python
string_1[position:position + length]
```

#### `bizstring.tolower`

Converts a string to lowercase. In Python, this is a method on the `str` type:

```python
string_1.lower()
```

#### `bizstring.toupper`

Converts a string to uppercase. In Python, this is a method on the `str` type:

```python
string_1.upper()
```

#### `bizstring.trim`

Removes whitespace on both ends of a string. In Python, this is a method on the `str` type:

```python
string_1.strip()
```

### ⚙️ `comm` module

This module provides communication out of BizHawk.

Most of its functionality is not implemented because TASauria *is* your communication out of BizHawk, making it unnecessary.

TODO

### ⚙️ `console` module

This module allows Lua scripts to write to the Lua console.

TASauria does not even require the Lua console to be open, so there is not much sense allowing access to it.
You can use `print` and so on from Python instead.

### ⚙️ `forms` module

This module provides access to Windows Forms bindings, allowing Lua scripts to make custom GUIs.

Because this surfaces UI on the emulator side, it doesn't make a lot of sense to support it in TASauria.
If you really need GUI, you can do so with a Python-based GUI framework on the script's end.

### ⚙️ `memorysavestate` module

This module takes snapshots of core state without metadata.

It's really rare to find a use case for this module in most typical scripts because it smothers TAS data and reproducibility, however, it is nevertheless possible to extract the core state out of a regular save state if you really need it.

### ⚙️ `SQL` module

This module allows creation of and interaction with SQLite databases.

Almost all builds of Python come with the `sqlite3` module built-in, allowing you to handle SQLite on the script's end if you need it.

## Modules with provided equivalents

The modules from this point onwards contain equivalents in TASauria, usually in the form of commands.

Some functionality that is represented by separate functions in Lua are combined in TASauria to reduce the amount of calls required to the emulator.

In addition, some functionality has no provided equivalents, usually because they are deprecated, are not exposed in TASauria for security reasons, or because their relevance or implementation is dubious.

The parts of a module that are not implemented are under a separate subheading with a justification as to their non-inclusion.

## ⚙️ `client` module

TODO

## ⚙️ `emu` module

TODO

## ⚙️ `event` module

TODO

## ⚙️ `gameinfo` module

TODO

## ⚙️ `genesis` module

TODO

## ⚙️ `gui` module

TODO

## ⚙️ `input` module

TODO

## ⚙️ `joypad` module

TODO

## ⚙️ `LuaCanvas` class

TODO

## ⚙️ `mainmemory` and `memory` module

TODO

## ⚙️ `movie` module

TODO

## ⚙️ `nds` module

TODO

## ⚙️ `nes` module

TODO

## ⚙️ `savestate` module

TODO

## ⚙️ `snes` module

TODO

## ⚙️ `tastudio` module

TODO

## ⚙️ `userdata` module

TODO
