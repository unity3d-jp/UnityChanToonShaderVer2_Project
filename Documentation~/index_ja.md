# UTS/UniversalToon Ver.2.2.3 マニュアル
***Read this document in [English](index.md)***  

[![](Images_jpg/SDUnitychan_URP.jpg)](https://www.youtube.com/watch?v=TfZ8B409uqM)
<img width = "800" src="Images_jpg/CRS03.jpg">
[![](Images_jpg/CRS_VFXJ.jpg)](https://www.youtube.com/watch?v=p4azFua4rJo)
<img width = "800" src="Images_jpg/TPK_04.jpg">
<img width = "800" src="Images_jpg/HiUni01.jpg">

# UTS/UniversalToon とは？
**UTS2 (ユニティちゃんトゥーンシェーダー 2.0)** は、セル風3DCGアニメーションの制作現場での要望に応えるような形で設計された、トゥーンシェーダーです。他のプリレンダー向けトゥーンシェーダーとは異なり、**すべての機能がUnity上でリアルタイムで調整可能なことが、UTS2の最大の特長です**。  

**UTS/UniversalToon** は、Unityのユニバーサルレンダーパイプライン向けのUTS2です。  
**UTS/UniversalToon** は、UTS2 v.2.0.7.5の全機能を実装した、ウーバーシェーダー（統合型シェーダー）として設計されています。  
**ユニバーサルレンダーパイプライン（Universal Render Pipeline）** については、詳しくは[こちら](https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@9.0/manual/index.html)を参照してください。  


# UTS/UniversalToon の紹介
UTS/UniversalToon は、セルルック3DCGアニメーションの制作現場での要望に応えるような形で設計された、映像志向のトゥーンシェーダーです。  

<img width = "800" src="Images_jpg/UT2018_UTS2_SuperTips_10.jpg">

セルルック3DCGアニメーションの制作現場向けの設計になっていますので、いわゆる「影」は色設計担当者が作成しやすいような「影色設定」を使う方式であり、かつ各パーツの形状（フォルム）を強調する「影」や、キャラクターのデザイン上、光源の位置や強さとは関係なく、必ず必要となる「影」が出しやすいように設計されています。  

特にこれら「影」の調整機能は強力で、多数のライトを使わなくてもシェーダー内のスライダーだけで調整することが可能です。  

<img width = "800" src="Images_jpg/SS_SampleScene.jpg">

カラーやテクスチャは、「**基本色（ベースカラー）**」、「**１影色**」、「**２影色**」による３色塗り分けに加えて、「**ハイカラー**」や「**リムライト**」、「**MatCap**（スフィアマッピング）」、「**エミッシブ**（自己発光）」などの沢山のオプションを追加することができます。  

<img width = "600" src="Images_jpg/UTS2_TopImage03.gif">

また各カラー間のぼかし加減も、Unity上でリアルタイムに調整することが可能となっています。  

<img width = "800" src="Images_jpg/UT2018_UTS2_SuperTips_13.jpg">

デザイン上必要となる固定影の配置も、各影色ごとに発生する位置を設定できる「**ポジションマップ**」に加え、ライティングによって影の出やすさを変えることのできる「**シェーディンググレードマップ**」と、２種類の手法を選べます。  
さらに「**瞳や眉毛の前髪への透過**」などに使われる「**ステンシル機能**」のような、アニメ風キャラクター表現に便利な機能も搭載しています。  

結果として、UTS/UniversalToonでは、セルルックから始まり、ラノベ風のイラスト表現まで幅広いキャラクター表現が可能となっています。  
もちろんUnityのシステムシャドウにも対応しています。  

<img width = "800" src="Images_jpg/URP_image037.jpg">

<img width = "800" src="Images_jpg/URP_image038.jpg">

さらに物理ベースレンダリング（PBR）に対応するUniversal Render Pipline/Litシェーダーで表現できる絵的要素を、UTS/UniversalToonでは、全てノンフォトリアリスティックレンダリング（NPR）で表現することが可能です。  

また昨今のVRChatでのユーザーの声を反映し、様々なライティング設定の環境下でも、キャラクターが美しく表現されるように様々な工夫が実装されています。  

是非、貴方のご自慢のキャラクターモデルをUTS/UniversalToonで彩ってみてください。  
今まで以上に、キャラクターが美しく表現されるものと思います。  

本マニュアルは、UTS/UniversalToonの最新版 **UTS/UniversalToon v.2.2.2** 向けに書かれています。  

## 【UTS/UniversalToonを使い始める】
UTS/UniversalToonは、マテリアルインスペクターの**Shaders**メニューより、**Universal Render Pipeline**グループの中にある**Toon**シェーダーを新規マテリアルに割り当てることで、使用を開始できます。以下、その手順を示します。  

1. Projectウィンドウの「＋」メニューを開き、「Material」を選択し、新規マテリアルを作成します。  

<center><img width = "300" src="Images_jpg/URP_image000.jpg"></center>

2. 作成した新規マテリアルを選択します。インスペクターを見ると、現在「Universal Render Pipeline/Lit」シェーダーが割り当てられています。  

<center><img width = "300" src="Images_jpg/URP_image001.jpg"></center>

3. インスペクターの「Shaders」メニューを開き、Shadersウィンドウより「Universal Render Pipeline」を選択し、さらに「Toon」を選択します。  

<center><img width = "600" src="Images_jpg/URP_image002.jpg"></center>

4. マテリアルのシェーダーが変更されます。インスペクターより「Universal Render Pipeline/Toon」が割り当てられていることを確認します。初期状態ではもっとも基本的なUTS2シェーダーである、「Double Shade With Feather」ワークフローが割り当てられます。  

<center><img width = "300" src="Images_jpg/URP_image003.jpg"></center>


## 【統合シェーダーとしての UTS/UniversalToon】
レガシーパイプライン版のUTS2は、機能別に複数のシェーダーに別れていました。  

<img width = "480" src="Images_jpg/UTS2_Standard.jpg">

UTS2/UinversalToonは統合型シェーダー（ウーバーシェーダー）として再設計されましたので、全ての機能を１つの「Universal Render Piplene/Toon」シェーダーでまかなうことが可能となりました。  

<img width = "300" src="Images_jpg/URP_image006.jpg">

全ての機能は、マテリアルインスペクターより必要なものを呼び出して使用します。  

### ● ワークフローモードの切り替え
UTS2にはワークフローモードとして、「DoubleShadeWithFeather」シェーダーと、その高機能版である「ShadingGradeMap」シェーダーが存在します。この２つのシェーダーの切り替えは、UTS/UniversalToonでは、マテリアルインスペクターのほぼトップにある「Workflow Mode」より選択します。  

<img width = "400" src="Images_jpg/URP_image004.jpg">

デフォルトは「DoubleShadeWithFeather」です。２つのワークフローの違いは、後に解説します。  

### ● 「ステンシル機能」などの特殊機能を使用するには
「ステンシル機能」や「様々なカットアウト機能」、「半透明機能」など、従来はそれぞれの機能の組み合わせに応じてUTS2シェーダー自体を切り替える必要があった「特殊機能」に関しては、全て「Basic Shader Settings」メニュー下に動作モードとしてまとめられています。  
ユーザーは欲しい機能を、各動作モードを有効にすることで、自由に組み合わせることができます。  

<img width = "400" src="Images_jpg/URP_image005.jpg">

各動作モードに関しては、後に解説します。  

### ● レガシーパイプライン版UTS2マテリアルとの互換性について
レガシーパイプライン版のUTS2マテリアルとは、マテリアルプロパティの設定値に関して互換性があります。  
従って、レガシーパイプライン版のUTS2マテリアルをユニバーサルレンダーパイプラインの環境下にコピーした後で、シェーダーを「Universal Render Pipeline/Toon」に切り替えると、使用するWorkflow Modeやテクスチャ名、ぼかしの段階などの値に関してはそのまま反映されます。  

レガシーパイプライン版でシェーダーファイルに応じて切り替えていた「ステンシル」や「カットオフ」などの特殊機能に関しては、上の手順で「Basic Shader Settings」メニューより、必要な特殊機能の動作モードを有効にしてください。  

<img width = "800" src="Images_jpg/URP_image007.jpg">

<center><small>↑ ユニバーサルレンダーパイプラインの環境下では、レガシーパイプライン版のUTS2マテリアルは、左図のように正常にレンダリングされないが、「Universal Render Pipeline/Toon」にシェーダーを切り替えると、正常に表示されるようになる。</small></center>  

## 【ワークフローモードの選択】

<img width = "400" src="Images_jpg/URP_image008.jpg">

UTS/UniversalToonには、大きく分けて2つのワークフローモードがあります。  
* `DoubleShadeWithFeather` : UTS/UniversalToonの標準ワークフローモードです。2つの影色（Double Shade Colors）と、各々のカラーの境界にぼかし（Feather）を入れることができます。  
* `ShadingGradeMap` : 高機能版のワークフローモードです。DoubleShadeWithFeatherの機能に加えて、ShadingGradeMapという強力なシャドウコントロールマップを持つことができます。  

<img width = "800" src="Images_jpg/URP_image035.jpg">



搭載されている基本機能はほぼ同じですので、共に**色分け段階**（`_Step`）と**ぼかし程度**（`_Feather`）の数値を合わせれば、同じルックを作ることができます。  
どちらを使うかは好みの問題ですが、パキッとした色分けが必要なセルルックには`DoubleShadeWithFeather`モードが向いており、ぼかしを多用するイラストルックには`ShadingGradeMap`モードが向いているようです。  

２つのワークフローモードは、マテリアルインスペクターのトップに近くにある「Workflow Mode」メニューから随時切り替えできます。  

## 【各特殊機能モードを有効にする】

レガシーパイプライン版のUTS2において、`Transparent`、`StencilMask`、`StencilOut`、`Clipping`、`TransClipping`のようなサフィックス名で区別されていた各シェーダー別の特殊機能は、UTS/UniversalToonでは「Basic Shder Settings」の各特殊機能の動作モードから有効にできます。  

<img width = "800" src="Images_jpg/URP_image009.jpg">

動作モードから基本となるシェーダーに追加できる特殊機能は、以下の3つです。  
1. Transparent Shader：半透明機能を基本シェーダーに追加
2. StencilMask or StencilOut Shader：ステンシル機能を基本シェーダーに追加
3. Clipping Shader、もしくはTransClipping Shader：クリッピング機能を基本シェーダーに追加

<small>【**注意**】マテリアルインスペクターを、`All Properties`表示で使っている場合、動作モードを切り替えた後で、`Change CustomUI`ボタンよりUTS2カスタムインスペクター表示に戻してください。UTS2カスタムインスペクター表示に戻った時に、必要なシェーダーキーワードが設定され、各動作モードが有効になります。</small>  

---
### Transparent Shader
<img width = "400" src="Images_jpg/URP_image010.jpg">

`Transparent Mode`を`On`にすることで、半透明マテリアル向けのシェーダーになります。この時、必ずTransClipping機能が有効になります。  

`Auto Queue`を`Active`にしておくと、最適なRender Queueが設定されます。  
もし複数の半透明マテリアルを重ねた場合で描画の順序がおかしい時は、`Auto Queue`を`Off`にすると、カスタムレンダーキューが設定できるようになります。正しい描画が実現できる値を適宜`Render Queue`に設定してください。  
Render Queueに設定する値について、より詳しく知りたい場合、Unityマニュアルから項目「Rendering Order - Queue tag」（[英語](https://docs.unity3d.com/Manual/SL-SubShaderTags.html)）/「レンダリング順 - Queue タグ」（[日本語](https://docs.unity3d.com/ja/current/Manual/SL-SubShaderTags.html)）を参照してください。  

---
### StencilMask or StencilOut Shader
<img width = "400" src="Images_jpg/URP_image011.jpg">

`Stencil Mode`を設定することで、ステンシル機能を追加します。ステンシル機能を使うことで、アニメ＆イラスト表現でしばしば使われる「前髪を透過する眉毛」のような表現ができるようになります。  

1. `Off` ： ステンシル機能をオフにします。  
2. `StencilOut` : 透過される側のパーツに割り当てるマテリアルで設定します。必ず`StencilMask`マテリアルとペアで使います。下の例だと、「眉毛」パーツを透過させる側である「前髪」パーツに使用するマテリアルで使用します。  
3. `StencilMask` : 透過する側のパーツに割り当てるマテリアルで設定します。必ず`StencilOut`マテリアルとペアで使います。下の例だと、「眉毛」パーツのマテリアルに割り当てます。常に「前髪」パーツよりも前面に表示されるようになります。  

<img width = "800" src="Images_jpg/URP_image036.jpg">

---
### Clipping Shader および TransClipping Shader
<img width = "800" src="Images_jpg/URP_image012.jpg">

`Clipping Mode`もしくは、`Trans Clipping`から各機能を有効にすることで、基本シェーダー機能にクリッピングマスクを持たせることができます。クリッピングマスクを使うことで、「テクスチャの抜き」（カットアウトやディゾルブ）ができるようになります。  

#### ● Clipping Mode（DoubleShadeWithFeatherの場合）
1. `Off` ： クリッピング機能をオフにします。
2. `On` ： クリッピング機能を有効にします。
3. `TransClippingMode` ： クリッピング機能を、より高性能なトランスクリッピングに設定します。マスクのα透明度（Transparency）を考慮した「テクスチャの抜き」ができるモードです。より綺麗な抜きができるぶん、負荷は通常のクリッピング機能よりも高くなります。

#### ● Trans Clipping（ShadingGradeMapの場合）
1. `Off` ： トランスクリッピング機能をオフにします。
2. `On` ： トランスクリッピング機能を有効にします。トランスクリッピング機能では、マスクのα透明度（Transparency）を考慮した「テクスチャの抜き」ができます。
---
### 【参考】その他の特殊機能シェーダーの呼び出し方
<small>レガシーパイプライン版UTS2にあった、それ以外の特殊機能シェーダーは、以下の手順で呼び出すことができます。  
ほとんどがシェーダー基本機能に統合されています。</small>  

#### ● NoOutline系シェーダー
<small>マテリアルインスペクターの「Outline Settings」より、`Outline`ボタンを`Off`に設定します。</small>  

#### ● AngelRing系シェーダー
<small>以下の手順で機能を呼び出すことができます。</small>  
<small>1. マテリアルインスペクターより、`Workflow Mode`を`ShadingGradeMap`に設定する。</small>  
<small>2. マテリアルインスペクターの「AngelRing Projection Settings」より、`AngelRing Projection`を`Active`にする。</small>

#### ● Mobile系シェーダー
<small>ユニバーサルレンダーパイプラインでは、Forward Addパスの扱いが変更になり、通常のレンダリングパスと統合されました。  
それに合わせて、UTS/UniversalToonでは、Mobile系シェーダーは廃止されました。</small>  

#### ● Tessellation系シェーダー
<small>ユニバーサルレンダーパイプラインでは、DX11 Tesellationがサポートされていませんので、廃止されました。</small>  

#### ● Helper系シェーダー
<small>UTS/UniversalToonでは廃止されました。</small>  

---
# サンプルシーン
サンプルプロジェクトを開くと、`\Assets\Sample Scenes(Universal)`フォルダ以下に、次のようなサンプルシーンがあります。  

* ToonShader.unity			：イラストルックのシェーダー設定  
* ToonShader_CelLook.unity	：セルルックのシェーダー設定  
* ToonShader_Emissive.unity	：エミッシブを使ったシェーダー設定  
* ToonShader_Firefly.unity	：複数のリアルタイムポイントライト  
* AngelRing\AngelRing.unity：「天使の輪」および ShadingGradeMap を使ったキャラクターのサンプル  
* Baked Normal\Cube_HardEdge.unity：Baked Normalの参考  
* BoxProjection\BoxProjection.unity		：Box Projection を使った暗い部屋のライティング  
* EmissiveAnimation\EmisssiveAnimation.unity：EmissiveAnimationのサンプル  
* LightAndShadows\LightAndShadows.unity：PBRシェーダーとUTS2との比較  
* MatCapMask\MatCapMask.unity：MatcapMaskのサンプル  
* Mirror\MirrorTest.unity：鏡オブジェクトチェック用サンプルシーン  
* NormalMap\NormalMap.unity	：UTS2でノーマルマップを使う際のコツ  
* PointLightTest\PointLightTest.unity：ポイントライトを使ったセルルック表現のサンプル  
* Sample\Sample.unity		：UTS2の基本シェーダーの紹介  
* ShaderBall\ShaderBall.unity：シェーダーボールを使ってUTS2を設定する  

各シーンは、シェーダーやライティングの設定の参考用です。  
作りたいルックやシーンの参考にしてください。  

# UTS/UniversalToon 設定メニュー：UTS2カスタムインスペクター

ここからは、UTS/UniversalToon の各機能を設定するユーザーインタフェース「**UTS2カスタムインスペクター**」の機能解説をします。  

「UTS2カスタムインスペクター」（下図左）は、`Show All Properties` ボタンをクリックすることで、旧来の「プロパティリスト型インスペクター」（下図右）に切り替えることができます。  

<img width = "300" src="Images_jpg/URP_image033.jpg">
<img width = "300" src="Images_jpg/URP_image034.jpg">

プロパティリスト型の機能解説は[こちら](Props_ja.md)です。
プロパティリスト型のインスペクターは、`Change CustomUI` ボタンで元に戻すことができます。  
プロパティリスト型のインスペクターでの設定値のいくつかは、UTS2カスタムインスペクターに戻ることで有効になります。従って、**通常はUTS2カスタムインスペクターの状態で使用する**ことを強くお薦めいたします。  

---
## 1. 「Basic Shader Settings」メニュー

<img width = "400" src="Images_jpg/URP_image013.jpg">

こちらのメニューでは、UTS/UniversalToon の基本設定をおこないます。  
基本となる2つのワークフローモードを選択する他にも、様々な動作モードをオン/オフにすることで、基本となるシェーダーに多彩な特殊機能を追加できます。  

| `アイテム`  | 機能解説 | プロパティ |
|:-------------------|:-------------------|:-------------------|
| `日本語マニュアル` | ブラウザを利用して、UTS2日本語公式マニュアルにジャンプします。 |  |
| `English Manual` | ブラウザを利用して、UTS2英語公式マニュアルにジャンプします。 |  |
| `Workflow Mode` | ワークフローモードを `DoubleShadeWithFeather` もしくは `ShadingGradeMap` のいずれかから選びます。 |  |
| `Culling Mode` | ポリゴンのどちら側を描画しないか（カリング）を指定します。「`Culling Off`（両面描画）/ `Front Culling`（正面カリング）/ `Back Culling`（背面カリング）」が選べます。通常は`Back`で指定します。`Culling Off`はノーマルマップやライティング表示がおかしくなる場合がありますので、注意してください。 | _CullMode |
| `Auto Queue` | `Active`の時、シェーダーの機能に合わせて、適切にレンダーキューを設定します。`Off`の時、下の`Render Queue`ボックスに値を入れることで、カスタムレンダーキューを設定できます。 |  |
| `Render Queue` | カスタムレンダーキューの値を設定します。 |  |
| `Transparent Mode` | 半透明機能を `On` / `Off` します。半透明機能を`On`にすると、自動的にクリッピングは `TransClippingMode` に切り替わります。 |  |
| `Stencil Mode` | ステンシル機能を設定します。`Off`でステンシル機能は無効となります。`StencilOut`もしくは`StencilMask`に切り替えることで、それぞれのステンシル機能が有効になります |  |
| `Stencil No` | `StencilMask` / `StencilOut`の各機能で使用します。0～255の範囲で、ステンシルリファレンスナンバーを指定します（255には特別の意味がある場合がありますので、注意してください）。抜く側のマテリアルと抜かれる側のマテリアルで、数字を合わせます。 | _StencilNo |
| `Clipping Mode` | Workflow Modeが`DoubleShadeWithFeather`の時、クリッピング機能を設定します。`Off`でクリッピング機能が無効に、`On`でクリッピング機能が有効となります。`TransClippingMode`に設定すると、マスクのαチャンネルを考慮したクリッピング機能が有効になります。 |  |
| `Trans Clipping` | Workflow Modeが`ShadingGradeMap`の時、マスクのαチャンネルを考慮するトランスクリッピング機能を設定します。`Off`でクリッピング機能が無効に、`On`でクリッピング機能が有効となります。 |  |
| `Clipping Mask` | `Clipping Mode` / `Trans Clipping`各機能で使用します。グレースケールのクリッピングマスクを指定します。白が「抜き」になります。何も指定しない場合、クリッピング機能は有効になりません。 | _ClippingMask |
| `Inverse Clipping Mask` | クリッピングマスクを反転します。 | _Inverse_Clipping |
| `Clipping Level` | クリッピングマスクの抜き強度を指定します。 | _Clipping_Level |
| `Transparency Level` | `Trans Clipping`機能で使用します。クリッピングマスクのグレースケールレベルをα値として考慮することで、マスクの透過度を調整します。上の`Clipping Level`と合わせて調整することで、滑らかなマスク抜きが実現できます。 | _Tweak_transparency |
| `Use BaseMap α as Clipping Mask` | `TransClipping`シェーダーのみのプロパティです。`On`にすることで、`BaseMap`に含まれるAチャンネルをクリッピングマスクとして使用します。この場合、`ClippingMask`には指定する必要はありません。 | _IsBaseMapAlphaAsClippingMask |
| Option Menu | 以下、オプション機能のメニューになります。 |  |
| `Currnet UI Type` | ボタン上に現在選択されているユーザーインタフェースが表示されています。ボタンを押すことで、ユーザーインターフェースを`Beginner`モードに切り替えます。`Beginner`モードでは、必要最小限のUTS2コントロールができます。トグルで`Pro / Full Controll`モードに戻ります。 |  |
| `VRChat Recommendation` | 様々なライティング環境が混在している、VRChatのワールドのような環境において、カラーをなるべく破綻しないで楽しむのに便利な設定を一括でおこないます。VRChat向けにセットアップをする場合、まずこちらから始めてみることをお薦めします。 |  |

---
### 【参考】VRChatユーザー向けの便利機能について

<small>【**注意**】2020年5月の段階では、[VRChat](https://www.vrchat.com/)はユニバーサルレンダーパイプラインを採用してはいません。ユニバーサルレンダーパイプラインが採用されるまでは、[レガシー版のUTS2](https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project)を使うようにしてください。</small>  

UTS/UniversalToonは、Unityの様々なプロジェクトで使うことのできる、汎用トゥーンシェーダーです。  
VRChat上でUTS/UniversalToonを楽しむ場合、以下の便利機能を使うことで、UTS/UniversalToonの高機能を活かしつつ、VRChatの様々なライティング環境下でも安定して楽しむことができるようになります。  

#### ● UTS/UniversalToonでマテリアル設定をはじめる時
UTS/UniversalToonで各マテリアルの設定をはじめる時に、Basic Shader Settings > Option Menu内の`VRChat Recommendation`ボタンを実行してください。  
このコマンドを実行することで、VRChatの様々なライティング環境にUTS2を馴染みやすくします。  
VRChat上にアバターをアップロードして、どうも自分の意図した表示と違うと感じる時には、まず最初にこちらのコマンドを試してみるといいでしょう。  

#### ● 暗いワールドでのキャラの見え方を明るくしたい場合
`VRChat Recommendation`ボタンを実行した後で、主にポイントライトしかない暗いワールドでのキャラの見え方を、もっと明るめに調整したい場合があります。  
その場合、「Environmental Lighting Contributions Setups」メニュー内の`Unlit Intensity`スライダーを調整することで、暗い場所での明るさを底上げすることができます。  

<img width = "300" src="Images_jpg/Unlit_Intensity_Comp.gif">

<small>【**ヒント**】：Unlit Intensityは、周りの明るさを考慮しつつ、暗い場所でのマテリアルの明るさをブーストする機能ですので、元々の環境光が暗めに設定されているワールドで極端に明るくすることはできません。  

ただし、暗いワールドでは同時にポストエフェクトのブルームも強めに設定されている場合がよくあります。そのようなワールドで**Unlit Intensityの値をデフォルトの1以上にすると、ブルームの影響も受けやすくなります**ので、十分に注意してください。</small>  

---
## 2. 「Basic Three Colors and Control Maps Setups」メニュー

<img width = "400" src="Images_jpg/URP_image014.jpg">

このメニューでは、UTS/UniversalToonの基本となる、基本色/１影色/２影色に用いるカラーを定義します。  
これらのカラーは、**光源方向から順に、基本色⇒１影色⇒２影色**のように配置されます。  
おのおののカラーは、テクスチャの各ピクセルに対して各カラーを乗算し、さらにライトカラーを乗算することで決まります。  
**※ヒント：各影色は、基本色よりも暗い必要はありませんし、２影色が１影色よりも明るくても問題ありません。特に２影色を１影色よりも明るくすると、環境からの照り返しのような表現ができます。**  

**※ヒント：２影色を使うかどうかは、デザインによります。必要のない場合には、指定しなくてかまいません。**  

さらにサブメニューから、基本3色用テクスチャのシェアリング設定や、ノーマルマップ、シャドウコントロールマップの設定が行えます。

<img width = "400" src="Images_jpg/URP_image015.jpg">

| `アイテム`  | 機能解説 | プロパティ |
|:-------------------|:-------------------|:-------------------|
| `BaseMap` | 基本色（明色）テクスチャと`BaseMap`に乗算されるカラーを指定します。テクスチャを指定せず、カラーのみの指定の場合、こちらを基本色（明色）設定として使います。右側のボタンを押すことで、`BaseMap`に指定されているテクスチャを`1st ShadeMap`にも適用します。 | _MainTex, _BaseColor, _Use_BaseAs1st |
| `1st ShadeMap` | １影色テクスチャと`1st_ShaderMap`に乗算されるカラーを指定します。テクスチャを指定せず、カラーのみの指定の場合、こちらを１影色設定として使います。右側のボタンを押すことで、`1st ShadeMap`に指定されているテクスチャを`2nd ShadeMap`にも適用します。同時に`1st ShadeMap`も`BaseMap`と共有している場合は、`BaseMap`が`2nd_ShadeMap`にも適用されます。 | _1st_ShadeMap, _1st_ShadeColor, _Use_1stAs2nd |
| `2nd ShadeMap` | ２影色テクスチャと`2nd_ShaderMap`に乗算されるカラーです。テクスチャを指定せず、カラーのみの指定の場合、こちらを２影色設定として使います。 | _2nd_ShadeMap, _2nd_ShadeColor |

---
### 「NormalMap Settings」サブメニュー
このメニューでは、ノーマルマップに関する設定を行います。  

<img width = "600" src="Images_jpg/Is_NormalToBase.jpg">

**UTS/UniversalToonでは、ノーマルマップは主に影色のぼかし表現に使います。**  
通常のシェーディング表現にノーマルマップを足してやることで、より複雑なぼかし表現をすることが可能となります。上の図で、**左側がノーマルマップをカラーに反映させたもの、右が反映させていないもの**です。  

他にもノーマルマップは、スケールと共に使うことで**肌の質感**を調整したり、MatCap用のノーマルマップを別途用意することで、**髪の毛の質感**を表現するのに使われます。  

<img width = "600" src="Images_jpg/NormalMap01.jpg">
<img width = "600" src="Images_jpg/NormalMapforMatCap.jpg">

ノーマルマップを使いこなすことで、様々な表現を楽しむことができます。  

<img width = "400" src="Images_jpg/URP_image016.jpg">

| `アイテム`  | 機能解説 | プロパティ |
|:-------------------|:-------------------|:-------------------|
| `NormalMap` | ノーマルマップを指定します。右のスライダーは、ノーマルマップの強さを変化させるスケールです。 | _NormalMap, _BumpScale |
| NormalMap Effectiveness | ノーマルマップを各カラーに反映させるかを選びます。ボタンが**Off**の場合、そのカラーはノーマルマップを反映せず、オブジェクトのジオメトリそのものの形状で評価されます。 |
| `3 Basic Colors` | ノーマルマップを基本となる3カラーに反映させる時に**Active**にします。 | _Is_NormalMapToBase |
| `HighColor` | ノーマルマップをハイカラーに反映させる時に**Active**にします。 | _Is_NormalMapToHighColor |
| `RimLight` | ノーマルマップをリムライトに反映させる時に**Active**にします。 | _Is_NormalMapToRimLight |

[![](https://img.youtube.com/vi/Hdyp8f7l0VI/0.jpg)](https://www.youtube.com/watch?v=Hdyp8f7l0VI)

**※ヒント**：もちろんノーマルマップをバンプのように疑似立体表現として利用することもできます。ただしバンプ表現に用いられる場合、ノーマルマップは実際にジオメトリの表面を凸凹させるものではなく、ライティングでその凹凸を表現するものですので、**ライティングの変化が現れやすくするように、基本色/1影色/2影色のステップを設定してやる**必要があります。[上の例](https://twitter.com/nyaa_toraneko/status/1051359237631164417)の場合、基本色側のステップを0.8、影色側のステップを0.5ぐらいにした上で、さらに少し暗めのハイカラーを足してやることで立体感を強調してやっています。  

---
### 「Shadow Control Maps」サブメニュー
影の落ち具合を調整する、ポジションマップやシェーディンググレードマップを指定します。  
使用するワークフローに応じて、サブメニュー内のアイテムが切り替わります。  

### ● DoubleShadeWithFeather ワークフロー
<img width = "400" src="Images_jpg/URP_image017.jpg">

| `アイテム`  | 機能解説 | プロパティ |
|:-------------------|:-------------------|:-------------------|
| `1st Shade Position Map` | ライティングに関係なく、１影色の位置を強制的に指定したい場合、ポジションマップを割り当てます。必ず影を落としたい部分を黒で指定します。 | _Set_1st_ShadePosition |
| `2nd Shade Position Map` | ライティングに関係なく、２影色の位置を強制的に指定したい場合、ポジションマップを割り当てます。必ず影を落としたい部分を黒で指定します。(１影色のポジションマップにも影響を受けます） | _Set_2nd_ShadePosition |

#### 【ポジションマップとは？】
<img width = "800" src="Images_jpg/0906-18_03.jpg">

ライティングと関係なく影を落としたい部分をポジションマップで指定できます。  
各シーンごとの特殊な影や、演出上追加したい影などがある場合、ライティングに加えて追加できます。  
**※ヒント：Substance Painterなどの3Dペインターを使って、影位置を直接作画してしまうのが簡単です。**  

#### ● １影と２影の各ポジションマップの相互作用について
<img width = "800" src="Images_jpg/0102-22_03.jpg">

ライトの状態に関係なく**常に２影色を表示したい場所は、１影色のポジションマップと２影色のポジションマップの同じ位置を塗りつぶし**ます。  
常に２影色が表示されている領域は、ライトが作る影の中でも常に２影色が表示される領域になります。  
一方、**明るいところでは２影色が表示されない領域**（２影色のポジションマップでは指定されているが、１影のポジションマップでは指定されていない領域）は、ライトが作る影の中に入った時のみ２影色が表示されます。  

---
### ● ShadingGradeMap ワークフロー
<img width = "400" src="Images_jpg/URP_image018.jpg">

| `アイテム`  | 機能解説 | プロパティ |
|:-------------------|:-------------------|:-------------------|
| `ShadingGradeMap` | Shading Grade Mapをグレースケールで指定します。 Shading Grade Mapに使用するテクスチャは、テクスチャインポートセッティングで、必ず `SRGB (Color Texture)` を `OFF` にするようにしてください。 | _ShadingGradeMap |
| `ShadingGradeMap Level` | Shading Grade Mapのグレースケール値をレベル補正します。デフォルトは0で、±0.5の範囲で調整が可能です。 | _Tweak_ShadingGradeMapLevel |
| `Blur Level of ShadingGradeMap` | Mip Map機能を利用して、Shading Grade Mapをぼかします。Mip Mapを有効にするためには、テクスチャインポートセッティングで、Advanced > `Generate Mip Maps` を `ON` にしてください。デフォルトは0（ぼかさない）です。 | _BlurLevelSGM |

#### 【シェーディンググレードマップとは？】
UTS/UniversalToonの標準ワークフローは、`DoubleShadeWithFeather`ですが、その標準ワークフローの機能を元にシェーディンググレードマップというグレースケールのマップを使うことで、さらに影の掛かり方をUV座標単位で制御できるように拡張したワークフローが、`ShadingGradeMap`ワークフローです。  

<img width = "800" src="Images_jpg/0122-06_04.jpg">

通常のトゥーンシェーダーに`Shading Grade Map`（シェーディングの掛かり方傾斜マップ）を足すことで、UV単位で１影色および２影色の掛かりやすさを制御できます。  
このマップを使うことで、部分的に影の出やすさを調整できるので、「**ライトに照らされている面にはでない**服のしわの影」みたいな表現が可能となります。  
画像の例では、`Shading Grade Map`上の黒部分が２影色になり、グレー部分がその濃度によって影の掛かり方が変わります。  
グレー濃度が強いほうが影がかかりやすいので、二つのグレーの境界間にも影が発生します。  

**Ambient Occlusionマップなどの遮蔽マップ**をシェーディンググレードマップに適用すると、ライティングに対してより影をかかりやすくすることができます。他にも、前髪の形状に沿った影とか、服のしわの凹部分とかに使うとよいでしょう。  

---
## 3.「Basic Lookdevs : Shading Step and Feather Settings」メニュー
<img width = "400" src="Images_jpg/URP_image019.jpg">


このメニューでは、基本色/１影色/２影色の各カラーの塗り分け範囲の設定（**Step**）と、各カラー境界ぼかしの強さ(**Feather**)を設定します。リアルタイムのディレクショナルライトの設定と共に、UTS2を使う上で最も重要な設定です。**このブロックの設定で、基本的なルックは決まります**。セルルックおよびイラストレーションルックを作るための基本的なアイテムが集まっているのが、本メニューです。  
これらのアイテムの設定は、Unity上でリアルタイムで繰り返しチェックをすることができます。  
プロパティ変更の結果をいちいちレンダリングして確認する必要がありませんので、じっくりと取り組んでみてください。  
光源方向が同じでも、各Stepと各Featherのパラメタを変えることで、まったく違ったルックを作ることができます。  

### 【Step/Feather各スライダーの基本的な使い方】
[![](https://img.youtube.com/vi/eM3iwE67ICM/0.jpg)](https://www.youtube.com/watch?v=eM3iwE67ICM)

<small>↑ 塗り分け段階を設定するStepスライダー、各色の境界をぼかすFeatherスライダーの基本的な使い方です。</small>  

---
### ● DoubleShadeWithFeather ワークフロー

UTS/UniversalToonの標準ワークフローである、DoubleShadeWithFeatherのアイテムです。  
ライティングとは関係なく、モデルの指定位置に各々１影/２影色を配置できる、**ポジションマップ**を２枚持てるのが特徴です。  
<img width = "400" src="Images_jpg/URP_image020.jpg">

| `アイテム`  | 機能解説 | プロパティ |
|:-------------------|:-------------------|:-------------------|
| `BaseColor Step` | 基本色（明色）と影色領域の塗り分け段階を設定します。 | _BaseColor_Step |
| `Base/Shade Feather` | 基本色（明色）と影色領域の境界をぼかします。 | _BaseShade_Feather |
| `ShadeColor Step` | 影色領域より１影色と２影色の塗り分け段階を設定します。２影色を使用しない場合には、ゼロにしてください。 | _ShadeColor_Step |
| `1st/2nd_Shades Feather` | １影色と２影色の境界をぼかします。 | _1st2nd_Shades_Feather |

---
### ● ShadingGradeMap ワークフロー

高機能版UTS2ワークフローである、ShadingGradeMapワークフローのアイテムです。  
**シェーディンググレードマップ**と呼ばれる、ライティングに対する影の出やすさを制御できるマップを持つことができます。  
シェーディンググレードマップを使うことで、ジオメトリや法線の状態とは関係なく、指定の位置に決まった形状の影色を配置ことができます。  
ポジションマップとの違いは、シェーディンググレードマップは影色を決まった位置に表示するだけでなく、ライトの当て方次第でその出方を調整できるところにあります。  
<img width = "400" src="Images_jpg/URP_image021.jpg">

| `アイテム`  | 機能解説 | プロパティ |
|:-------------------|:-------------------|:-------------------|
| `1st ShadeColor Step` | 基本色（明色）と１影色の塗り分け段階を設定します。`BaseColor Step`と同じ機能です。 | _1st_ShadeColor_Step |
| `1st ShadeColor Feather` | 基本色(明色）と１影色の境界をぼかします。`Base/Shade Feather`と同じ機能です。 | _1st_ShadeColor_Feather |
| `2nd ShadeColor Step` | １影色と２影色の塗り分け段階を設定します。`ShadeColor Step`と同じ機能です。 | _2nd_ShadeColor_Step |
| `2nd ShadeColor Feather` | １影色と２影色の境界をぼかします。`1st/2nd_Shades Feather`と同じ機能です。 | _2nd_ShadeColor_Feather |

---
### 「System Shadows : Self Shadows Receiving」アイテム

Unityのシャドウシステムとトゥーンシェーディングを馴染ませるための調整アイテムです。  
トゥーンシェードの場合、システムが提供する影は、キャラのセルフシャドウ（自身への落ち影）を表現するために必要なものです。  
「Basic Lookdevs : Shading Step and Feather Settings」サブメニューアイテムで塗り分けレベルを決定した後で、さらに微調整をしたい時や、セルフシャドウ等のReceiveShadowの出方を微調整したい時に使用します。  

<img width = "400" src="Images_jpg/URP_image022.jpg">

| `アイテム`  | 機能解説 | プロパティ |
|:-------------------|:-------------------|:-------------------|
| `Receive System Shadows` | Unityのシャドウシステムを使う場合、**Active**にします。ReceiveShadowを使いたい場合には、必ず**Active**します。（同時にMesh Renderer側の`ReceiveShadow`もチェックされている必要があります。） | _Set_SystemShadowsToBase |
| `System Shadows Level` | Unityのシステムシャドウのレベル調整をします。デフォルトは0で、±0.5の範囲で調整が可能です。` | _Tweak_SystemShadowsLevel |
| `Raytraced Hard Shadow`| 上級者向け機能である**リアルタイムレイトレースハードシャドウ（RTHS）機能**を有効にします。DXR(DirectX Raytracing)が正常に稼働する環境で、かつメインカメラに`ShadowRaytracer`コンポーネントがアタッチされている場合、`Active`にすることで、リアルタイムレイトレースによるハードシャドウをシャドウマップとして適用することができます。||

【NOTE】RTHS機能および`ShadowRaytracer`コンポーネントに関しては、詳しくは[こちら](https://github.com/unity3d-jp/RaytracedHardShadow/blob/dev/Documentation~/Readme.md)を参照してください。  


[![](https://img.youtube.com/vi/LXV37a1jhUE/0.jpg)](https://www.youtube.com/watch?v=LXV37a1jhUE)

<small>↑ Unityでシステムシャドウを使いつつ、Stepスライダーを調整していると、影色との領域にノイズが現れることがあります。これらのノイズは、セルルックでは困りものですので、それらを`System Shadows Level`スライダーや`Tessellation`（※Tessellation対応はレガシー版UTS2のみ）を使って改善する方法を紹介しています。</small>  

---
### 「Additional Settings」サブメニュー

主にリアルタイムポイントライト群に対する調整アイテムです。  
<img width = "400" src="Images_jpg/URP_image023.jpg">

| `アイテム`  | 機能解説 | プロパティ |
|:-------------------|:-------------------|:-------------------|
| `Step Offset for PointLights`| リアルタイムポイントライトのステップ（塗り分け段階）を微調整します。 | _StepOffset |
| `PointLights Hi-Cut Filter` | リアルタイムポイントライトの基本色（明色）領域にかかる不要なハイライトをカットします。 特にぼかしのないセルルック時に、セルルック感を高めます。 | _Is_Filter_HiCutPointLightColor |


---
### ● ポイントライトによるカラー塗り分けを微調整する：Step Offset、PointLights Hi-Cut Filter

[![](https://img.youtube.com/vi/fJX8uQKzWhc/0.jpg)](https://www.youtube.com/watch?v=fJX8uQKzWhc)

UTS/UniversalToonは、ポイントライトだけでもセル風のルックが実現できます。  
セルルックは、基本色（明色）/１影色、１影色/２影色の各Stepスライダーを調整して設定しますが、ポイントライトの場合、ディレクショナルライト以上に移動に対する影の変化が顕著になります。  
それらの変化ををある程度抑え込むための微調整用として、`Step Offset`スライダーを使います。  

[![](https://img.youtube.com/vi/WkJId-e2TKk/0.jpg)](https://www.youtube.com/watch?v=WkJId-e2TKk)

`Step Offset`を使うことで、ポイントライトなどのリアルタイムライトのステップ（塗り分け段階）を微調整できます。  
塗り分け用に使われる`BaseColor Step`などの調整は、メインライトによる塗り分け段階を決めるのと同時に、ポイントライト側の設定にも使われます。  
そこに`Step Offset`を併用することで、さらに細かくポイントライトの当たり方を調整できます。特にメカ表現などで、ワカメハイライトなどを表現するのに便利です。  

またポイントライトは、仕様上距離に対して明るさが減衰しますので、特に基本色（明色）部分のハイライトが必要以上に目立つことがあります。  
そのような時に、`PointLights Hi-Cut Filter`をオンにすると、不要なハイライトが抑えられて、よりセルルックに馴染みやすくなります。  
逆に積極的にハイライトを付けたい場合は、`PointLights Hi-Cut Filter`をオフにして使うといいでしょう。  

---
## 4.「HighColor Settings」メニュー

**「ハイカラー」** は、**ハイライト、スペキュラ**とも呼ばれる表現です。  
メインとなるディレクショナルライトからの「光」を照り返す表現として使われます。光の照り返し表現ですので、**ライトが動くと現れる位置も動きます**。  
UTS/UniversalToonでは、ハイカラー表現に対して様々な調整をすることが可能です。  

<img width = "400" src="Images_jpg/URP_image024.jpg">

| `アイテム`  | 機能解説 | プロパティ |
|:-------------------|:-------------------|:-------------------|
| `HighColor` | ハイカラー指定するカラーを指定します。使用しない場合には`黒(0,0,0)`を設定してください。なおハイカラーは光源の方向に従って移動します。 カラー指定と同様にテクスチャも指定できます。テクスチャを利用することで、複雑なカラーを載せることが可能になります。右のパレットのカラーと乗算されますので、テクスチャのカラーをそのまま出したい場合には、パレットを`白(1,1,1)`に設定してください。必要がない場合、テクスチャは設定しなくても大丈夫です。 | _HighColor, _HighColor_Tex |
| `HighColor Power` | ハイカラーの範囲の大きさ（※スペキュラ的には「強さ」になります）を設定します。 | _HighColor_Power |
| `Specular Mode` | `Active`の場合、ハイカラー領域をスペキュラ（グロッシイ光沢）として描画します。`Off`の場合、ハイカラー領域の境界を円形で描画します。 | _Is_SpecularToHighColor |
| `Color Blend Mode` | `Additive`の場合、ハイカラーの合成を加算（結果は明るくなります）にします。スペキュラは加算モードでしか使えません。`Multiply`の場合、ハイカラーの合成を乗算（結果は暗くなります）にします。 | _Is_BlendAddToHiColor |
| `ShadowMask on HighColor` | `Active`の場合、影部分にかかるハイカラー領域をマスクします。 | _Is_UseTweakHighColorOnShadow |
| `HighColor Power on Shadow` | 影部分にかかるハイカラーの強さを調整します。 | _TweakHighColorOnShadow |
| HighColor Mask | 以下、ハイカラーマスクの設定をします。 |  |
| `HighColor Mask` | UV座標に基づきハイカラーをマスクします。白で100%表示、黒でハイカラーを表示しません。必要がない場合、設定しなくても大丈夫です。 | _Set_HighColorMask |
| `HighColor Mask Level` | ハイカラーマスクのレベル補正をします。デフォルト値は0です。 | _Tweak_HighColorMaskLevel |

**※ヒント：リアルタイムポイントライトのハイカラーを有効にしたい場合は、`PointLights Hi-Cut Filter`を`Off`にします。**  

<img width = "800" src="Images_jpg/UT2018_UTS2_SuperTips_rev_16.jpg">

ハイカラーマスクを適用することで、角度によっては肌がテカってしまうような部分を抑えることができます。  
頬や胸に載せる肌のハイカラー表現などで、特に有効です。  

<img width = "800" src="Images_jpg/UT2018_UTS2_SuperTips_31.jpg">
<img width = "800" src="Images_jpg/UT2018_UTS2_SuperTips_32.jpg">

またハイカラーマスクは、鏡面反射を調整するスペキュラマップとしても使うことができますので、金属などの質感を表現するのにも使えます。  
暁ゆ～き（@AkatsukiWorks）さんの作例では、ハイカラーマスクやリムライトマスクを使うことで、イラスト風でありながら同時にそこに使われている各素材（マテリアル）の質感を魅力たっぷりに引き出しています。  


---
## 5.「RimLight Settings」メニュー

**「リムライト」** は、実写の世界では「ライトが被写体の周縁（リム）を照らすように配置する」テクニックを指しています。  
トゥーンシェーダーを含むノンフォトリアリスティックな表現では、形状を強調するのに同じようにエッジにハイライトを置きますが、これもしばしば「リムライト」と呼ばれています。  
UTS/UniversalToonでは、リムライトに関しても様々なアイテムが利用できます。  

<img width = "400" src="Images_jpg/URP_image025.jpg">


| `アイテム`  | 機能解説 | プロパティ |
|:-------------------|:-------------------|:-------------------|
| `RimLight` | `Active`の場合、リムライトを有効にします。 | _RimLight |
| RimLight Settings | 以下、リムライトの設定をします。 |  |
| `RimLight Color` | リムライトのカラーを指定します。 | _RimLightColor |
| `RimLight Power` | リムライトの強さを指定します。 | _RimLight_Power |
| `RimLight Inside Mask` | リムライトの内側マスクの強度を指定します。 | _RimLight_InsideMask |
| `RimLight FeatherOff` | `Active`の場合、リムライトのぼかしをカットします。 | _RimLight_FeatherOff |
| `LightDirection Mask` | `Active`の場合、光源方向にのみリムライトを発生します。 | _LightDirection_MaskOn |
| `LightDirection MaskLevel` | 光源方向リムマスクのレベル調整をします。 | _Tweak_LightDirection_MaskLevel |
| `Antipodean(Ap)_RimLight` | `Active`の場合、光源方向に対し反対方向の位置にリムライト（**APリムライト/対蹠リムライト**）を発生させます。 | _Add_Antipodean_RimLight |
| Ap_RimLight Settings | 以下、APリムライト（対蹠リムライト）の設定をします。 |  |
| `Ap_RimLight Color` | APリムライトのカラーを指定します。 | _Ap_RimLightColor |
| `Ap_RimLight Power` | APリムライトの強さを指定します。 | _Ap_RimLight_Power |
| `Ap_RimLight FeatherOff` | `Active`の場合、APリムライトのぼかしをカットします。 | _Ap_RimLight_FeatherOff |
| RimLight Mask | 以下、リムライトマスクの設定をします。 |
| `RimLight Mask` | UV座標に基づきリムライトをマスクします。白で100%表示、黒でリムライトを表示しません。必要がない場合、設定しなくても大丈夫です。 | _Set_RimLightMask |
| `RimLight Mask Level` | リムライトマスクのレベル補正をします。デフォルト値は0です。 | _Tweak_RimLightMaskLevel |

<img width = "800" src="Images_jpg/UT2018_UTS2_SuperTips_14.jpg">

**基本的なリムライトは、カメラから見てオブジェクトの周縁に表示**されます。  
上に加えてUTS2では、メインライトが存在する方向を考慮してリムライトの出る位置を調整することができます。（`LightDirection Mask`）  
さらに**光源とは反対方向のリムライト（対蹠リムライト）も設定できます**（`Antipodean(Ap)_RimLight`が`Active`）ので、「照り返し」も表現することが可能です。  
もし光源方向のリムライトもカットして、光源方向の反対のみにリムライトを発生したい場合には、光源方向のリムライトのカラー（`RimLight Color`）を`黒（0,0,0）`に指定してください。  

<img width = "800" src="Images_jpg/UT2018_UTS2_SuperTips_15.jpg">

またリムライトは、ハイカラーと同様にカメラの角度によってはひどくテカってしまうことがあります。  
UTS2では、リムライトマスクを設定することで、それらのテカりを抑えることができます。  
上の画像では、光源方向と照り返し方向のリムライトのカラーを変えた上に、脇の下などにリムライトマスクをかけることで不要なテカリを避けています。  

<img width = "800" src="Images_jpg/UT2018_UTS2_SuperTips_33.jpg">
<img width = "800" src="Images_jpg/UT2018_UTS2_SuperTips_34.jpg">
<img width = "800" src="Images_jpg/UT2018_UTS2_SuperTips_35.jpg">

またリムライトマスクを使うことで、「**金属的な材質表現**」を他の素材と調整することで強調したり、服に差し込む入射光を調整することで 「**ベルベット風衣類のしわ表現**」などをすることが可能です。  

---
## 6.「MatCap : Texture Projection Settings」メニュー

**「[マットキャップ（MatCap）](http://wiki.polycount.com/wiki/Matcap)」** とは、カメラベースでオブジェクトに貼り付けるスフィアマップのことです。ZBrushの質感表現で使われています。  
Google画像検索で、「Matcap」で検索すると、様々なMatcapの例を見ることができます。物理ベースシェーダーが普及する以前は、金属的なテカリを表現する時によく使われました。  
それらの金属的な質感表現だけでなく、Matcapは工夫次第で様々な質感を表現することが可能です。  
UTS/UniversalToonでは、Matcapテクスチャを乗算だけでなく加算でも合成できます。  

<small>**※ヒント：UTS/UniversalToonでは、カメラパースによるMatCapの歪みに対して適切な補正がはいるので、オブジェクトがカメラの端に来てもMatCapが歪みません。この設定は、`MatCap Projection Camera`で調整します。**</small>  

<img width = "400" src="Images_jpg/URP_image026.jpg">

| `アイテム`  | 機能解説 | プロパティ |
|:-------------------|:-------------------|:-------------------|
| `MatCap` | `Active`の場合、MatCapを有効にします。 | _MatCap |
| MatCap Settings | 以下、MatCapの設定をします。 |
| `MatCap Sampler` | MatCapとして使用するテクスチャを設定します。右側のカラーがテクスチャに乗算されます。 | _MatCap_Sampler, _MatCapColor |
| `Blur Level of MatCap Sampler` | Mip Map機能を利用して、MatCap_Samplerをぼかします。Mip Mapを有効にするためには、テクスチャインポートセッティングで、Advanced > `Generate Mip Maps` を `ON` にしてください。デフォルトは0（ぼかさない）です。 | _BlurLevelMatcap |
| `Color Blend Mode` | `Additive`の場合、MatCapのブレンドが**加算モード**になります（結果は明るくなります）。`Multiply`の場合には**乗算モード**で合成されます（結果は暗くなります）。 | _Is_BlendAddToMatCap |
| `Scale MatCapUV` | `MatCap Sampler`のUVを中央から円形にスケールすることで、MatCapの領域調整ができます。 | _Tweak_MatCapUV |
| `Rotate MatCapUV` | `MatCap Sampler`のUVを中央を軸に回転します。 | _Rotate_MatCapUV |
| `CameraRolling Stabillizer` | `Activate`にすることで、カメラのローリング（奥行き方向を軸とした回転のこと）に対してMatCapが回転してしまうのを抑止します。MatCapをカメラのローリングに対して固定したい時に便利な機能です。 | _CameraRolling_Stabilizer |
| `NormalMap for MatCap` | `Active`にすることで、MatCapにMatCap専用ノーマルマップを割り当てます。MatCapをスペキュラ的に使っている場合には、スペキュラマスクとして使用できます。 | _Is_NormalMapForMatCap |
| NormalMap for MatCap as SpecularMask | 以下、MatCap専用ノーマルマップの設定をします。 |  |
| `NormalMap` | MatCap専用ノーマルマップを設定します。右側のスライダーはスケールです。 | _NormalMapForMatCap, _BumpScaleMatcap |
| `Rotate NormalMapUV` | MatCap専用ノーマルマップのUVを中央を軸に回転します。 | _Rotate_NormalMapForMatCapUV |
| `MatCap on Shadow` | `Active`にすることで、影部分にかかるMatCap領域をマスクします。 | _Is_UseTweakMatCapOnShadow |
| `MatCap Power on Shadow` | 影部分にかかるMatCapの強さを調整します。 | _TweakMatCapOnShadow |
| `MatCap Projection Camera` | ゲームビュー内で使用するカメラのプロジェクションを指定します。**パースカメラ（`Perspective`）の時には、カメラ歪み補正が働きます**。 | _Is_Ortho |
| MatCap Mask | 以下、MatCap Maskの設定をします。 |  |
| `Matcap Mask` |MatCapにグレースケールのマスクを設定することで、MatCapの出方を調整します。Matcap Maskは、MatCapが投影されるメッシュのUV座標基準で配置されます。黒でマスク、白で抜きになります。 | _Set_MatcapMask |
| `Matcap Mask Level` | Matcap Maskの強さを調整します。値が1の時、マスクのあるなしに関わらずMatCapを100％表示します。値が-1の時には、MatCapは一切表示されず、MatCapがオフの状態と同じになります。デフォルト値は0です。 | _Tweak_MatcapMaskLevel |
| `Inverse Matcap Mask` | `Active`にすることで、Matcap Maskを反転します。 | _Inverse_MatcapMask |

<img width = "800" src="Images_jpg/UT2018_UTS2_SuperTips_36.jpg">

上の例では、**Matcapを疑似環境マップとして利用**しています。  
他にもMatcapを利用することで、つるつるした表面に光が反射する**ヌルテカ表現**も、適度なイラスト感を保ったまま、まとめることができます。  

<img width = "800" src="Images_jpg/URP_image039.jpg">

上の例では、**サラサラ感のある髪の毛の光沢を表現する**のに、`MatCap`と`NormalMap for MatCap`、`Matcap Mask`を使用しています。  

* MatCap Sampler : 髪の上に乗算合成される、光の輪を表現します。  
* NormalMap for MatCap : MatCap単体だとそのままの形状で合成されてしまいますが、NormalMap for MatCapを細かいリピートで重ねることで、三日月型の光沢をサラサラ感のある光に散らしています。このような使い方を**スペキュラマスク**と呼びます。ここで使われるノーマルマップは、バンプ的な表現には使われません。  
* MatCap Mask : MatCapが表示される範囲を調整します。垂直方向のグラデーションマスクを設定することで、`Matcap Mask Level`スライダーを調整することで、MatCapが表示される範囲を簡単に制御することができます。  

<img width = "800" src="Images_jpg/URP_image040.jpg">

MatCap Maskを使うことで、上のようなライトクッキー的な表現も可能です。  

---
## 7.「AngelRing Projection Settings」メニュー

**「AngelRing（天使の輪）」** とは、カメラから見て常に固定の位置に現れるハイライト表現で、髪のハイライト表現として使われます。「天使の輪」機能は、`ShadingGradeMap`ワークフローで利用することができます。  

<img width = "300" src="Images_jpg/AngelRing.jpg">
<img width = "300" src="Images_jpg/UTS2_TopImage06.gif">

「天使の輪」は、それが投映されるメッシュのUV2を参照しますので、Mayaや3ds Max、BlenderなどのDCCツールで、事前にUV2を設定しておく必要があります。  

<img width = "400" src="Images_jpg/URP_image027.jpg">

| `アイテム`  | 機能解説 | プロパティ |
|:-------------------|:-------------------|:-------------------|
| `AngelRing Projection` | `Active`の場合、「天使の輪」機能を有効にします。 | _AngelRing |
| AngelRing Sampler Settings | 以下、AngelRing Samplerの設定をします。 |  |
| `AngelRing` | 「天使の輪」テクスチャを指定します。右に指定したカラーがテクスチャに乗算されます。 | _AngelRing_Sampler, _AngelRing_Color |
| `Offset U` | 「天使の輪」表示を水平方向に微調整します。 | _AR_OffsetU |
| `Offset V` | 「天使の輪」表示を垂直方向に微調整します。 | _AR_OffsetV |
| `Use α channel as Clipping Mask` | `Active`の場合、「天使の輪」テクスチャに含まれるαチャンネルをクリッピングマスクとして利用します。`Off`の場合、αチャンネルは利用しません。 | _ARSampler_AlphaOn |

### ●「天使の輪」用素材の作成
まず「天使の輪」機能を適用する髪の毛のメッシュに、２つめのUVを設定しましょう。  

「天使の輪」用のUVは、通常の髪用テクスチャのUVとは別に、「天使の輪」を適用する髪全体をキャラの正面方向から平面投影して作成します。  
<img width = "800" src="Images_jpg/HairModel.jpg">

**※UV2の作成を含むこれらの作業は、Mayaや3ds Max、BlenderなどのDCCツールで行います。**  

「天使の輪」用UVをガイドに、ハイライト部分のテクスチャを描きます。ハイライト部分のカラーは元のカラーに加算で合成されます。  
作成したテクスチャは、`AngelRing`のテクスチャに登録します。  
ハイライトは白で描いて、後に乗算でカラーを載せてもよいでしょう。  

<img width = "800" src="Images_jpg/Hair_UV1.jpg">

`Use α channel as Clipping Mask`を`Active`にすると、下の図のように「天使の輪」テクスチャのαチャンネルがクリッピングマスクとして利用できるようになります。  
「天使の輪」のカラーを加算でなく、直接指定できるようになります。  

<img width = "800" src="Images_jpg/0609-04_13.jpg">

---
## 8.「Emissive : Self-luminescene Setings」メニュー

**「エミッシブ」** とは、自己発光のことです。  
カラーに**HDRカラー**（明るさとして1以上の値を持てるカラー仕様のこと）を定義することで、周りのカラーよりも明るい領域を設定することができます。  
<img width = "800" src="Images_jpg/UT2018_UTS2_SuperTips_17.jpg">

**[Post-Processing in UniversalRP](https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@9.0/manual/integration-with-post-processing.html)の[Bloom](https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@9.0/manual/post-processing-bloom.html)など、カメラにアタッチされるポストエフェクトと共に使われることで、パーツを効果的に光らせることができます。**  

<img width = "400" src="Images_jpg/URP_image028.jpg">

| `アイテム`  | 機能解説 | プロパティ |
|:-------------------|:-------------------|:-------------------|
| `Emissive` | エミッシブ用のテクスチャを設定します。グレースケールでテクスチャを作成し、それに乗算するカラーを載せることで光らせることもできます。 右側のカラーが、テクスチャの各ピクセルカラーに乗算されます。多くの場合、 **[HDRカラー](https://docs.unity3d.com/ja/current/Manual/HDRColorPicker.html)** を設定します。他のパーツと重ねて**光って欲しくない部分は、テクスチャ上で黒（RGB:0,0,0）にしておきます。** | _Emissive_Tex.rgb, _Emissive_Color |
| `Emissivテクスチャのαチャンネル` | v.2.0.7より、αチャンネルがエミッシブテクスチャのマスクとして使えるようになりました。UVベースで、αチャンネルを白（RGB = (1,1,1)）に設定した位置にエミッシブを表示します。黒（RGB=(0,0,0)）でエミッシブが表示されなくなります。 | _Emissive_Tex.a |
| `Emissive Animation` | `Active`にすることで、`Emissive`で指定したテクスチャのRGBチャンネル部分を、様々な方法でアニメーションします。**αチャンネルはマスクですので、アニメーションの対象にはなりません。** | EMISSIVE MODE = ANIMATION |
| `Base Speed (Time)` | アニメーションの基本となる更新スピードを指定します。値1の時、1秒で更新することになります。値2を指定すると、値1の時の2倍のスピードになりますので、 0.5秒で更新することになります。 | _Base_Speed |
| `UV Coord Scroll`、`View Coord Scroll` | スクロールに使用する座標系を指定します。`UV Coord Scroll`の場合、Emissive_TexのUV座標を基準にスクロールをします。`View Coord Scroll`の場合、MatCapと同様のビュー座標を基準にスクロールをします。ビュー座標系でのスクロールは、テクスチャのUV座標を考慮しないで済むのでとても便利ですが、キューブのようなフラットな面を持つオブジェクトでは、うまく表示できない場合がほとんどです。一方、キャラクターなどの曲面が多いオブジェクトでは、ビュー座標系は大変便利に使えます。 | _Is_ViewCoord_Scroll |
| `Scroll U direction` | アニメーションの更新にあたり、EmissiveテクスチャをU方向（X軸方向）にどれだけスクロールさせるかを指定します。-1～1範囲で指定し、デフォルトは0です。スクロールアニメーションは、最終的には、`Base Speed (Time)`×`Scroll U Direction`×`Scroll V Direction`の結果として決まります。 | _Scroll_EmissiveU |
| `Scroll V direction` | アニメーションの更新にあたり、EmissiveテクスチャをV方向（Y軸方向）にどれだけスクロールさせるかを指定します。-1～1範囲で指定し、デフォルトは0です。 | _Scroll_EmissiveV |
| `Rotate around UV center` | アニメーションの更新にあたり、EmissiveテクスチャをUV座標の中央（UV=(0.5,0.5)）を軸にどれだけ回転させるかを指定します。Base Speed=1の時、値1で右まわり方向に1回転します。スクロールと組み合わせた場合、スクロール後に回転することになります。 | _Rotate_EmissiveUV |
| `PingPong Move for Base` | `Active`にすることで、アニメーションの進行方向をPingPong（行ったり来たり）にします。 | _Is_PingPong_Base |
| `ColorShift with Time` | `Active`にすることで、Emissiveテクスチャに掛け合わせるカラーを、`Destination Color`に向かう線形補間（Lerp）で変化させます。**この機能を利用する時には、Emissiveテクスチャでの指定はグレースケールとし、掛け合わせるカラー側でカラー設計をしたほうがよいでしょう。** | _Is_ColorShift |
| `Destination Color` | カラーシフトをする際の、ターゲットとなるカラーです。HDRで指定できます。 | _ColorShift |
| `ColorShift Speed (Time)` | カラーシフトをする際の、基準となるスピードを設定します。値が1の時、1サイクルの変化はおおよそ6秒程度を目安としてください。 | _ColorShift_Speed |
| `ViewShift of Color` | `Active`にすることで、オブジェクトを見るカメラのビュー角に対してカラーをシフトさせます。オブジェクトのサーフェイスに対し真っ正面から見た場合は、通常状態のEmissiveカラーが表示され、ビュー角が徐々に傾いていくにつれてシフト先のカラーに変化します。 | _Is_ViewShift |
| `ViewShift Color` | ビューシフトする際の、変化先となるカラーです。HDRで指定します。 | _ViewShift |

<img width = "800" src="Images_jpg/UT2018_UTS2_SuperTips_42.jpg">
<img width = "800" src="Images_jpg/UT2018_UTS2_SuperTips_43.jpg">
<img width = "800" src="Images_jpg/UT2018_UTS2_SuperTips_48.jpg">

あいんつばい（@einz_zwei）さんの作例。エミッシブパーツが大変効果的に使われています。  
しかもカラーマップとエミッシブマップを組み合わせることで、ライトの明るさの変化に応じて、ディティールが追加されるような仕組みになっています。  

---
### ● αチャンネル付きテクスチャを作成するには

αチャンネル付きテクスチャは、PhotoshopなどのDCCツールで作成します。  
チャンネルタブより、新規チャンネルを追加し、できたチャンネルの上にグレースケールの画像を貼り付ければ、αチャンネルとして利用出来ます。Targa形式などαチャンネルが持てる画像形式の場合は、このままセーブできます。  

<img width = "800" src="Images_jpg/Emissive_Tex00.jpg">

Unity上でαチャンネルを有効にするためには、各テクスチャのImport Settingsで、`Alpha Source`を`Input Texture Alpha`にしてください。  

**PNG形式の場合**は画像仕様上、直接αチャンネルを持てないので、Photoshop上でαチャンネルを選択範囲として読み込んだ後で、「レイヤーマスク＞選択範囲外をマスク」で指定し、PNG形式で保存します。  

<img width = "800" src="Images_jpg/Emissive_Tex01.jpg">
<img width = "800" src="Images_jpg/Emissive_Tex02.jpg">

続いてUnityに読み込み、Import Settingsで、`Alpha Source`を`Input Texture Alpha`に、`Alpha Is Transparency`を`ON`にしてください。

<img width = "500" src="Images_jpg/Emissive_Tex03.jpg">

---
### ● Destination Color設定の際のTips

カラーシフト機能を使う際に、`Destination Color`をターゲットに設定しますが、元のカラーとターゲットとなるカラーが同色相の場合、想定していないカラーがフレームに混じることがあります。例えば、下図の矢印左側のカラーから、一見見た目は同じような右側の２つのいずれかのカラーにシフトさせると、矢印右側１つめのカラーは同色相の範囲でカラーシフトしますが、矢印右側２つめのカラーは、青っぽいフレームが混じります。  

<img width = "800" src="Images_jpg/DestColor00.jpg">

これは、青っぽいフレームが混じるほうのカラーには、元のカラーのRGBと比較してみると、値が高くなっているBチャンネルがあるからです。  

このように、**同色相内で輝度が違うカラーをターゲットにシフトさせる場合、各RGBの変化の方向を揃える**ことで、想定外のカラーがフレームに混入するのを避けることができます。  

<img width = "800" src="Images_jpg/DestColor01.jpg">

↑同一色相内でカラーがシフトする例。ターゲットカラーのRGBの値は、いずれも元のカラーよりも小さい。  

<img width = "800" src="Images_jpg/DestColor02.jpg">

↑色相外のフレームが混じる例。ターゲットカラーのBの値が元のカラーよりも高く、かつG値の変化が極端に大きい。  

---
## 9.「Outline Settings」メニュー

アウトライン関連の様々な設定をおこないます。なお本メニューは、`Transparent Mode`が`Off`の時、有効になります。`Transparent Mode`が`On`の時は、表示されません（この時、アウトライン機能も`Off`になります）。  

<small>【**参考**】UTS/UniversalToonでは、`Transparent Mode`が`Off`で、かつ`TransClipping Mode`が有効の時、アウトラインを表示することができます。この時、半透明部分ではプレデプス処理がおこなわれませんので、`Transparent Mode`と表示が異なる場合がありますが、こちらは仕様といたします。</small>  

UTS2では、アウトライン機能として、**マテリアルベースのオブジェクト反転方式のアウトライン**を採用しています。  
この方式を簡単に説明すると、シェーダーで元のオブジェクトよりも少し大きめのオブジェクトを面法線だけ反転して生成します。  
新たに生成したアウトライン用オブジェクトには、フロントカリングで描画されますので、元よりも少し大きめに生成したぶんだけ、それが元のオブジェクトによって上書きされると、はみ出した部分がアウトラインのように見えるというものです。  
この方式は比較的軽い上に調整が楽にできるので、ゲーム用のアウトラインとして伝統的に使われてきました。  
**実際にオブジェクトの周りにラインを引いているわけではない**ということに、注意してください。  

**＊参考：実際にオブジェクトの周りにラインを描画する方式もありますが、それらは主にポストプロセス（ポストエフェクト）方式のアウトラインとして知られています。**  
ポストプロセス方式のアウトラインは採用する方式によって、スピードもクオリティも異なります。実際のゲームでは、従来型のオブジェクト反転方式に、軽めのポストプロセス方式を加えて補正する場合が多いです。  

<img width = "400" src="Images_jpg/URP_image031.jpg">

| `アイテム`  | 機能解説 | プロパティ |
|:-------------------|:-------------------|:-------------------|
| `Outline` | `Active`の時、アウトライン機能を有効にします。`Off`の時、無効になります |  |
| `Outline Mode` | アウトライン用反転オブジェクトの生成方式を指定します。`Normal Direction`（法線反転方式） / `Position Scalling`（ポジションスケーリング方式）から選択できます。多くの場合、法線反転方式が使われますが、ハードエッジだけで構成されているキューブのようなメッシュの場合、ポジションスケーリング方式のほうがアウトラインが途切れにくくなります。比較的単純な形状はポジションスケーリング方式で、キャラクターなどの複雑な形状のものは法線反転方式を使うといいでしょう。 | _OUTLINE |
| `Outline Width` | アウトラインの幅を設定します。 **※注意：この値は、Unityへのモデルインポート時のスケールに依存します** ので、取り込みスケールが１でない時には注意してください。 | _Outline_Width |
| `Outline Color` | アウトラインのカラーを指定します。 | _Outline_Color |
| `BlendBaseColor to Outline` | オブジェクトの基本カラーにアウトラインのカラーを馴染ませたい場合に、`Active`にします。 | _Is_BlendBaseColor |
| `Outline Sampler` | アウトラインの幅に入り抜きを入れたい場合や特定のパーツにのみアウトラインを乗せたくない場合などにアウトラインサンプラー（テクスチャ）で指定します。白で最大幅、黒で最小幅になります。必要がない場合、設定しなくても大丈夫です。 | _Outline_Sampler |
| `Offset Outline with Camera Z-axis` | アウトラインをカメラの奥行き方向（Ｚ方向）にオフセットします。スパイク形状の髪型などの場合、プラスの値を入れることでスパイク部分にはアウトラインがかかりにくくなります。通常は０を入れておいてください。 | _Offset_Z |

---
### 「Advanced Outline Settings」サブメニュー

本サブメニューのアイテムで、アウトライン機能をさらに強化することができます。

<img width = "400" src="Images_jpg/URP_image032.jpg">

| `アイテム`  | 機能解説 | プロパティ |
|:-------------------|:-------------------|:-------------------|
| `Farthest Distance to vanish` | カメラとオブジェクトの距離でアウトラインの幅が変化する、最も遠い距離を指定します。この距離でアウトラインがゼロになります。 | _Farthest_Distance |
| `Nearest Distance to draw with Outline Width` | カメラとオブジェクトの距離でアウトラインの幅が変化する、最も近い距離を指定します。この距離でアウトラインが`Outline_Width`等で設定した最大の幅になります。 | _Nearest_Distance |
| `Use Outline Texture` | アウトライン用反転オブジェクトにテクスチャを貼りたい場合、`Active`にします。 | _Is_OutlineTex |
| `Outline Texture` | アウトラインに特別なテクスチャを割り当てたい時に使用します。テクスチャを工夫することで、アウトラインに模様を入れたりすることができる他、フロントカリングされる反転オブジェクトに貼られるテクスチャだと考えると、一風変わった表現ができます。 | _OutlineTex |
| `Use Baked Normal for Outline` | `Active`の場合、`BakedNormal for Outline`を有効にします。このアイテムは、アウトラインの描画方式が法線反転方式の時のみ現れます。 | _Is_BakedNormal |
| `Baked NormalMap for Outline` | 事前に他のモデルから頂点法線を焼き付けたノーマルマップを、法線反転方式アウトラインの設定時に追加として読み込みます。詳しい説明は[下](index_ja.md#%E3%83%99%E3%82%A4%E3%82%AF%E3%81%97%E3%81%9F%E9%A0%82%E7%82%B9%E6%B3%95%E7%B7%9A%E3%82%92%E8%BB%A2%E5%86%99%E3%81%99%E3%82%8Bbaked-normal-for-outline)を参照してください。 | _BakedNormal |

---
### ● アウトラインの強弱を調整する：**Outline Sampler**
<img width = "800" src="Images_jpg/0906-18_01.jpg">

黒でラインなし、白でラインの幅が100%になります。  
適宜 Outline_Sampler を設定することで、アウトラインに入り抜き（強弱）が発生します。  

**※Tips：Outline Sampler を複数キャラに適用する際に、各キャラのパーツのUV配置を共通化する一工夫をすると、モデル汎用に入り抜きの制御が調整できるようになって便利です。**  

---
### ● ベイクした頂点法線を転写する：**Baked Normal for Outline**  
頂点法線を焼き付けたノーマルマップを、法線反転アウトラインの設定時に追加的に読み込むことができるようになりました。本機能を使うことで、ハードエッジのオブジェクトに、ソフトエッジのオブジェクトのアウトラインを、事前にベイクしたノーマルマップを経由して適用することができるようになります。  

Baked Normalマップを使用する時には、UTS2のアウトライン設定メニューで、  
1.  Outline Mode を **"Normal Direction"** に  
2.  Use Baked Normal for Outline を **"Active"** に  
3.  Baked Normal for Outline に使用したいマップを適用します。 

**Baked Normal for Outline として適用できるノーマルマップ**は以下のような仕様となっています。  
1.  適用するオブジェクトの UV は重ならないこと。つまり、**全てのノーマルマップが重ならないように UV 展開がされていること**が必須です。  
2.  ノーマルマップ自体の仕様は、Unity と同じで、**OpenGL 準拠**となります。  
3.  使用するノーマルマップのテクスチャ設定は、以下のようになります。  
・Texuture Type は **"Default"** にする。 **※注意： "Normal map" に設定してはいけません**。  
・sRGB (Color Texture) を必ず **"OFF"** にする。  

詳しくはサンプルプロジェクト内の Baked Normal フォルダ内のアセットを確認してください。  

**※注意：この方式による頂点法線の調整は、バーテックスシェーダー側で行われますので、適用される頂点数にそのまま依存します**。つまり、ピクセルシェーダー側のように頂点法線間で補正するものではありませんので、注意してください。  

---
### ● アウトラインをカメラの奥に移動する：**Offset Outline with Camera Z-axis**
<img width = "800" src="Images_jpg/0205-11_01.jpg">

`Offset Outline with Camera Z-axis`に値を入れることで、アウトラインがカメラの奥行き方向（Ｚ方向）にオフセットされます。  
図のようなスパイク形状の髪型の場合に、スパイク部分のアウトラインの出方を調整するのに使用します。  
通常は０を入れておいてください。  

---
## 10.「LightColor Contribution to Materials」メニュー

各カラーに対する、シーン内のリアルタイムライトのカラーの影響を、個別にON/OFFできるスイッチを集めたものです。
`Active`の場合、各カラーに対するリアルタイムライトのカラーの影響が有効となり、`Off`の場合、インテンシティが１の時の各カラーの設定色がそのまま表示されます。

[![](https://img.youtube.com/vi/THzoRcWpUmU/0.jpg)](https://www.youtube.com/watch?v=THzoRcWpUmU)

本メニューから、各カラーへのライトカラーコントリビューション（寄与）のあり/なしを一元的に管理できます。  
実際にシーン内で使用するキャラクターライトを使いながら、各カラーへの影響がライトコントリビューションのあり/なしでどう変わるかをリアルタイムで確認できます。ルックデブの仕上げに使うとよいでしょう。  

<img width = "400" src="Images_jpg/URP_image029.jpg">

| `アイテム`  | 機能解説 | プロパティ |
|:-------------------|:-------------------|:-------------------|
| `Base Color` | 基本色に対しライトカラーを有効にします。 | _Is_LightColor_Base |
| `1st ShadeColor` | １影色に対しライトカラーを有効にします。 | _Is_LightColor_1st_Shade |
| `2nd ShadeColor` | ２影色に対しライトカラーを有効にします。 | _Is_LightColor_2nd_Shade |
| `HighColor` | ハイカラーに対しライトカラーを有効にします。 | _Is_LightColor_HighColor |
| `RimLight` | リムライトに対しライトカラーを有効にします。 | _Is_LightColor_RimLight |
| `Ap_RimLight` | APリムライト（対蹠リムライト）に対しライトカラーを有効にします。 | _Is_LightColor_Ap_RimLight |
| `MatCap` | MatCapに対しライトカラーを有効にします。 | _Is_LightColor_MatCap |
| `AngelRing` | 「天使の輪」に対しライトカラーを有効にします。 | _Is_LightColor_AR |
| `Outline` | アウトラインに対しライトカラーを有効にします。アウトラインに対するライトカラーの寄与は次のとおりです。「OFF」の時、アウトラインカラーに設定したカラーがそのまま表示されます。「Activeの時でシーン中にリアルタイムディレクショナルライトが１灯ある」時、リアルタイムディレクショナルライトのカラーと明るさにアウトラインカラーが反応します。「Activeの時でシーン中にリアルタイムディレクショナルライトがない」時、Environment LightingのSourceの内、Colorの色と明るさにアウトラインカラーが反応します。**この時、Skyboxを使用していてもColorの値が参照されることに注意してください。またリアルタイムのポイントライトやColor以外の環境光には、反応しませんので合わせてご注意ください。**  | _Is_LightColor_Outline |

※ヒント：各カラーの設定が `Off` の場合、シーン内のライトの強さに関わらず、「オフにされたカラーは、常にライトのIntensityが１、ライトカラーが白の状態で照らされている状態」になります。  

---
## 11.「Environmental Lighting Contributions Setups」メニュー

本メニューには、シーン内の環境光設定（Skybox、Gradient、ColorなどのEnvironment Lighting）やライトプローブに対して、UTS2がどの程度反応をするか調整したり、リアルタイムディレクショナルライトがない環境で起動するシェーダービルトインライトの明るさを調整をするアイテムが含まれています。  
また**VRChatユーザーに便利な機能**である、**SceneLights Hi-Cut Filter**のような白飛び防止機能のON/OFFも、このメニューからコントロールすることが可能です。  

<img width = "400" src="Images_jpg/URP_image030.jpg">

| `アイテム`  | 機能解説 | プロパティ |
|:-------------------|:-------------------|:-------------------|
| `GI Intensity` | `GI Intensity` を０以上に設定することで、UnityのLightingウィンドウ内で管理されているGIシステム、特に[ライトプローブ](https://docs.unity3d.com/ja/current/Manual/LightProbes.html)に対応します。 `GI Intensity` が１の時、シーン内のGIの強度が100％となります。 | _GI_Intensity |
| `Unlit Intensity` | シーン内に有効なリアルタイムディレクショナルライトが１灯もない時に、[Environment LightingのSource設定](https://docs.unity3d.com/ja/current/Manual/GlobalIllumination.html)を元にシーンの明るさとカラーを求め、それを`Unlit Intensity`の値でブーストして光源として使用します（本機能を **「アンビエントブレンディング」** と呼んでいます）。デフォルトは１（アンビエントカラーをそのまま受ける）で、０にすると完全に消灯します。本機能は環境カラーにマテリアルカラーを馴染ませたい時に使いますが、 **より暗めに馴染ませたい場合は 0.5～1 程度** に設定し、 **より明るくカラーを出したい場合は 1.5～2 程度** に設定するとよいでしょう。 | _Unlit_Intensity |
| `SceneLights Hi-Cut Filter` | シーン内に極端に明るさ（Intensity）が高い、複数のリアルタイムディレクショナルライトやリアルタイムポイントライトがある場合に、白飛びを抑えます。`Active`にすることで、各々のライトのカラーと減衰特性を保ちつつ、マテリアルカラーが白飛びするような高いインテンシティだけをカットします。デフォルトは`Off`です。本機能を使用する時には、「LightColor Contribution to Materials」メニュー内の、基本３色の設定がすべて`Active`になっていることを確認してください。 **VRChatユーザーは`Active`にすることをお薦めします。** ※ヒント：この機能を使っても白飛びが発生する場合、ポストエフェクト側のブルームなどの設定をチェックしてみてください。（特にブルームのスレッショルドの値が１以下だと白飛びしやすくなります。） | _Is_Filter_LightColor |
| `Built-in Light Direction` | 上級者向け機能として、ビルトインライトディレクション（シェーダー内に組み込まれているバーチャルライトの方向ベクトル）を有効にします。本機能が有効な時、ライトの明るさとカラーは、シーン内の有効なリアルタイムディレクショナルライトの値を使用します。もしそのようなライトがない場合は、アンビエントブレンディングの値を使用します。 | _Is_BLD |
| Built-in Light Direction Settings | 以下、ビルトインライトディレクションの設定をします。 |  |
| `Offset X-Axis Direction` | ビルトインライトディレクションによって生成される、バーチャルライトを左右に動かします。 | _Offset_X_Axis_BLD |
| `Offset Y-Axis Direction` | ビルトインライトディレクションによって生成される、バーチャルライトを上下に動かします。 | _Offset_Y_Axis_BLD |
| `Inverse Z-Axis Direction` | ビルトインライトディレクションによって生成される、バーチャルライトの向きを前後で切り替えます。 | _Inverse_Z_Axis_BLD |

---
### ● ライトプローブの明るさを決定する：GI Intensity

<img width = "800" src="Images_jpg/GI_Intensity.jpg">

<small>**↑ 左側：GI Intensity = 0、右側：GI Intensity = 1。GI Intensityの数値を上げると、マテリアルカラーにライトプローブのカラーが加算される。**</small>  

<img width = "800" src="Images_jpg/LightProbe.jpg">

<small>**↑ ステージ上に配置された、ベイク用ポイントライトとライトプローブの例。ベイクドライトは、各レンジが重なっても問題ない。ライトプローブは、ユニティちゃんの足元から背の高さまで敷き詰める。**</small>  

`GI Intensity` を０以上に設定することで、ライトプローブなどの加算合成系のGIシステムに対応します。  
ベイクドライトと一緒にシーン内にベイクされたライトプローブは、環境補助色としてマテリアルカラーに加算されます。  
`GI Intensity`が１の時、ライトプローブに焼き付けられたカラーを100％加算します。０の時は、元のマテリアルカラーのままです。  

<img width = "800" src="Images_jpg/GI_IntensityOFF.jpg">

<small>**↑ GI Intensity = 0</small>**  

<img width = "800" src="Images_jpg/GI_IntensityON.jpg">

<small>**↑ GI Intensity = 1</small>**  

---
### ● アンビエントブレンディングを調整する：Unlit Intensity  

アンビエントライトの設定をライトカラーが反映するようになりました。  
その結果として、ディレクショナルライトのインテンシティの下限が、シーンのアンビエントライトの設定となります。  
VRChatで、アンビエントライトの設定に基づくワールドごとの明るさの差異を自動で調整できます。  
なおアンビエントライトからの明るさは、Unlit_Intensity スライダーで調整することができます。Unlit_Intensityは、アンビエントライトの明るさをブーストします。  
デフォルトは 1（そのまま）になっています。  

また有効なディレクショナルライトがシーン中にない場合、シェーダーに組み込まれたデフォルトライトが有効になりますが、その向きが常にカメラが見る方向に追従するようになりました。  
結果、カメラから見て常に良い感じにライティングされるようになりました。  
このライトは、アンビエントライトブレンディング動作中に機能します。  

以下に、アンビエントブレンディングとUnlit_Intensityの機能を解説するムービーを用意しました。  

[![](https://img.youtube.com/vi/7-k6m69JQ2g/0.jpg)](https://www.youtube.com/watch?v=7-k6m69JQ2g)

---
### ● 極めて明るいライトが複数存在するシーンでの白飛びを抑える：SceneLights Hi-Cut Filter  

**SceneLights Hi-Cut Filter** は、VRChatユーザーには大変便利な機能です。  
下に詳しい機能説明のムービーを用意しました。  
またムービー中では、PPSでトーンマッパーを設定する方法も簡単に説明しています。  

[![](https://img.youtube.com/vi/FM8TomuNwnI/0.jpg)](https://www.youtube.com/watch?v=FM8TomuNwnI)

---
### ● アドバンス機能：Built-in Light Direction  

上級者向け機能として、シェーダー内にビルトインされているライトディレクションベクトルを任意の方向に設定できます。  
Built-in Light Directionを有効にしたマテリアルは、それが適応されるメッシュのオブジェクト座標に対して、独自のシェーディング用ライトディレクションベクトルを持つことができるので、専用の固定ライトを持つことと同じ効果が得られます。  
そのパーツが落とすドロップシャドウは、シーン中のディレクショナルライトを使いますので、シェーディングの落ち方とドロップシャドウの落ち方を変えることもできます。  
Built-in Light Directionのライトカラーは、シーン中のメインとなるディレクショナルライトの設定を使います。  

Built-in Light Directionの使い方は、下のムービーを見てみてください。  

[![](https://img.youtube.com/vi/IFAPrbAGfmw/0.jpg)](https://www.youtube.com/watch?v=IFAPrbAGfmw)
