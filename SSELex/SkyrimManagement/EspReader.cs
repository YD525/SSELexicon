using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace SSELex.SkyrimManagement
{
    public class SubRecordData
    {
        public string Sig { get; set; }
        public byte[] Data { get; set; }
        public bool IsLocalized { get; set; }
        public uint StringID { get; set; }
        public string Content { get; set; }
    }

    public class EspRecordInfo
    {
        public string Sig { get; set; }
        public uint FormID { get; set; }
        public uint Flags { get; set; }
        public List<SubRecordData> SubRecords { get; set; }

        public EspRecordInfo()
        {
            SubRecords = new List<SubRecordData>();
        }

        public string GetUniqueKey()
        {
            return $"{Sig}:{FormID}";
        }

        public string GetFormIDHex()
        {
            return $"{FormID:X8}";
        }

        public string GetEditorID()
        {
            var edid = SubRecords.Find(s => s.Sig == "EDID");
            return edid?.Content ?? "";
        }

        public string GetDisplayName()
        {
            var full = SubRecords.Find(s => s.Sig == "FULL");
            if (full != null && !string.IsNullOrEmpty(full.Content))
                return full.Content;
            return GetEditorID();
        }
    }

    public static class EspInterop
    {
        private const string DllName = "EspReader.dll";

        #region P/Invoke 

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_Init();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_InitDefaultFilter();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int C_SetDefaultFilter();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_ClearFilter();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int C_SetFilter(
            [MarshalAs(UnmanagedType.LPStr)] string parentSig,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] childSigs,
            int childCount);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int C_ReadEsp([MarshalAs(UnmanagedType.LPWStr)] string espPath);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr C_SearchBySig(
            [MarshalAs(UnmanagedType.LPStr)] string parentSig,
            [MarshalAs(UnmanagedType.LPStr)] string childSig,
            out int outCount);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void FreeSearchResults(IntPtr arr, int count);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_Close();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_Clear();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr C_GetRecordSig(IntPtr record);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern uint C_GetRecordFormID(IntPtr record);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern uint C_GetRecordFlags(IntPtr record);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern int C_GetSubRecordCount(IntPtr record);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr C_GetSubRecordSig(IntPtr record, int index);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr C_GetSubRecordString(IntPtr record, int index);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool C_IsSubRecordLocalized(IntPtr record, int index);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern uint C_GetSubRecordStringID(IntPtr record, int index);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern int C_GetSubRecordDataSize(IntPtr record, int index);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool C_GetSubRecordData(IntPtr record, int index, byte[] buffer, int bufferSize);

        #endregion

        public static void SetFilter(Dictionary<string, string[]> filterConfig)
        {
            C_ClearFilter();

            foreach (var kvp in filterConfig)
            {
                C_SetFilter(kvp.Key, kvp.Value, kvp.Value.Length);
            }
        }

        static EspInterop()
        {
            C_Init();
            C_InitDefaultFilter();
            C_SetDefaultFilter();
        }

        public static int LoadEsp(string Path)
        {
            if (File.Exists(Path))
            {
                return C_ReadEsp(Path);
            }
            return -1;
        }

        public static List<EspRecordInfo> SearchBySig(string ParentSig = "ALL", string ChildSig = "")
        {
            int Count;
            IntPtr ResultsPtr = C_SearchBySig(ParentSig, ChildSig, out Count);

            var Results = new List<EspRecordInfo>();

            if (ResultsPtr == IntPtr.Zero || Count == 0)
            {
                return Results;
            }

            try
            {
                for (int i = 0; i < Count; i++)
                {
                    IntPtr RecordPtr = Marshal.ReadIntPtr(ResultsPtr, i * IntPtr.Size);

                    if (RecordPtr == IntPtr.Zero)
                        continue;

                    var Record = new EspRecordInfo();

                    IntPtr SigPtr = C_GetRecordSig(RecordPtr);
                    if (SigPtr != IntPtr.Zero)
                    {
                        Record.Sig = Marshal.PtrToStringAnsi(SigPtr);
                    }

                    Record.FormID = C_GetRecordFormID(RecordPtr);
                    Record.Flags = C_GetRecordFlags(RecordPtr);

                    int SubRecordCount = C_GetSubRecordCount(RecordPtr);

                    for (int j = 0; j < SubRecordCount; j++)
                    {
                        var SubRecord = new SubRecordData();

                        IntPtr SubSigPtr = C_GetSubRecordSig(RecordPtr, j);
                        if (SubSigPtr != IntPtr.Zero)
                        {
                            SubRecord.Sig = Marshal.PtrToStringAnsi(SubSigPtr);
                        }

                        IntPtr ContentPtr = C_GetSubRecordString(RecordPtr, j);
                        if (ContentPtr != IntPtr.Zero)
                        {
                            SubRecord.Content = Marshal.PtrToStringAnsi(ContentPtr);
                        }

                        SubRecord.IsLocalized = C_IsSubRecordLocalized(RecordPtr, j);
                        SubRecord.StringID = C_GetSubRecordStringID(RecordPtr, j);

                        int DataSize = C_GetSubRecordDataSize(RecordPtr, j);
                        if (DataSize > 0)
                        {
                            SubRecord.Data = new byte[DataSize];
                            C_GetSubRecordData(RecordPtr, j, SubRecord.Data, DataSize);
                        }
                        else
                        {
                            SubRecord.Data = new byte[0];
                        }

                        Record.SubRecords.Add(SubRecord);
                    }

                    Results.Add(Record);
                }

                return Results;
            }
            finally
            {
                FreeSearchResults(ResultsPtr, Count);
            }
        }
    }

    public static class EspReader
    {
        public class RecordItem
        {
            public uint StringID = 0;//StringsFile id
            public string FormID = "";
            public string ParentSig = "";
            public string ChildSig = "";
            public string UniqueKey = "";
            public string String = "";
        }

        public static List<RecordItem> Records = new List<RecordItem>();
        public static List<RecordItem> LoadEsp(string Path)
        {
            Records.Clear();
            List<RecordItem> RecordItems = new List<RecordItem>();
            var State = EspInterop.LoadEsp(Path);

            if (State >= 0)
            {
                foreach (var GetRecord in EspInterop.SearchBySig("ALL"))
                {
                    string ParentFormID = GetRecord.GetFormIDHex();
                    string ParentSig = GetRecord.Sig;

                    foreach (var Sub in GetRecord.SubRecords)
                    {
                        var MergeSig = ParentSig + ":" + Sub.Sig;

                        string UniqueKey = ParentFormID + "-" + MergeSig;

                        RecordItem NRecordItem = new RecordItem();
                        NRecordItem.StringID = Sub.StringID;
                        NRecordItem.FormID = ParentFormID;
                        NRecordItem.ParentSig = ParentSig;
                        NRecordItem.ChildSig = Sub.Sig;
                        NRecordItem.UniqueKey = UniqueKey;
                        NRecordItem.String = Sub.Content;

                        if (NRecordItem.String.Length > 0)
                        {
                            RecordItems.Add(NRecordItem);
                        }
                    }
                }
            }

            Records = RecordItems;
            return RecordItems;
        }

        public static void Close()
        {
            EspInterop.C_Clear();
        }
    }
}
