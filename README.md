# Instant Exit - Created By QuestDragon
Version: 1.0.1
## 作成した経緯
GTAVって様々な車に乗り降りすることができますけど、好きなときに降りられないときがありますよね。

例えば、宙を舞っているときとか。遊んでいるとふと思うわけですよ、車だけが輝くんじゃなくて、運転手も輝かなければ。空中で。

…というわけで、車が宙を舞っていようがなんだろうが、即座に車から降りられるようにしたいと思い、作成した次第です。

## 機能
iniファイルで指定したキーを押すとどんなときでも車から降りることができます。

空中で車から降りた場合、そのままだと落下死してしまうので、パラシュートを自動装備します。※高さが低すぎる場合は自動装備されないことがあります。

## 機能追加、フィードバックについて
制作者は初心者なので何かと至らないところがあると思います。

不具合等を発見しましたら、QuestDragonまでご連絡ください。

また、「こんな機能がほしい！」「ここはこうしてほしい！」という要望がありましたらご相談ください。

こちらもスクリプトModについて勉強したいので、ご意見や要望はいつでもお待ちしております。

## 開発環境
C#を使用しています。

ScriptHookV DotNetを使用しており、バージョンは3.6.0のNightly ビルド 57で開発しています。

## インストール
以下から各種ファイルをダウンロードし、スクリプトMod本体はScriptsフォルダに、前提条件のファイルはGTA5.exeと同じフォルダにコピーしてください。

| [Instant Exit](https://github.com/QuestDragon/GTAV_InstantExit/releases/latest/download/InstantExit.zip) | [ScriptHookV](http://dev-c.com/gtav/scripthookv/) | [ScriptHookV DotNet 3.6.0 Nightly](https://github.com/scripthookvdotnet/scripthookvdotnet-nightly/releases/latest) |
| ------------- | ------------- | ------------- | 

## インストール時のSCRIPT HOOK V ERRORについて
ScriptHookV DotNet Nightlyビルドを導入してGTA5を起動すると、「SCRIPT HOOK V ERROR」が表示され、scriptsフォルダに導入されている.NETスクリプトのすべてが読み込まれない現象になることがあります。

これは、ScriptHookV DotNetの前提条件を満たしていないことが原因です。**Releaseビルドでは動いていても、Nightlyビルドでは動かないことがあります。**

そのため、今一度次のコンポーネントがインストールされているかご確認ください。

| [.NET Framework 4.8 （ランタイム、開発者パックの"両方"が必要です。）](https://dotnet.microsoft.com/download/dotnet-framework/net48) | [Visual C++ Redistributable for Visual Studio 2019 x64](https://support.microsoft.com/en-us/help/2977003/the-latest-supported-visual-c-downloads) |
| ------------- | ------------- |

## 各種設定
設定はiniファイルから行います。

設定内容は3項目しかありません。

- InstantExitKey：本スクリプトを使用して車から降りるキーです。デフォルト値はFキーです。Noneにするとスクリプトを無効にできます。
- ModifierKey ： 本スクリプトを使用して車から降りる修飾キーです。デフォルト値は左Shiftキー（LShiftKey）です。NoneにするとInstantExitKeyで指定したキーを押すだけで動作します。
- DebugMode ： 本スクリプトの動作状況を通知機能で確認することができます。通常はFalseで使用して下さい。


## 使い方
乗り物に乗って、iniファイルに設定したキーを押すと車から降りることができます。

ModifierKeyをNone以外に設定している場合は、ModifierKeyも一緒に押している必要があります。

空中で降りた場合はパラシュートが装備されます。※低空で降りた場合など、装備されない場合もあります。

## 余談
ある方が作成したカオスMODには「緊急脱出機能」として内蔵されていたのですが、このような機能だけを持つスクリプトModがなかったので作ってみました。

テキトーに作ったので粗があるかもしれません…。

あとChatGPTにも少しだけ協力してもらいました。Function.Call（より高度な命令をGTAVに送れる関数のこと）苦手なので…。

## 免責事項
本スクリプトModを使用したことにより生じた被害に関して、私QuestDragonは一切の責任を負いかねます。自己責任でご使用ください。

ファイルに一切変更を加えない状態での2次配布は禁止です。

予告なく配布を停止することがあります。予めご了承ください。

改造はご自由にしていただいて構いませんが、配布の際はクレジット表記していただけると助かります。

「一から自作した」というのではなく、「QuestDragonのスクリプト(Instant Exit)の〇〇を△△にした」のように表記していただければと思います。

## 制作者
QuestDragon
