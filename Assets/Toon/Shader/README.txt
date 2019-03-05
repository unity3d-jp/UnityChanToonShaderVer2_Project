README.md

# 【ユニティちゃんトゥーンシェーダー Ver.2.0.6】
「ユニティちゃんトゥーンシェーダー」は、セル風3DCGアニメーションの制作現場での要望に応えるような形で設計された、映像志向のトゥーンシェーダーです。  

ユニティちゃんトゥーンシェーダーVer.2.0では、従来の機能に加えて大幅な機能強化を行いました。  
Ver.1.0でできる絵づくりをカバーしつつ、さらに高度なルックが実現できるようになっています。  

● **[日本語マニュアル（v.2.0.6版）](https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project/blob/master/Manual/UTS2_Manual_ja.md)が提供されています。合わせてご利用ください。**  


## 【Unity-Chan Toon Shader Ver.2.0.6】
Unity-Chan Toon Shader is a toon shader for video and images that is designed to meet your needs when creating cel-shaded 3DCG animations.  

We have greatly enhanced the performance and features in Unity-Chan Toon Shader Ver. 2.0.  
It still has the same rendering capabilities as Ver. 1.0, but now you can give your creations an even more sophisticated look.  

● **[English manual for v.2.0.6](https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project/blob/master/Manual/UTS2_Manual_en.md) is available now.**  



----
## 【重要】v.2.0.4.3p1から、直接v.2.0.6へバージョンアップをする場合の注意
* 内部パラメタの名前変更のために、すでに設定されているマテリアルから、BaseMapが外れる可能性があります。外れてしまった場合、お手数ですが再設定をお願いします。  
* HiColor_Powerのスライダの感度調整をしました。Is_SpecularToHighColor=OFF/Is_BlendAddToHiColor=0FFの場合、HiColor_Powerの値を再調整する必要があります。Is_SpecularToHighColor=ONで利用している場合には、特に修正する必要はありません。  

### 【Important】 Notes on upgrading from v.2.0.4.3p1 directly to v.2.0.5
* There is a possibility that BaseMap may be removed from material already set because of renaming internal parameters. If it has come off, sorry to trouble you but please set it again.  
* Adjusted the sensitivity of the slider of HiColor_Power. If Is_SpecularToHighColor = OFF / Is_BlendAddToHiColor = 0FF, you need to readjust the value of HiColor_Power. If Is_SpecularToHighColor = ON is used, there is no need to modify it in particular.  

-----
## 【ターゲット環境】
Unity5.6.x もしくはそれ以降が必要です。Unity 2018.2.20f1 以降でも使用できます。  
Unity 2017.4 15f1 LTSでの動作確認済み。  
本パッケージは、Unity5.6.7f1で作成されています。  

### 【Target Environment】
Requires Unity 5.6.x or later. Available on Unity 2018.2.20f1 or lator, too.  
Confirmed on Unity 2017.4 15f1 LTS.  
This pack was created in Unity 5.6.7f1.  


-----
## 【提供ライセンス】
「ユニティちゃんトゥーンシェーダーVer.2.0」は、UCL2.0（ユニティちゃんライセンス2.0）で提供されます。  
ユニティちゃんライセンスについては、以下を参照してください。  
http://unity-chan.com/contents/guideline/

### 【License】
Unity-Chan Toon Shader 2.0 is provided under the Unity-Chan License 2.0 terms.  
Please refer to the following link for information regarding the Unity-Chan License.  
http://unity-chan.com/contents/guideline_en/


-----
## 【プロジェクト全体のダウンロード/Download whole project】
### [UnityChanToonShaderVer2_Project (Zip)](https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project/archive/master.zip)  


-----
## 【インストールの注意】
### [UTS2_ShaderOnly_v2.0.6_Release.unitypackage](https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project/blob/master/UTS2_ShaderOnly_v2.0.6_Release.unitypackage)  

新規インストールは、Unityにそのまま本パッケージをD&Dすればインストールされます。  
上書きインストール時には、コードが改修されていますので、注意が必要です。  
1. 元のプロジェクトのバックアップをとっておく  
2. Unityでプロジェクトを開き、新規シーンを作成して開いておく。  
3. 元のトゥーンシェーダーが入っているフォルダ（Assets/Toon/Shader）をUnity上から削除する。  
4. 本パッケージをUnityにD&Dする。  

まず元のシェーダーを消した後で、すぐに新しいシェーダーをインストールすれば、既存のマテリアルへのリンクは途切れないので、そちらでやってみてください。  

個人でみられる範囲でバグチェックはしていますが、何か不具合があったらご連絡よろしくお願いします。

