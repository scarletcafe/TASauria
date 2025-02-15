# -*- coding: utf-8 -*-

import pathlib
import re
import typing


ROOT = pathlib.Path(__file__).parent
BIZHAWK_DIRECTORY = ROOT.parent.parent / 'BizHawk'

with open(BIZHAWK_DIRECTORY / 'src' / 'BizHawk.Common' / 'VersionInfo.cs', 'r', encoding='utf-8') as fp:
    version = re.search(r"MainVersion = \"(.+)\"", fp.read())

    if version is not None:
        version_parts = version.group(1).split(".")
        major_version = version_parts[0]
        minor_version = version_parts[1]
        patch_version = version_parts[2] if len(version_parts) > 2 else "0"

        version_tuple = (int(major_version), int(minor_version), int(patch_version))
        constants: typing.List[str] = []

        for sub_minor in range(12):
            if version_tuple < (2, sub_minor):
                constants.append(f"BIZHAWK_VERSION_PRE_2_{sub_minor}_X")

        print(";".join(constants))
