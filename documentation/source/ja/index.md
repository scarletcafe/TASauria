---
# https://vitepress.dev/reference/default-theme-home-page
layout: home

hero:
  name: "TASauriaのドキュメント"
  text: null
  tagline: "BizHawkエミュレーターをリモート制御するためのプラグインとPythonライブラリ「TASauria」用のユーザーガイドとAPIリファレンス。"
  image:
    src: /favicon.svg
    alt: TASauriaのロゴ
  actions:
    - theme: brand
      text: はじめに
      link: /ja/getting-started
    - theme: alt
      text: Python APIリファレンス
      link: /ja/python-api/first-steps

features:
  - title: ⏱️ 非同期対応
    details: TASauriaのPythonモジュールはasyncioを使用しており、効率的で並行性のあるスクリプトを作成しやすくなっています。
  - title: 🧩 Lua 機能相当性
    details: TASauriaは、BizHawkの組み込みLuaスクリプティングによって提供される大部分の機能と同等性を保つことを目指しており、スクリプトの移植を容易にします。
  - title: 🌐 バージョン非依存
    details: TASauriaプラグインは複数のBizHawkリリースを対象にビルドされるため、古いエミュレータバージョンでも使用可能です。PythonライブラリはPython 3.9以降すべてで動作し、ほぼすべての環境で利用可能です。
---