### 【Installation】
### [UTS2_ShaderOnly_v2.0.6_Release.unitypackage](https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project/blob/master/UTS2_ShaderOnly_v2.0.6_Release.unitypackage)  
When installing for the first time, simply drag and drop this package into Unity to begin the installation process.  

When over-writing a previous version, the code will be revised, so please take the following precautions:  
1. Back-up all previous projects.  
2. When opening a project in Unity, create a new scene beforehand.  
3. Erase the folder containing previous versions of the toon shader (Assets/Toon/Shader) from within Unity.  
4. Drag and drop this pack into Unity.  

We recommend first erasing the previous shader then installing the new shader, to preserve existing links between materials.   

Please contact us if you have any issues. 


-----
## 【サンプルシーンについて】  
プロジェクトを開くと、`Sample Scenes`フォルダに以下のサンプルシーンがあります。  

・BoxProjection.unity：Box Projection を使った暗い部屋のライティング  
・ToonShader.unity：イラストルックのシェーダー設定  
・ToonShader_CelLook.unity：セルルックのシェーダー設定  
・ToonShader_Emissive.unity：エミッシブを使ったシェーダー設定  
・ToonShader_Firefly.unity：ビルトインライトと複数のリアルタイムポイントライト  
・Baked Normal/Cube_HardEdge.unity：Baked Normalの参考  
・Sample/Sample.unity：UTS2の基本シェーダーの紹介  
・ShaderBall/ShaderBall.unity：シェーダーボールを使ってUTS2を設定する  
・PointLightTest/PointLightTest.unity：ポイントライトを使ったセルルック表現のサンプル  
・SSAO Test/SSAO.unity：SSAO in PPSのテスト用  
・LightAndShadows/LightAndShadows.unity：Standard ShaderとUST2の比較  
・AngelRing/AngelRing.unity：「天使の輪」および ShadingGradeMap を使ったキャラクターのサンプル  
・MatCapMask/MatCapMask.unity：MatcapMaskのサンプル  

各シーンは、シェーダーやライティングの設定の参考用です。  
作りたいルックやシーンの参考に役立つと思います。  

### 【About Sample scenes】  
When you open this project, there are the following sample scenes in `Sample Scenes` Folder  

・BoxProjection.unity: For lighting settings to dark room using Box Projection  
・ToonShader.unity: Illustration-like shader settings  
・ToonShader_CelLook.unity: Cellook Shader settings  
・ToonShader_Emissive.unity: Shader settings using Emissive  
・ToonShader_Firefly.unity: built-in light and multiple real-time point lights  
・Baked Normal / Cube_HardEdge.unity: Reference of Baked Normal  
・Sample / Sample.unity: Introduction of basic shaders of UTS2  
・ShaderBall / ShaderBall.unity: Set UTS2 using shader ball  
・PointLightTest / PointLightTest.unity: Sample of CelLook style using point lights  
・SSAO Test / SSAO.unity: For testing SSAO in PPS  
・LightAndShadows / LightAndShadows.unity: Comparison between Standard Shader and UST2  
・AngelRing / AngelRing.unity: Sample of "Angel's Ring"  
・MatCapMask / MatCapMask.unity: Sample MatcapMask  

Each and every scenes are for reference of shader and lighting settings.  
They will be useful for reference of the look and scene you want to make!  

-----
## 【Version】
### 2019/03/05：2.0.6 Release：修正リリース版2  
* 「UTS2カスタムインスペクター」のUI表示にタイポが3つありましたので修正しました。  

### 2019/02/28：2.0.6 Release：修正リリース版  
* 「UTS2カスタムインスペクター」のデザインを再調整しました。合わせてマニュアルも更新しました。  

### 2019/02/21：2.0.6 Release：リリース版  
* 以下のバグフィックスと新規機能を追加。さらに一部機能の強化を行いました。  

#### 新UIを搭載  
* ShaderGUIベースの専用ユーザーインタフェース「UTS2カスタムインスペクター」を搭載しました。マニュアルも一新されています。  

#### バグフィックス  
* MatCapの合成モードが乗算（Is_BlendAddToMatCap = OFF）の時、リムライトが反映されてなかったのを修正しました。  
* MatCapの合成モードが乗算（Is_BlendAddToMatCap = OFF）の時、シャドウマスクとの順番が正しくなかったのを修正しました。  
* アウトラインモード/ポジションスケーリング方式の面判定を微調整しました。  

