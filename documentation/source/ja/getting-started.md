
# はじめに


TASauriaは、BizHawkエミュレーターをリモート制御するためのプラグインとPythonライブラリです。

始めるには、まず、BizHawkエミュレーターをセットアップしましょう。

## Step 1. BizHawkをインストール {#bizhawk}

BizHawkのダウンロードは、[BizHawkのGitHubリリースページ](https://github.com/TASEmulators/BizHawk/releases/)からできます。

BizHawkバージョンずつ、対応しているTASauriaのプラグインファイルがあるから、どのバージョンをダウンロードしたのか覚えておいてください。

アーカイブを展開して、BizHawkのエミュレーターを使用するには `EmuHawk`という実行ファイルを起動してください。
BizHawkは、複数のプラットフォームとコアを対応しているから、もっと詳しく知りたい場合は[ウィキページ](https://tasvideos.org/Bizhawk)を参照してください。

エミュレーターの動作を確認し、進む前に設定を好きに調整してください。

## Step 2. TASauriaプラグインをインストール {#plugin}

TASauriaプラグインのビルドは、 [TASauriaのGitHubリリースページ](https://github.com/scarletcafe/tasauria/releases) から取得できます。

BizHawkでプラグインを使用するには、 `.dll` ファイルを `EmuHawk`の隣となる`ExternalTools`のフォルダーに置く必要があります。他に外部ツールをもっていない、それとも BizHawk を使い始めたばかりの場合は、このフォルダーは存在しなく、自分で作成しないといけないかもしれません。

フォルダーの構造は以下の通りになります：

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

正常に配置したら、 `EmuHawk`を起動できます。
プラグインは正しく検出されたら、`Tools` > `External Tool` から `TASauria` の項目が表示されて、それから起動できます。

TASauriaのウィンドウからいろんな設定を調整できます。

プラグインが正しく動作していることを確認したら、次のステップに進んでください。

::: info
プラグインのリリースバージョンの使用を強くお勧めします。けれど、ある時、例えばまだリリースに出ていない機能やバグの修正が必要な場合、開発版を試したくなる時があるかもしれません。

[GitHub Actions のワークフロー](https://github.com/scarletcafe/tasauria/actions)　にプラグインのアルファ版が常にコンパイルしてアップロードされています。 このワークフローのアーティファクトページから、該当するビルドをダウンロードできます。

このビルドは安定さの保証がありませんから、長期的に使用することはおすすめしません。もしバグがでたら、[イシュートラッカー](https://github.com/scarletcafe/tasauria/issues)　で報告してください。
:::

## Step 3. Pythonをインストール {#python}

Pythonはすでにパソコンにインストールされていない場合は、インストールする必要があります。

WindowsとmacOSでは、Pythonの[公式ウェブサイト](https://www.python.org/downloads/)からダウンロードすることが一番簡単なやり方です。

Linuxでは、ディストリビューションのパッケージマネージャからインストールするか、[`pyenv`](https://github.com/pyenv/pyenv?tab=readme-ov-file#installation)などのPythonのインストールを管理するツールを使えば適切なバージョンを取得できます。

TASauriaのPythonパッケージは、Python 3.8以上を対応としています。

Python が正しく導入され、pip（Python パッケージマネージャー）が利用可能か確認すべきです。Linux で `pip` コマンド単体を使うだけでは動作しない場合があり、その場合は別のパッケージ（例: `python3-pip`）もインストールする必要があります。

Pythonと`pip`のバージョンはこうやって確認できます：

::: code-group

```powershell [Windows]
py -3 -m pip -V
```

```bash [Linux]
python3 -m pip -V
```

:::

## Step 4. tasauriaのPythonパッケージをインストール {#module}

Python の環境は完了したら、`tasauria` パッケージをこうやって PyPI から入手できます：

::: code-group

```powershell [Windows]
py -3 -m pip install -U tasauria
```

```bash [Linux]
python3 -m pip install -U tasauria
```

:::

> [!WARNING]
> `tasauria`パッケージをインストールしようとしている時は、特に管理者としてPythonをインストールした場合か、Linuxでパッケージマネージャからインストールした場合には、権限に関するエラーが出るかもしれません。
>
> 一番シンプルな回避策は、仮想環境を作成することです。仮想環境を使えば、パッケージはシステムと別のフォルダーにインストールされ、権限のエラーを避けられます。
>
> 仮想環境について学ぶには、[Pythonのドキュ](https://docs.python.org/ja/3/library/venv.html)を閲覧することがお勧めします。ですけど、簡単の説明は以下の通りです：
>
> #### 仮想環境の作成
>
> これは、`tasauria_environment`と名付けられたフォルダーの中に仮想環境を作成します：
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
> #### 仮想環境をアクティベート化
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
> #### パッケージをインストール
>
> ```bash
> python -m pip install -U tasauria
> ```
>
> これからは、作成時間と同じフォルダーから仮想環境をアクティベートし、TASauriaを使うスクリプトを実行できます。

`pip`は、自動にパッケージとその依存のインストールを管理します。インストールが終わったら、TASauriaを使うスクリプトは実行可能となります。
