using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SSELex.SkyrimManagement
{
    public class EspReader
    {
        private const string DllName = "EspReader.dll";

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_Init();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_InitDefaultFilter();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int C_SetDefaultFilter();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int C_SetFilter(
            [MarshalAs(UnmanagedType.LPStr)] string parentSig,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] childSigs,
            int childCount);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_ClearFilter();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int C_ReadEsp([MarshalAs(UnmanagedType.LPWStr)] string espPath);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr C_SearchBySig(
            [MarshalAs(UnmanagedType.LPStr)] string parentSig,
            [MarshalAs(UnmanagedType.LPStr)] string childSig,
            out int outCount);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void FreeSearchResults(IntPtr arr, int count);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_Close();

    }
}