#### 新規機能  
* カメラのローリングに対してMatCapが回転してしまうのを抑止する Activate CameraRolling_Stabilizer を搭載しました。本機能は、TwitterID：@ShowBuyS さんのコントリビューションにより実現しました。  
* MatCap_Sampler に、Mip Mapに基づくぼかし機能 Blur Level of MatCap_Sampler を追加しました。  
* MatcapMaskテクスチャを反転する Inverse_MatcapMask を追加しました。  
* ShadingGradeMap機能を強化しました。レベル補正を行う Tweak_ShadingGradeMapLevel を追加した他、Mip Mapに基づくぼかし機能 Blur Level of ShadingGradeMap を追加しました。  

#### 機能強化  
* システムシャドウによるレシーブシャドウとシェーディングを馴染ませる Tweak_SystemShadowsLevel 機能を強化しました。  
* PointLights HiCut_Filter (ForwardAdd Only) = OFF の時、リアルタイムポイントライトにハイカラーが追加されるようにしました。  
* テッセレーションシェーダーのプロパティをわかりやすくした他、DX11 Tess : Extrusion Amount を調整しやすいようにスライダー化しました。  
* GIの強さを設定する GI_Intensity を機能強化しました。GIをUTS2マテリアルに反映させたい時には、まず GI_Intensity = 1 に設定してみてください。Standard Shaderとほぼ同様の明るさで反映されます。  
* ノーマルマップにバンプスケールを追加しました。  
* Transparent系シェーダーにデプスバッファの書き込み処理を追加しました。  

#### 変更  
* Unlit_Intensity の最大値を2から4へ変更しました。  

---
### 2019/03/05: 2.0.6 Release: Fixed release version 2
* Fixed three typos in the UI display of "UTS2 Custom Inspector".  

### 2019/02/28: 2.0.6 Release: Fixed release version
* We re-adjusted the design of "UTS2 Custom Inspector". Together we updated the manual as well.  

### 2019/02/21: 2.0.6 Release: Release version  
* Added following bug fixes and new features.  

#### New UI installed
* ShaderGUI based user interface "UTS 2 custom inspector" was installed. The manual has also been updated and redesigned.  

#### Bug Fix  
* Fixed that the rimlight was not reflected when the blend mode of MatCap was multiplying (Is_BlendAddToMatCap = OFF).  
* When the color blend mode of MatCap is multiplication (Is_BlendAddToMatCap = OFF), the order of shadow masks was incorrect was corrected.  
* Fine adjustment of surface judgment of outline mode / position scaling method.  

#### New Features  
* The new feature "Activate Camera Rolling_Stabilizer" which suppresses rotation of MatCap against the rolling of the camera was carried. This function was realized by Contribution of TwitterID: @ShowBuyS.  
* The new feature "Blur Level of MatCap_Sampler" based on Mip Map has been added to MatCap_Sampler.  
* The new function "Inverse_MatcapMask" to invert texture for MatcapMask was added.  
* The ShadingGradeMap function has been enhanced. In addition to adding "Tweak_ShadingGradeMapLevel" for level correction, "Blur Level of ShadingGradeMap" based on Mip Map has been added.  

#### Enhancements  
* Enhanced the Tweak_SystemShadowsLevel function to adapt to receive shadows and shading with system shadows.  
* When "PointLights HiCut_Filter (ForwardAdd Only) = OFF", high color is added to real time point light.  
* In addition to making the properties of tessellation shaders easy to use, "DX 11 Tess: Extrusion Amount" was made slidable for easy adjustment.  
* Improved the GI_Intensity to set the strength of GI. If you want GI to reflect on UTS2 material, please first set "GI_Intensity = 1". It is reflected at almost the same brightness as Standard Shader.  
* Bump scale added to normal map.  
* Depth buffer write processing has been added to Transparent shaders.  

#### Change  
* The maximum value of Unlit_Intensity has been changed from 2 to 4.


---
### 2019/01/07：2.0.5 Release：リリース版  
* macOS / Unity 2018.3.0f2上でプロジェクトを開くと不正終了するのを修正しました。  

### 2018/11/22：2.0.5 Release：リリース版  
* マニュアル/README.mdを最新に更新しました  

### 2018/11/18：2.0.5 RC1：以下の更新をしました。  
* UTS2 v.2.0.5 Test07 をリリース候補版にリネームしました。コードに変更はありません。  
* RC1向けにREADME.mdの更新をしました。  

### 2018/11/17：2.0.5 Test07：以下のバグ修正をしました。  
* SceneLights Hi-Cut_FilterがONの時、ポイントライトのカラーに不具合が起こっていたので、修正しました。  

