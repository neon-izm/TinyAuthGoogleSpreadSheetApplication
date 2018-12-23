# TinyAuthGoogleSpreadSheetApplication
simple google spreadsheet as a web api 

Unity製のDesktopアプリを内輪とか限られた人たちに配布するけど、なんのフィードバックも無いと悲しいのでシンプルに起動時だけ端末のGuidベースの情報を取得して
GoogleSpreadSheetに起動しておく仕組みをUnityとGoogleSpreadSheet で実現するプロジェクトサンプルです。

Unity2018.2.14f1で動作確認済み

HTTP.cs は
Autoyaの物を改変して使っているので、こちらのライセンスに従ってください。
https://github.com/sassembla/Autoya

# 使い方
## Google SpreadSheet上にWebアプリケーションをデプロイする
https://qiita.com/kunichiko/items/7f64c7c80b44b15371a3
この記事を参考にデプロイしてください。

## Google SpreadSheetのheader列を書き込む

こんな感じに1行目に書いておくと、2行目以降は勝手に追記されていきます

| date | deviceName | guid | appName | appVersion | optionString|
| ---- | ---- |---- | ---- | ---- | ---- |
| 2018/12/1/00:00 | neon-izm's macbook air | segfdfgdfgda(long guid) | TinyAuthGoogleSpreadSheetApplication| 0.1 | defaultValue|

## Unityの設定をする
上で発行したアプリケーションURLを下記の部分のurlに当て嵌めてください

https://github.com/neon-izm/TinyAuthGoogleSpreadSheetApplication/blob/master/Assets/TinyAuthGoogleSpreadSheetApplication/Scripts/StartUpAuthWithGoogleSpreadSheet.cs#L13

