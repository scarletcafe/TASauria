---
# https://vitepress.dev/reference/default-theme-home-page
layout: home

hero:
  name: "TASauria Documentation"
  text: null
  tagline: "User guide and API reference for TASauria, a plugin and library for remotely controlling the BizHawk emulator."
  image:
    src: /favicon.svg
    alt: TASauria Logo
  actions:
    - theme: brand
      text: Getting started
      link: /en/getting-started
    - theme: alt
      text: Python API reference
      link: /en/python-api/first-steps

features:
  - title: ⏱️ Async-ready
    details: The TASauria Python module uses asyncio, allowing you to make efficient and concurrent scripts easily.
  - title: 🧩 Lua feature parity
    details: TASauria aims to have parity with the majority of the functionality offered by BizHawk's built-in Lua scripting, making it easy to port scripts.
  - title: 🌐 Version agnostic
    details: The TASauria plugin builds targeting multiple BizHawk releases so you can use it even with older emulator versions. The Python library functions on any Python 3.9 and later, making it available in almost every environment.
---
