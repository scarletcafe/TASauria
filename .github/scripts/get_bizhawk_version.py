# -*- coding: utf-8 -*-

import pathlib
import re


ROOT = pathlib.Path(__file__).parent
BIZHAWK_DIRECTORY = ROOT.parent.parent / 'BizHawk'

with open(BIZHAWK_DIRECTORY / 'src' / 'BizHawk.Common' / 'VersionInfo.cs', 'r', encoding='utf-8') as fp:
    version = re.search(r"MainVersion = \"(.+)\"", fp.read())

    if version is not None:
        print(version.group(1))
