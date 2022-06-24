README.txt

# 【ユニティちゃんトゥーンシェーダー Ver.2.0.9】
「ユニティちゃんトゥーンシェーダー」は、セル風3DCGアニメーションの制作現場での要望に応えるような形で設計された、映像志向のトゥーンシェーダーです。  

ユニティちゃんトゥーンシェーダーVer.2.0では、従来の機能に加えて大幅な機能強化を行いました。  
Ver.1.0でできる絵づくりをカバーしつつ、さらに高度なルックが実現できるようになっています。  

### 2022/06/14：2.0.9 Release：新規機能追加    
* リリース環境を、Unity 2019.4.31f1に変更。Unity 2020.3.x LTSでの動作確認。  
* シングルパスインスタンシング レンダリング(ステレオインスタンシングとも呼ばれます)に対応。サポートするプラットフォームは、[Unity マニュアル](https://docs.unity3d.com/ja/2019.4/Manual/SinglePassInstancing.html)を参照してください。  
* 本リリースより、おまけのUTS2イメージエフェクトUnityPackageはサポート外として削除しました。 
* リアルタイムディレクショナルライトがない環境での、拡張アウトラインオブジェクトの環境ライティングへの馴染ませ具合を向上しました。  

● **[日本語マニュアル（v.2.0.9版）](https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project/blob/blob/release/legacy/2.0/Manual/UTS2_Manual_ja.md)が提供されています。合わせてご利用ください。**  


## 【Unity-Chan Toon Shader Ver.2.0.9】
Unity-Chan Toon Shader is a toon shader for video and images that is designed to meet your needs when creating cel-shaded 3DCG animations.  

We have greatly enhanced the performance and features in Unity-Chan Toon Shader Ver. 2.0.  
It still has the same rendering capabilities as Ver. 1.0, but now you can give your creations an even more sophisticated look.  

### 2022/05/23: 2.0.9 Release: new features added.    
* Changed release environment to Unity 2019.4.31f1, tested with Unity 2020.3.x LTS.  
* Single Pass Instanced rendering (also known as Stereo Instancing), support. See [Unity Manual](https://docs.unity3d.com/2019.4/Documentation/Manual/SinglePassInstancing.html) for supported platforms.  
* Note that the UnityPackages for UTS2 extra image effects has been removed as unsupported from this release.  
* Improved blending of extended outline objects with environmental lighting in environments without real-time directional lighting.  

● **[English manual for v.2.0.9](https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project/blob/blob/release/legacy/2.0/Manual/UTS2_Manual_en.md) is available now.**  

----
## 【重要】旧バージョンから、直接v.2.0.9へバージョンアップをする場合の注意

* v.2.0.5以降は、そのままシェーダーのみ上書きアップデートをして大丈夫です。  
* v.2.0.4.3p1以前からアップデートをする場合、シェーダーを上書きアップデートした後で、各マテリアルをプロジェクトウィンドウ内から再度選択することで、マテリアルを更新してください。BaseMapが元通りに修復されます。  
* v.2.0.4.3p1以前からアップデートをする場合、HiColor_Powerのスライダの感度調整をした影響が出る場合があります。以下に従って調整をしてください。  
1. Is_SpecularToHighColor=OFF/Is_BlendAddToHiColor=0FFの場合には、HiColor_Powerの値を今までよりも低めに調整してください。  
2. Is_SpecularToHighColor=ONで利用している場合には、特に修正する必要はありません。  


## [Important] Note on upgrading to version 2.0.9 directly

* In v.2.0.5 or later, you can overwrite and update only the shader.  
* When updating from v.2.0.4.3p1 or earlier, update the materials by selecting each material again from within the project window after overwriting and updating the shader. BaseMap is restored as it was.  
* When updating from v.2.0.4.3p1 or earlier, the sensitivity of the slider of HiColor_Power may be affected. Please adjust according to the following.  
1. If Is_SpecularToHighColor = OFF / Is_BlendAddToHiColor = 0FF, adjust the HiColor_Power value lower than before.  
2. If you use Is_SpecularToHighColor = ON, there is no need to modify it.  

-----
## 【ターゲット環境】
* UTS2シェーダー本体およびUTS2マテリアルは、Unity 5.6.7f1以降対応。（Unity 2019.4.31f1以降の使用を推奨します）  
* サンプルシーンを正常に再生するには、Unity 2019.4.31f1 もしくはそれ以降が必要です。  
* Unity 2019.4.31f1からUnity 2020.3.34f1、Unity 2021.3.3f1、Unity2022.1.1f1までの動作確認が終了しています。  
* 本パッケージは、Unity 2019.4.31f1で作成されています。  

### 【Target Environment】
* UTS2 shader itself and UTS2 materials are compatible with Unity 5.6.7f1 or later. (Unity 2019.4.31f1 or later is recommended)  
* Unity 2019.4.31f1 or later is required to properly play the sample scenes.  
* Unity 2019.4.31f1 through Unity 2020.3.34f1, Unity 2021.3.3f1, and Unity 2022.1.1f1 have been tested.  
* This package was created with Unity 2019.4.31f1.  

-----
## 【提供ライセンス】
「ユニティちゃんトゥーンシェーダーVer.2.0」は、UCL2.0（ユニティちゃんライセンス2.0）で提供されます。  
ユニティちゃんライセンスについては、以下を参照してください。  
https://unity-chan.com/contents/guideline/

### 【License】
Unity-Chan Toon Shader 2.0 is provided under the Unity-Chan License 2.0 terms.  
Please refer to the following link for information regarding the Unity-Chan License.  
https://unity-chan.com/contents/guideline_en/


-----
Latest Version: 2.0.9 Release
Update: 2022/06/14  
Category: 3D  
File format: zip/unitypackage  
