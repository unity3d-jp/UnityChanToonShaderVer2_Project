README.md

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
Unity5.6.x もしくはそれ以降が必要です。Unity 2018.1.0f2以降でも使用できます。  
本パッケージは、Unity5.6.3p1で作成されています。

### 【Target Environment】
Requires Unity 5.6.x or later. Available on Unity 2018.1.0f2 or lator, too.
This pack was created in Unity 5.6.3p1.


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
### UTS2_ShaderOnly_v2.0.4.3_Release_p1.unitypackage  
新規インストールは、Unityにそのまま本パッケージをD&Dすればインストールされます。  
上書きインストール時には、コードが全面的に改修されていますので、注意が必要です。  
1. 元のプロジェクトのバックアップをとっておく  
2. Unityでプロジェクトを開き、新規シーンを作成して開いておく。  
3. 元のトゥーンシェーダーが入っているフォルダ（Assets/Toon/Shader）をUnity上から削除する。  
4. 本パッケージをUnityにD&Dする。  

まず元のシェーダーを消した後で、すぐに新しいシェーダーをインストールすれば、既存のマテリアルへのリンクは途切れないので、そちらでやってみてください。

個人でみられる範囲でバグチェックはしていますが、何か不具合があったらご連絡よろしくお願いします。

### 【Installation】
### UTS2_ShaderOnly_v2.0.4.3_Release_p1.unitypackage  
When installing for the first time, simply drag and drop this package into Unity to begin the installation process.

When over-writing a previous version, the code will be completely revised, so please take the following precautions:  
1. Back-up all previous projects.  
2. When opening a project in Unity, create a new scene beforehand.  
3. Erase the folder containing previous versions of the toon shader (Assets/Toon/Shader) from within Unity.  
4. Drag and drop this pack into Unity.  

We recommend first erasing the previous shader then installing the new shader, to preserve existing links between materials. 

Please contact us if you have any issues. 

-----
## 【サンプルシーンについて】  
プロジェクトを開くと、以下のサンプルシーンがあります。  

・BoxProjection.unity：Box Projection を使った暗い部屋のライティング  
・ToonShader.unity：イラストルックのシェーダー設定  
・ToonShader_CelLook.unity：セルルックのシェーダー設定  
・ToonShader_Emissive.unity：エミッシブを使ったシェーダー設定  
・ToonShader_Firefly.unity：ビルトインライトと複数のリアルタイムポイントライト  
・Baked Normal/Cube_HardEdge.unity：Baked Normalの参考  
・Sample/Sample.unity：UTS2の基本シェーダーの紹介  
・ShaderBall/ShaderBall.unity：シェーダーボールを使ってUTS2を設定する  

各シーンは、シェーダーやライティングの設定の参考用です。  
作りたいルックやシーンの参考に役立つと思います。  

### 【About Sample scenes】  
When you open this project, there are the following sample scenes.  

・BoxProjection.unity: For lighting settings to dark room using Box Projection  
・ToonShader.unity: Illustration-like shader settings  
・ToonShader_CelLook.unity: Cellook Shader settings  
・ToonShader_Emissive.unity: Shader settings using Emissive  
・ToonShader_Firefly.unity: built-in light and multiple real-time point lights  
・Baked Normal / Cube_HardEdge.unity: Reference of Baked Normal  
・Sample / Sample.unity: Introduction of basic shaders of UTS2  
・ShaderBall / ShaderBall.unity: Set UTS2 using shader ball  

Each and every scenes are for reference of shader and lighting settings.  
They will be useful for reference of the look and scene you want to make!  

-----
## 【新規】
### 2018/09/10：2.0.4.3 Release Patch 1：バグフィックス版
・スポットライトが正常に使えなかったのを修正しました。  
・リアルタイムポイントライトがレンジおよび距離に対し、正しく減衰するように修正しました。  
・修正が確認されましたので、iOS/OSX METAL環境での注意を削除しました。  

### 2018/09/05：2.0.4.3 Release：以下の修正と追加をしました。特にVRChat向けに便利な機能を搭載しています。  
### ●アンビエントライトブレンディングを搭載  
アンビエントライトの設定をライトカラーが反映するようになりました。  
その結果として、ディレクショナルライトのインテンシティの下限が、シーンのアンビエントライトの設定となります。　　
VRChatで、アンビエントライトの設定に基づくワールドごとの明るさの差異を自動で調整できます。  
なおアンビエ## 【サンプルシーンについて】  
ントライトからの明るさは、Unlit_Intensity スライダーで調整することもできます。デフォルトは 1（そのまま）になっています。  

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
最新バージョン：2.0.4.3  
最終リリース日：2018/09/10  
カテゴリー：3D  
形式：zip  

Latest Version: 2.0.4,3  
Update: 2018/09/10  
Category: 3D  
File format: zip  