### 2018/11/16：2.0.5 Test06：以下のバグ修正および機能追加をしました。  
* SceneLights Hi-Cut_Filterを搭載。Directional Light Intensity Filterを機能強化し、複数のリアルタイムポイントライトに対応しました。  
* _Is_SystemShadowsToBaseスイッチをForwardADDパス側でも動作するようにしました。  
* 乗算合成時のTweak_MatcapMaskLevelの挙動を正しくしました。  
* Is_SpecularToHighColorスイッチがオンの時、常に加算合成するように仕様変更をしました。この場合、Is_BlendAddToHicolorスイッチは無効になります。  
* HiColor_Powerのスライダの感度調整をしました。スペキュラ使用時の感度に合わせました。  
* Use BaseMap as 1stShade_Map、Use 1stShade_Map as 2ndShade_Mapスイッチを追加しました。  
* その他、コードの最適化を行いました。  

### 2018/11/11：2.0.5 Test05：以下の機能追加をしました。  
* かにひら氏（@kanihira）考案のカメラ補正付きMatCapを搭載しました。オブジェクトがカメラ描画面の端に寄った時の、MatCapの歪みが出なくなります。  

### 2018/11/08：2.0.5 Test04：以下の機能追加と仕様変更をしました。  
* プロパティ名の統一のために、ShadingGradeMap系シェーダーの`Is_NormalMap`スイッチを、`Is_NormalMapToBase`スイッチに改名。全てのシェーダーで名称を統一しました。お手数をおかけしますが、すでに設定済みのマテリアルは再度スイッチを設定し直してください。  
* リアルタイムポイントライト使用時のセルルック品質が大いに向上しました。リアルタイムポイントライトだけでも綺麗なセルルックが実現できます。  
* FowardAddパスで機能する`PointLights HiCut_Filter`を搭載しました。セルルック時に基本色内に現れるポイントライト由来のハイライトを抑え、ルックを向上させます。  

### 2018/11/06：2.0.5 Test03：以下の機能追加と仕様変更をしました。  
* Step_Offstの不具合修正。
* Unlit_Intensityの上限値を2に変更。
* Is_LightColor_カラー名スイッチをONにすると、複数リアルタイムライトがある環境で白飛びしやすかったのを改善。
* Directional Light Intensity Filterを機能強化。VRChatで複数のディレクショナルライトがある為に白飛びがしやすい環境になっている場合にも、白飛びを抑えるようにした。  

### 2018/10/31：2.0.5 Test02：以下の機能追加をしました。  
VRChatで、ディレクショナルライトのインテンシティが高めの設定がしてあるワールドで、白飛びを抑えるフィルタを実装したテストバージョン。
Directional Light Intensity FilterをONにすることで機能します。  

### 2018/10/06：2.0.5 Test：以下の機能追加と仕様変更をしました。  
### ●【重要】BaseMapの内部変数名を変更しました。  
PostProcessing Stackに搭載されているSSAOでの不具合対処のために、BaseMapの内部変数名(`_BaseMap`)を`_MainTex`に変更しました。  

また.shaderファイルのプロパティブロックに以下の行を追加しました。  
`[HideInInspector] _Color ("Color", Color) = (1,1,1,1)`  
元のSSAOの結果に戻したい時には、適宜コメントアウトしてください。  

### ●Forward Addパス内で主にポイントライトのシェードステップを調整する、Step_Offsetスライダーを追加  
BaseColor_Step、ShadeColor_Step、1st_ShadeColor_Step、2nd_ShadeColor_Stepでの設定に加えて、Step_Offsetでさらに微調整を加えることで、特にセルルック時でのポイントライトの調整ができるようになりました。  

### ●アドバンス機能として、Built-in Light Directionを追加  
上級者向け機能として、シェーダー内にビルトインされているライトディレクションベクトルを任意の方向に設定できるようにしました。  
Built-in Light Directionを有効にしたマテリアルは、それが適応されるメッシュのオブジェクト座標に対して、独自のシェーディング用ライトディレクションベクトルを持つことができるので、専用の固定ライトを持つことと同じ効果が得られます。そのパーツが落とすドロップシャドウは、シーン中のディレクショナルライトを使いますので、シェーディングの落ち方とドロップシャドウの落ち方を変えることもできます。  
Built-in Light Directionのライトカラーは、シーン中のメインとなるディレクショナルライトの設定を使います。  

### ●以下の修正をしました。  
・一部のプロパティの並び順を、より作業しやすいように入れ替えしました。  
・シーン中のアンビエントカラーの取得方法を変更し、よりシーン全体のアンビエントライトのカラーを反映するようにしました。  

