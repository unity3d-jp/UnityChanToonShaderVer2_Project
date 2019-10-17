
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEditor.Graphing;
using UnityEngine;
using UnityEditor.ShaderGraph;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine.Rendering;
using UnityEngine.Rendering.LWRP;
using System.IO;

namespace UTJ.Experimental.UTS2LWRP
{

    [Serializable]
    class UTS2SubShader : IUTS2SubShader
    {

        static readonly NeededCoordinateSpace k_PixelCoordinateSpace = NeededCoordinateSpace.World;

        struct Pass
        {
            public string Name;
            public List<int> VertexShaderSlots;
            public List<int> PixelShaderSlots;
        }

        Pass m_ForwardPassMetallic = new Pass
        {
            Name = "LightweightForward",
            PixelShaderSlots = new List<int>
        {
            UTS2LWRPMasterNode.Albedo0SlotId,
            UTS2LWRPMasterNode.Albedo1SlotId,
            UTS2LWRPMasterNode.Albedo2SlotId,
            UTS2LWRPMasterNode.BaseColorStepSlotId,
            UTS2LWRPMasterNode.ShadeColorStepSlotId,
            UTS2LWRPMasterNode.NormalSlotId,
            UTS2LWRPMasterNode.HiColorSlotId,
            UTS2LWRPMasterNode.SpecularPowerSlotId,
            UTS2LWRPMasterNode.RimLightColorSlotId,
            UTS2LWRPMasterNode.RimLightLevelSlotId,
            UTS2LWRPMasterNode.RimLightPowerSlotId,
            UTS2LWRPMasterNode.GiIntensitySlotId,
            UTS2LWRPMasterNode.EmissionSlotId,
            UTS2LWRPMasterNode.MetallicSlotId,
            UTS2LWRPMasterNode.SmoothnessSlotId,
            UTS2LWRPMasterNode.OcclusionSlotId,
            UTS2LWRPMasterNode.AlphaSlotId,
            UTS2LWRPMasterNode.AlphaThresholdSlotId,
            UTS2LWRPMasterNode.IsLightColorBaseId,
            UTS2LWRPMasterNode.IsLightColor1stShadeId,
            UTS2LWRPMasterNode.IsLightColor2ndShadeId,
            UTS2LWRPMasterNode.IsLightColorHighColorId,
            UTS2LWRPMasterNode.IsSpecularToHighColorId,
            UTS2LWRPMasterNode.UnlitIntensityId

        },
            VertexShaderSlots = new List<int>()
        {
            UTS2LWRPMasterNode.PositionSlotId
        }
        };

        Pass m_ForwardPassSpecular = new Pass()
        {
            Name = "LightweightForward",
            PixelShaderSlots = new List<int>()
        {
            UTS2LWRPMasterNode.Albedo0SlotId,
            UTS2LWRPMasterNode.Albedo1SlotId,
            UTS2LWRPMasterNode.Albedo2SlotId,
            UTS2LWRPMasterNode.BaseColorStepSlotId,
            UTS2LWRPMasterNode.ShadeColorStepSlotId,
            UTS2LWRPMasterNode.NormalSlotId,
            UTS2LWRPMasterNode.HiColorSlotId,
            UTS2LWRPMasterNode.SpecularPowerSlotId,
            UTS2LWRPMasterNode.RimLightColorSlotId,
            UTS2LWRPMasterNode.RimLightLevelSlotId,
            UTS2LWRPMasterNode.RimLightPowerSlotId,
            UTS2LWRPMasterNode.GiIntensitySlotId,
            UTS2LWRPMasterNode.EmissionSlotId,
            UTS2LWRPMasterNode.SpecularSlotId,
            UTS2LWRPMasterNode.SmoothnessSlotId,
            UTS2LWRPMasterNode.OcclusionSlotId,
            UTS2LWRPMasterNode.AlphaSlotId,
            UTS2LWRPMasterNode.AlphaThresholdSlotId,
            UTS2LWRPMasterNode.IsLightColorBaseId,
            UTS2LWRPMasterNode.IsLightColor1stShadeId,
            UTS2LWRPMasterNode.IsLightColor2ndShadeId,
            UTS2LWRPMasterNode.IsLightColorHighColorId,
            UTS2LWRPMasterNode.IsSpecularToHighColorId,
            UTS2LWRPMasterNode.UnlitIntensityId
        },
            VertexShaderSlots = new List<int>()
        {
            UTS2LWRPMasterNode.PositionSlotId
        }
        };

