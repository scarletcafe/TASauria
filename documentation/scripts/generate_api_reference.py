#!/usr/bin/env python3
# -*- coding: utf-8 -*-

"""
MIT License

Copyright (c) 2025 Devon (scarletcafe) R

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
"""

import dataclasses
import functools
import inspect
import pathlib
import typing

from lxml import etree
from jinja2 import Environment
from jinja2.environment import Template
from jinja2.loaders import BaseLoader

import tasauria


SCRIPTS_ROOT = pathlib.Path(__file__).parent
DOCUMENTATION_FOLDER = SCRIPTS_ROOT.parent
REPOSITORY_ROOT = DOCUMENTATION_FOLDER.parent

SOURCE_FOLDER = DOCUMENTATION_FOLDER / 'source'
TEMPLATE_FOLDER = DOCUMENTATION_FOLDER / 'templates'

TASAURIA_FOLDER = REPOSITORY_ROOT / 'tasauria'

assert SOURCE_FOLDER.is_dir()
assert TEMPLATE_FOLDER.is_dir()
assert TASAURIA_FOLDER.is_dir()


ENVIRONMENT = Environment(loader=BaseLoader())

INSTALLED_TASAURIA = pathlib.Path(tasauria.__file__)


if not INSTALLED_TASAURIA.is_relative_to(TASAURIA_FOLDER):
    raise RuntimeError(
        """
        The installed `tasauria` package did not resolve to the repository's version.

        You should either:

        - run this script from the root of the repo with `tasauria` NOT installed:
            `pip uninstall tasauria`
            `python documentation/scripts/generate_api_reference.py`

        - run the script with `tasauria` installed in build-editable mode:
            `pip install -e .`
            `python documentation/scripts/generate_api_reference.py`
        """
    )


CLIENT = tasauria.TASauria

LANGUAGES = {'en', 'ja'}

FUNCTION_NAME_OVERRIDES: dict[str, typing.Optional[str]] = {
    '__init__': 'TASauria',
    '__aenter__': None,
}

FUNCTION_TITLE_OVERRIDES: dict[str, dict[str, str]] = {
    '__init__': {
        'en': '`TASauria(...)` (constructor)',
        'ja': '`TASauria(...)` (コンストラクタ)',
    },
    '__aenter__': {
        'en': '`async with TASauria(...) as emu:`',
        'ja': '`async with TASauria(...) as emu:`'
    }
}


@dataclasses.dataclass
class DocumentableFunction:
    function: typing.Callable[..., typing.Any]
    title: dict[str, str]
    name: typing.Optional[str]
    description: dict[str, str]

    def document(self, language: str) -> str:
        return inspect.cleandoc(
            f"""
### ⚙️ {self.title[language]}

::: code-group
{self.code_groups(language)}
:::

{self.description.get(language, self.description.get('en', 'No description available.'))}
            """
        )

    def code_groups(self, language: str) -> str:
        code_groups: list[str] = []

        if self.name is not None:
            code_groups.append(f"```python [Function signature]\n{self.function_signature()}\n```")

        return '\n'.join(code_groups)

    def function_signature(self) -> str:
        argument_lines: list[str] = []
        signature = inspect.signature(self.function)

        for argument_name, argument in signature.parameters.items():
            if argument_name == 'self':
                continue

            argument_lines.append(f"    {argument},")

        if not argument_lines:
            argument_lines.append("    # no arguments")

        return f"{self.name}(\n" + "\n".join(argument_lines) + "\n)"


FUNCTIONS_BY_SECTION: dict[typing.Optional[str], list[DocumentableFunction]] = {}

print("Processing client functions")

for attribute_key in dir(CLIENT):
    if attribute_key not in FUNCTION_TITLE_OVERRIDES and attribute_key.startswith('_'):
        continue

    attribute_value = getattr(CLIENT, attribute_key)

    if not inspect.isfunction(attribute_value):
        continue

    function_name = FUNCTION_NAME_OVERRIDES[attribute_key] if attribute_key in FUNCTION_NAME_OVERRIDES else f"emu.{attribute_key}"

    function_title = FUNCTION_TITLE_OVERRIDES.get(attribute_key, {
        language: f"`.{attribute_key}(...)`"
        for language in LANGUAGES
    })

    print(f"-> {function_title.get('en', attribute_key)}")

    description: dict[str, str] = {}
    section: typing.Optional[str] = None

    documentation = inspect.getdoc(attribute_value)

    if documentation:
        root = etree.fromstring(f"<root>\n{documentation}\n</root>")

        section_tags = root.xpath('/root/section')

        if section_tags:
            section = typing.cast(str, section_tags[0].text)

        for node in root.xpath('/root/description'):
            language = node.get('language', 'en')
            content = node.text.strip()

            description[language] = content

    section_dict = FUNCTIONS_BY_SECTION[section] = FUNCTIONS_BY_SECTION.get(section, [])
    section_dict.append(
        DocumentableFunction(
            function=attribute_value,
            title=function_title,
            name=function_name,
            description=description
        )
    )


print("Processing sections")

for section, section_content in FUNCTIONS_BY_SECTION.items():
    print(f'-> {section}')

    def sort_criteria(function: DocumentableFunction):
        _lines, lineno = inspect.getsourcelines(function.function)

        return lineno

    section_content.sort(key=sort_criteria)


def get_section_language(language: str, section: str) -> str:
    functions = FUNCTIONS_BY_SECTION.get(section, None)

    if not functions:
        return "Documentation not available..."

    return "\n\n".join(
        function.document(language)
        for function in functions
    )


print("Generating files")

for language in LANGUAGES:
    print(f'-> {language}')

    with open(TEMPLATE_FOLDER / f'api_reference.{language}.md', 'r', encoding='utf-8') as fp:
        template: Template = ENVIRONMENT.from_string(fp.read())

    with open(SOURCE_FOLDER / language / 'python-api' / 'client-api-reference.md', 'w', encoding='utf-8') as fp:
        output = template.render({
            "section": functools.partial(get_section_language, language)
        })

        # Jinja loves obliterating trailing newlines
        if not output.endswith('\n'):
            output += '\n'

        fp.write(output)
