/**
[TinyAuthGoogleSpreadSheetApplication]
Copyright (c) 2019 @izm
This software is released under the MIT License.
http://opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AutoyaFramework.Connections.HTTP;

public class StartUpAuthWithGoogleSpreadSheet : MonoBehaviour
{
    string _connectionID = String.Empty;
    private HTTPConnection _connection = null;

    //https://qiita.com/kunichiko/items/7f64c7c80b44b15371a3 を参考にしています
    private string url =
        "https://script.google.com/macros/s/ここに自分だけのURLを入れよう！！！";

    //Get sampleのGoogle Spread Sheetはこんな感じ
    /*
     * // GETリクエストに対する処理
    function doGet(e) {
    var output = ContentService.createTextOutput("Application available");
    output.setMimeType(ContentService.MimeType.TEXT);
    return output;
    }
 
    // POSTリクエストに対する処理
    function doPost(e) {
    // JSONをパース
    if (e == null || e.postData == null || e.postData.contents == null) {
    return;
    }
    var requestJSON = e.postData.contents;
    var requestObj = JSON.parse(requestJSON);
    
    //  
    // 結果をスプレッドシートに追記
    //
    
    var ss = SpreadsheetApp.getActive()
    var sheet = ss.getActiveSheet();
    
    // ヘッダ行を取得
    var headers = sheet.getRange(1,1,1,sheet.getLastColumn()).getValues()[0];
    
    // ヘッダに対応するデータを取得
    var values = [];
    for (i in headers){
    var header = headers[i];
    var val = "";
    switch(header) {
      case "date":
        val = new Date();
        break;
      case "mimeType":
        val = e.postData.type;
        break;
      default:
        val = requestObj[header];
        break;
    }
    values.push(val);
    }
    
    // 行を追加
    sheet.appendRow(values);
    }
    */

    string myGuid = String.Empty;

    private static bool _checkFinished = false;

    //ポストするアプリと端末情報
    //対象のGoogleSpreadSheetのヘッダ列に、この通りに書いておくと良い
    //あと、A1はdateって書いておくと良い
    //つまりSpreadSheetの１列目は
    //date deviceName  guid  appName  appVersion  optionString 
    //みたいになっていてほしい
    [System.Serializable]
    public class DeviceInfo
    {
        public string deviceName;
        public string guid;
        public string appName;
        public string appVersion;
        public string optionString;
    }

    // Use this for initialization
    void Start()
    {
        if (_checkFinished == true)
        {
            //起動チェック済みだから気にしなくていいと思う
            return;
        }

        //端末固有番号を取得する。とは言えmacアドレス収集とかはセンシティブなので
        //本当はここを暗号化するとグッド！
        if (PlayerPrefs.HasKey("guid"))
        {
            myGuid = PlayerPrefs.GetString("guid");
        }
        else
        {
            //izmのMacBookAir__q(長い文字列)we9werfgw___a(長い文字列)erwereqwrq3　みたいなのが取得出来て、こいつをGUIDとする
            myGuid = SystemInfo.deviceName + "__" + SystemInfo.deviceUniqueIdentifier + "__" +
                     Guid.NewGuid().ToString();
            PlayerPrefs.SetString("guid", myGuid);
        }

        _connectionID = Guid.NewGuid().ToString();

        _connection = new HTTPConnection();
        //Getのサンプル、最初に導通を確認したい的な
        StartCoroutine(
            _connection.Get(_connectionID, null, url, (s, i, arg3, responseBody) =>
                {
                    if (responseBody.Contains("available") == false)
                    {
                        Debug.LogAssertion("ウェブ側で起動制限を掛けてるよ～！！！");
                        //Maybe Quit Application??
                        //ここで、Getの文字列が規定じゃないってことはたぶん、開発者としてはアプリが流出してロックを掛けてるってことだから
                        //アプリを終了させちゃっても良い気がする
                        Application.Quit();
                    }

                    Debug.Log(responseBody);
                },
                (s, i, arg3, arg4) =>
                {
                    Debug.LogAssertion("http not connection");
                    //Maybe Quit Application??
                    //ここで、Getが通じないってことはたぶんオフラインか、リバースプロクシで妨害してるっぽいってことだから、
                    //アプリを終了させちゃっても良い気がする
                    Application.Quit();
                }, 10));


        //ポストする
        DeviceInfo info = new DeviceInfo()
        {
            deviceName = SystemInfo.deviceName,
            guid = myGuid,
            appName = Application.productName,
            appVersion = Application.version,
            optionString = "default value"
        };
        //ここでoptionに「初投稿です…ども…」とか入れる

        //Postして自分の端末情報をGoogle Spread Sheetに送る
        StartCoroutine(_connection.Post(_connectionID, null, url, JsonUtility.ToJson(info),
            (s, i, arg3, body) =>
            {
                _checkFinished = true;
                Debug.Log("post success: " + body);
            },
            (s, i, arg3, arg4) => { Debug.LogError("failed"); }, 5d));
    }
}
