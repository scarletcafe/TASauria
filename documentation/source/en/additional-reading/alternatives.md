
# Alternatives

While I've put a lot of effort into trying to make TASauria as flexible and applicable as possible, there are still likely to be times when it is not the best tool for the job.

BizHawk, being open source and having a large community, has a lot of alternative ways you can TAS:

- [Built-in Lua scripting](https://tasvideos.org/Bizhawk/LuaFunctions) - The main advantage TASauria has over BizHawk's Lua scripting is the wealth of existing libraries for Python that it is possible to integrate in as part of your scripting experience, however, if you can write your algorithms yourself, or can get by with only the functionality provided by Lua's standard library, it's hard to beat the performance and integration on offer.

- [TAStudio](https://tasvideos.org/Bizhawk/TastudioDevelopment) - TAStudio is a built-in tool included with BizHawk that includes features that can sometimes reduce the need for external scripting in the first place.

- [birds-eye by SkiHatDuckie](https://github.com/SkiHatDuckie/birds-eye) is another Python-centric external scripting option that uses TCP sockets instead of an HTTP/WebSocket interface.

- [HissHawk by tuxie](https://github.com/magnusjjj/HissHawk) is an external tool that integrates Python directly, without using any socket at all. This can be more difficult to test with, but boasts better performance and allows lower-level access for people more familiar with BizHawk's codebase.

- [Writing your own external tool](https://github.com/TASEmulators/BizHawk-ExternalTools/wiki) requires the most setup and familiarity with both C# and BizHawk's codebase, but it will let you do everything TASauria can and more, at the best performance possible.

More options will likely arise as time goes on, so keep your eyes open.

In addition, if you have ideas about how to improve TASauria or want to let me know your use case, you can do so on the [GitHub](https://github.com/scarletcafe/TASauria/issues).
