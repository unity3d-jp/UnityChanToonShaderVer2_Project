
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEditor.Graphing;
using UnityEngine;
using UnityEditor.ShaderGraph;
using UnityEditor;
using UnityEngine.UIElements;

namespace UTJ.Experimental.UTS2LWRP
{
    [Serializable]
    [Title("Master", "UTS2 LWRP (Experimental)")]
    class UTS2LWRPMasterNode : MasterNode<IUTS2Shader>, IMayRequirePosition,IMayRequireNormal
    {
        public const string AlbedoSlotName = "Albedo";
        public const string NormalSlotName = "Normal";
        public const string EmissionSlotName = "Emission";
        public const string MetallicSlotName = "Metallic";
        public const string SpecularSlotName = "Specular";
        public const string SmoothnessSlotName = "Smoothness";
        public const string OcclusionSlotName = "Occlusion";
        public const string AlphaSlotName = "Alpha";
        public const string AlphaClipThresholdSlotName = "AlphaClipThreshold";
        public const string PositionName = "Position";

        public const int AlbedoSlotId = 0;
        public const int NormalSlotId = 1;
        public const int MetallicSlotId = 2;
        public const int SpecularSlotId = 3;
        public const int EmissionSlotId = 4;
        public const int SmoothnessSlotId = 5;
        public const int OcclusionSlotId = 6;
        public const int AlphaSlotId = 7;
        public const int AlphaThresholdSlotId = 8;
        public const int PositionSlotId = 9;

        [MenuItem("Assets/Create/Shader/UTS2 Graph", false, 208)]
        public static void CreateUTS2MasterMaterialGraph()
        {
            GraphUtil.CreateNewGraph(new UTS2LWRPMasterNode());
        }

        public UTS2LWRPMasterNode()
        {
            UpdateNodeAfterDeserialization();
        }

        public sealed override void UpdateNodeAfterDeserialization()
        {
            base.UpdateNodeAfterDeserialization();
            name = "UTS2 Master";
            AddSlot(new PositionMaterialSlot(PositionSlotId, PositionName, PositionName, CoordinateSpace.Object, ShaderStageCapability.Vertex));
            AddSlot(new ColorRGBMaterialSlot(AlbedoSlotId, AlbedoSlotName, AlbedoSlotName, SlotType.Input, Color.grey.gamma, ColorMode.Default, ShaderStageCapability.Fragment));
            AddSlot(new NormalMaterialSlot(NormalSlotId, NormalSlotName, NormalSlotName, CoordinateSpace.Tangent, ShaderStageCapability.Fragment));

            // clear out slot names that do not match the slots
            // we support
            RemoveSlotsNameNotMatching(
                new[]
            {
                PositionSlotId,
                AlbedoSlotId,
                NormalSlotId,
            }, true);
        }

        protected override VisualElement CreateCommonSettingsElement()
        {
            return new UTS2LWRPSettingsView(this);
        }

        public NeededCoordinateSpace RequiresNormal(ShaderStageCapability stageCapability = ShaderStageCapability.All)
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

        public NeededCoordinateSpace RequiresPosition(ShaderStageCapability stageCapability = ShaderStageCapability.All)
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