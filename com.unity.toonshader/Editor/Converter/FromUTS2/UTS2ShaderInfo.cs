using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
namespace UnityEditor.Rendering.Toon
{ 
    internal class UTS2ShaderInfo 
    {
        // Start is called before the first frame update

        static internal readonly UTSGUID[] stdShaders =
        {
            new UTSGUID(  "9baf30ce95c751649b14d96da3a4b4d5", "Toon_DoubleShadeWithFeather"),
            new UTSGUID(  "96d4d9f975e6c8849bd1a5c06acfae84", "ToonColor_DoubleShadeWithFeather"),
            new UTSGUID(  "ccd13b7f8710b264ea8bd3bc4f51f9e4", "ToonColor_DoubleShadeWithFeather_Clipping"),
            new UTSGUID(  "9c3978743d5db18448a8b945c723a6eb", "ToonColor_DoubleShadeWithFeather_Clipping_StencilMask"),
            new UTSGUID(  "d7da29588857e774bb0650f1fae494c6", "ToonColor_DoubleShadeWithFeather_Clipping_StencilOut"),
            new UTSGUID(  "315897103223dab42a0746aa65ec251a", "ToonColor_DoubleShadeWithFeather_StencilMask"),
            new UTSGUID(  "2e5cc2da6af713844956264245e092e4", "ToonColor_DoubleShadeWithFeather_StencilOut"),
            new UTSGUID(  "369d674ae1ba36249bb00e2f73b0cd10", "ToonColor_DoubleShadeWithFeather_TransClipping"),
            new UTSGUID(  "8600b2bec3ae31145afa80084df20c61", "ToonColor_DoubleShadeWithFeather_TransClipping_StencilMask"),
            new UTSGUID(  "43d0eeb4c46f52841b0941e99ac9b16b", "ToonColor_DoubleShadeWithFeather_TransClipping_StencilOut"),
            new UTSGUID(  "97b7edb5fc0f5744c9b264c2224a0b1e", "ToonColor_DoubleShadeWithFeather_Transparent"),
            new UTSGUID(  "3b20fc0febd34f94baf0304bf47841d8", "ToonColor_DoubleShadeWithFeather_Transparent_StencilOut"),
            new UTSGUID(  "af8454e09b3a41448a4140e792059446", "ToonColor_ShadingGradeMap"),
            new UTSGUID(  "295fec4a7029edd4eb9522bef07f41ce", "ToonColor_ShadingGradeMap_AngelRing"),
            new UTSGUID(  "e32270aa38f4b664b90f04cc475fdb81", "ToonColor_ShadingGradeMap_AngelRing_StencilOut"),
            new UTSGUID(  "29a860a3f3c4cec43ab821338e28eac8", "ToonColor_ShadingGradeMap_AngelRing_TransClipping"),
            new UTSGUID(  "d5d9c1f4718235248ad37448b0c74c68", "ToonColor_ShadingGradeMap_AngelRing_TransClipping_StencilOut"),
            new UTSGUID(  "6439813c08a1f8947bb0ca6599499dd7", "ToonColor_ShadingGradeMap_StencilMask"),
            new UTSGUID(  "b39692f1382224b4cbe21c12ae51c639", "ToonColor_ShadingGradeMap_StencilOut"),
            new UTSGUID(  "cd7e85b59edbb7740841003baeb510b5", "ToonColor_ShadingGradeMap_TransClipping"),
            new UTSGUID(  "6b4b6d07944415f44b1fc2f0fc24535f", "ToonColor_ShadingGradeMap_TransClipping_StencilMask"),
            new UTSGUID(  "31c75b34739dfc64fb57bf49005e942d", "ToonColor_ShadingGradeMap_TransClipping_StencilOut"),
            new UTSGUID(  "7737ca8c4e3939f4086a6e08f93c2ebd", "ToonColor_ShadingGradeMap_Transparent"),
            new UTSGUID(  "be27d4be45de7dd4ab2e69c992876edb", "ToonColor_ShadingGradeMap_Transparent_StencilOut"),
            new UTSGUID(  "345def18d0906d544b7d12b050937392", "Toon_DoubleShadeWithFeather_Clipping"),
            new UTSGUID(  "7a735f9b121d96349b6da0a077299424", "Toon_DoubleShadeWithFeather_Clipping_StencilMask"),
            new UTSGUID(  "ed7fba947f3bccb4cbc78f55d7a56a70", "Toon_DoubleShadeWithFeather_Clipping_StencilOut"),
//            new UTS2GUID(  "1d10c7840eb6ba74c889a27f14ba6081", "Toon_DoubleShadeWithFeather_Mobile"),
//            new UTS2GUID(  "88791c14394118d42a5e176b433af322", "Toon_DoubleShadeWithFeather_Mobile_Clipping"),
//            new UTS2GUID(  "41f4ee183cb66ad40bc74a9f8f944974", "Toon_DoubleShadeWithFeather_Mobile_Clipping_StencilMask"),
//            new UTS2GUID(  "dec01cbdbc5b8da4ca8671815cda1557", "Toon_DoubleShadeWithFeather_Mobile_StencilMask"),
//            new UTS2GUID(  "55e8b9eeaaff205469365133fe7bc744", "Toon_DoubleShadeWithFeather_Mobile_StencilOut"),
//            new UTS2GUID(  "d4c592285a93c3844aafdaafffc07ec7", "Toon_DoubleShadeWithFeather_Mobile_TransClipping"),
//            new UTS2GUID(  "100d373b596f44d49ac9bb944d671d32", "Toon_DoubleShadeWithFeather_Mobile_TransClipping_StencilMask"),
            new UTSGUID(  "036bc90bfe3475b4c9fadb85d0520621", "Toon_DoubleShadeWithFeather_StencilMask"),
            new UTSGUID(  "0a1e4c9dcc0e9ea4db38ae9cb5059608", "Toon_DoubleShadeWithFeather_StencilOut"),
            new UTSGUID(  "e8e7d781c3155254b9ea8956c5bd1218", "Toon_DoubleShadeWithFeather_TransClipping"),
            new UTSGUID(  "79add09e32e5c4541980118f6c4045b6", "Toon_DoubleShadeWithFeather_TransClipping_StencilMask"),
            new UTSGUID(  "fb47be5a840097b45bac228446468ef3", "Toon_DoubleShadeWithFeather_TransClipping_StencilOut"),
//            new UTSGUID(  "42a47eda2ed77084c9136507eadb8641", "Toon_OutlineObject"),
//            new UTSGUID(  "2e2edd12fbf6bcb4ea1f34c17ee42df5", "Toon_OutlineObject_StencilOut"),
            new UTSGUID(  "ca035891872022e4f80c952b3916e450", "Toon_ShadingGradeMap"),
            new UTSGUID(  "9aadc53d7cdc63f4898ea042aa9d853b", "Toon_ShadingGradeMap_AngelRing"),
//            new UTSINFO(  "23e399973d807464fb195291a44a614c", "Toon_ShadingGradeMap_AngelRing_Mobile"),
//            new UTSINFO(  "8d33e4e4084e5af449f3e762fecce3c9", "Toon_ShadingGradeMap_AngelRing_Mobile_StencilOut"),
            new UTSGUID(  "415f07ab6fd766048ac6f8c2f2b406a9", "Toon_ShadingGradeMap_AngelRing_StencilOut"),
            new UTSGUID(  "b2a70923168ea0c40a3051a013c93a8a", "Toon_ShadingGradeMap_AngelRing_TransClipping"),
            new UTSGUID(  "d1e11a558d143f14c864edf263332764", "Toon_ShadingGradeMap_AngelRing_TransClipping_StencilOut"),
//            new UTSINFO(  "f90e11a40dcf4f745ae6b21b857943fa", "Toon_ShadingGradeMap_Mobile"),
//            new UTSINFO(  "206c554c8b0c60041a9d242385f543d3", "Toon_ShadingGradeMap_Mobile_StencilMask"),
//            new UTSINFO(  "cfc201757f2519c4bb6ef9265a046582", "Toon_ShadingGradeMap_Mobile_StencilOut"),
//            new UTSINFO(  "cce1da34c52aff745adf0222f56a356c", "Toon_ShadingGradeMap_Mobile_TransClipping"),
//            new UTSINFO(  "e88039bab21b7894e918126e8fce5d1b", "Toon_ShadingGradeMap_Mobile_TransClipping_StencilMask"),
            new UTSGUID(  "aa2e05ed58ca15441bd0989f008da78b", "Toon_ShadingGradeMap_StencilMask"),
            new UTSGUID(  "923058fda1b61544b93d91eeee772086", "Toon_ShadingGradeMap_StencilOut"),
            new UTSGUID(  "aebd33b74ef849a4882b4a8d55f0f0c9", "Toon_ShadingGradeMap_TransClipping"),
            new UTSGUID(  "0a05dd221bacbb448afac3d63e6bd833", "Toon_ShadingGradeMap_TransClipping_StencilMask"),
            new UTSGUID(  "67212ac11ff43b04a833d3986b997a9f", "Toon_ShadingGradeMap_TransClipping_StencilOut"),

        };
        static internal readonly UTSGUID[] tessShaders =
        {
            new UTSGUID(  "5b8a1502578ed764c9880a7be65c9672", "ToonColor_DoubleShadeWithFeather_Clipping_Tess", true),
            new UTSGUID(  "682e6e6cf60a51040ade19437a3f53e2", "ToonColor_DoubleShadeWithFeather_Clipping_Tess_StencilMask", true),
            new UTSGUID(  "148d1eca2cf299e4eb949d15c4cf95ee", "ToonColor_DoubleShadeWithFeather_Clipping_Tess_StencilOut", true),
            new UTSGUID(  "e987cf9cca0941042aa68d1dd51ee20f", "ToonColor_DoubleShadeWithFeather_Tess", true),
            new UTSGUID(  "97df86a7afe06ef41b2a2c242b10593e", "ToonColor_DoubleShadeWithFeather_Tess_StencilMask", true),
            new UTSGUID(  "b179fb8a87955a347b5f594a18b43475", "ToonColor_DoubleShadeWithFeather_Tess_StencilOut", true),
            new UTSGUID(  "60fe384b76fb67d40bc7e38411073dd6", "ToonColor_DoubleShadeWithFeather_TransClipping_Tess", true),
            new UTSGUID(  "4a20b66d106d3f5409f759b5193ecdc2", "ToonColor_DoubleShadeWithFeather_TransClipping_Tess_StencilMask", true),
            new UTSGUID(  "a7842aa9522c7584cae2169b8e1ddb86", "ToonColor_DoubleShadeWithFeather_TransClipping_Tess_StencilOut", true),
            new UTSGUID(  "0cb6c9e6216a91e4a9d38cd2acb4ccb6", "ToonColor_DoubleShadeWithFeather_Transparent_Tess", true),
            new UTSGUID(  "f28bba8b2f259bb40b697d91849c8794", "ToonColor_DoubleShadeWithFeather_Transparent_Tess_StencilOut", true),
            new UTSGUID(  "4876871966ca2344793e439d7391d7b0", "ToonColor_ShadingGradeMap_AngelRing_Tess", true),
            new UTSGUID(  "7c48bdc9fed28c14b8ad0748673b1369", "ToonColor_ShadingGradeMap_AngelRing_Tess_StencilOut", true),
            new UTSGUID(  "d3fb22770ec830b43bdb5ccb973e6f76", "ToonColor_ShadingGradeMap_AngelRing_Tess_TransClipping", true),
            new UTSGUID(  "11e8f1e181e558a47a387492d3ecdb88", "ToonColor_ShadingGradeMap_AngelRing_TransClipping_Tess_StencilOut", true),
            new UTSGUID(  "01494e58d87212f44ab51d29caea84e4", "ToonColor_ShadingGradeMap_Tess", true),
            new UTSGUID(  "24c20b8ed5be113499b40f4e3b6b03e6", "ToonColor_ShadingGradeMap_Tess_StencilMask", true),
            new UTSGUID(  "9cf7e8eb46e9128438d50adf7a841de6", "ToonColor_ShadingGradeMap_Tess_StencilOut", true),
            new UTSGUID(  "3c39a77fda28b5043a7a17c7877cf7b2", "ToonColor_ShadingGradeMap_TransClipping_Tess", true),
            new UTSGUID(  "bf840a439c33c8b4a99d52e6c3d8511f", "ToonColor_ShadingGradeMap_TransClipping_Tess_StencilMask", true),
            new UTSGUID(  "8eff803eae89c994fae3acf2f686fafa", "ToonColor_ShadingGradeMap_TransClipping_Tess_StencilOut", true),
            new UTSGUID(  "0959cb8822a344c4da890457e668fdc9", "ToonColor_ShadingGradeMap_Transparent_Tess", true),
            new UTSGUID(  "6d115cf94d14d1842a56dfff76b57f42", "ToonColor_ShadingGradeMap_Transparent_Tess_StencilOut", true),
            new UTSGUID(  "f0b2fc9b8a189134da9c7d24f361caf4", "Toon_DoubleShadeWithFeather_Clipping_Tess", true),
            new UTSGUID(  "8c94ee3046ef0574f87f6b658b4e4691", "Toon_DoubleShadeWithFeather_Clipping_Tess_StencilMask", true),
            new UTSGUID(  "c4aed8662ca0f194284f3ab649e66d23", "Toon_DoubleShadeWithFeather_Clipping_Tess_StencilOut", true),
            new UTSGUID(  "1f248db3b28fc5f44aabd7aca618bd1e", "Toon_DoubleShadeWithFeather_Tess", true),
            new UTSGUID(  "a3214384442742648aa664ef0039d397", "Toon_DoubleShadeWithFeather_Tess_Light", true),
            new UTSGUID(  "3073cd2564e4cde45a19c05e0012d22a", "Toon_DoubleShadeWithFeather_Tess_Light_StencilMask", true),
            new UTSGUID(  "7e7690a767a07da4f943439680e70db8", "Toon_DoubleShadeWithFeather_Tess_Light_StencilOut", true),
            new UTSGUID(  "08c65988dc25d9f44b791fcc18fb543a", "Toon_DoubleShadeWithFeather_Tess_StencilMask", true),
            new UTSGUID(  "3fb99ac3775edeb4aa9530db5a614c92", "Toon_DoubleShadeWithFeather_TransClipping_Tess", true),
            new UTSGUID(  "9855f226cd8152d4e99085272aceede6", "Toon_DoubleShadeWithFeather_TransClipping_Tess_StencilMask", true),
            new UTSGUID(  "2a0d4af863770404faee6488b86fe3c9", "Toon_DoubleShadeWithFeather_TransClipping_Tess_StencilOut", true),
            // new UTSGUID(  "1847c44f729b68e49ba63610abdf9132", "Toon_OutlineObject_Tess", UTS2INFO.OPAQUE),
            // new UTSGUID(  "06cae78b869a3234bab02eeb52197e1c", "Toon_OutlineObject_Tess_StencilOut", UTS2INFO.OPAQUE),
            new UTSGUID(  "3a1af221400a61a4b94bae19aa79da2b", "Toon_ShadingGradeMap_AngelRing_Tess", true),
            new UTSGUID(  "a1449ab672051624ca3160737b630f5e", "Toon_ShadingGradeMap_AngelRing_Tess_Light", true),
            new UTSGUID(  "79d3dc54c32b69b42be17c48d33575f2", "Toon_ShadingGradeMap_AngelRing_Tess_Light_StencilOut", true),
            new UTSGUID(  "18c9172cdf36a344f9aca9bbc0e7002d", "Toon_ShadingGradeMap_AngelRing_Tess_StencilOut", true),
            new UTSGUID(  "54a94f776a43a074c8c2d205bb934005", "Toon_ShadingGradeMap_AngelRing_TransClipping_Tess", true),
            new UTSGUID(  "d496a1c70c797ad43836d5bfff575b5f", "Toon_ShadingGradeMap_AngelRing_TransClipping_Tess_StencilOut", true),
            new UTSGUID(  "183ea557143786346b1bfc862ad22636", "Toon_ShadingGradeMap_Tess", true),
            new UTSGUID(  "356dd5af8f0d40e41b647d3d0a0555c1", "Toon_ShadingGradeMap_Tess_Light", true),
            new UTSGUID(  "ffadecfbd9e31f840ba4109fea0f0436", "Toon_ShadingGradeMap_Tess_Light_StencilMask", true),
            new UTSGUID(  "98ac5d198a471494da681b7b8d1e1727", "Toon_ShadingGradeMap_Tess_Light_StencilOut", true),
            new UTSGUID(  "0d799eb857c0e2c45bbdfb2c033d33e6", "Toon_ShadingGradeMap_Tess_StencilMask", true),
            new UTSGUID(  "e667137c8b6fd3d4390fc364b2e5c70b", "Toon_ShadingGradeMap_Tess_StencilOut", true),
            new UTSGUID(  "feba437d8ff93f745a78828529e9a272", "Toon_ShadingGradeMap_TransClipping_Tess", true),
            new UTSGUID(  "8d1395a9f4bfad44d8fddd0f2af19b1e", "Toon_ShadingGradeMap_TransClipping_Tess_StencilMask", true),
            new UTSGUID(  "08c6bb334aed21c4198cf46b71ebca2d", "Toon_ShadingGradeMap_TransClipping_Tess_StencilOut", true),

        };
        const string kUnityChanToonShader = "\"UnityChanToonShader/";
        const string kForward = "/FORWARD\"";
        static string GetShaderPath(string lineInShaderFile)
        {
            string[] words = lineInShaderFile.Split(RenderPipelineConverterContainer.wordSepeators, StringSplitOptions.None);
            var targetWord = Array.Find<string>(words, word => word.StartsWith("UsePass"));
            if (targetWord == null)
            {
                return null;
            }
            Debug.Assert(lineInShaderFile.Contains(kUnityChanToonShader));
            if (!lineInShaderFile.Contains(kUnityChanToonShader))
            {
                return null;
            }
            var shaderPath = Array.Find<string>(words, word => word.Contains(kUnityChanToonShader));
            if (!shaderPath.EndsWith(kForward))
            {
                return null;
            }
            var length = shaderPath.Length - kForward.Length - 1;
            shaderPath = shaderPath.Substring(1, length);
            return shaderPath;
        }
        static string FindShaderFile(string shaderName, List<UTSGUID[]> infoSrcTables)
        {
            foreach (var srcTable in infoSrcTables)
            {
                foreach (var shaderInfo in srcTable)
                {
                    string path = AssetDatabase.GUIDToAssetPath(shaderInfo.m_Guid);
                    Debug.Assert(!string.IsNullOrEmpty(path));
                    Shader shader = AssetDatabase.LoadAssetAtPath<Shader>(path);
                    string content = File.ReadAllText(path);
                    string[] lines = content.Split(RenderPipelineConverterContainer.lineSeparators, StringSplitOptions.None);
                    foreach (var line in lines)
                    {
                        string[] words = line.Split(RenderPipelineConverterContainer.wordSepeators, StringSplitOptions.None);
                        if (words.Length > 2 && words[0].StartsWith("Shader", StringComparison.OrdinalIgnoreCase))
                        {
                            var targetShaderName = Array.Find<string>(words, word => word.Contains(kUnityChanToonShader));
                            Debug.Assert(targetShaderName.Length > 2);
                            targetShaderName = targetShaderName.Substring(1, targetShaderName.Length - 2);
                            if ( targetShaderName == shaderName)
                            {
                                return path;
                            }

                        }
                    }
                }
            }
            return null;
        }

