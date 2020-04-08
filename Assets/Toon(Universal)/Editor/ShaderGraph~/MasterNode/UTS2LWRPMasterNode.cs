
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEditor.Graphing;
using UnityEngine;
using UnityEditor.ShaderGraph;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.ShaderGraph.Drawing.Controls;

namespace UTJ.Experimental.UTS2LWRP
{
    [Serializable]
    [Title("Master", "UTS2 LWRP (Experimental)")]
    class UTS2LWRPMasterNode : MasterNode<IUTS2SubShader>, IMayRequirePosition, IMayRequireNormal
    {
        public const string Albedo0SlotName = "Albedo0";
        public const string Albedo1SlotName = "Albedo1";
        public const string Albedo2SlotName = "Albedo2";
        public const string BaseColorStepSlotName = "BaseColorStep";
        public const string ShadeColorStepSlotName = "ShadeColorStep";
        public const string NormalSlotName = "Normal";
        public const string HiColorSlotName = "HiColor";
        public const string SpecularPowerSlotName = "SpecularPower";
        public const string RimLightColorSlotName = "RimLightColor";
        public const string RimLightLevelSlotName = "RimLightLevel";
        public const string RimLightPowerSlotName = "RimLightPower";
        public const string GiIntensitySlotName = "GiIntensity";
        public const string EmissionSlotName = "Emission";
        public const string MetallicSlotName = "Metallic";
        public const string SpecularSlotName = "Specular";
        public const string SmoothnessSlotName = "Smoothness";
        public const string OcclusionSlotName = "Occlusion";
        public const string AlphaSlotName = "Alpha";
        public const string AlphaClipThresholdSlotName = "AlphaClipThreshold";
        public const string PositionName = "Position";
        public const string IsLightColorBaseName = "IsLightColorBase";
        public const string IsLightColor1stShadeName = "IsLightColor1stShade";
        public const string IsLightColor2ndShadeName = "IsLightColor2ndShade";
        public const string IsLightColorHighColorName = "IsLightColorHighColor";
        public const string IsSpecularToHighColorName = "IsSpecularToHighColor";
        public const string UnlitIntensityName = "UnlitIntensity";

        public const int Albedo0SlotId = 0;
        public const int Albedo1SlotId = 1;
        public const int Albedo2SlotId = 2;
        public const int BaseColorStepSlotId = 3;
        public const int ShadeColorStepSlotId = 4;
        public const int NormalSlotId = 5;
        public const int HiColorSlotId = 6;
        public const int SpecularPowerSlotId = 7;
        public const int RimLightColorSlotId = 8;
        public const int RimLightLevelSlotId = 9;
        public const int RimLightPowerSlotId = 10;
        public const int GiIntensitySlotId = 11;
        public const int MetallicSlotId = 12;
        public const int SpecularSlotId = 13;
        public const int EmissionSlotId = 14;
        public const int SmoothnessSlotId =15;
        public const int OcclusionSlotId = 16;
        public const int AlphaSlotId = 17;
        public const int AlphaThresholdSlotId = 18;
        public const int PositionSlotId = 19;
        public const int IsLightColorBaseId = 20;
        public const int IsLightColor1stShadeId = 21;
        public const int IsLightColor2ndShadeId = 22;
        public const int IsLightColorHighColorId = 23;
        public const int IsSpecularToHighColorId = 24;
        public const int UnlitIntensityId = 25;


        [MenuItem("Assets/Create/Shader/UTS2 Graph", false, 208)]
        public static void CreateUnlitMasterMaterialGraph()
        {
            GraphUtil.CreateNewGraph(new UTS2LWRPMasterNode());
        }

        public enum Model
        {
            Specular,
            Metallic
        }

        [SerializeField]
        Model m_Model = Model.Metallic;

        public Model model
        {
            get { return m_Model; }
            set
            {
                if (m_Model == value)
                    return;

                m_Model = value;
                UpdateNodeAfterDeserialization();
                Dirty(ModificationScope.Topological);
            }
        }

        [SerializeField]
        SurfaceType m_SurfaceType;

        public SurfaceType surfaceType
        {
            get { return m_SurfaceType; }
            set
            {
                if (m_SurfaceType == value)
                    return;

                m_SurfaceType = value;
                Dirty(ModificationScope.Graph);
            }
        }

        [SerializeField]
        AlphaMode m_AlphaMode;

        public AlphaMode alphaMode
        {
            get { return m_AlphaMode; }
            set
            {
                if (m_AlphaMode == value)
                    return;

                m_AlphaMode = value;
                Dirty(ModificationScope.Graph);
            }
        }