-----
### 2018/09/10：2.0.4.3 Release Patch 1：バグフィックス版
・スポットライトが正常に使えなかったのを修正しました。  
・リアルタイムポイントライトがレンジおよび距離に対し、正しく減衰するように修正しました。  
・修正が確認されましたので、iOS/OSX METAL環境での注意を削除しました。  

### 2018/09/05：2.0.4.3 Release：以下の修正と追加をしました。特にVRChat向けに便利な機能を搭載しています。  
### ●アンビエントライトブレンディングを搭載  
アンビエントライトの設定をライトカラーが反映するようになりました。  
その結果として、ディレクショナルライトのインテンシティの下限が、シーンのアンビエントライトの設定となります。　　
VRChatで、アンビエントライトの設定に基づくワールドごとの明るさの差異を自動で調整できます。  
なおアンビエントライトからの明るさは、Unlit_Intensity スライダーで調整することもできます。デフォルトは 1（そのまま）になっています。  

### ●カメラ追従型のデフォルトライトを搭載  
ディレクショナルライトがシーン中にない場合、シェーダーに組み込まれたデフォルトライトが有効になりますが、その向きが常にカメラに追従するようになりました。  
結果、カメラから見て常に良い感じにライティングされるようになりました。  

### ●Baked Normal for Outline搭載  
頂点法線を焼き付けたノーマルマップを、法線反転アウトラインの設定時に読み込むことができるようになりました。本機能を使うことで、ハードエッジのオブジェクトに、ソフトエッジのオブジェクトのアウトラインを、事前にベイクしたノーマルマップを経由して適用することができるようになります。  

Baked Normalマップを使用する時には、UTS2のアウトライン設定プロパティで、  
1.  OUTLINE MODE を "NML" に  
2.  Is_BakedNormal を "ON" に  
3.  Baked Normal for Outline に使用したいマップを適用します。 

Baked Normal for Outline として適用できるノーマルマップは以下のような仕様となっています。  
1.  適用するオブジェクトの UV は重ならないこと。つまり、全てのノーマルマップが重ならないように UV 展開がされていることが必須です。  
2.  ノーマルマップ自体の仕様は、Unity と同じで、OpenGL 準拠となります。  
3.  使用するノーマルマップのテクスチャ設定は、以下のようになります。  
・Texuture Type は "Default" にする。※ "Normal map" に設定してはいけません。  
・sRGB (Color Texture) を必ず "OFF" にする。  

詳しくはサンプルプロジェクト内の Baled Normal フォルダ内のアセットを確認してください。

### ●Helperフォルダ内にアウトラインオブジェクト表示専用シェーダーを追加  
シェーダーバリエーションとして、アウトラインオブジェクトのみを表示するシェーダーを Helper フォルダ内に追加しました。必要に応じてマルチマテリアルとしてお使いください。  

### ●Forward Add パス内の処理の簡略化。他、バグフィックス  
Forward Add パス内の処理の簡略化を含め、処理の軽減化およびバグ修正をしました。 

-----
### 2018/08/21：2.0.4.2 Release：以下の修正と追加をしました。  
VRChat内の鏡オブジェクトに表示した時、Offset_Camera_Zに不具合が発生するのを修正。  
VRChat向けにMobile用クリッピングシェーダーのバリエーションを6つ追加。  

-----
### 2018/08/16：2.0.4.2 Release：MatcapMaskを追加  
MatCapに、メッシュのUVベースで効果のかかり具合を調整できるMatcapMaskを追加。Matcapマスクは「白」ほど効果が出やすくなります。  
Tweak_MatcapMaskLevelプロパティで強度が調整できます。  

-----
### 2018/07/04：2.0.4.1 Release：Unlit_Intensityプロパティを追加  
シーン内に有効なライトがない場合に、UTS2がデフォルトのライティングに切り替えますが、その明るさを調整できるようにしました。  
主にVR Chatでの特殊なシーン向けでの設定を想定しています。  
シーン内に有効なディレクショナルライトがある場合には、本プロパティは影響はしません。

-----
### 2018/05/04：2.0.4 Release：ターゲット環境を正式にUnity5.6.x以降としました。Unity2018.1.0f2にも対応しています。  
コードの全面的な改修およびバグ修正の他、以下の仕様を新規に追加しました。

### ●UTS_EdgeDetection.unitypackage
ポストエフェクトタイプのエッジ抽出フィルタです。  
元々はUnityのStandard Assetsにあったものを改造したフィルタ3つに加えて、新規に作成したSobel Color Filterが追加されています。  
Sobel Color Filterを使うことで、効果的にトゥーンラインエッジを強調し、セル画時代の色トレス風の雰囲気を出すことができます。  
本ポストエフェクトは、ポストエフェクトスタックの前に入れるとよいと思います。

