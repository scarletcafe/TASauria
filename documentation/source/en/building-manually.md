
# Building TASauria manually

::: warning
If you've completed the previous step in [Getting Started](getting-started), you do not need to do this step.

You can go on ahead to the examples or API reference.

This page is intended for people who want to contribute to TASauria's codebase, such as by developing new features or fixing bugs.

If that's not you, and you simply want to use TASauria for your own scripts, feel free to ignore this page.

This page is only for developers of TASauria itself.
:::

To get started, clone the TASauria repository.

```bash
git clone --recurse-submodules https://github.com/scarletcafe/tasauria
```

## Managing and building BizHawk

The TASauria repository has BizHawk cloned into the repository root as a submodule. This allows the targeted BizHawk version to be easily updated or changed, and ensures that both the location of BizHawk's built libraries and the `EmuHawk` executable are in consistent locations for MSBuild and Visual Studio.

In order to build TASauria, please first checkout BizHawk to whatever your target release or commit is and then move forward with compiling as per [BizHawk's own documentation](https://github.com/TASEmulators/BizHawk?tab=readme-ov-file#building).

The `EmuHawk` client and its libraries must be built before it is possible to move forward onto building the TASauria plugin. Please ensure that the build is successful and that the build output is present.

## Building the TASauria plugin

I strongly recommend using the `dotnet` CLI to build the TASauria plugin. Visual Studio *can* be used for building, but it takes longer for it to warm up so I prefer to reserve it for debugging purposes only.

First, change into the plugin directory:
```bash
cd TASauriaPlugin
```

Then build using the `dotnet` CLI. You will need both Python and a version of `git` installed so that the appropriate compile flags can be enabled for compatibility:
```bash
dotnet build TASauriaPlugin.sln -c "Debug" -p:AdditionalBuildConstants="$(python ../.github/scripts/generate_bizhawk_version_constants.py)" -p:TargetedBizHawkVersion="$(python ../.github/scripts/fetch_bizhawk_version.py)"
```

Building with Visual Studio is *possible*, but because it doesn't set these above flags it is likely to fail unless you check out BizHawk to `master`.

Building will **automatically** place the resulting DLL in `BizHawk/output/ExternalTools/TASauriaPlugin.dll`, allowing you to immediately load up the emulator to test its functionality.

From here, you can load up BizHawk with the tool activated using the following command:

::: code-group

```powershell [Windows]
..\BizHawk\output\EmuHawk.exe --open-ext-tool-dll=TASauriaPlugin
```

```bash [Linux]
exec ../BizHawk/output/EmuHawkMono.sh --open-ext-tool-dll=TASauriaPlugin
```

:::

## Building the Python package

For development purposes, it's recommended to install the package straight from the repository in editable (`-e`) mode. This means any changes made to the code are reflected immediately in new instances of Python as opposed to requiring the package to be build every time.

