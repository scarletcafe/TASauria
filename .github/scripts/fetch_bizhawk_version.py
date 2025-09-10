# -*- coding: utf-8 -*-

import pathlib
import re


ROOT = pathlib.Path(__file__).parent
BIZHAWK_DIRECTORY = ROOT.parent.parent / 'BizHawk'

with open(BIZHAWK_DIRECTORY / 'src' / 'BizHawk.Common' / 'VersionInfo.cs', 'r', encoding='utf-8') as fp:
    version = re.search(r"MainVersion = \"(.+)\"", fp.read())

    if version is not None:
        version_parts = version.group(1).split(".")
        major_version: int = int(version_parts[0])
        minor_version: int = int(version_parts[1])
        patch_version: int = int(version_parts[2]) if len(version_parts) > 2 else 0

        print(f"{major_version}.{minor_version}.{patch_version}")
