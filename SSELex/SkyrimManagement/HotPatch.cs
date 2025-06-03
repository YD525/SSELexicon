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
    public static class HotPatch
    {
        public static void Apply()
        {
            var Asm = Assembly.Load("Mutagen.Bethesda.Skyrim"); // Or use typeof(someKnownType).Assembly instead

            // Use Reflection To Find Types
            var OverlayType = Asm.GetType("Mutagen.Bethesda.Skyrim.AddonNodeBinaryOverlay");
            var CreateTranslationType = Asm.GetType("Mutagen.Bethesda.Skyrim.AddonNodeBinaryCreateTranslation");

            if (OverlayType == null || CreateTranslationType == null)
                throw new Exception("Target Types Not Found");

            // Use Reflection To Get Methods
            var OverlayMethod = OverlayType.GetMethod("GetAlwaysLoadedCustom", BindingFlags.Public | BindingFlags.Instance);
            var FillMethod = CreateTranslationType.GetMethod("FillBinaryAlwaysLoadedCustom", BindingFlags.Public | BindingFlags.Static);

            if (OverlayMethod == null || FillMethod == null)
                throw new Exception("Target Methods Not Found");

            var Harmony = new Harmony("YD525.AlwaysLoadedPatch");

            // Prefix Methods Replace Static Methods Via Reflection
            var PrefixOverlay = typeof(HotPatch).GetMethod(nameof(GetAlwaysLoadedCustom_Prefix), BindingFlags.NonPublic | BindingFlags.Static);
            var PrefixFill = typeof(HotPatch).GetMethod(nameof(FillBinaryAlwaysLoadedCustom_Prefix), BindingFlags.NonPublic | BindingFlags.Static);

            // Patch
            Harmony.Patch(OverlayMethod, prefix: new HarmonyMethod(PrefixOverlay));
            Harmony.Patch(FillMethod, prefix: new HarmonyMethod(PrefixFill));
        }

        // object __instance Instead Of Concrete Type
        private static bool GetAlwaysLoadedCustom_Prefix(object __instance, ref bool __result)
        {
            var type = __instance.GetType();

            var fieldDataInfo = type.GetField("_recordData", BindingFlags.NonPublic | BindingFlags.Instance);
            var fieldLocationInfo = type.GetField("_AlwaysLoadedLocation", BindingFlags.NonPublic | BindingFlags.Instance);

            if (fieldDataInfo == null || fieldLocationInfo == null)
                throw new Exception("Fields Not Found");

            var fieldDataObj = fieldDataInfo.GetValue(__instance);
            var offsetObj = fieldLocationInfo.GetValue(__instance);

            if (fieldDataObj == null || offsetObj == null)
                throw new Exception("Field Values Are Null");

            // Assume _recordData Is byte[]
            if (fieldDataObj is byte[] bytes && offsetObj is int offset)
            {
                ushort val = BinaryPrimitives.ReadUInt16LittleEndian(bytes.AsSpan(offset));
                __result = val switch
                {
                    0 => false,
                    1 => false,
                    3 => true,
                    _ => throw new NotImplementedException()
                };
                return false; // Skip Original Method
            }
            else
            {
                throw new Exception("_recordData Field Type Mismatch");
            }
        }

        private static bool FillBinaryAlwaysLoadedCustom_Prefix(object frame, object item)
        {
            // frame Is MutagenFrame, item Is IAddonNodeInternal, Both Types Unknown So Use Reflection
            var frameType = frame.GetType();
            var itemType = item.GetType();

            // Call frame.ReadUInt16()
            var readUInt16Method = frameType.GetMethod("ReadUInt16", BindingFlags.Public | BindingFlags.Instance);
            if (readUInt16Method == null)
                throw new Exception("ReadUInt16 Method Not Found");

            var flagsObj = readUInt16Method.Invoke(frame, null);
            if (flagsObj == null)
                throw new Exception("ReadUInt16 Returned Null");

            ushort flags = (ushort)flagsObj;

            // Set item.AlwaysLoaded Property
            var alwaysLoadedProp = itemType.GetProperty("AlwaysLoaded", BindingFlags.Public | BindingFlags.Instance);
            if (alwaysLoadedProp == null)
                throw new Exception("AlwaysLoaded Property Not Found");

            bool val = flags switch
            {
                0 => false,
                1 => false,
                3 => true,
                _ => throw new NotImplementedException()
            };

            alwaysLoadedProp.SetValue(item, val);

            return false; // Skip Original Method
        }
    }
}
