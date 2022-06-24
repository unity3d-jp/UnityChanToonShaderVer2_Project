# UTS2リリース履歴
## Version
### 2022/06/14：2.0.9 Release：新規機能追加    
* リリース環境を、Unity 2019.4.31f1に変更。Unity 2020.3.x LTS、Unity 2021.3.x LTS、Unity 2022.1.1f1での動作確認。  
* シングルパスインスタンシング レンダリング(ステレオインスタンシングとも呼ばれます)に対応。サポートするプラットフォームは、[Unity マニュアル](https://docs.unity3d.com/ja/2019.4/Manual/SinglePassInstancing.html)を参照してください。  
* 本リリースより、おまけのUTS2イメージエフェクトUnityPackageはサポート外として削除しました。  
* リアルタイムディレクショナルライトがない環境での、拡張アウトラインオブジェクトの環境ライティングへの馴染ませ具合を向上しました。  

### 2021/09/08：2.0.8 Release：アウトラインでの不具合修正版    
* VRChatの一部のワールドで、アウトラインの明るさが急に変化する場合があるのに対応（Issue#82）。  

### 2019/05/22：2.0.7 Release：修正リリース版５  
* 修正リリース版４を破棄し、新規に「アウトラインにZ-Offsetを指定した場合、鏡オブジェクトに映った像のアウトラインの表示不正」を修正しました。  
* アウトラインカラーへライトカラーを反応させる、"_Is_LightColor_Outline"を追加しました。カスタムGUIからは「LightColor Contribution to Materials」メニューから"Outline"ボタンを"Active"にすることで機能をONにすることができます。アウトラインの場合、ライトカラーへの反応はいくつか仕様上の制約がありますので、詳しくはマニュアルを参照してください。  

### 2019/05/15：2.0.7 Release：修正リリース版４  
* アウトラインにZ-Offsetを指定した場合、鏡オブジェクトに映った像のアウトラインの表示不正の発生を緩和しました。  

### 2019/05/10：2.0.7 Release：修正リリース版３  
* テスト用シーン（Mirror/MirrorTest.unity）を追加しました。  
* OpenGL時の_Offset_Zの符号を修正しました（UCTS_Outline.cginc）。  

### 2019/04/17：2.0.7 Release：修正リリース版２  
* UTS2 v.2.0.4.3p1以前のバージョンから、v.2.0.7へのアップデートが簡単になりました。  

### 2019/03/28：2.0.7 Release：修正リリース版１  
* 以下の新規機能を追加しました。  

#### 新規機能
* Basic Shader Settings内Option Menuに、`Remove Unused Keywords/Properties from Material`ボタンを追加しました。  
本機能を実行することで、UTS2マテリアル内の不要なシェーダーキーワードや、使用されていないプロパティ値を整理して削除することができます。  
プロジェクトのパブリッシュ時に、特にVRChatにアバターをアップロードする直前に適用することで、システムに対する不要な負荷を下げる効果が期待できます。  
本機能は、ACiiL TwitterID：@__aciil さんのIssue #18に基づき開発いたしました。  


### 2019/03/23：2.0.7 Release：リリース版  
* 以下の新規機能を追加しました。  

#### 新規機能
* Emissive Animation機能を追加しました。  
* 本バージョンよりVulkan対応をしました。ただし、DX11 Tessellation版UTS2には対応していません。  

#### 機能強化
* Angel Ring Camera Rolling Stabilizerを搭載しました。（常にONです）  
* 全てのバージョンのUnityで、鏡に映るMatCapの像とCamera Rolling Stabilizerが正しく機能するようになりました。  

### 2019/02/28：2.0.6 Release：修正リリース版  
* 「UTS2カスタムインスペクター」のデザインを再調整しました。合わせてマニュアルも更新しました。  

---
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
## 過去のリリース履歴
2019/01/07：2.0.5 Release：macOS / Unity 2018.3.0f2上でプロジェクトを開くと不正終了するのを修正  
2018/11/22：2.0.5 Release：リリース版  
2018/11/17：2.0.5 Test07：バグ修正  
2018/11/16：2.0.5 Test06：SceneLights Hi-Cut_Filterを搭載。VR Chat対応完了  
2018/11/11：2.0.5 Test05：カメラ補正付きMatCapを搭載 
2018/11/08：2.0.5 Test04：リアルタイムポイントライト使用時のセルルック品質向上
2018/11/06：2.0.5 Test03：機能追加  
2018/10/31：2.0.5 Test02：機能追加  
2018/10/06：2.0.5 Test：BaseMapの内部変数名を変更。Built-in Light Directionを追加  
2018/09/10：2.0.4.3 Release Patch 1：バグフィックス版  
2018/09/05：2.0.4.3 Release：VRChat向けに便利な機能を搭載。アンビエントライトブレンディングなど  
2018/08/21：2.0.4.2 Release：バグフィックス版  
2018/08/16：2.0.4.2 Release：MatcapMaskを追加  
2018/07/04：2.0.4.1 Release：Unlit_Intensityプロパティを追加  
2018/05/04：2.0.4 Release：ターゲット環境をUnity5.6.x以降に。DX11 Phong Tessellation対応。VR Chat対応を開始  
2017/06/19：2.0.3：Set_HighColorMask、Set_RimLightMaskの追加  
2017/06/09：2.0.2：Nintendo Switch、PlayStation 4に正式対応  
2017/05/20：2.0.1：トランスペアレント系シェーダーを２つ追加  
2017/05/07：2.0.0：最初のバージョン  

