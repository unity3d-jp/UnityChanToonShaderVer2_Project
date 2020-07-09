# 【UTS/UniversalToon Ver.2.2.0】
---
<img width = "800" src="Documentation~/Images_jpg/UTS2_TopImage00.jpg">

***Read this document in [English](README.md)***  

## 【UTS/UniversalToonの概要】

<img width = "800" src="Documentation~/Images_jpg/TPK_04.jpg">

**UTS2 (ユニティちゃんトゥーンシェーダー 2.0)** は、セル風3DCGアニメーションの制作現場での要望に応えるような形で設計された、トゥーンシェーダーです。他のプリレンダー向けトゥーンシェーダーとは異なり、**すべての機能がUnity上でリアルタイムで調整可能なことが、UTS2の最大の特長です**。  

**UTS/UniversalToon** は、Unityのユニバーサルレンダーパイプライン向けのUTS2です。  
**UTS/UniversalToon** は、UTS2 v.2.0.7.5の全機能を実装した、ウーバーシェーダー（統合型シェーダー）として設計されています。  

<img width = "800" src="Documentation~/Images_jpg/IllustSample_UTS2.jpg">

UTS/UniversalToonの強力な機能を使うことで、**セルルックから始まり、ラノベ風のイラスト表現**まで幅広いキャラクター表現が可能となっています。  

<img width = "800" src="Documentation~/Images_jpg/UTS2_TopImage02.gif">

UTS/UniversalToonは、「**基本色（ベースカラー）**」、「**１影色**」、「**２影色**」からなる基本３色による塗り分けに加えて、「**ハイカラー**」や「**リムライト**」、「**MatCap**（スフィアマッピング）」、「**エミッシブ**（自己発光）」などの沢山のオプションを追加することで、各カラーやテクスチャを様々に彩ることができます。  

<img width = "800" src="Documentation~/Images_jpg/UTS2_TopImage05.jpg">

「**アクセントカラー**」には、どんな色を選択しますか？ アクセントカラーとは、光源の方向の反対側に設定されるカラーのことです。  

UTS/UniversalToonでは、アクセントカラーとして**2影色とAp(対蹠)リムライト**を使用できます。もちろんこれらのアクセントカラーもライトに対して動的に変化します。  

<img width = "480" src="Documentation~/Images_jpg/UTS2_TopImage03.gif">

また**各カラー間のぼかし加減も、Unity上でリアルタイムに調整することが可能**です。  

<img width = "800" src="Documentation~/Images_jpg/UTS2_TopImage04.jpg">

アニメーション制作の現場では、各シーンごとに各々のパーツに対してカラーデザインがなされます。またこれらのカラーデザインを作るスペシャリストがいるのが一般的です。UTS/UniversalToonはそのようなパイプラインに適した設計になっています。  

アニメーション映画では、影は光の差し込む方向を表すためだけでなく、キャラクターの形状を明確にするためにも使用されます。影は、単なる影に留まらず、キャラクターデザインの重要な部分を占めています。  

<img width = "350" src="Documentation~/Images_jpg/UTS2_TopImage06.gif">

これらデザイン上必要となる固定影の配置も、各影色ごとに発生する位置を設定できる「**ポジションマップ**」と、ライティングによって影の出やすさを変えることのできる「**シェーディンググレードマップ**」の、２つの手法が選べます。上のムービーは、**シェーディンググレードマップと天使の輪**機能のサンプルです。  

<img width = "800" src="Documentation~/Images_jpg/UTS2_TopImage07.jpg">

これら2つの画像は、同じ条件のライティング下での **URP/Lit Shader** と **UTS/UniversalToon** の比較です。  

写実的な（フォトリアリスティックな）イメージとノンフォトリアリスティックなイメージの違いがありますが、リアルタイムライトに対するすべての表面反射に注目すると、両者が同じ領域に発生していることがわかります。**UTS/UniversalToonは、さまざまなライティングの条件下で、URP/Lit Shaderと同様に扱うことができます。**  

ゲームシーンを美しいライティングで飾りたいならば、UTS/UniversalToonは非常に役に立ちます。  

-----
## 【ユーザーマニュアル】
**[日本語マニュアル（v.2.2.0版）](Documentation~/index_ja.md)が提供されています。合わせてご利用ください。**  

ユーザーマニュアルには、トゥーンスタイルに関する、豊富なナレッジが集まっています。  
マニュアルを読みつつ、実際にUTS/UniversalToonを使ってみるを繰り返すことで、美しいトゥーンスタイルを作り上げるための方法論が自然に身につきます。  


-----
## 【ターゲット環境】
Unity 2019.3.4f1 もしくはそれ以降が必要です。  
Universal RP Version 7.3.1 もしくはそれ以降が必要です。  
ポストプロセスエフェクトを使う場合には, Post Procesing Version 2.3.0 もしくはそれ以降が必要です。  

Forwardレンダリング環境。リニアカラースペースでの使用を推奨します。  
（ガンマカラースペースでも使用できますが、ガンマカラーの特性上、陰影の階調変化が強めに出る傾向があります。詳しくは、[リニアのワークフローとガンマのワークフロー](https://docs.unity3d.com/ja/current/Documentation~/LinearRendering-LinearOrGammaWorkflow.html) を参照してください。）  


-----
## 【ターゲットプラットフォーム】
Windows, MacOS, iOS, Android, PlayStation4, Xbox One, Nintendo Switch  


-----
## 【インストール】
- UTS/UniversalToonは __Package Manager__ からインストールします。 
1. githubからパッケージをダウンロードする。
2. お使いのUnityからPackage Managerを開く。 （ __メニュー＞Window>Package Manager__ ）
3.  __+__ ボタンをクリックしてパッケージを追加する。ダウンロードしたパッケージは "add package from disk..." で追加出来ます。

インストール前に元のプロジェクトのバックアップを取っておくことを推奨します。
インストール後は、必ず[マニュアル](Documentation~/index_ja.md)を確認するようにしてください。  
マニュアルには、UTS/UniversalToonの使い方が詳しく解説されています。  

-----
## 【不具合報告】
不具合の報告は[こちら](https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project/issues)からお願いします。  
新規報告の際、以下の情報をお願いします。 
* 使用している**UTS/UniversalToonのバージョン**　：例　UTS/UniversalToon v.2.2.0
* 使用している**Unityのバージョン**　：例　Unity 2019.3.4f1
* Unityを使用している**OSの種類**　：例　Windows10
* 使用している**Universal RPのバージョン**　：例　Version 7.3.1

【**お願い**】不具合報告の前に、必ず最新のUTS/UniversalToonに更新して、それでも不具合が出るか確認してください。また[サンプルプロジェクト](https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project/archive/urp/master.zip)内のシーンがご自身の環境で正常に動作するかも、合わせてご確認をお願いします。

-----
## 【変更履歴】  
UTS/UniversalToonの変更履歴は、[こちら](CHANGELOG_ja.md)。  

-----
## 【インフォメーション】  
最新バージョン：2.2.0 URP専用  
最終リリース日：2020/07/07  
カテゴリー：3D / Shader  
形式：UPM package

-----
**README_ja.md 2020/07/07**  
