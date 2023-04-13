# DeZeroUnity
ゼロから作るディープラーニング3で作ったDeep Learning用フレームワークである"DeZero"をUnityで動かせるようにする試みです。  
※現在は製作途中です

## インストール方法
Nugetを利用できるようにするため下記の項目をPackagesのmanifest.json（"dependencies"の上など）に追加してください。
```
  "scopedRegistries": [
    {
      "name": "Unity NuGet",
      "url": "https://unitynuget-registry.azurewebsites.net",
      "scopes": [
        "org.nuget"
      ]
    }
  ],
```
上記項目を追加した上で以下のurlを利用してUPMからパッケージをインストールすることができます。
```
https://github.com/tsubasa-alife/DeZeroUnity.git?path=/Packages/DeZeroUnity
```