### ●DX11 Phong Tessellation対応
対応部分のコードは、Nora氏の https://github.com/Stereoarts/UnityChanToonShaderVer2_Tess を参考にさせていただきました。  
Tessellationは、使えるプラットフォームが限られている上に、かなりパワフルなPC環境を要求しますので、覚悟して使ってください。  
想定している用途は、パワフルなWindows10/DX11のマシンを使って、映像＆VR向けに使用することです。  
Light版とあるものは、ライトをディレクショナルライト１灯に制限した代わりに軽量化したバリエーションです。  

### ●Positionスケーリングベースのアウトラインを搭載。
従来の法線反転方式だとアウトラインが切れてしまう、キューブのようなモデルに綺麗にアウトラインが出せます。  

### ●クリッピング系シェーダーでアウトラインのアルファ抜きに対応。
アルファ付きテクスチャと組み合わせた時に、背面から見てもアウトラインポリゴンがアルファに従って抜けるようにした。  

### ●アウトライン用テクスチャ（Outline_Tex）の搭載。
### ●ハイカラー用テクスチャ（HiColor_Tex）の搭載。
### ●PostProcessingStackと一緒に使うことを前提に、エミッシブカラー（Emissive_Color）およびエミッシブ用テクスチャ（Emissive_Tex）を搭載。
エミッシブカラー側でHDR値が設定できるので、PostProcessingStackのブルームエフェクトと組み合わせることで、エミッシブ部分を光らせることができるようになった。  
※今回搭載された新規テクスチャに関しても、今まで通り、必要なければ使わなくても問題ないようになっています。  

### ●VR Chatに対応
シーン内に有効なライトが存在しない場合（シーン内にライトオブジェクトがない場合と、ライトのインテンシティが0.001以下の場合を含みます）、デフォルトの明るさで照らされます。
なおVR Chat向けには、システムへの負荷を考慮してMobileフォルダ内のシェーダーを使うことをお勧めします。その場合、ライトは1灯のみの対応となります。

-----
## 【Version】
### 2019/01/07：2.0.5 Release: Release version.  
* Fixed illegal termination when opening the project on macOS / Unity 2018.3.0f2.  

### 2018/11/22：2.0.5 Release: Release version.  
* Updated Manual and README.md.  

### 2018/11/18: 2.0.5 RC1: The following updates were made.  
* Renamed UTS2 v.2.0.5 Test 07 to release candidate version. There is no change in the code.  
* README.md was updated for RC1.  

### 2018/11/16：2.0.5 Test06：　Fixed a bug below:  
* When SceneLights Hi-Cut_Filter is ON, a deterioration of point light color is fixed.  

### 2018/11/16：2.0.5 Test06：　Fixed bugs and added a new feature below:  
* Equipped "SceneLights Hi-Cut_Filter". "Directional Light Intensity Filter" has been enhanced to support multiple real-time hi-intensity point lights.
* _Is_SystemShadowsToBase switch has been made to work also on the ForwardADD pass side.  
* Tweak_MatcapMaskLevel works correctly in Multiply blend mode.  
* Is_SpecularToHighColor Specification changed so that additive synthesis always occurs when switch is on. In this case, the Is_BlendAddToHicolor switch is disabled.  
* Adjusted the sensitivity of the slider of HiColor_Power. It matched the sensitivity when using specular.  
* Added Use BaseMap as 1stShade_Map, Use 1stShade_Map as 2ndShade_Map switches.  
* And, also optimized the codes...:-)

### 2018/11/11：2.0.5 Test05：　Added a new feature below:  
* Thanks for the idea of Kanohira (@kanihira)-san, MatCap with camera skew correction was equipped. Distortion of MatCap image will not come out when the object is close to the edge of the camera drawing surface.  
* Tweak_MatcapMaskLevel works correctly in Multiply blend mode.  
* Is_SpecularToHighColor Specification changed so that additive synthesis always occurs when switch is on. In this case, the Is_BlendAddToHicolor switch is disabled.  
* We adjusted the sensitivity of the slider of HiColor_Power. It matched the sensitivity when using specular.  