        [SerializeField]
        bool m_TwoSided;

        public ToggleData twoSided
        {
            get { return new ToggleData(m_TwoSided); }
            set
            {
                if (m_TwoSided == value.isOn)
                    return;
                m_TwoSided = value.isOn;
                Dirty(ModificationScope.Graph);
            }
        }

        public UTS2LWRPMasterNode()
        {
            UpdateNodeAfterDeserialization();
        }


        public sealed override void UpdateNodeAfterDeserialization()
        {
            var Asbedo0Gray = new Color(204.0f / 255.0f, 204.0f / 255.0f, 204.0f / 255.0f);
            var Asbedo1Gray = new Color(171.0f / 255.0f, 171.0f / 255.0f, 171.0f / 255.0f);
            var Asbedo2Gray = new Color(96.0f / 255.0f, 96.0f / 255.0f, 96.0f / 255.0f);
            base.UpdateNodeAfterDeserialization();
            name = "UTS2 Master";
            AddSlot(new PositionMaterialSlot(PositionSlotId, PositionName, PositionName, CoordinateSpace.Object, ShaderStageCapability.Vertex));
            AddSlot(new ColorRGBMaterialSlot(Albedo0SlotId, Albedo0SlotName, Albedo0SlotName, SlotType.Input, Color.grey.gamma, ColorMode.Default, ShaderStageCapability.Fragment));
            AddSlot(new ColorRGBMaterialSlot(Albedo1SlotId, Albedo1SlotName, Albedo1SlotName, SlotType.Input, Color.grey.gamma * 0.7f, ColorMode.Default, ShaderStageCapability.Fragment));
            AddSlot(new ColorRGBMaterialSlot(Albedo2SlotId, Albedo2SlotName, Albedo2SlotName, SlotType.Input, Color.grey.gamma * 0.4f, ColorMode.Default, ShaderStageCapability.Fragment));
            AddSlot(new Vector1MaterialSlot(BaseColorStepSlotId, BaseColorStepSlotName, BaseColorStepSlotName, SlotType.Input, 0.8f, ShaderStageCapability.Fragment));
            AddSlot(new Vector1MaterialSlot(ShadeColorStepSlotId, ShadeColorStepSlotName, ShadeColorStepSlotName, SlotType.Input, 0.5f, ShaderStageCapability.Fragment));

            AddSlot(new NormalMaterialSlot(NormalSlotId, NormalSlotName, NormalSlotName, CoordinateSpace.Tangent, ShaderStageCapability.Fragment));
            AddSlot(new ColorRGBMaterialSlot(HiColorSlotId, HiColorSlotName, HiColorSlotName, SlotType.Input, Color.white.gamma, ColorMode.Default, ShaderStageCapability.Fragment));
            AddSlot(new Vector1MaterialSlot(SpecularPowerSlotId, SpecularPowerSlotName, SpecularPowerSlotName, SlotType.Input, 0.28f, ShaderStageCapability.Fragment));

            AddSlot(new ColorRGBMaterialSlot(RimLightColorSlotId, RimLightColorSlotName, RimLightColorSlotName, SlotType.Input, Color.white.gamma, ColorMode.Default, ShaderStageCapability.Fragment));
            AddSlot(new Vector1MaterialSlot(RimLightLevelSlotId, RimLightLevelSlotName, RimLightLevelSlotName, SlotType.Input, 0.0f, ShaderStageCapability.Fragment));
            AddSlot(new Vector1MaterialSlot(RimLightPowerSlotId, RimLightPowerSlotName, RimLightPowerSlotName, SlotType.Input, 0.35f, ShaderStageCapability.Fragment));
            AddSlot(new Vector1MaterialSlot(GiIntensitySlotId, GiIntensitySlotName, GiIntensitySlotName, SlotType.Input, 0.0f, ShaderStageCapability.Fragment));

            AddSlot(new ColorRGBMaterialSlot(EmissionSlotId, EmissionSlotName, EmissionSlotName, SlotType.Input, Color.black, ColorMode.Default, ShaderStageCapability.Fragment));

            if (model == Model.Metallic)
                AddSlot(new Vector1MaterialSlot(MetallicSlotId, MetallicSlotName, MetallicSlotName, SlotType.Input, 0, ShaderStageCapability.Fragment));
            else
                AddSlot(new ColorRGBMaterialSlot(SpecularSlotId, SpecularSlotName, SpecularSlotName, SlotType.Input, Color.grey, ColorMode.Default, ShaderStageCapability.Fragment));
            AddSlot(new Vector1MaterialSlot(SmoothnessSlotId, SmoothnessSlotName, SmoothnessSlotName, SlotType.Input, 0.5f, ShaderStageCapability.Fragment));
            AddSlot(new Vector1MaterialSlot(OcclusionSlotId, OcclusionSlotName, OcclusionSlotName, SlotType.Input, 1f, ShaderStageCapability.Fragment));
            AddSlot(new Vector1MaterialSlot(AlphaSlotId, AlphaSlotName, AlphaSlotName, SlotType.Input, 1f, ShaderStageCapability.Fragment));
            AddSlot(new Vector1MaterialSlot(AlphaThresholdSlotId, AlphaClipThresholdSlotName, AlphaClipThresholdSlotName, SlotType.Input, 0.5f, ShaderStageCapability.Fragment));
            
            AddSlot(new Vector1MaterialSlot(IsLightColorBaseId, IsLightColorBaseName, IsLightColorBaseName, SlotType.Input, 0.0f, ShaderStageCapability.Fragment));
            AddSlot(new Vector1MaterialSlot(IsLightColor1stShadeId, IsLightColor1stShadeName, IsLightColor1stShadeName, SlotType.Input, 0.0f, ShaderStageCapability.Fragment));
            AddSlot(new Vector1MaterialSlot(IsLightColor2ndShadeId, IsLightColor2ndShadeName, IsLightColor2ndShadeName, SlotType.Input, 0.0f, ShaderStageCapability.Fragment));
            AddSlot(new Vector1MaterialSlot(IsLightColorHighColorId, IsLightColorHighColorName, IsLightColorHighColorName, SlotType.Input, 0.0f, ShaderStageCapability.Fragment));
            AddSlot(new Vector1MaterialSlot(IsSpecularToHighColorId, IsSpecularToHighColorName, IsSpecularToHighColorName, SlotType.Input, 0.0f, ShaderStageCapability.Fragment));
            AddSlot(new Vector1MaterialSlot(UnlitIntensityId, UnlitIntensityName, UnlitIntensityName, SlotType.Input, 0.0f, ShaderStageCapability.Fragment));

            
            // clear out slot names that do not match the slots
            // we support
            RemoveSlotsNameNotMatching(
                new[]
            {
                PositionSlotId,
                Albedo0SlotId,
                Albedo1SlotId,
                Albedo2SlotId,
                BaseColorStepSlotId,
                ShadeColorStepSlotId,
                NormalSlotId,
                HiColorSlotId,
                SpecularPowerSlotId,
                RimLightColorSlotId,
                RimLightLevelSlotId,
                RimLightPowerSlotId,
                GiIntensitySlotId,
                EmissionSlotId,
                model == Model.Metallic ? MetallicSlotId : SpecularSlotId,
                SmoothnessSlotId,
                OcclusionSlotId,
                AlphaSlotId,
                AlphaThresholdSlotId,
                IsLightColorBaseId,
                IsLightColor1stShadeId,
                IsLightColor2ndShadeId,
                IsLightColorHighColorId,
                IsSpecularToHighColorId,
                UnlitIntensityId

            }, true);
        }

