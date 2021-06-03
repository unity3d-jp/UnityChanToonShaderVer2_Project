# 【Unity Toon Shader (Unity-Chan Toon Shader 3)】
---
<img width = "800" src="./Documentation~/images/SDUnitychan_URP.png?raw=true">

***Read this document in other languages: [English](../README.md)***  

## 【Unity Toon Shaderの概要】

<img width = "800" src="./Documentation~/images/TPK_04.png?raw=true">

**Unity Toon Shader (Unitychan Toon Shader 3)** は、セル風3DCGアニメーションの制作現場での要望に応えるような形で設計された、トゥーンシェーダーです。他のプリレンダー向けトゥーンシェーダーとは異なり、**すべての機能がUnity上でリアルタイムで調整可能なことが、Unity Toon Shaderの最大の特長です**。  

[![](./Documentation~/images/CRS_VFXJ.png)](https://www.youtube.com/watch?v=p4azFua4rJo)

<img width = "800" src="./Documentation~/images/IllustSample_UTS2.png?raw=true">

Unity Toon Shaderの強力な機能を使うことで、**セルルックから始まり、ラノベ風のイラスト表現**まで幅広いキャラクター表現が可能となっています。  

<img width = "800" src="./Documentation~/images/UTS3_TopImage02.gif?raw=true">

Unity Toon Shaderは、「**基本色（ベースカラー）**」、「**１影色**」、「**２影色**」からなる基本３色による塗り分けに加えて、「**ハイカラー**」や「**リムライト**」、「**MatCap**（スフィアマッピング）」、「**エミッシブ**（自己発光）」などの沢山のオプションを追加することで、各カラーやテクスチャを様々に彩ることができます。  

<img width = "800" src="./Documentation~/images/UTS3_TopImage05.png?raw=true">

「**アクセントカラー**」には、どんな色を選択しますか？ アクセントカラーとは、光源の方向の反対側に設定されるカラーのことです。  

Unity Toon Shaderでは、アクセントカラーとして**2影色とAp(対蹠)リムライト**を使用できます。もちろんこれらのアクセントカラーもライトに対して動的に変化します。  

<img width = "480" src="./Documentation~/images/UTS2_TopImage03.gif?raw=true">

また**各カラー間のぼかし加減も、Unity上でリアルタイムに調整することが可能**です。  

<img width = "800" src="./Documentation~/images/UTS3_TopImage04.png?raw=true">

アニメーション制作の現場では、各シーンごとに各々のパーツに対してカラーデザインがなされます。またこれらのカラーデザインを作るスペシャリストがいるのが一般的です。UTS3はそのようなパイプラインに適した設計になっています。  

アニメーション映画では、影は光の差し込む方向を表すためだけでなく、キャラクターの形状を明確にするためにも使用されます。影は、単なる影に留まらず、キャラクターデザインの重要な部分を占めています。  

<img width = "350" src="./Documentation~/images/UTS2_TopImage06.gif?raw=true">

これらデザイン上必要となる固定影の配置も、各影色ごとに発生する位置を設定できる「**ポジションマップ**」と、ライティングによって影の出やすさを変えることのできる「**シェーディンググレードマップ**」の、２つの手法が選べます。上のムービーは、**シェーディンググレードマップと天使の輪**機能のサンプルです。  

<img width = "800" src="./Documentation~/images/UTS3_TopImage07.png?raw=true">

これら2つの画像は、同じ条件のライティング下での **Lit Shader(Standard Shader)** と **Unity Toon Shader** の比較です。  

写実的な（フォトリアリスティックな）イメージとノンフォトリアリスティックなイメージの違いがありますが、リアルタイムライトに対するすべての表面反射に注目すると、両者が同じ領域に発生していることがわかります。**Unity Toon Shaderは、さまざまなライティングの条件下で、Lit Shader(Standard Shader)と同様に扱うことができます。**  

ゲームシーンを美しいライティングで飾りたいならば、Unity Toon Shaderは非常に役に立ちます。  

<img width = "340" src="./Documentation~/images/GameRecommendation.png?raw=true">

また昨今のVRChat/Gameでのユーザーの声を反映し、様々なライティング設定の環境下でも、キャラクターが美しく表現されるように様々な工夫が実装されています。  

-----
## 【ユーザーマニュアル】
**[日本語マニュアル](./Documentation~/en/index_en.md)が提供されています。合わせてご利用ください。**  

ユーザーマニュアルには、トゥーンスタイルに関する、豊富なナレッジが集まっています。  
マニュアルを読みつつ、実際にUnity Toon Shaderを使ってみるを繰り返すことで、美しいトゥーンスタイルを作り上げるための方法論が自然に身につきます。  


-----
## 【ターゲット環境】
* **Unity 2019.4.21f1 もしくはそれ以降が必要です。**
* Forwardレンダリング環境。リニアカラースペースでの使用を推奨します。  
（ガンマカラースペースでも使用できますが、ガンマカラーの特性上、陰影の階調変化が強めに出る傾向があります。詳しくは、[リニアのワークフローとガンマのワークフロー](https://docs.unity3d.com/ja/current/Manual/LinearRendering-LinearOrGammaWorkflow.html) を参照してください。）  


-----
## 【ターゲットプラットフォーム】
Windows, MacOS, iOS, Android, PlayStation4, Xbox One, Nintendo Switch  

* テッセレーション版は、DX11が正常に動く環境のみのサポートです。  

-----
## 【提供ライセンス】
「Unity Toon Shader」は、 
**Unity Companion License for Unity-dependent projects** で提供されます。
[Unity Companion License](http://www.unity3d.com/legal/licenses/Unity_Companion_License) をご参照ください.



-----
## 【シェーダーのインストール】
インストール後は、必ず[マニュアル](Documentation~/ja/index_ja.md)を確認するようにしてください。  
マニュアルには、Unity Toon Shaderの使い方が詳しく解説されています。  

個人でみられる範囲でバグチェックはしていますが、何か不具合があったらご連絡よろしくお願いします。

-----
## 【リリース履歴】  
Unity Toon Shader のリリース履歴は、[こちら](./CHANGELOG.md)。 
UTS2 のリリース履歴は、[こちら](./Documentation~/ja/HISTORY_ja.md)。  

-----
## 【インフォメーション】  
Latest Version: x.x.x Release: 
Update: 2021/02/19  
Category: 3D  
File format: zip/unitypackage  

-----
**README_ja.md 2021/02/19**  
