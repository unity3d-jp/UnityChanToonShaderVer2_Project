README.txt

# 【ユニティちゃんトゥーンシェーダー Ver.2.0.8】
「ユニティちゃんトゥーンシェーダー」は、セル風3DCGアニメーションの制作現場での要望に応えるような形で設計された、映像志向のトゥーンシェーダーです。  

ユニティちゃんトゥーンシェーダーVer.2.0では、従来の機能に加えて大幅な機能強化を行いました。  
Ver.1.0でできる絵づくりをカバーしつつ、さらに高度なルックが実現できるようになっています。  

● **[日本語マニュアル（v.2.0.8版）](https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project/blob/blob/release/legacy/2.0/Manual/UTS2_Manual_ja.md)が提供されています。合わせてご利用ください。**  


## 【Unity-Chan Toon Shader Ver.2.0.8】
Unity-Chan Toon Shader is a toon shader for video and images that is designed to meet your needs when creating cel-shaded 3DCG animations.  

We have greatly enhanced the performance and features in Unity-Chan Toon Shader Ver. 2.0.  
It still has the same rendering capabilities as Ver. 1.0, but now you can give your creations an even more sophisticated look.  

● **[English manual for v.2.0.8](https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project/blob/blob/release/legacy/2.0/Manual/UTS2_Manual_en.md) is available now.**  



----
## 【重要】旧バージョンから、直接v.2.0.8へバージョンアップをする場合の注意

* v.2.0.5以降は、そのままシェーダーのみ上書きアップデートをして大丈夫です。  
* v.2.0.4.3p1以前からアップデートをする場合、シェーダーを上書きアップデートした後で、各マテリアルをプロジェクトウィンドウ内から再度選択することで、マテリアルを更新してください。BaseMapが元通りに修復されます。  
* v.2.0.4.3p1以前からアップデートをする場合、HiColor_Powerのスライダの感度調整をした影響が出る場合があります。以下に従って調整をしてください。  
1. Is_SpecularToHighColor=OFF/Is_BlendAddToHiColor=0FFの場合には、HiColor_Powerの値を今までよりも低めに調整してください。  
2. Is_SpecularToHighColor=ONで利用している場合には、特に修正する必要はありません。  


## [Important] Note on upgrading to version 2.0.8 directly

* In v.2.0.5 or later, you can overwrite and update only the shader.  
* When updating from v.2.0.4.3p1 or earlier, update the materials by selecting each material again from within the project window after overwriting and updating the shader. BaseMap is restored as it was.  
* When updating from v.2.0.4.3p1 or earlier, the sensitivity of the slider of HiColor_Power may be affected. Please adjust according to the following.  
1. If Is_SpecularToHighColor = OFF / Is_BlendAddToHiColor = 0FF, adjust the HiColor_Power value lower than before.  
2. If you use Is_SpecularToHighColor = ON, there is no need to modify it.  

-----
## 【ターゲット環境】
Unity5.6.x もしくはそれ以降が必要です。  
Unity 2018.2.21f1からUnity 2019.2.0a9までの動作確認が終了しています。  
Unity 2017.4.15f1 LTSを含む、Unity 2017.4.x LTSでの動作確認済み。  
本パッケージは、Unity5.6.7f1で作成されています。  

### 【Target Environment】
Requires Unity 5.6.x or higher.  
The operation check from Unity 2018.2.21f1 to Unity 2019.2.0a9 has been completed.  
It has been tested on Unity 2017.4.x LTS, including Unity 2017.4.15f1 LTS.  
This pack was created in Unity 5.6.7f1.  


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
最新バージョン：2.0.8 Release  
最終リリース日：2021/09/08  
カテゴリー：3D  
形式：zip/unitypackage  

Latest Version: 2.0.8 Release  
Update: 2021/09/08  
Category: 3D  
File format: zip/unitypackage  
