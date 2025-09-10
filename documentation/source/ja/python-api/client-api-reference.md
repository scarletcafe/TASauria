

# クライアント API リファレンス

## TASauria のライフサイクル

### ⚙️ `TASauria(...)` (コンストラクタ)

::: code-group
```python [Function signature]
TASauria(
    url: Union[str, yarl.URL, NoneType] = None,
    adapter_type: Optional[Type[tasauria.adapters.async_.AsyncAdapter]] = None,
)
```
:::

これは、`TASauria` クラスのコンストラクタです。
            

### ⚙️ `.connect(...)`

::: code-group
```python [Function signature]
emu.connect(
    # no arguments
)
```
:::

Attempts to connect to the TASauria server specified by the URL in the classes' constructor.
This will not overwrite an existing connection if there is one.
            

### ⚙️ `.close(...)`

::: code-group
```python [Function signature]
emu.close(
    # no arguments
)
```
:::

Closes the connection to the TASauria server, after which this client will be unusable.
            

## メタ機能

### ⚙️ `.ping(...)`

::: code-group
```python [Function signature]
emu.ping(
    # no arguments
)
```
:::

Sends a ping to the TASauria server. This should always return `True`.
            

### ⚙️ `.batch_commands(...)`

::: code-group
```python [Function signature]
emu.batch_commands(
    commands: Sequence[Tuple[Type[tasauria.commands.Command[Any, Dict[str, Any], Dict[str, Any], Any]], Dict[str, Any]]],
)
```
:::

Allows batching of commands.
This requires lower-level understanding of TASauria to use, consider checking the [performance page](../additional-reading/performance) page for guidance.
            

## ジョイパッド操作

### ⚙️ `.get_joypad(...)`

::: code-group
```python [Function signature]
emu.get_joypad(
    controller: Optional[int] = None,
    with_movie: bool = False,
    immediate: bool = False,
)
```
:::

Gets the current joypad state.
            

### ⚙️ `.set_joypad(...)`

::: code-group
```python [Function signature]
emu.set_joypad(
    state: tasauria.types.BizHawkInput,
    controller: Optional[int] = None,
)
```
:::

Sets the current joypad state.
            

## メモリーの読み込み

### ⚙️ `.read_u8_be(...)`

::: code-group
```python [Function signature]
emu.read_u8_be(
    address: int,
    domain: Optional[str] = None,
)
```
:::

Reads a big endian `u8` (unsigned 8 bit integer) from the requested address and domain.
            

### ⚙️ `.read_u8_le(...)`

::: code-group
```python [Function signature]
emu.read_u8_le(
    address: int,
    domain: Optional[str] = None,
)
```
:::

Reads a little endian `u8` (unsigned 8 bit integer) from the requested address and domain.
            

### ⚙️ `.read_i8_be(...)`

::: code-group
```python [Function signature]
emu.read_i8_be(
    address: int,
    domain: Optional[str] = None,
)
```
:::

Reads a big endian `i8` (signed 8 bit integer) from the requested address and domain.
            

### ⚙️ `.read_i8_le(...)`

::: code-group
```python [Function signature]
emu.read_i8_le(
    address: int,
    domain: Optional[str] = None,
)
```
:::

Reads a little endian `i8` (signed 8 bit integer) from the requested address and domain.
            

### ⚙️ `.read_u16_be(...)`

::: code-group
```python [Function signature]
emu.read_u16_be(
    address: int,
    domain: Optional[str] = None,
)
```
:::

Reads a big endian `u16` (unsigned 16 bit integer) from the requested address and domain.
            

### ⚙️ `.read_u16_le(...)`

::: code-group
```python [Function signature]
emu.read_u16_le(
    address: int,
    domain: Optional[str] = None,
)
```
:::

Reads a little endian `u16` (unsigned 16 bit integer) from the requested address and domain.
            

### ⚙️ `.read_i16_be(...)`

::: code-group
```python [Function signature]
emu.read_i16_be(
    address: int,
    domain: Optional[str] = None,
)
```
:::

Reads a big endian `i16` (signed 16 bit integer) from the requested address and domain.
            

### ⚙️ `.read_i16_le(...)`

::: code-group
```python [Function signature]
emu.read_i16_le(
    address: int,
    domain: Optional[str] = None,
)
```
:::

Reads a little endian `i16` (signed 16 bit integer) from the requested address and domain.
            

### ⚙️ `.read_u24_be(...)`

::: code-group
```python [Function signature]
emu.read_u24_be(
    address: int,
    domain: Optional[str] = None,
)
```
:::

Reads a big endian `u24` (unsigned 24 bit integer) from the requested address and domain.
            

### ⚙️ `.read_u24_le(...)`

::: code-group
```python [Function signature]
emu.read_u24_le(
    address: int,
    domain: Optional[str] = None,
)
```
:::

Reads a little endian `u24` (unsigned 24 bit integer) from the requested address and domain.
            

### ⚙️ `.read_i24_be(...)`

::: code-group
```python [Function signature]
emu.read_i24_be(
    address: int,
    domain: Optional[str] = None,
)
```
:::