        static void WriteTable(List<UTS2INFO> targetTable)
        {
            var fileName = "D:/TMP/Table.cs";
            using (StreamWriter sw = new StreamWriter(fileName, false, System.Text.Encoding.ASCII))
            {
                sw.WriteLine("using System;");
                sw.WriteLine("using System.Collections;");
                sw.WriteLine("using System.Collections.Generic;");
                sw.WriteLine("using System.IO;");
                sw.WriteLine("using UnityEngine;");
                sw.WriteLine("namespace UnityEditor.Rendering.Toon");
                sw.WriteLine("{");
                sw.WriteLine("    internal class UTS2Table");
                sw.WriteLine("    {");
                sw.WriteLine("        static internal readonly UTS2INFO[] tables =  ");
                sw.WriteLine("        {");

                foreach (var table in targetTable)
                {
                    // Debug.Log(table.GetConstructorString());
                    sw.WriteLine("             " + table.GetConstructorString());
                }
                sw.WriteLine("        };");
                sw.WriteLine("    };");
                sw.WriteLine("}");
            }
        }

#if false   // internal use only.
        [MenuItem("Window/Rendering/Create UTS2 Table", false, 51)]
#endif
        static void CreateUTS2Table()
        {

            List<UTS2INFO> targetTable = new List<UTS2INFO>();
            List<UTSGUID[]> infoSrcTables = new List<UTSGUID[]>();
            infoSrcTables.Add(stdShaders);
            infoSrcTables.Add(tessShaders);
            var materials = new List<Material>();
            foreach ( var srcTable in infoSrcTables )
            {
                foreach (var shaderInfo in srcTable)
                {
                    string path = AssetDatabase.GUIDToAssetPath(shaderInfo.m_Guid);
                    Debug.Assert(!string.IsNullOrEmpty(path));
                    string originalPath = path;

                    Shader shader = AssetDatabase.LoadAssetAtPath<Shader>(path);
                    var material = new Material(shader);
                    materials.Add(material);

                    string renderType = UTS2INFO.OPAQUE;
                    UTS2RenderQueue queueInTable = UTS2RenderQueue.None;
                    switch (shader.renderQueue)
                    {
                        case (int)UnityEngine.Rendering.RenderQueue.Geometry:
                            queueInTable = UTS2RenderQueue.None;
                            break;
                        case (int)UnityEngine.Rendering.RenderQueue.AlphaTest:
                            queueInTable = UTS2RenderQueue.AlphaTest;
                            break;
                        case (int)UnityEngine.Rendering.RenderQueue.AlphaTest - 1:
                            queueInTable = UTS2RenderQueue.AlphaTestMinus1;
                            break;
                        case (int)UnityEngine.Rendering.RenderQueue.Transparent:
                            queueInTable = UTS2RenderQueue.Transparent;
                            break;
                        default:
                            Debug.Assert(false);
                            break;
                    }

                    string content = File.ReadAllText(path);
                    string[] lines = content.Split(RenderPipelineConverterContainer.lineSeparators, StringSplitOptions.None);

                    // stencil
                    // first of all we need to locate shaders set stencil mode in others
                    // 
                    foreach (var line in lines)
                    {
                        string[] words = line.Split(RenderPipelineConverterContainer.wordSepeators, StringSplitOptions.None);
                        var targetWord = Array.Find<string>(words, word => word.StartsWith("UsePass"));
                        if (targetWord == null)
                        {
                            continue;
                        }
                        Debug.Assert(line.Contains(kUnityChanToonShader));
                        if (!line.Contains(kUnityChanToonShader))
                        {
                            continue;
                        }
                        var shaderName = Array.Find<string>(words, word => word.Contains(kUnityChanToonShader));
                        if (!shaderName.Contains(kForward))
                        {
                            continue;
                        }
                        shaderName = GetShaderPath(line);
                        if (shaderName != null)
                        {
                            path = FindShaderFile(shaderName, infoSrcTables);
                            if (path != null)
                            {
                                content = File.ReadAllText(path);
                                lines = content.Split(RenderPipelineConverterContainer.lineSeparators, StringSplitOptions.None);
                                break;
                            }
                        }
                    }
                    // done,  usepass shader file.
                    int lineNo = 0;
                    bool findingShaderBlock = false;
                    bool parsingShaderBlock = false;

                    bool findingProperyBlock = false;
                    bool parsingProperyBlock = false;

                    bool findingSubShaderBlock = false;
                    bool parsingSubShaderBlock = false;

                    bool findingStencilBlock = false;
                    bool parsingStencilBlock = false;

                    bool isForwardPass = false;
                    bool findingPassBlock = false;
                    bool parsingPassBlock = false;

                    bool findingTagsInSubShader = false;
                    bool parsingTagsInSubShader = false;

                    bool isSGM = false;

                    int balanceLevel = 0;
                    int clippngMode = 0;
                    UTS3GUI.UTS_StencilMode stencilMode = UTS3GUI.UTS_StencilMode.Off;
                    foreach (var lineTmp in lines)
                    {
                        string line = lineTmp;
                        if (lineTmp.Contains("{") )
                        {
                            line = lineTmp.Replace("{", " { ");
                        }
                        if (lineTmp.Contains("}"))
                        {
                            line = lineTmp.Replace("}", " } ");

                        }
                        if (lineTmp.Contains("="))
                        {
                            line = lineTmp.Replace("=", " = ");
                        }
                        string[] words = line.Split(RenderPipelineConverterContainer.wordSepeators, StringSplitOptions.RemoveEmptyEntries);


                        if (words == null || words.Length == 0)
                        {
                            lineNo++;
                            continue;
                        }
                        bool commentFlag = false;
                        for (int ii = 0; ii < words.Length; ii++)
                        {

                            if (words[ii].StartsWith("//") || commentFlag)
                            {
                                words[ii] = "//";
                                commentFlag = true;
                            }
                        }

                        List<string> wordsTmp = new List<string>();
                        List<string> wordsCaseSensitveTmp = new List<string>();
                        foreach ( var word in words)
                        {
                            wordsTmp.Add(word.ToUpper());
                            wordsCaseSensitveTmp.Add(word);
                        }
                        for (int ii = 0; ii < words.Length; ii++)
                        {
                            words[ii] = words[ii].ToLower();
                        }
                        string[] wordsUpper = wordsTmp.ToArray();
                        string[] wordsCaseSensitive = wordsCaseSensitveTmp.ToArray();

                        var indexOfKakko = Array.IndexOf<string>(words, "{");
                        var indexOfKokka = Array.IndexOf<string>(words, "}");
                        if (!findingShaderBlock && !parsingShaderBlock)
                        {
                            if ( balanceLevel != 0)
                            {
                                Debug.LogError("Parse Error.");
                            }
                            Debug.Assert(balanceLevel == 0);
                            if ( Array.IndexOf<string>(words,"shader") >= 0)
                            {
                                findingShaderBlock = true;
                            }
                        }
                        if ( balanceLevel == 1)
                        {
                            if (Array.IndexOf<string>(words, "properties") >= 0)
                            {
                                findingProperyBlock = true;
                            }

                            if (Array.IndexOf<string>(words, "subshader") >= 0)
                            {
                                findingSubShaderBlock = true;
                            }
                        }
                        if (parsingProperyBlock)
                        {
                            // SGM ?
                            if (Array.IndexOf<string>(words, "_utstechnique") >= 0)
                            {
                                var indexOfEqueal = Array.IndexOf<string>(words, "=");
                                Debug.Assert(indexOfEqueal > 1);
                                isSGM = words[indexOfEqueal + 1] == "1";

                            }
                        }
                        else if ( parsingSubShaderBlock )
                        {
                            if (!findingTagsInSubShader)
                            {
                                if (Array.IndexOf<string>(words, "tags") >= 0)
                                {
                                    findingTagsInSubShader = true;
                                }
                            }
                            if (!findingPassBlock)
                            {
                                if ( Array.IndexOf<string>(words,"pass") >= 0)
                                {
                                    findingPassBlock = true;
                                }
                            }
                            if ( parsingPassBlock )
                            {
                                if (Array.IndexOf<string>(words,"name") == 0)
                                {
                                    if (Array.IndexOf<string>(words, "\"forward\"") == 1)
                                    {
                                        isForwardPass = true;
                                    }
                                }
                            }
                            if ( parsingTagsInSubShader )
                            {
                                var renderTypeIndex = Array.IndexOf<string>(words, "\"rendertype\"");
                                if ( renderTypeIndex >= 0 )
                                {
                                    var equalIndex = Array.IndexOf<string>(words, "=");
                                    Debug.Assert(equalIndex == renderTypeIndex + 1);
                                    renderType = wordsCaseSensitive[2].Substring(1, wordsCaseSensitive[2].Length - 2);
                                }
                            }
                            if (isForwardPass && parsingPassBlock)
                            {
                                // Stencil Mode
                                if (Array.IndexOf<string>(words, "stencil") >= 0)
                                {
                                    findingStencilBlock = true;
                                }
                                if (parsingStencilBlock)
                                {
                                    if (Array.IndexOf<string>(words, "comp") == 0)
                                    {
                                        Debug.Assert(words.Length >= 2);
                                        if (Array.IndexOf<string>(words, "always") > 0)
                                        {
                                            stencilMode = UTS3GUI.UTS_StencilMode.StencilMask;
                                        }
                                        if (Array.IndexOf<string>(words, "notequal") > 0)
                                        {
                                            stencilMode = UTS3GUI.UTS_StencilMode.StencilOut;
                                        }
                                    }
                                }

                                // Clipping Mode
                                var indexOfMultiCompile = Array.IndexOf<string>(words, "multi_compile");
                                if (indexOfMultiCompile >= 1)
                                {
                                    var indexOfPragma = Array.IndexOf<string>(words, "#pragma");
                                    if (indexOfPragma >= 0 && indexOfMultiCompile > indexOfPragma)
                                    {
                                        if (isSGM)
                                        {
                                            if (Array.IndexOf<string>(wordsUpper, UTS3GUI.ShaderDefineIS_TRANSCLIPPING_OFF) > 0)
                                            {
                                                clippngMode = 0;
                                            }
                                            if (Array.IndexOf<string>(wordsUpper, UTS3GUI.ShaderDefineIS_TRANSCLIPPING_ON) > 0)
                                            {
                                                clippngMode = 1;
                                            }
                                        }
                                        else
                                        {
                                            if (Array.IndexOf<string>(wordsUpper, UTS3GUI.ShaderDefineIS_CLIPPING_OFF) > 0)
                                            {
                                                clippngMode = 0;
                                            }
                                            if (Array.IndexOf<string>(wordsUpper, UTS3GUI.ShaderDefineIS_CLIPPING_MODE) > 0)
                                            {
                                                clippngMode = 1;
                                            }
                                            if (Array.IndexOf<string>(wordsUpper, UTS3GUI.ShaderDefineIS_CLIPPING_TRANSMODE) > 0)
                                            {
                                                clippngMode = 2;
                                            }
                                        }
                                    }
                                }

                            } // if (isForwardPass)


                        }


                        if (indexOfKakko >= 0)
                        {

                            if (balanceLevel == 0 && findingShaderBlock)
                            {
                                parsingShaderBlock = true;
                                findingShaderBlock = false;
                            }
                            if (balanceLevel == 1)
                            {
                                Debug.Assert(parsingShaderBlock);

                                if (findingProperyBlock)
                                {
                                    findingProperyBlock = false;
                                    parsingProperyBlock = true;
                                }


                                if (findingSubShaderBlock )
                                {
                                    findingSubShaderBlock = false;
                                    parsingSubShaderBlock = true;
                                }
                            }
                            if (findingStencilBlock)
                            {
                                findingStencilBlock = false;
                                parsingStencilBlock = true;
                            }
                            if (findingPassBlock)
                            {
                                findingPassBlock = false;
                                parsingPassBlock = true;
                            }
                            if (findingTagsInSubShader)
                            {
                                findingTagsInSubShader = false;
                                parsingTagsInSubShader = true;

                            }
                            balanceLevel++;
                        }
                        if (indexOfKokka >= 0)
                        {
                            balanceLevel--;
                            if (parsingShaderBlock && balanceLevel == 0)
                            {
                                parsingShaderBlock = false;
                            }
                            if (parsingProperyBlock == true && balanceLevel == 1)
                            {
                                parsingProperyBlock = false;
                            }
                            if (parsingSubShaderBlock == true && balanceLevel == 1)
                            {
                                parsingSubShaderBlock = false;
                            }
                            if ( parsingStencilBlock == true && balanceLevel == 3)
                            {
                                parsingStencilBlock = false;
                            }
                            if ( parsingPassBlock == true && balanceLevel == 2)
                            {
                                parsingPassBlock = false;
                            }
                            if (parsingTagsInSubShader == true && balanceLevel == 2)
                            {
                                parsingTagsInSubShader = false;
                            }
                        }
                        
                        lineNo++;
                    } // 
                    Debug.Assert(!parsingShaderBlock);
                    if (balanceLevel != 0)
                    {
                        Debug.LogError("Parse Error2.");
                    }
                    Debug.Assert(balanceLevel == 0);
                    UTS2INFO targetInfo = new UTS2INFO(shaderInfo.m_Guid,
                        Path.GetFileName(originalPath),
                        renderType,
                        queueInTable,
                        stencilMode,
                        clippngMode,
                        tessellation: shaderInfo.m_Tessellation);
                    targetTable.Add(targetInfo);
                }
            }
            WriteTable(targetTable);
        }

    }
}