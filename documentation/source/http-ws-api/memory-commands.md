
# Memory commands (`/memory`)


## Domain info
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/memory/domains
```
::: code-group
```typescript [Argument schema]
{
    /* none */
}
```
```typescript [Response schema]
{
    /* The name of the current domain. */
    current: string,
    /* An array of the memory domains available */
    domains: {
        /* The name of the domain */
        name: string,
        /* The size of the domain in bytes */
        size: number,
    }[],
}
```
```json [Example arguments]
{}
```
```json [Example response]
{
    "current": "System Bus",
    "domains": [
        {
            "name": "RAM",
            "size": 2048
        },
        {
            "name": "System Bus",
            "size": 65536
        },
        {
            "name": "PPU Bus",
            "size": 16384
        },
        {
            "name": "CIRAM (nametables)",
            "size": 2048
        },
        {
            "name": "PALRAM",
            "size": 32
        },
        {
            "name": "OAM",
            "size": 256
        },
        {
            "name": "PRG ROM",
            "size": 32768
        },
        {
            "name": "CHR VROM",
            "size": 8192
        }
    ],
    "status": 200,
    "messageIdentifier": null
}
```
:::

Requests information about the domains available with this core.

If no core is loaded or the core does not support memory reading this command will return an error.

This is equivalent to the Lua `memory.getmemorydomainlist()` and `memory.getmemorydomainsize()`.


## Read integer
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/memory/readinteger
```
::: code-group
```typescript [Argument schema]
{
    /* The address to read from as a zero-indexed offset. */
    address: number,
    /* The size of the integer to read in *bytes* */
    /* This can be a value from 1 (8-bit) to 4 (32-bit). */
    size: number,
    /* Whether the value should be read as signed or not. */
    /* If this is true, it will read a s8/s16/s24/s32. */
    /* If this is false it will read a u8/u16/u24/u32. */
    signed: boolean,
    /* Determines the endianness of this read. */
    /* If this is true, then the integer is read as little-endian. */
    /* If this is false, then the integer is read as big-endian. */
    /* If this is null or not provided, the integer is read as big-endian. */
    little: boolean?,
    /* Which memory domain to read from. */
    /* If this is null, the default memory domain set by the core is used. */
    /* If the domain specified does not exist it will read the default memory domain set by the core instead. */
    domain: string?,
}
```
```typescript [Response schema]
{
    /* The value of the integer that was read. */
    data: number,
    /* The memory domain from which the value was read. */
    domain: string,
}
```
```json [Example arguments]
{
    "address": 8192,
    "size": 4,
    "signed": true,
    "little": false,
    "domain": "RDRAM",
}
```
```json [Example response]
{
    "data": 324,
    "domain": "RDRAM",
    "status": 200,
    "messageIdentifier": null
}
```
:::

Reads a signed or unsigned integer of the requested size from the specified memory address with the specified endianness.

If no core is loaded or the core does not support memory reading this command will return an error.

This is equivalent to these Lua functions:
- `mainmemory.read_u8()` and `memory.read_u8()` (when `size: 1` and `signed: false`),
- `mainmemory.read_s8()` and `memory.read_s8()` (when `size: 1` and `signed: true`),
- `mainmemory.read_u16_be()` and `memory.read_u16_be()` (when `size: 2`, `signed: false` and `little: false`),
- `mainmemory.read_u16_le()` and `memory.read_u16_le()` (when `size: 2`, `signed: false` and `little: true`),
- `mainmemory.read_s16_be()` and `memory.read_s16_be()` (when `size: 2`, `signed: true` and `little: false`),
- `mainmemory.read_s16_le()` and `memory.read_s16_le()` (when `size: 2`, `signed: true` and `little: true`),
- `mainmemory.read_u24_be()` and `memory.read_u24_be()` (when `size: 3`, `signed: false` and `little: false`),
- `mainmemory.read_u24_le()` and `memory.read_u24_le()` (when `size: 3`, `signed: false` and `little: true`),
- `mainmemory.read_s24_be()` and `memory.read_s24_be()` (when `size: 3`, `signed: true` and `little: false`),
- `mainmemory.read_s24_le()` and `memory.read_s24_le()` (when `size: 3`, `signed: true` and `little: true`),
- `mainmemory.read_u32_be()` and `memory.read_u32_be()` (when `size: 4`, `signed: false` and `little: false`),
- `mainmemory.read_u32_le()` and `memory.read_u32_le()` (when `size: 4`, `signed: false` and `little: true`),
- `mainmemory.read_s32_be()` and `memory.read_s32_be()` (when `size: 4`, `signed: true` and `little: false`),
- `mainmemory.read_s32_le()` and `memory.read_s32_le()` (when `size: 4`, `signed: true` and `little: true`)


