
# Getting started

TASauria is a plugin and library for remotely controlling the BizHawk emulator.

To get started, let's first get the BizHawk emulator up and running.

## Step 1. Install BizHawk {#bizhawk}

You can install BizHawk by getting one of the [releases from the BizHawk GitHub](https://github.com/TASEmulators/BizHawk/releases/).

You will need to make a note of which version you have downloaded, as each version of BizHawk has its own respective plugin version.

Extract the archive, and then you can run the BizHawk emulator by using the `EmuHawk` executable. BizHawk supports many different platforms and cores, you can read more information about this on its [wiki page](https://tasvideos.org/Bizhawk).

You should test that the emulator works and configure it before advancing to the next step.

## Step 2. Install the TASauria plugin {#plugin}

You can download a build of the TASauria plugin from the [releases page on the TASauria GitHub](https://github.com/scarletcafe/tasauria/releases).

In order to use the plugin in BizHawk, you must place the `.dll` file in the `ExternalTools` folder alongside `EmuHawk`. If you don't use any other external tools or have only just started using BizHawk, you may not have this folder and you will have to create it yourself.

The folder structure should look something like this:

```ansi
[0;34m📁 BizHawk[0m
├─ [0;34m📁 dll[0m
├─ [0;34m📁 ExternalTools[0m
│   └─ [0;32mTASAuriaPlugin.dll[0m
├─ [0;30m... (other folders)[0m
├─ [0;32mDiscoHawk.exe[0m
├─ [0;32mEmuHawk.exe[0m
└─ [0;30m... (other files)[0m
```

You can then start `EmuHawk`.
If the plugin is functioning correctly, you will be able to find the `TASauria` entry in the `Tools` > `External Tool` menu.

TASauria begins running by default even without the window open. However, you can use the window to adjust settings, such as what host and port it listens for clients on.

Once you have confirmed that the plugin is functioning, you can move onto the next step.

::: info
It is always recommended to use a release build of the plugin when possible. However, in certain cases, such as experimental bugfixes, you may want to install a developmental build instead.

Plugin builds are automatically compiled as part of a [GitHub Actions workflow](https://github.com/scarletcafe/tasauria/actions). You can download the appropriate plugin version as an artifact from any recently built commit.

In general, you should not rely on these builds to be stable, and you should avoid using them long term. Please report any bugs or problems you experience with the plugin to the [issue tracker](https://github.com/scarletcafe/tasauria/issues).
:::

## Step 3. Installing Python {#python}

If you don't already have a copy of Python installed, you will need to install one.

On Windows and macOS, the easiest method of installing Python is to download the installer from the [official website](https://www.python.org/downloads/).

On Linux, you will most likely be able to install a version of CPython from your package manager, or, alternatively, you can install a Python version management tool like [`pyenv`](https://github.com/pyenv/pyenv?tab=readme-ov-file#installation) which can set up a desired version for you.

The TASauria Python package supports Python 3.8 and newer.

You should test that your Python installation works, and that the package manager `pip` is available. On some Linux distros, it may be necessary to install a separate system package to use `pip`.

You can check your Python and `pip` version with the following command:

::: code-group

```powershell [Windows]
py -3 -m pip -V
```

```bash [Linux]
python3 -m pip -V
```

:::

## Step 4. Install the tasauria Python package {#module}

Once your Python environment is set up, you can install the `tasauria` package from PyPI using the following command:

::: code-group

```powershell [Windows]
py -3 -m pip install -U tasauria
```

```bash [Linux]
python3 -m pip install -U tasauria
```

:::

> [!WARNING]
> You may get permission errors when trying to install the `tasauria` package, especially if you installed Python as an administrator on Windows, or you installed it from your system package manager on Linux.
>
> The easiest way to work around this problem is to create a virtual environment. This allows packages to be installed into a custom folder, instead of to the location where Python is installed.
>
> You can read more information about virtual environments in the [Python documentation](https://docs.python.org/3/library/venv.html). However, here is a brief overview:
>
> #### Creating a virtual environment
>
> This will create the environment in a folder called `tasauria_environment`:
>
> ::: code-group
>
> ```powershell [Windows]
> py -3 -m venv tasauria_environment
> ```
>
> ```bash [Linux]
> python3 -m pip install -U tasauria
> ```
>
> :::
>
> #### Activating the virtual environment
> ::: code-group
>
> ```powershell [Windows (PowerShell)]
> .\tasauria_environment\Scripts\Activate.ps1
> ```
>
> ```cmd [Windows (cmd)]
> .\tasauria_environment\Scripts\activate.bat
> ```
>
> ```bash [Linux]
> source tasauria_environment/bin/activate
> ```
>
> :::
>
> #### Installing the package
>
> ```bash
> python -m pip install -U tasauria
> ```
>
> From then on, you can then simply activate the virtual environment from the same directory to run scripts that use TASauria.

`pip` will automatically handle installing the package and any dependencies. Once the installation has finished, you can run any script that uses TASauria.