        Pass m_DepthShadowPass = new Pass()
        {
            Name = "",
            PixelShaderSlots = new List<int>()
        {
            UTS2LWRPMasterNode.Albedo0SlotId,
            UTS2LWRPMasterNode.Albedo1SlotId,
            UTS2LWRPMasterNode.Albedo2SlotId,
            UTS2LWRPMasterNode.EmissionSlotId,
            UTS2LWRPMasterNode.AlphaSlotId,
            UTS2LWRPMasterNode.AlphaThresholdSlotId
        },
            VertexShaderSlots = new List<int>()
        {
            UTS2LWRPMasterNode.PositionSlotId
        }
        };

        public int GetPreviewPassIndex() { return 0; }

        public string GetSubshader(IMasterNode masterNode, GenerationMode mode, List<string> sourceAssetDependencyPaths = null)
        {
            if (sourceAssetDependencyPaths != null)
            {
                // UTS2SubShader.cs
                sourceAssetDependencyPaths.Add(AssetDatabase.GUIDToAssetPath("10f802b230a2cd54782d75e7d20ca7aa"));
            }

            var templatePath = GetTemplatePath("UTS2_DoubleShadeFeather.template");
            var extraPassesTemplatePath = GetTemplatePath("UTS2_DoubleShadeFeatherExtra.template");

            if (!File.Exists(templatePath) || !File.Exists(extraPassesTemplatePath))
                return string.Empty;


            if (sourceAssetDependencyPaths != null)
            {
                sourceAssetDependencyPaths.Add(templatePath);
                sourceAssetDependencyPaths.Add(extraPassesTemplatePath);


                var relativePath = "Packages/com.unity.render-pipelines.lightweight/";
                var fullPath = Path.GetFullPath(relativePath);
                var shaderFiles = Directory.GetFiles(Path.Combine(fullPath, "ShaderLibrary")).Select(x => Path.Combine(relativePath, x.Substring(fullPath.Length)));
                sourceAssetDependencyPaths.AddRange(shaderFiles);
            }

            string forwardTemplate = File.ReadAllText(templatePath);
            string extraTemplate = File.ReadAllText(extraPassesTemplatePath);


            var pbrMasterNode = masterNode as UTS2LWRPMasterNode;
            var pass = pbrMasterNode.model == UTS2LWRPMasterNode.Model.Metallic ? m_ForwardPassMetallic : m_ForwardPassSpecular;
            var subShader = new ShaderStringBuilder();
            subShader.AppendLine("SubShader");
            using (subShader.BlockScope())
            {
                var materialTags = ShaderGenerator.BuildMaterialTags(pbrMasterNode.surfaceType);
                var tagsBuilder = new ShaderStringBuilder(0);
                materialTags.GetTags(tagsBuilder, LightweightRenderPipeline.k_ShaderTagName);
                subShader.AppendLines(tagsBuilder.ToString());

                var materialOptions = ShaderGenerator.GetMaterialOptions(pbrMasterNode.surfaceType, pbrMasterNode.alphaMode, pbrMasterNode.twoSided.isOn);
                subShader.AppendLines(GetShaderPassFromTemplate(
                        forwardTemplate,
                        pbrMasterNode,
                        pass,
                        mode,
                        materialOptions));

                subShader.AppendLines(GetShaderPassFromTemplate(
                        extraTemplate,
                        pbrMasterNode,
                        m_DepthShadowPass,
                        mode,
                        materialOptions));

                /*
                string txt = GetShaderPassFromTemplate(
                        lightweight2DTemplate,
                        pbrMasterNode,
                        pass,
                        mode,
                        materialOptions);
                subShader.AppendLines(txt);
                */

            }
            subShader.Append("CustomEditor \"UnityEditor.ShaderGraph.PBRMasterGUI\"");
//            subShader.Append("CustomEditor \"UnityEditor.ShaderGraph.UTS2MasterGUI\"");

            return subShader.ToString();
        }

