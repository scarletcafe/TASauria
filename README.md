
[![Issues](https://img.shields.io/github/issues/scarletcafe/TASauria.svg?colorB=3333ff)](https://github.com/scarletcafe/TASauria/issues)

***
<h1 align="center">
<sub valign="center">
    <img src=".github/assets/tasauria_logo.svg" height="128">
</sub>
&nbsp;
TASauria
</h1>
<p align="center">
<sup>
a plugin and library for remotely controlling BizHawk with Python
</sup>
<br>
<sup>
    <a href="https://scarletcafe.github.io/TASauria/">Read the documentation</a>
</sup>
</p>

***

TASauria is a collection of two main components:
- An plugin ("external tool") for BizHawk that allows the client to be controlled via a WebSocket connection,
- A Python library that provides a flexible, object-oriented, and Pythonic interface to the plugin to make writing Python scripts for BizHawk easy.

The name TASauria is a portmanteau of TAS ([Tool Assisted Speedrunning](https://tasvideos.org/WelcomeToTASVideos#WhatIsATas)), and [Sauria](https://en.wikipedia.org/wiki/Sauria), the monophyletic group that bridges the [Hawk](https://tasvideos.org/Bizhawk) and the [Python](https://www.python.org/).

## Installation

> [!IMPORTANT]
> You can view more detailed installation instructions, as well as a user guide and information on how to make your own scripts, on the [documentation website](https://scarletcafe.github.io/TASauria/).

Download the appropriate plugin file for your BizHawk version from the [releases page](https://github.com/scarletcafe/tasauria/releases), and place it into your `ExternalTools` directory alongside `EmuHawk.exe`.

If the plugin is installed and detected correctly, you should be able to open the TASauria window from the `Tools` > `External Tool` menu.

You can then install the Python library using `pip`:
```bash
pip install -U tasauria
```

## Acknowledgements

I would like to thank all BizHawk contributors for having a great impact in keeping the passion for old video games alive. TASing is some of the most fun I've had and I hope it continues to inspire people to love these games into the future.