        protected override VisualElement CreateCommonSettingsElement()
        {
            return new UTS2LWRPSettingsView(this);
        }

        public NeededCoordinateSpace RequiresNormal(ShaderStageCapability stageCapability)
        {
            List<MaterialSlot> slots = new List<MaterialSlot>();
            GetSlots(slots);

            List<MaterialSlot> validSlots = new List<MaterialSlot>();
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].stageCapability != ShaderStageCapability.All && slots[i].stageCapability != stageCapability)
                    continue;

                validSlots.Add(slots[i]);
            }
            return validSlots.OfType<IMayRequireNormal>().Aggregate(NeededCoordinateSpace.None, (mask, node) => mask | node.RequiresNormal(stageCapability));
        }

        public NeededCoordinateSpace RequiresPosition(ShaderStageCapability stageCapability)
        {
            List<MaterialSlot> slots = new List<MaterialSlot>();
            GetSlots(slots);

            List<MaterialSlot> validSlots = new List<MaterialSlot>();
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].stageCapability != ShaderStageCapability.All && slots[i].stageCapability != stageCapability)
                    continue;

                validSlots.Add(slots[i]);
            }
            return validSlots.OfType<IMayRequirePosition>().Aggregate(NeededCoordinateSpace.None, (mask, node) => mask | node.RequiresPosition(stageCapability));
        }
    }
}