## Read float
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/memory/readfloat
```
::: code-group
```typescript [Argument schema]
{
    /* The address to read from as a zero-indexed offset. */
    address: number,
    /* Determines the endianness of this read. */
    /* If this is true, then the float is read as little-endian. */
    /* If this is false, then the float is read as big-endian. */
    /* If this is null or not provided, the float is read as big-endian. */
    little: boolean?,
    /* Which memory domain to read from. */
    /* If this is null, the default memory domain set by the core is used. */
    /* If the domain specified does not exist it will read the default memory domain set by the core instead. */
    domain: string?,
}
```
```typescript [Response schema]
{
    /* The value of the float that was read. */
    data: number,
    /* The memory domain from which the value was read. */
    domain: string,
}
```
```json [Example arguments]
{
    "address": 12288,
    "little": false,
    "domain": "RDRAM",
}
```
```json [Example response]
{
    "data": 1.75,
    "domain": "RDRAM",
    "status": 200,
    "messageIdentifier": null
}
```
:::

Reads a float from the specified memory address with the specified endianness.

If no core is loaded or the core does not support memory reading this command will return an error.

This is equivalent to the Lua `mainmemory.readfloat()` and `memory.readfloat()`.


## Read range
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/memory/readrange
```
::: code-group
```typescript [Argument schema]
{
    /* The address to begin reading from as a zero-indexed offset. */
    address: number,
    /* The amount of memory to read, in bytes. */
    size: number,
    /* Which memory domain to read from. */
    /* If this is null, the default memory domain set by the core is used. */
    /* If the domain specified does not exist it will read the default memory domain set by the core instead. */
    domain: string?,
}
```
```typescript [Response schema]
{
    /* The bytes read from the range, encoded as a Base64 string. */
    data: string,
    /* The memory domain from which the value was read. */
    domain: string,
}
```
```json [Example arguments]
{
    "address": 128,
    "size": 64,
    "domain": "RDRAM",
}
```
```json [Example response]
{
    "data": "PBqABydauIADQAAIAAAAAI1rABAxawABFWD/8AAAAAA8C7AAjWQACDwBABACwCglAIEgIzwBbAc0IYllAKEAGQ==",
    "domain": "RDRAM",
    "status": 200,
    "messageIdentifier": null
}
```
:::

Reads a contiguous byte range from memory with the specified size at the address given.

The `data` field contains a Base64-encoded string that represents the raw byte range.
This uses the RFC 4648 'standard' Base64 alphabet (i.e., it uses `+` and `/` as opposed to the URL-safe `-` and `_`), and the padding is preserved (the trailing `=`s are not removed).

If no core is loaded or the core does not support memory reading this command will return an error.

This is equivalent to the Lua `mainmemory.read_bytes_as_array()` and `memory.read_bytes_as_array()`, and can be used to achieve similar means as `comm.mmfCopyFromMemory()`.


## Read entire domain
```ansi
[0;34mPOST[0m   [0;30mhttp://127.0.0.1:20251[0m/memory/readdomain
```
::: code-group
```typescript [Argument schema]
{
    /* Which memory domain to read from. */
    /* If this is null, the default memory domain set by the core is used. */
    /* If the domain specified does not exist it will read the default memory domain set by the core instead. */
    domain: string?,
}
```
```typescript [Response schema]
{
    /* The bytes read from the memory domain, encoded as a Base64 string. */
    data: string,
    /* The memory domain from which the value was read. */
    domain: string,
}
```
```json [Example arguments]
{
    "domain": "RDRAM",
}
```
```json [Example response]
{
    "data": /* way too long to include... see readrange */,
    "domain": "RDRAM",
    "status": 200,
    "messageIdentifier": null
}
```
:::

Reads the entire byte content of the specified memory domain.

The `data` field contains a Base64-encoded string that represents the raw bytes of the domain content.
This uses the RFC 4648 'standard' Base64 alphabet (i.e., it uses `+` and `/` as opposed to the URL-safe `-` and `_`), and the padding is preserved (the trailing `=`s are not removed).

If no core is loaded or the core does not support memory reading this command will return an error.

There is no direct Lua equivalent for this, but this can be used to achieve similar means as `comm.mmfCopyFromMemory()`.

