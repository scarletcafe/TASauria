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
        major_version: int = int(version_parts[0])
        minor_version: int = int(version_parts[1])
        patch_version: int = int(version_parts[2]) if len(version_parts) > 2 else 0

        version_tuple = (major_version, minor_version, patch_version)
        constants: typing.List[str] = [
            f"BIZHAWK_VERSION_EXACT_{major_version}_{minor_version}_X",
            f"BIZHAWK_VERSION_EXACT_{major_version}_{minor_version}_{patch_version}"
        ]

        for sub_minor in range(4, 12):
            if version_tuple < (2, sub_minor):
                constants.append(f"BIZHAWK_VERSION_PRE_2_{sub_minor}_X")

            if version_tuple > (2, sub_minor):
                constants.append(f"BIZHAWK_VERSION_POST_2_{sub_minor}_X")

            for sub_patch in range(5):
                if version_tuple < (2, sub_minor, sub_patch):
                    constants.append(f"BIZHAWK_VERSION_PRE_2_{sub_minor}_{sub_patch}")

                if version_tuple > (2, sub_minor, sub_patch):
                    constants.append(f"BIZHAWK_VERSION_POST_2_{sub_minor}_{sub_patch}")

        # For MSBuild compatibility we can't put a raw semicolon
        print("%3B".join(constants))