Reads a big endian `i24` (signed 24 bit integer) from the requested address and domain.
            

### ⚙️ `.read_i24_le(...)`

::: code-group
```python [Function signature]
emu.read_i24_le(
    address: int,
    domain: Optional[str] = None,
)
```
:::

Reads a little endian `i24` (signed 24 bit integer) from the requested address and domain.
            

### ⚙️ `.read_u32_be(...)`

::: code-group
```python [Function signature]
emu.read_u32_be(
    address: int,
    domain: Optional[str] = None,
)
```
:::

Reads a big endian `u32` (unsigned 32 bit integer) from the requested address and domain.
            

### ⚙️ `.read_u32_le(...)`

::: code-group
```python [Function signature]
emu.read_u32_le(
    address: int,
    domain: Optional[str] = None,
)
```
:::

Reads a little endian `u32` (unsigned 32 bit integer) from the requested address and domain.
            

### ⚙️ `.read_i32_be(...)`

::: code-group
```python [Function signature]
emu.read_i32_be(
    address: int,
    domain: Optional[str] = None,
)
```
:::

Reads a big endian `i32` (signed 32 bit integer) from the requested address and domain.
            

### ⚙️ `.read_i32_le(...)`

::: code-group
```python [Function signature]
emu.read_i32_le(
    address: int,
    domain: Optional[str] = None,
)
```
:::

Reads a little endian `i32` (signed 32 bit integer) from the requested address and domain.
            

### ⚙️ `.read_u64_be(...)`

::: code-group
```python [Function signature]
emu.read_u64_be(
    address: int,
    domain: Optional[str] = None,
)
```
:::

Reads a big endian `u64` (unsigned 64 bit integer) from the requested address and domain.
            

### ⚙️ `.read_u64_le(...)`

::: code-group
```python [Function signature]
emu.read_u64_le(
    address: int,
    domain: Optional[str] = None,
)
```
:::

Reads a little endian `u64` (unsigned 64 bit integer) from the requested address and domain.
            

### ⚙️ `.read_i64_be(...)`

::: code-group
```python [Function signature]
emu.read_i64_be(
    address: int,
    domain: Optional[str] = None,
)
```
:::

Reads a big endian `i64` (signed 64 bit integer) from the requested address and domain.
            

### ⚙️ `.read_i64_le(...)`

::: code-group
```python [Function signature]
emu.read_i64_le(
    address: int,
    domain: Optional[str] = None,
)
```
:::

Reads a little endian `i64` (signed 64 bit integer) from the requested address and domain.
            

### ⚙️ `.read_f32_be(...)`

::: code-group
```python [Function signature]
emu.read_f32_be(
    address: int,
    domain: Optional[str] = None,
)
```
:::

Reads a big endian `f32` (32 bit IEEE-754 floating point number) from the requested address and domain.
            

### ⚙️ `.read_f32_le(...)`

::: code-group
```python [Function signature]
emu.read_f32_le(
    address: int,
    domain: Optional[str] = None,
)
```
:::

Reads a little endian `f32` (32 bit IEEE-754 floating point number) from the requested address and domain.
            

### ⚙️ `.read_f64_be(...)`

::: code-group
```python [Function signature]
emu.read_f64_be(
    address: int,
    domain: Optional[str] = None,
)
```
:::

Reads a big endian `f64` (64 bit IEEE-754 floating point number) from the requested address and domain.
            

### ⚙️ `.read_f64_le(...)`

::: code-group
```python [Function signature]
emu.read_f64_le(
    address: int,
    domain: Optional[str] = None,
)
```
:::

Reads a little endian `f64` (64 bit IEEE-754 floating point number) from the requested address and domain.
            

### ⚙️ `.read_memory_range(...)`

::: code-group
```python [Function signature]
emu.read_memory_range(
    address: int,
    size: int,
    domain: Optional[str] = None,
)
```
:::

Reads a range of memory of the requested size from the requested address and domain.
            

### ⚙️ `.read_struct(...)`

::: code-group
```python [Function signature]
emu.read_struct(
    address: int,
    format: str,
    domain: Optional[str] = None,
)
```
:::

Reads and unpacks data from the requested address and domain,
using the same format as `struct.unpack` from the Python standard library.

The size of data to be read is automatically calculated with `struct.calcsize`.
            

### ⚙️ `.read_structs(...)`

::: code-group
```python [Function signature]
emu.read_structs(
    structs: Sequence[Tuple[int, str]],
    domain: Optional[str] = None,
)
```
:::

Reads and unpacks data from multiple addresses within the requested domain.

This uses batching internally, which means this method is faster than calling `emu.read_struct` multiple times,
and all of the reads are guaranteed to be from the same frame.
            

## メモリーの書き込み

### ⚙️ `.write_u8_be(...)`

::: code-group
```python [Function signature]
emu.write_u8_be(
    address: int,
    data: int,
    domain: Optional[str] = None,
)
```
:::

Writes a big endian `u8` (unsigned 8 bit integer) to the requested address and domain.
            