        public bool IsPipelineCompatible(RenderPipelineAsset renderPipelineAsset)
        {
            return renderPipelineAsset is LightweightRenderPipelineAsset;
        }

        static string GetTemplatePath(string templateName)
        {
            //            var basePath = "Packages/com.unity.render-pipelines.lightweight/Editor/ShaderGraph/";
            var basePath = "Assets/Toon(LWRP)/Editor/ShaderGraph";
            string templatePath = Path.Combine(basePath, templateName);

            if (File.Exists(templatePath))
                return templatePath;

            throw new FileNotFoundException(string.Format(@"Cannot find a template with name ""{0}"".", templateName));
        }

        static string GetShaderPassFromTemplate(string template, UTS2LWRPMasterNode masterNode, Pass pass, GenerationMode mode, SurfaceMaterialOptions materialOptions)
        {
            // ----------------------------------------------------- //
            //                         SETUP                         //
            // ----------------------------------------------------- //

            // -------------------------------------
            // String builders

            var shaderProperties = new PropertyCollector();
            var shaderPropertyUniforms = new ShaderStringBuilder(1);
            var functionBuilder = new ShaderStringBuilder(1);
            var functionRegistry = new FunctionRegistry(functionBuilder);

            var defines = new ShaderStringBuilder(1);
            var graph = new ShaderStringBuilder(0);

            var vertexDescriptionInputStruct = new ShaderStringBuilder(1);
            var vertexDescriptionStruct = new ShaderStringBuilder(1);
            var vertexDescriptionFunction = new ShaderStringBuilder(1);

            var surfaceDescriptionInputStruct = new ShaderStringBuilder(1);
            var surfaceDescriptionStruct = new ShaderStringBuilder(1);
            var surfaceDescriptionFunction = new ShaderStringBuilder(1);

            var vertexInputStruct = new ShaderStringBuilder(1);
            var vertexOutputStruct = new ShaderStringBuilder(2);

            var vertexShader = new ShaderStringBuilder(2);
            var vertexShaderDescriptionInputs = new ShaderStringBuilder(2);
            var vertexShaderOutputs = new ShaderStringBuilder(2);

            var pixelShader = new ShaderStringBuilder(2);
            var pixelShaderSurfaceInputs = new ShaderStringBuilder(2);
            var pixelShaderSurfaceRemap = new ShaderStringBuilder(2);

            // -------------------------------------
            // Get Slot and Node lists per stage

            var vertexSlots = pass.VertexShaderSlots.Select(masterNode.FindSlot<MaterialSlot>).ToList();
            var vertexNodes = ListPool<AbstractMaterialNode>.Get();
            NodeUtils.DepthFirstCollectNodesFromNode(vertexNodes, masterNode, NodeUtils.IncludeSelf.Include, pass.VertexShaderSlots);

            var pixelSlots = pass.PixelShaderSlots.Select(masterNode.FindSlot<MaterialSlot>).ToList();
            var pixelNodes = ListPool<AbstractMaterialNode>.Get();
            NodeUtils.DepthFirstCollectNodesFromNode(pixelNodes, masterNode, NodeUtils.IncludeSelf.Include, pass.PixelShaderSlots);

            // -------------------------------------
            // Get Requirements

            var vertexRequirements = ShaderGraphRequirements.FromNodes(vertexNodes, ShaderStageCapability.Vertex, false);
            var pixelRequirements = ShaderGraphRequirements.FromNodes(pixelNodes, ShaderStageCapability.Fragment);
            var graphRequirements = pixelRequirements.Union(vertexRequirements);
            var surfaceRequirements = ShaderGraphRequirements.FromNodes(pixelNodes, ShaderStageCapability.Fragment, false);

            var modelRequiements = ShaderGraphRequirements.none;
            modelRequiements.requiresNormal |= k_PixelCoordinateSpace;
            modelRequiements.requiresTangent |= k_PixelCoordinateSpace;
            modelRequiements.requiresBitangent |= k_PixelCoordinateSpace;
            modelRequiements.requiresPosition |= k_PixelCoordinateSpace;
            modelRequiements.requiresViewDir |= k_PixelCoordinateSpace;
            modelRequiements.requiresMeshUVs.Add(UVChannel.UV1);

            // ----------------------------------------------------- //
            //                START SHADER GENERATION                //
            // ----------------------------------------------------- //

            // -------------------------------------
            // Calculate material options

            var blendingBuilder = new ShaderStringBuilder(1);
            var cullingBuilder = new ShaderStringBuilder(1);
            var zTestBuilder = new ShaderStringBuilder(1);
            var zWriteBuilder = new ShaderStringBuilder(1);

            materialOptions.GetBlend(blendingBuilder);
            materialOptions.GetCull(cullingBuilder);
            materialOptions.GetDepthTest(zTestBuilder);
            materialOptions.GetDepthWrite(zWriteBuilder);

            // -------------------------------------
            // Generate defines

            if (masterNode.IsSlotConnected(UTS2LWRPMasterNode.NormalSlotId))
                defines.AppendLine("#define _NORMALMAP 1");

            if (masterNode.model == UTS2LWRPMasterNode.Model.Specular)
                defines.AppendLine("#define _SPECULAR_SETUP 1");

            if (masterNode.IsSlotConnected(UTS2LWRPMasterNode.AlphaThresholdSlotId))
                defines.AppendLine("#define _AlphaClip 1");

            if (masterNode.surfaceType == SurfaceType.Transparent && masterNode.alphaMode == AlphaMode.Premultiply)
                defines.AppendLine("#define _ALPHAPREMULTIPLY_ON 1");

            if (graphRequirements.requiresDepthTexture)
                defines.AppendLine("#define REQUIRE_DEPTH_TEXTURE");

            if (graphRequirements.requiresCameraOpaqueTexture)
                defines.AppendLine("#define REQUIRE_OPAQUE_TEXTURE");

            // ----------------------------------------------------- //
            //                START VERTEX DESCRIPTION               //
            // ----------------------------------------------------- //

            // -------------------------------------
            // Generate Input structure for Vertex Description function
            // TODO - Vertex Description Input requirements are needed to exclude intermediate translation spaces

            vertexDescriptionInputStruct.AppendLine("struct VertexDescriptionInputs");
            using (vertexDescriptionInputStruct.BlockSemicolonScope())
            {
                ShaderGenerator.GenerateSpaceTranslationSurfaceInputs(vertexRequirements.requiresNormal, InterpolatorType.Normal, vertexDescriptionInputStruct);
                ShaderGenerator.GenerateSpaceTranslationSurfaceInputs(vertexRequirements.requiresTangent, InterpolatorType.Tangent, vertexDescriptionInputStruct);
                ShaderGenerator.GenerateSpaceTranslationSurfaceInputs(vertexRequirements.requiresBitangent, InterpolatorType.BiTangent, vertexDescriptionInputStruct);
                ShaderGenerator.GenerateSpaceTranslationSurfaceInputs(vertexRequirements.requiresViewDir, InterpolatorType.ViewDirection, vertexDescriptionInputStruct);
                ShaderGenerator.GenerateSpaceTranslationSurfaceInputs(vertexRequirements.requiresPosition, InterpolatorType.Position, vertexDescriptionInputStruct);

                if (vertexRequirements.requiresVertexColor)
                    vertexDescriptionInputStruct.AppendLine("float4 {0};", ShaderGeneratorNames.VertexColor);

                if (vertexRequirements.requiresScreenPosition)
                    vertexDescriptionInputStruct.AppendLine("float4 {0};", ShaderGeneratorNames.ScreenPosition);

                foreach (var channel in vertexRequirements.requiresMeshUVs.Distinct())
                    vertexDescriptionInputStruct.AppendLine("half4 {0};", channel.GetUVName());
            }

            // -------------------------------------
            // Generate Output structure for Vertex Description function

            GraphUtil.GenerateVertexDescriptionStruct(vertexDescriptionStruct, vertexSlots);

            // -------------------------------------
            // Generate Vertex Description function

            GraphUtil.GenerateVertexDescriptionFunction(
                masterNode.owner as GraphData,
                vertexDescriptionFunction,
                functionRegistry,
                shaderProperties,
                mode,
                vertexNodes,
                vertexSlots);

            // ----------------------------------------------------- //
            //               START SURFACE DESCRIPTION               //
            // ----------------------------------------------------- //

            // -------------------------------------
            // Generate Input structure for Surface Description function
            // Surface Description Input requirements are needed to exclude intermediate translation spaces

            surfaceDescriptionInputStruct.AppendLine("struct SurfaceDescriptionInputs");
            using (surfaceDescriptionInputStruct.BlockSemicolonScope())
            {
                ShaderGenerator.GenerateSpaceTranslationSurfaceInputs(surfaceRequirements.requiresNormal, InterpolatorType.Normal, surfaceDescriptionInputStruct);
                ShaderGenerator.GenerateSpaceTranslationSurfaceInputs(surfaceRequirements.requiresTangent, InterpolatorType.Tangent, surfaceDescriptionInputStruct);
                ShaderGenerator.GenerateSpaceTranslationSurfaceInputs(surfaceRequirements.requiresBitangent, InterpolatorType.BiTangent, surfaceDescriptionInputStruct);
                ShaderGenerator.GenerateSpaceTranslationSurfaceInputs(surfaceRequirements.requiresViewDir, InterpolatorType.ViewDirection, surfaceDescriptionInputStruct);
                ShaderGenerator.GenerateSpaceTranslationSurfaceInputs(surfaceRequirements.requiresPosition, InterpolatorType.Position, surfaceDescriptionInputStruct);

                if (surfaceRequirements.requiresVertexColor)
                    surfaceDescriptionInputStruct.AppendLine("float4 {0};", ShaderGeneratorNames.VertexColor);

                if (surfaceRequirements.requiresScreenPosition)
                    surfaceDescriptionInputStruct.AppendLine("float4 {0};", ShaderGeneratorNames.ScreenPosition);

                if (surfaceRequirements.requiresFaceSign)
                    surfaceDescriptionInputStruct.AppendLine("float {0};", ShaderGeneratorNames.FaceSign);

                foreach (var channel in surfaceRequirements.requiresMeshUVs.Distinct())
                    surfaceDescriptionInputStruct.AppendLine("half4 {0};", channel.GetUVName());
            }

            // -------------------------------------
            // Generate Output structure for Surface Description function

            GraphUtil.GenerateSurfaceDescriptionStruct(surfaceDescriptionStruct, pixelSlots);

            // -------------------------------------
            // Generate Surface Description function

            GraphUtil.GenerateSurfaceDescriptionFunction(
                pixelNodes,
                masterNode,
                masterNode.owner as GraphData,
                surfaceDescriptionFunction,
                functionRegistry,
                shaderProperties,
                pixelRequirements,
                mode,
                "PopulateSurfaceData",
                "SurfaceDescription",
                null,
                pixelSlots);

            // ----------------------------------------------------- //
            //           GENERATE VERTEX > PIXEL PIPELINE            //
            // ----------------------------------------------------- //

            // -------------------------------------
            // Property uniforms

            shaderProperties.GetPropertiesDeclaration(shaderPropertyUniforms, mode, masterNode.owner.concretePrecision);

            // -------------------------------------
            // Generate Input structure for Vertex shader

            GraphUtil.GenerateApplicationVertexInputs(vertexRequirements.Union(pixelRequirements.Union(modelRequiements)), vertexInputStruct);

            // -------------------------------------
            // Generate standard transformations
            // This method ensures all required transform data is available in vertex and pixel stages

            ShaderGenerator.GenerateStandardTransforms(
                3,
                10,
                vertexOutputStruct,
                vertexShader,
                vertexShaderDescriptionInputs,
                vertexShaderOutputs,
                pixelShader,
                pixelShaderSurfaceInputs,
                pixelRequirements,
                surfaceRequirements,
                modelRequiements,
                vertexRequirements,
                CoordinateSpace.World);

            // -------------------------------------
            // Generate pixel shader surface remap

            foreach (var slot in pixelSlots)
            {
                pixelShaderSurfaceRemap.AppendLine("{0} = surf.{0};", slot.shaderOutputName);
            }

            // -------------------------------------
            // Extra pixel shader work

            var faceSign = new ShaderStringBuilder();

            if (pixelRequirements.requiresFaceSign)
                faceSign.AppendLine(", half FaceSign : VFACE");

            // ----------------------------------------------------- //
            //                      FINALIZE                         //
            // ----------------------------------------------------- //

            // -------------------------------------
            // Combine Graph sections

            graph.AppendLines(shaderPropertyUniforms.ToString());

            graph.AppendLine(vertexDescriptionInputStruct.ToString());
            graph.AppendLine(surfaceDescriptionInputStruct.ToString());

            graph.AppendLine(functionBuilder.ToString());

            graph.AppendLine(vertexDescriptionStruct.ToString());
            graph.AppendLine(vertexDescriptionFunction.ToString());

            graph.AppendLine(surfaceDescriptionStruct.ToString());
            graph.AppendLine(surfaceDescriptionFunction.ToString());

            graph.AppendLine(vertexInputStruct.ToString());

            // -------------------------------------
            // Generate final subshader

            var resultPass = template.Replace("${Tags}", string.Empty);
            resultPass = resultPass.Replace("${Blending}", blendingBuilder.ToString());
            resultPass = resultPass.Replace("${Culling}", cullingBuilder.ToString());
            resultPass = resultPass.Replace("${ZTest}", zTestBuilder.ToString());
            resultPass = resultPass.Replace("${ZWrite}", zWriteBuilder.ToString());
            resultPass = resultPass.Replace("${Defines}", defines.ToString());

            resultPass = resultPass.Replace("${Graph}", graph.ToString());
            resultPass = resultPass.Replace("${VertexOutputStruct}", vertexOutputStruct.ToString());

            resultPass = resultPass.Replace("${VertexShader}", vertexShader.ToString());
            resultPass = resultPass.Replace("${VertexShaderDescriptionInputs}", vertexShaderDescriptionInputs.ToString());
            resultPass = resultPass.Replace("${VertexShaderOutputs}", vertexShaderOutputs.ToString());

            resultPass = resultPass.Replace("${FaceSign}", faceSign.ToString());
            resultPass = resultPass.Replace("${PixelShader}", pixelShader.ToString());
            resultPass = resultPass.Replace("${PixelShaderSurfaceInputs}", pixelShaderSurfaceInputs.ToString());
            resultPass = resultPass.Replace("${PixelShaderSurfaceRemap}", pixelShaderSurfaceRemap.ToString());

            return resultPass;
        }
    }

}