### 2018/11/08：2.0.5 Test04： Bug fix and Added new features below:  
* For uniformity of property names, rename the 'Is_NormalMap` switch of ShadingGradeMap shaders to' Is_NormalMapToBase` switch. I unified the names with all the shaders. Sorry for the inconvenience but please set the switch again if you have the already set materials.  
* Cel-look quality when using real-time point lights has improved greatly. Even just real-time point lights can achieve clean cel-look.  
* Equipped `PointLights HiCut_Filter` functioning with FowardAdd pass. Suppresses the highlight from the point light appearing in the base color in cel-look, improving the look.  

### 2018/11/06: 2.0.5 Test 03: Bug fix version.  
* Fixed a bug in Step_Offst.  
* Change the upper limit of Unlit_Intensity to 2.  
* When Is_LightColor_* switches are turned on, the easy white over in an environment with multiple real-time lights is improved.  
* Enhanced Directional Light Intensity Filter. Even when there is more than one directional light in VRChat, suppress the whitening even when it is in an environment that makes it easy to white over.  

### 2018/10/31: 2.0.5 Test 02: Added new features below.  
In VRChat, this test version implements the filter that suppresses whitening in the world where the intensity of the directional light is set higher.  
Activate by turning on "Directional Light Intensity Filter".  

### 2018/10/06: 2.0.5 Test：Added new features below.  
### 【Caution】The internal variable name of BaseMap had been changed.  
The internal variable name of BaseMap(`_BaseMap`) had been changed into `_MainTex` in order to fix the problems with SSAO in PostProcessing Stack.  

Also added the following line to the property block of the .shader file.  
`[HideInInspector] _Color ("Color", Color) = (1, 1, 1, 1)`  
If you want to return to the original SSAO result, comment out as appropriate.  

### ●Add Step_Offset slider to adjust the shade stepping of the point lights mainly in the Forward-Add path.  
In addition to the settings in BaseColor_Step, ShadeColor_Step, 1st_ShadeColor_Step,and 2nd_ShadeColor_Step, you can now fine-tune the shade stepping with Step_Offset to adjust the shading with point lights especially at cel-look style.  

### ●Advanced function : Add Built-in Light Direction
As the function for advanced users, you can now set the built-in light deirection vector within shader to your favorite direction.
The material enabled Built-in Light Direction can have its own light deirection vector in the object coordination of the mesh, and shade it from as like as a fixed light. Drop shadow of the mesh has consistency with the directional light in the scene, so you can control how the shade falls and how the shadow dropps.  
The color of Built-in Light Direction uses the setting of the main directional light in the scene.  

### ●Made the following corrections.  
・Rearranged the order of some properties for your easier operation.  
・Changed the method of getting the ambient color in the scene, and so shader becomes to reflect the color of the ambient light from the whole scene more.  

-----
### 2018/09/10：2.0.4.3 Release Patch 1：Bug fix version.  
・Fixed the problem that spotlight could not work properly.  
・Fixed real-time point light to attenuate correctly with respect to range and distance.  
・Since bug-fixes were confirmed, the notes when using with iOS / OSX METAL environment were deleted.  

### 2018/09/05: 2.0.4.3 Release：Fixed bug and added new features below. Especially, added useful features for VRChat users! 

### ●Add Ambient light blending  
Light color now reflects ambient light settings.  
As a result, the lower limit of the intensity of the directional light becomes the setting of the ambient light of the scene.   
With VRChat, you can automatically adjust the brightness difference for each world based on ambient light settings.  
The brightness from the ambient light can also be adjusted with the Unlit_Intensity slider. The default is 1 (as it is).  

### ●Equipped with camera following type default light  
If the directional light is not in the scene, the default light built in the shader is enabled, and its orientation always follows the camera.  
As a result, you always keep good lighting from the view of camera.  

### ●Add Baked Normal for Outline  
You can now use normal maps baked vertex normals when setting normal-inversion outlines. By using this feature, you can apply the outline of the soft edge object to the hard edge object via the pre-baked normal map.  

When using the Baked Normal map, in the outline setting property of UTS2:  
1.  OUTLINE MODE is "NML",  
2.  Is_BakedNormal is "ON",  
3.  Apply the map you want to use for "Baked Normal for Outline" slot.  

Normal map applicable as Baked Normal for Outline has the following specifications.  
1. The UV of the object to be applied shall not overlap. In other words, it is essential that UV deployment is done so that all normal maps do not overlap.  
2. The specification of the normal map itself is the same as Unity, it is OpenGL compliant.  
3. The texture settings of the normal map to be used are as follows.  
· Set Texuture Type to "Default". ※ Do not set to "Normal map".  
· Be sure to turn OFF sRGB (Color Texture) check.  

For details, please check the assets in the Baled Normal folder in the sample project.

### ●Add the shaders for outline object display only, in Helper folder  
As the shader variations, add the shader that displays only outline objects in Helper folder. Please add them as multi-materials as necessary.  

### ●Simplify processing within the Forward Add path. Other, bug fixes  

-----
### 2018/08/21: 2.0.4.2 Release：Fixed bug and added shader variations.  
Fixed Offset_Camera_Z bug with mirror objects in VRChat.  
Added 6 shader variations to Mobile/_Clipping series for VRChat.  

-----
### 2018/08/16: 2.0.4.2 Release:Add MatcapMask.  
Added MatcapMask that can adjust the power of MatCap effect applied on the UV base of the mesh. MatcapMask is a grayscale map, "white" becomes to MatCap full effect.  
You can adjust the power of the mask with Tweak_MatcapMaskLevel property.  

-----
### 2018/07/04: 2.0.4.1 Release:Add Unlit_Intensity property.  
If there is no valid light in the scene, UTS2 switches to default lighting, and now you can adjust its brightness.  
This property is assuming for using in special scenes mainly in VR Chat.  
If there is a valid directional light in the scene, this property will not be affected.

-----
### 2018/05/04: 2.0.4 Release: Officially set Unity5.6.x and later versions as the target environment. Unity2018.1.0f2 is also supported.  
In addition to overall code revisions and bug fixes, the following has been added to this new version. 

### ●UTS_EdgeDetection.unitypackage
This is an edge extraction filter of post effect type.  
This package includes three modified filters modified from Unity's Standard Assets,and a newly created Sobel Color Filter.  
By using Sobel Color Filter, you can effectively emphasize the toon line edge and give out the color tress-like atmosphere of the cell picture era.  
Attach this post effect before the post-effect-stack.

### ●DX11 Phong Tessellation support
The supporting code referenced Nora's work https://github.com/Stereoarts/UnityChanToonShaderVer2_Tess  
Please be aware that only certain platforms support tessalation and it requires a considereably powerful PC environment.  
It is intended for use in video, images, and VR on powerful Windows10/DX11 machines.  
The "light version" uses lighter weight variation instead of limiting the directional lighting to 1 source.   

### ●Base outlines for Position Scaling 
In previous versions, outlines would be broken by the vector inversion formula, now you can cleanly outline models like cubes
  
### ●Support for clipping shaders with outlines that don't have an alpha
When paired with textures that have an alpha, the outline polygons will be removed according to the alpha, even when viewed from behind. 

### ●Outline textures (Outline_Tex)
### ●HiColor textures (HiColor_Tex)
### ●Assuming this shader will be used together with PostProcessingStack, Emissive Color (Emissive_Color) and Emissive Textures (Emissive_Tex)
HDR values can be set on the Emissive Color side, allowing the Emissive portion to shine when combined with PostProcessingStack's bloom effect.  
※As always, using these new textures is not required.   

### ●VR Chat ready!
If there is no valid light in the scene (including the case where there is no light object in the scene and the intensity of the light is less than 0.001), objects will be illuminated with the default brightness.
For VR Chat, we recommend using shaders in the Mobile folder in consideration of the load on the system. In that case, the light will correspond to only one light.

-----
## 【過去の修正履歴】
2017/06/19：2.0.3：Set_HighColorMask、Set_RimLightMaskの追加。機能強化の結果、Set_HighColorPositionは廃止。  
2017/06/09：2.0.2：Nintendo Switch、PlayStation 4に正式対応。モバイル軽量版の追加。その他機能強化。  
2017/05/20：2.0.1：TransClipping系シェーダーのブレンド仕様変更とリムライトに調整機能追加。  
　　　　　　　　　 上の仕様変更に伴い、トランスペアレント系シェーダーを２つ追加。  
　　　　　　　　　（ToonColor_DoubleShadeWithFeather_Transparent、ToonColor_ShadingGradeMap_Transparent）  
2017/05/07：2.0.0：最初のバージョン  

### 【Version Update History】
2017/06/19: 2.0.3: Added Set_HighColorMask and Set_RimLightMask, as a result of these improvements Set_HighColorPosition was removed.  
2017/06/09: 2.0.2: Official support for Nintendo Switch and PlayStation 4. Added lightweight version for mobile. Various other improvements  
2017/05/20: 2.0.1: Modified the blend methods for TransClipping shaders and added rim light regulation function. 
In addition to the above modifications, added 2 transparent shaders (ToonColor_DoubleShadeWithFeather_Transparent, ToonColor_ShadingGradeMap_Transparent)  
2017/05/07: 2.0.0: Initial version   


-----
最新バージョン：2.0.6 Fixed Release v.2  
最終リリース日：2019/03/05  
カテゴリー：3D  
形式：zip/unitypackage  

Latest Version: 2.0.6 Fixed Release v.2  
Update: 2019/03/05  
Category: 3D  
File format: zip/unitypackage  
