README.md

UTC-ac shader
Tweaks by ACIIL.


_Intro:
This is a modified version of UTS2_ShaderOnly_v2.0.4 to Support typical lighting environments in vrchat. Further support and setting configurations will slowly progress. 
The dev branch will be up to date and error prone.
Any UTC shader that uses the DoubleShadeWithFeather base should look correct.


_Install: 
Copy over the Toon\ folder in assets into you unity install and verify the UnityChanToonShader\ shaders can be used as materials.


_Shader setup: 
UTC is a very technical and manual shader compared to other toon shaders seen in vrchat. The freedom comes with the risk of needing to manually balance every aspect in the settings.
Follow the documentation for UTC  setup. I recommend installing the unity post proccessing stack in you avatar scene with bloom on and tracking when it goes off. 
Balance color lumination (V) when combining albedo, highColor, outlines, and additive matcap, know that these effects ADD color together and into the emission range, because of this start the lum about 128 (50%) and increase from there depending on intended material context. 
Please Use outlines with Opaque mode only.


_Current bugs:
Please send issue reports to the github page. 
Some vrchat maps are setup incorrectly, this shader's light and color inputs are not clamped and use full HDR range, maps without proper light balancing and postproccessing usage will explode in brightness. Balance your maps against standard shader with white albedo.
Stencil lighting may have wrongly setup light passes, its being rechecked later.
Incomplete Fog support with outlines.

Potential issues with alpha and light passes with the transparency shaders. Using outlines outside of Opaque may look incorrect. 
Recommend using only DoubleShadeWithFeather shader variants with or without outlines.


_Support credits to:
Noe, TCL, June, Cubed, Silent, RetroGeo, Xiexe, Mel0n, Cibbi, Hakanai


_Hosting site:
https://github.com/ACIIL/UnityChanToonShaderVer2_Project
A fork of:
https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project


_License:
Unity-Chan Toon Shader 2.0 is provided under the Unity-Chan License 2.0 terms.  
Please refer to the following link for information regarding the Unity-Chan License.  
http://unity-chan.com/contents/guideline_en/


As someone who likes to see avatars that do not use the standard Cubed shader, i love to see screenshots of UTC-ac in use.


---------------------------------------------------------------


# 【ユニティちゃんトゥーンシェーダー Ver.2.0.4】
「ユニティちゃんトゥーンシェーダー」は、セル風3DCGアニメーションの制作現場での要望に応えるような形で設計された、映像志向のトゥーンシェーダーです。

ユニティちゃんトゥーンシェーダーVer.2.0では、従来の機能に加えて大幅な機能強化を行いました。  
Ver.1.0でできる絵づくりをカバーしつつ、さらに高度なルックが実現できるようになっています。

## 【Unity-Chan Toon Shader Ver.2.0.4】
Unity-Chan Toon Shader is a toon shader for video and images that is designed to meet your needs when creating cel-shaded 3DCG animations.

We have greatly enhanced the performance and features in Unity-Chan Toon Shader Ver. 2.0.  
It still has the same rendering capabilities as Ver. 1.0, but now you can give your creations an even more sophisticated look.


-----
## 【ターゲット環境】
Unity5.6.x もしくはそれ以降が必要です。Unity 2018.1.0f2で使用できます。  
本パッケージは、Unity5.6.5p2で作成されています。

### 【Target Environment】
Requires Unity 5.6.x or later. Available on Unity 2018.1.0f2.  
This pack was created in Unity 5.6.5p2.


-----
## 【iOS/OSX METALで使用する際の注意】
iOS/OSX METALで使用する場合、CullMode=OFF（両面表示）の時に、正しい表示が出来ない場合があります。  
その場合、メッシュを両面に貼って、それぞれにCullMode=Back（背面カリング）/CullMode=Front（正面カリング）のマテリアルを設定するようにしてください。

### 【Use with iOS/OSX METAL】
When using with iOS / OSX METAL, objects may not display correctly when CullMode = OFF (double-sided drawing).  
To correct this, place meshes on both sides of the object and set materials to CullMode = Back (back-face culling) / CullMode = Front (front-face culling) on each side.


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
## 【インストールの注意】
### UTS2_ShaderOnly_v2.0.4_Release.unitypackage  
新規インストールは、Unityにそのまま本パッケージをD&Dすればインストールされます  
上書きインストール時には、コードが全面的に改修されていますので、注意が必要です。  
1. 元のプロジェクトのバックアップをとっておく  
2. Unityでプロジェクトを開き、新規シーンを作成して開いておく。  
3. 元のトゥーンシェーダーが入っているフォルダ（Assets/Toon/Shader）をUnity上から削除する。  
4. 本パッケージをUnityにD&Dする。  

まず元のシェーダーを消した後で、すぐに新しいシェーダーをインストールすれば、既存のマテリアルへのリンクは途切れないので、そちらでやってみてください。

個人でみられる範囲でバグチェックはしていますが、何か不具合があったらご連絡よろしくお願いします。

### 【Installation】
### UTS2_ShaderOnly_v2.0.4_Release.unitypackage  
When installing for the first time, simply drag and drop this package into Unity to begin the installation process.

When over-writing a previous version, the code will be completely revised, so please take the following precautions:  
1. Back-up all previous projects.  
2. When opening a project in Unity, create a new scene beforehand.  
3. Erase the folder containing previous versions of the toon shader (Assets/Toon/Shader) from within Unity.  
4. Drag and drop this pack into Unity.  

We recommend first erasing the previous shader then installing the new shader, to preserve existing links between materials. 

Please contact us if you have any issues. 


-----
## 【新規】
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


### 【Version】
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


-----
## 【過去の修正履歴】
2017/06/25：2.0.3：マニュアル修正。【iOS/OSX METALで使用する際の注意】を追加。  
2017/06/19：2.0.3：Set_HighColorMask、Set_RimLightMaskの追加。機能強化の結果、Set_HighColorPositionは廃止。  
2017/06/09：2.0.2：Nintendo Switch、PlayStation 4に正式対応。モバイル軽量版の追加。その他機能強化。  
2017/05/20：2.0.1：TransClipping系シェーダーのブレンド仕様変更とリムライトに調整機能追加。  
　　　　　　　　　 上の仕様変更に伴い、トランスペアレント系シェーダーを２つ追加。  
　　　　　　　　　（ToonColor_DoubleShadeWithFeather_Transparent、ToonColor_ShadingGradeMap_Transparent）  
2017/05/07：2.0.0：最初のバージョン  

### 【Version Update History】
2017/06/25: 2.0.3: Manual updated, added 【Use with iOS/OSX METAL】.  
2017/06/19: 2.0.3: Added Set_HighColorMask and Set_RimLightMask, as a result of these improvements Set_HighColorPosition was removed.  
2017/06/09: 2.0.2: Official support for Nintendo Switch and PlayStation 4. Added lightweight version for mobile. Various other improvements  
2017/05/20: 2.0.1: Modified the blend methods for TransClipping shaders and added rim light regulation function. 
In addition to the above modifications, added 2 transparent shaders (ToonColor_DoubleShadeWithFeather_Transparent, ToonColor_ShadingGradeMap_Transparent)  
2017/05/07: 2.0.0: Initial version   


-----
最新バージョン：2.0.4  
最終リリース日：2018/05/04  
カテゴリー：3D  
形式：zip  

Latest Version: 2.0.4  
Update: 2018/05/04  
Category: 3D  
File format: zip  
