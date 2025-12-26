using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using System.Windows.Documents;
using PhoenixEngine.ConvertManager;
using PhoenixEngine.EngineManagement;
using SSELex.SkyrimManage;

namespace SSELex.SkyrimManagement
{
    public class SubRecordData
    {
        public string Sig { get; set; }
        public byte[] Data { get; set; }
        public bool IsLocalized { get; set; }
        public uint StringID { get; set; }
        public string Content { get; set; }
        public int OccurrenceIndex { get; set; }  
        public int GlobalIndex { get; set; }       
    }

    public class EspRecordInfo
    {
        public IntPtr Handle { get; set; }  // Intptr
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

        // SubRecordData Api
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr C_GetSubRecordData_Ptr(IntPtr record, int index);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int C_SubRecordData_GetOccurrenceIndex(IntPtr subRecord);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int C_SubRecordData_GetGlobalIndex(IntPtr subRecord);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr C_SubRecordData_GetSig(IntPtr subRecord);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr C_SubRecordData_GetString(IntPtr subRecord);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int C_SubRecordData_GetStringUtf8(IntPtr subRecord, byte[] buffer, int bufferSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int C_SubRecordData_GetSigUtf8(IntPtr subRecord, byte[] buffer, int bufferSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool C_SubRecordData_IsLocalized(IntPtr subRecord);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint C_SubRecordData_GetStringID(IntPtr subRecord);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int C_SubRecordData_GetDataSize(IntPtr subRecord);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool C_SubRecordData_GetData(IntPtr subRecord, byte[] buffer, int bufferSize);

        // Record Api
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr C_GetRecordSig(IntPtr record);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern uint C_GetRecordFormID(IntPtr record);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern uint C_GetRecordFlags(IntPtr record);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern int C_GetSubRecordCount(IntPtr record);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool C_ModifySubRecord(
         uint FormID,
         IntPtr RecordSig,
         IntPtr SubSig,
         int OccurrenceIndex,
         int GlobalIndex,
         IntPtr NewUtf8Data
        );

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool C_SaveEsp(IntPtr utf8Path);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_Close();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_Clear();

        #endregion

        public static bool SaveEsp(string path)
        {
            IntPtr ptr = IntPtr.Zero;
            try
            {
                ptr = StringToUtf8IntPtr(path);
                return C_SaveEsp(ptr);
            }
            finally
            {
                if (ptr != IntPtr.Zero) Marshal.FreeHGlobal(ptr);
            }
        }

        private static IntPtr StringToUtf8IntPtr(string str)
        {
            byte[] utf8Bytes = Encoding.UTF8.GetBytes(str + "\0"); 
            IntPtr ptr = Marshal.AllocHGlobal(utf8Bytes.Length);
            Marshal.Copy(utf8Bytes, 0, ptr, utf8Bytes.Length);
            return ptr;
        }

        public static bool ModifySubRecord(uint formId,string recordSig,string subSig,int occurrenceIndex,int globalIndex,string newUtf8Data)
        {
            IntPtr ptrRecordSig = IntPtr.Zero;
            IntPtr ptrSubSig = IntPtr.Zero;
            IntPtr ptrNewData = IntPtr.Zero;

            try
            {
                ptrRecordSig = StringToUtf8IntPtr(recordSig ?? "");
                ptrSubSig = StringToUtf8IntPtr(subSig ?? "");
                ptrNewData = StringToUtf8IntPtr(newUtf8Data ?? "");

                return C_ModifySubRecord(formId, ptrRecordSig, ptrSubSig, occurrenceIndex, globalIndex, ptrNewData);
            }
            finally
            {
                if (ptrRecordSig != IntPtr.Zero) Marshal.FreeCoTaskMem(ptrRecordSig);
                if (ptrSubSig != IntPtr.Zero) Marshal.FreeCoTaskMem(ptrSubSig);
                if (ptrNewData != IntPtr.Zero) Marshal.FreeCoTaskMem(ptrNewData);
            }
        }

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
                    Record.Handle = RecordPtr;

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
                        IntPtr SubRecordPtr = C_GetSubRecordData_Ptr(RecordPtr, j);
                        if (SubRecordPtr == IntPtr.Zero)
                            continue;

                        var SubRecord = new SubRecordData();

                        SubRecord.Sig = GetSubRecordSigUtf8(SubRecordPtr);
                        SubRecord.Content = GetSubRecordStringUtf8(SubRecordPtr);
                        SubRecord.IsLocalized = C_SubRecordData_IsLocalized(SubRecordPtr);
                        SubRecord.StringID = C_SubRecordData_GetStringID(SubRecordPtr);

                        SubRecord.OccurrenceIndex = C_SubRecordData_GetOccurrenceIndex(SubRecordPtr);
                        SubRecord.GlobalIndex = C_SubRecordData_GetGlobalIndex(SubRecordPtr);

                        int DataSize = C_SubRecordData_GetDataSize(SubRecordPtr);
                        if (DataSize > 0)
                        {
                            SubRecord.Data = new byte[DataSize];
                            C_SubRecordData_GetData(SubRecordPtr, SubRecord.Data, DataSize);
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

        public static string MarshalUtf8String(IntPtr Ptr)
        {
            if (Ptr == IntPtr.Zero) return string.Empty;

            int Len = 0;
            while (Marshal.ReadByte(Ptr, Len) != 0) Len++;

            if (Len == 0) return string.Empty;

            byte[] buffer = new byte[Len];
            Marshal.Copy(Ptr, buffer, 0, Len);

            return Encoding.UTF8.GetString(buffer);
        }

        private static string GetSubRecordStringUtf8(IntPtr SubRecordPtr)
        {
            int Len = C_SubRecordData_GetStringUtf8(SubRecordPtr, null, 0);
            if (Len <= 0) return string.Empty;

            byte[] Buffer = new byte[Len + 1];
            C_SubRecordData_GetStringUtf8(SubRecordPtr, Buffer, Buffer.Length);

            return Encoding.UTF8.GetString(Buffer, 0, Len);
        }

        private static string GetSubRecordSigUtf8(IntPtr SubRecordPtr)
        {
            byte[] Buffer = new byte[8]; 
            int Len = C_SubRecordData_GetSigUtf8(SubRecordPtr, Buffer, Buffer.Length);
            if (Len <= 0) return string.Empty;

            return Encoding.UTF8.GetString(Buffer, 0, Len);
        }
    }

    public static class EspReader
    {
        public static StringsFileReader StringsReader = new StringsFileReader();
        public class RecordItem
        {
            public uint StringID = 0;      // StringsFile id
            public uint RealFormID = 0;
            public string FormID = "";
            public string ParentSig = "";
            public string ChildSig = "";
            public string UniqueKey = "";
            public string String = "";
            public int GlobalIndex = 0;
            public int OccurrenceIndex = 0;
            public bool IsModify = false;
        }

        public static Dictionary<string, RecordItem> Records = new Dictionary<string, RecordItem>();
        public static List<string> Types = new List<string>();

        public static void SelectSig(string Sig)
        {
            Records.Clear();

            foreach (var GetRecord in EspInterop.SearchBySig(Sig))
            {
                uint RealFormID = GetRecord.FormID;
                string ParentFormID = GetRecord.GetFormIDHex();
                string ParentSig = GetRecord.Sig;

                foreach (var Sub in GetRecord.SubRecords)
                {
                    var MergeSig = Engine.GetFileUniqueKey() + ":" + ParentFormID + ":" + ParentSig + ":" + Sub.Sig + ":" + Sub.GlobalIndex + ":" + Sub.OccurrenceIndex;
                    string UniqueKey = "[" + Crc32Helper.ComputeCrc32(MergeSig) + "]" + Sub.Sig;

                    RecordItem NRecordItem = new RecordItem
                    {
                        RealFormID = RealFormID,
                        StringID = Sub.StringID,
                        FormID = ParentFormID,
                        ParentSig = ParentSig,
                        ChildSig = Sub.Sig,
                        UniqueKey = UniqueKey,
                        String = Sub.Content,
                        GlobalIndex = Sub.GlobalIndex,
                        OccurrenceIndex = Sub.OccurrenceIndex
                    };

                    if (NRecordItem.String.Length > 0)
                    {
                        if (!Records.ContainsKey(NRecordItem.UniqueKey))
                        {
                            Records.Add(NRecordItem.UniqueKey, NRecordItem);
                        }
                        else
                        {
                            throw new Exception("Warning: Duplicate key detected: {NRecordItem.UniqueKey}");
                        }
                    }
                }
            }
        }

        //Fast Load Esp
        public static bool LoadEsp(string Path)
        {
            Records.Clear();
            Types.Clear();
            var State = EspInterop.LoadEsp(Path);

            if (State >= 0)
            {
                foreach (var GetRecord in EspInterop.SearchBySig("ALL"))
                {
                    string ParentFormID = GetRecord.GetFormIDHex();
                    string ParentSig = GetRecord.Sig;

                    if (!Types.Contains(ParentSig))
                    {
                        Types.Add(ParentSig);
                    }
                }

                return true;
            }

            return false;
        }

        public static void TestSaveEsp()
        {
            int ModifyCount = 0;
            for (int i = 0; i < Records.Count; i++)
            {
                var Record = Records[Records.ElementAt(i).Key];

                if (EspInterop.ModifySubRecord(Record.RealFormID, Record.ParentSig, Record.ChildSig, Record.OccurrenceIndex, Record.GlobalIndex,i.ToString()))
                {
                    ModifyCount++;
                }
            }

            EspInterop.SaveEsp(DeFine.GetFullPath(@"\Test.esp"));
        }

        public static void Close()
        {
            EspInterop.C_Clear();
        }
    }
}