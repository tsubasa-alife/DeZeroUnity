# DeZeroUnity
## 概要
ゼロから作るDeep Learning3のフレームワーク [DeZero](https://github.com/oreilly-japan/deep-learning-from-scratch-3) をUnityで動かせるようにする試みです。  

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
上記項目を追加した上で以下のGit URLを利用してUPMからパッケージをインストールすることができます。  
参考：[Install a package from a Git URL](https://docs.unity3d.com/Manual/upm-ui-giturl.html)
```
https://github.com/tsubasa-alife/DeZeroUnity.git?path=/Packages/DeZeroUnity
```
