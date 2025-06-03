using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Mutagen.Bethesda.Skyrim;
using HarmonyLib;
using Mutagen.Bethesda.Plugins.Binary.Streams;

namespace SSELex.SkyrimManagement
{
    // This patch modifies the behavior of AddonNode's AlwaysLoaded flag parsing.
    // It includes a custom interpretation of certain flag values (0,1 treated as false).
    // Note: This patch is experimental and its stability is not fully guaranteed.
    // Use with caution and report any unexpected behavior.

    // This patch is based on the Mutagen.Bethesda.Skyrim framework, which is licensed under GNU GPL v3.
    // For more details and source code, see:
    //https://github.com/Mutagen-Modding/Mutagen/tree/dev/Mutagen.Bethesda.Skyrim
    public static class HotPatch
    {
        public static void Apply()
        {
            var Asm = Assembly.Load("Mutagen.Bethesda.Skyrim"); // Or Use typeof(SomeKnownType).Assembly Instead

            // Find Types Using Reflection
            var OverlayType = Asm.GetType("Mutagen.Bethesda.Skyrim.AddonNodeBinaryOverlay");
            var CreateTranslationType = Asm.GetType("Mutagen.Bethesda.Skyrim.AddonNodeBinaryCreateTranslation");

            if (OverlayType == null || CreateTranslationType == null)
                throw new Exception("Target Types Not Found");

            // Get Methods Using Reflection
            var OverlayMethod = OverlayType.GetMethod("GetAlwaysLoadedCustom", BindingFlags.Public | BindingFlags.Instance);
            var FillMethod = CreateTranslationType.GetMethod("FillBinaryAlwaysLoadedCustom", BindingFlags.Public | BindingFlags.Static);

            if (OverlayMethod == null || FillMethod == null)
                throw new Exception("Target Methods Not Found");

            var Harmony = new Harmony("YD525.AlwaysLoadedPatch");

            // Prefix Methods To Replace Original Methods Via Reflection
            var PrefixOverlay = typeof(HotPatch).GetMethod(nameof(GetAlwaysLoadedCustom_Prefix), BindingFlags.NonPublic | BindingFlags.Static);
            var PrefixFill = typeof(HotPatch).GetMethod(nameof(FillBinaryAlwaysLoadedCustom_Prefix), BindingFlags.NonPublic | BindingFlags.Static);

            // Apply Patches
            Harmony.Patch(OverlayMethod, prefix: new HarmonyMethod(PrefixOverlay));
            Harmony.Patch(FillMethod, prefix: new HarmonyMethod(PrefixFill));
        }

        // Use object __instance Instead Of Specific Type
        private static bool GetAlwaysLoadedCustom_Prefix(object __Instance, ref bool __Result)
        {
            var Type = __Instance.GetType();

            var FieldDataInfo = Type.GetField("_recordData", BindingFlags.NonPublic | BindingFlags.Instance);
            var FieldLocationInfo = Type.GetField("_AlwaysLoadedLocation", BindingFlags.NonPublic | BindingFlags.Instance);

            if (FieldDataInfo == null || FieldLocationInfo == null)
                throw new Exception("Fields Not Found");

            var FieldDataObj = FieldDataInfo.GetValue(__Instance);
            var OffsetObj = FieldLocationInfo.GetValue(__Instance);

            if (FieldDataObj == null || OffsetObj == null)
                throw new Exception("Field Values Are Null");

            // Assume _recordData Is byte[]
            if (FieldDataObj is byte[] Bytes && OffsetObj is int Offset)
            {
                ushort Val = BinaryPrimitives.ReadUInt16LittleEndian(Bytes.AsSpan(Offset));
                __Result = Val switch
                {
                    0 => false,
                    1 => false,
                    3 => true,
                    _ => throw new NotImplementedException()
                };
                return false; // Prevent Original Method Execution
            }
            else
            {
                throw new Exception("_recordData Field Type Mismatch");
            }
        }

        private static bool FillBinaryAlwaysLoadedCustom_Prefix(object Frame, object Item)
        {
            // Frame Is MutagenFrame, Item Is IAddonNodeInternal; Use Reflection Because Types Are Not Known
            var FrameType = Frame.GetType();
            var ItemType = Item.GetType();

            // Invoke frame.ReadUInt16()
            var ReadUInt16Method = FrameType.GetMethod("ReadUInt16", BindingFlags.Public | BindingFlags.Instance);
            if (ReadUInt16Method == null)
                throw new Exception("ReadUInt16 Method Not Found");

            var FlagsObj = ReadUInt16Method.Invoke(Frame, null);
            if (FlagsObj == null)
                throw new Exception("ReadUInt16 Returned Null");

            ushort Flags = (ushort)FlagsObj;

            // Set item.AlwaysLoaded Property
            var AlwaysLoadedProp = ItemType.GetProperty("AlwaysLoaded", BindingFlags.Public | BindingFlags.Instance);
            if (AlwaysLoadedProp == null)
                throw new Exception("AlwaysLoaded Property Not Found");

            bool Val = Flags switch
            {
                0 => false,
                1 => false,
                3 => true,
                _ => throw new NotImplementedException()
            };

            AlwaysLoadedProp.SetValue(Item, Val);

            return false; // Prevent Original Method Execution
        }
    }
}
