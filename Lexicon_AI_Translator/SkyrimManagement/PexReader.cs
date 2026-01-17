using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace LexTranslator.SkyrimManagement
{
    public static class PexInterop
    {
        private const string DllName = "PexReader.dll";

        public static string Version = "";

        #region P/Invoke Declarations

        // Version info
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr C_GetVersion();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int C_GetVersionLength();

        // PEX operations
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int C_ReadPex([MarshalAs(UnmanagedType.LPWStr)] string pexPath);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int C_SavePex([MarshalAs(UnmanagedType.LPWStr)] string pexPath);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_Close();

        // Header info
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint C_GetHeaderMagic();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern IntPtr C_GetHeaderSourceFileName();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern IntPtr C_GetHeaderUsername();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern IntPtr C_GetHeaderMachineName();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte C_GetHeaderMajorVersion();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte C_GetHeaderMinorVersion();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort C_GetHeaderGameId();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong C_GetHeaderCompilationTime();

        // String table
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort C_GetStringTableCount();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int C_GetStringUtf8(ushort index, byte[] buffer, int bufferSize);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int C_GetStringWide(ushort index, char[] buffer, int bufferSize);

        // Debug info
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte C_HasDebugInfo();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong C_GetDebugModificationTime();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort C_GetDebugFunctionCount();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int C_GetDebugFunctionInfo(ushort index,
            out ushort objectNameIndex, out ushort stateNameIndex, out ushort functionNameIndex,
            out byte functionType, out IntPtr lineNumbers, out int lineCount);

        // User flags
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort C_GetUserFlagCount();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int C_GetUserFlagInfo(ushort index, out ushort flagNameIndex, out byte flagIndex);

        // Objects
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort C_GetObjectCount();

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int C_GetObjectInfo(ushort index, out ushort nameIndex, out uint size);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int C_GetObjectData(ushort objectIndex,
            out ushort parentClassName, out ushort docString, out uint userFlags, out ushort autoStateName);

        // Variables
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort C_GetVariableCount(ushort objectIndex);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int C_GetVariableInfo(ushort objectIndex, ushort varIndex,
            out ushort name, out ushort typeName, out uint userFlags, out byte dataType, IntPtr dataValue);

        // Properties
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort C_GetPropertyCount(ushort objectIndex);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int C_GetPropertyInfo(ushort objectIndex, ushort propIndex,
            out ushort name, out ushort type, out ushort docstring, out uint userFlags,
            out byte flags, out ushort autoVarName);

        // States
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort C_GetStateCount(ushort objectIndex);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int C_GetStateInfo(ushort objectIndex, ushort stateIndex,
            out ushort name, out ushort numFunctions);

        // Functions
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int C_GetStateFunctionInfo(ushort objectIndex, ushort stateIndex,
            ushort funcIndex, out ushort functionName, out ushort returnType, out ushort docString,
            out uint userFlags, out byte flags, out ushort numParams, out ushort numLocals,
            out ushort numInstructions);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort C_GetFunctionParamCount(ushort objectIndex, ushort stateIndex, ushort funcIndex);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int C_GetFunctionParamInfo(ushort objectIndex, ushort stateIndex,
            ushort funcIndex, ushort paramIndex, out ushort name, out ushort type);

        // Function locals
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort C_GetFunctionLocalCount(ushort objectIndex, ushort stateIndex, ushort funcIndex);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int C_GetFunctionLocalInfo(ushort objectIndex, ushort stateIndex,
            ushort funcIndex, ushort localIndex, out ushort name, out ushort type);

        // Instructions
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int C_GetInstructionInfo(ushort objectIndex, ushort stateIndex,
            ushort funcIndex, ushort instrIndex, out byte opcode, out ushort argCount);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int C_GetInstructionArgument(ushort objectIndex, ushort stateIndex,
            ushort funcIndex, ushort instrIndex, ushort argIndex, out byte type, IntPtr value);

        // Memory management
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_FreeBuffer(IntPtr buffer);

        #endregion

        public static string GetVersion()
        {
            try
            {
                int length = C_GetVersionLength();
                if (length <= 0)
                {
                    return "Unknown";
                }

                IntPtr ptr = C_GetVersion();
                if (ptr == IntPtr.Zero)
                {
                    return "Unknown";
                }

                return Marshal.PtrToStringAnsi(ptr, length);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting PEX version: " + ex.Message);
                return "Error";
            }
        }

        public static int LoadPex(string path)
        {
            if (System.IO.File.Exists(path))
            {
                return C_ReadPex(path);
            }
            return -1;
        }

        public static int SavePex(string path)
        {
            return C_SavePex(path);
        }

        public static void Close()
        {
            C_Close();
        }

        // Helper methods for string retrieval
        public static string GetHeaderSourceFileName()
        {
            IntPtr ptr = C_GetHeaderSourceFileName();
            if (ptr != IntPtr.Zero)
            {
                string result = Marshal.PtrToStringUni(ptr);
                if (result != null) return result;
            }
            return "";
        }

        public static string GetHeaderUsername()
        {
            IntPtr ptr = C_GetHeaderUsername();
            if (ptr != IntPtr.Zero)
            {
                string result = Marshal.PtrToStringUni(ptr);
                if (result != null) return result;
            }
            return "";
        }

        public static string GetHeaderMachineName()
        {
            IntPtr ptr = C_GetHeaderMachineName();
            if (ptr != IntPtr.Zero)
            {
                string result = Marshal.PtrToStringUni(ptr);
                if (result != null) return result;
            }
            return "";
        }

        public static uint GetHeaderMagic()
        {
            return C_GetHeaderMagic();
        }

        public static string GetStringUtf8(ushort index)
        {
            try
            {
                int length = C_GetStringUtf8(index, null, 0);
                if (length <= 0) return "";

                byte[] buffer = new byte[length + 1];
                int actualLength = C_GetStringUtf8(index, buffer, buffer.Length);

                // Find null terminator
                int nullIndex = Array.IndexOf(buffer, (byte)0);
                if (nullIndex >= 0)
                    actualLength = nullIndex;

                return Encoding.UTF8.GetString(buffer, 0, actualLength);
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string GetStringWide(ushort index)
        {
            try
            {
                int length = C_GetStringWide(index, null, 0);
                if (length <= 0) return "";

                char[] buffer = new char[length + 1];
                int actualLength = C_GetStringWide(index, buffer, buffer.Length);

                // Find null terminator
                int nullIndex = Array.IndexOf(buffer, '\0');
                if (nullIndex >= 0)
                    actualLength = nullIndex;

                return new string(buffer, 0, actualLength);
            }
            catch (Exception)
            {
                return "";
            }
        }


        // Helper to get debug function line numbers
        public static ushort[] GetDebugFunctionLineNumbers(IntPtr lineNumbersPtr, int lineCount)
        {
            if (lineNumbersPtr == IntPtr.Zero || lineCount <= 0)
                return new ushort[0];

            try
            {
                ushort[] lineNumbers = new ushort[lineCount];

                byte[] byteArray = new byte[lineCount * 2];
                Marshal.Copy(lineNumbersPtr, byteArray, 0, byteArray.Length);

                for (int i = 0; i < lineCount; i++)
                {
                    lineNumbers[i] = BitConverter.ToUInt16(byteArray, i * 2);
                }

                return lineNumbers;
            }
            catch (Exception)
            {
                return new ushort[0];
            }
        }

        static PexInterop()
        {
            try
            {
                Version = GetVersion();
            }
            catch (Exception ex)
            {
                Version = "Error: " + ex.Message;
            }
        }
    }

    public class PexReader
    {
        public string PexPath { get; private set; }
        public PexHeader Header { get; private set; }
        public List<PexString> StringTable { get; private set; }
        public List<PexObject> Objects { get; private set; }
        public List<PexUserFlag> UserFlags { get; private set; }
        public PexDebugInfo DebugInfo { get; private set; }

        public class PexString
        {
            public ushort Index { get; set; }
            public string Value { get; set; }
            public string ValueWide { get; set; }

            public PexString()
            {
                Value = "";
                ValueWide = "";
            }
        }

        public class PexUserFlag
        {
            public ushort FlagNameIndex { get; set; }
            public byte FlagIndex { get; set; }
            public string GetFlagName(PexReader reader)
            {
                if (reader != null)
                    return reader.GetString(FlagNameIndex);
                return "";
            }
        }

        public class PexDebugFunction
        {
            public ushort ObjectNameIndex { get; set; }
            public ushort StateNameIndex { get; set; }
            public ushort FunctionNameIndex { get; set; }
            public byte FunctionType { get; set; }
            public ushort InstructionCount { get; set; }
            public ushort[] LineNumbers { get; set; }
            public string GetObjectName(PexReader reader)
            {
                if (reader != null)
                    return reader.GetString(ObjectNameIndex);
                return "";
            }

            public string GetStateName(PexReader reader)
            {
                if (reader != null)
                    return reader.GetString(StateNameIndex);
                return "";
            }

            public string GetFunctionName(PexReader reader)
            {
                if (reader != null)
                    return reader.GetString(FunctionNameIndex);
                return "";
            }

            public PexDebugFunction()
            {
                LineNumbers = new ushort[0];
            }
        }

        public class PexDebugInfo
        {
            public bool HasDebugInfo { get; set; }
            public ulong ModificationTime { get; set; }
            public ushort FunctionCount { get; set; }
            public List<PexDebugFunction> Functions { get; set; }

            public PexDebugInfo()
            {
                Functions = new List<PexDebugFunction>();
            }
        }

        public class PexHeader
        {
            public uint Magic { get; set; }
            public byte MajorVersion { get; set; }
            public byte MinorVersion { get; set; }
            public ushort GameId { get; set; }
            public ulong CompilationTime { get; set; }
            public string SourceFileName { get; set; }
            public string Username { get; set; }
            public string MachineName { get; set; }

            public PexHeader()
            {
                SourceFileName = "";
                Username = "";
                MachineName = "";
            }
        }

        public class PexVariable
        {
            public ushort NameIndex { get; set; }
            public ushort TypeNameIndex { get; set; }
            public uint UserFlags { get; set; }
            public byte DataType { get; set; }
            public object DataValue { get; set; }
            public string GetName(PexReader reader)
            {
                if (reader != null)
                    return reader.GetString(NameIndex);
                return "";
            }

            public string GetTypeName(PexReader reader)
            {
                if (reader != null)
                    return reader.GetString(TypeNameIndex);
                return "";
            }

            public PexVariable()
            {
                DataValue = "";
            }
        }

        public class PexProperty
        {
            public ushort NameIndex { get; set; }
            public ushort TypeIndex { get; set; }
            public ushort DocstringIndex { get; set; }
            public uint UserFlags { get; set; }
            public byte Flags { get; set; }
            public ushort AutoVarNameIndex { get; set; }
            public string GetName(PexReader reader)
            {
                if (reader != null)
                    return reader.GetString(NameIndex);
                return "";
            }

            public string GetType(PexReader reader)
            {
                if (reader != null)
                    return reader.GetString(TypeIndex);
                return "";
            }

            public string GetDocstring(PexReader reader)
            {
                if (reader != null)
                    return reader.GetString(DocstringIndex);
                return "";
            }

            public string GetAutoVarName(PexReader reader)
            {
                if (reader != null)
                    return reader.GetString(AutoVarNameIndex);
                return "";
            }
        }

        public class PexState
        {
            public ushort NameIndex { get; set; }
            public ushort NumFunctions { get; set; }
            public List<PexFunction> Functions { get; set; }

            public string GetName(PexReader reader)
            {
                if (reader != null)
                    return reader.GetString(NameIndex);
                return "";
            }

            public PexState()
            {
                Functions = new List<PexFunction>();
            }
        }

        public class PexFunctionParam
        {
            public ushort NameIndex { get; set; }
            public ushort TypeIndex { get; set; }

            public string GetName(PexReader reader)
            {
                if (reader != null)
                    return reader.GetString(NameIndex);
                return "";
            }

            public string GetTypeName(PexReader reader)
            {
                if (reader != null)
                    return reader.GetString(TypeIndex);
                return "";
            }
        }
        public class PexFunctionLocal
        {
            public ushort NameIndex { get; set; }
            public ushort TypeIndex { get; set; }

            public string GetName(PexReader reader)
            {
                if (reader != null)
                    return reader.GetString(NameIndex);
                return "";
            }

            public string GetTypeName(PexReader reader)
            {
                if (reader != null)
                    return reader.GetString(TypeIndex);
                return "";
            }
        }

        public class PexInstructionArgument
        {
            public byte Type { get; set; }
            public object Value { get; set; }

            public string GetValueAsString(PexReader reader)
            {
                if (Value == null)
                    return "null";

                switch (Type)
                {
                    case 0: // null
                        return "null";
                    case 1: // identifier
                    case 2: // string
                        if (Value is ushort stringIndex)
                            return reader?.GetString(stringIndex) ?? stringIndex.ToString();
                        return Value.ToString() ?? "";
                    case 3: // integer
                        return Value.ToString() ?? "0";
                    case 4: // float
                        return Value is float f ? f.ToString("F6") : "0.000000";
                    case 5: // bool
                        return (Value is bool b && b) ? "true" : "false";
                    default:
                        return Value.ToString() ?? "";
                }
            }
        }
        public class PexInstruction
        {
            public byte Opcode { get; set; }
            public List<PexInstructionArgument> Arguments { get; set; }

            public PexInstruction()
            {
                Arguments = new List<PexInstructionArgument>();
            }

            public string GetOpcodeName()
            {
                switch (Opcode)
                {
                    case 0x00: return "nop";
                    case 0x01: return "iadd";
                    case 0x02: return "fadd";
                    case 0x03: return "isub";
                    case 0x04: return "fsub";
                    case 0x05: return "imul";
                    case 0x06: return "fmul";
                    case 0x07: return "idiv";
                    case 0x08: return "fdiv";
                    case 0x09: return "imod";
                    case 0x0A: return "not";
                    case 0x0B: return "ineg";
                    case 0x0C: return "fneg";
                    case 0x0D: return "assign";
                    case 0x0E: return "cast";
                    case 0x0F: return "cmp_eq";
                    case 0x10: return "cmp_lt";
                    case 0x11: return "cmp_le";
                    case 0x12: return "cmp_gt";
                    case 0x13: return "cmp_ge";
                    case 0x14: return "jmp";
                    case 0x15: return "jmpt";
                    case 0x16: return "jmpf";
                    case 0x17: return "callmethod";
                    case 0x18: return "callparent";
                    case 0x19: return "callstatic";
                    case 0x1A: return "return";
                    case 0x1B: return "strcat";
                    case 0x1C: return "propget";
                    case 0x1D: return "propset";
                    case 0x1E: return "array_create";
                    case 0x1F: return "array_length";
                    case 0x20: return "array_getelement";
                    case 0x21: return "array_setelement";
                    case 0x22: return "array_findelement";
                    case 0x23: return "array_rfindelement";
                    default: return $"0x{Opcode:X2}";
                }
            }
        }

        public class PexFunction
        {
            public ushort FunctionNameIndex { get; set; }
            public ushort ReturnTypeIndex { get; set; }
            public ushort DocStringIndex { get; set; }
            public uint UserFlags { get; set; }
            public byte Flags { get; set; }
            public ushort NumParams { get; set; }
            public ushort NumLocals { get; set; }
            public ushort NumInstructions { get; set; }

            public List<PexFunctionParam> Parameters { get; set; }
            public List<PexFunctionLocal> Locals { get; set; }
            public List<PexInstruction> Instructions { get; set; }

            public PexFunction()
            {
                Parameters = new List<PexFunctionParam>();
                Locals = new List<PexFunctionLocal>();
                Instructions = new List<PexInstruction>();
            }

            public string GetFunctionName(PexReader reader)
            {
                if (reader != null)
                    return reader.GetString(FunctionNameIndex);
                return "";
            }

            public string GetReturnType(PexReader reader)
            {
                if (reader != null)
                    return reader.GetString(ReturnTypeIndex);
                return "";
            }

            public string GetDocString(PexReader reader)
            {
                if (reader != null)
                    return reader.GetString(DocStringIndex);
                return "";
            }
        }

        public class PexObject
        {
            public ushort NameIndex { get; set; }
            public uint Size { get; set; }
            public ushort ParentClassNameIndex { get; set; }
            public ushort DocStringIndex { get; set; }
            public uint UserFlags { get; set; }
            public ushort AutoStateNameIndex { get; set; }
            public List<PexVariable> Variables { get; set; }
            public List<PexProperty> Properties { get; set; }
            public List<PexState> States { get; set; }

            // 改为方法
            public string GetName(PexReader reader)
            {
                if (reader != null)
                    return reader.GetString(NameIndex);
                return "";
            }

            public string GetParentClassName(PexReader reader)
            {
                if (reader != null)
                    return reader.GetString(ParentClassNameIndex);
                return "";
            }

            public string GetDocString(PexReader reader)
            {
                if (reader != null)
                    return reader.GetString(DocStringIndex);
                return "";
            }

            public string GetAutoStateName(PexReader reader)
            {
                if (reader != null)
                    return reader.GetString(AutoStateNameIndex);
                return "";
            }

            public PexObject()
            {
                Variables = new List<PexVariable>();
                Properties = new List<PexProperty>();
                States = new List<PexState>();
            }
        }

        public PexReader()
        {
            PexPath = "";
            Header = new PexHeader();
            StringTable = new List<PexString>();
            Objects = new List<PexObject>();
            UserFlags = new List<PexUserFlag>();
            DebugInfo = new PexDebugInfo();
        }

        public void LoadPex(string path)
        {
            Clear();

            int state = PexInterop.LoadPex(path);
            if (state > 0)
            {
                PexPath = path;
                LoadHeader();
                LoadStringTable();
                LoadDebugInfo();
                LoadUserFlags();
                LoadObjects();
            }
            else
            {
                throw new Exception("Failed to load PEX file: " + path);
            }
        }

        private void LoadHeader()
        {
            Header = new PexHeader
            {
                Magic = PexInterop.GetHeaderMagic(),
                MajorVersion = PexInterop.C_GetHeaderMajorVersion(),
                MinorVersion = PexInterop.C_GetHeaderMinorVersion(),
                GameId = PexInterop.C_GetHeaderGameId(),
                CompilationTime = PexInterop.C_GetHeaderCompilationTime(),
                SourceFileName = PexInterop.GetHeaderSourceFileName(),
                Username = PexInterop.GetHeaderUsername(),
                MachineName = PexInterop.GetHeaderMachineName()
            };
        }

        private void LoadStringTable()
        {
            StringTable.Clear();
            ushort count = PexInterop.C_GetStringTableCount();

            for (ushort i = 0; i < count; i++)
            {
                StringTable.Add(new PexString
                {
                    Index = i,
                    Value = PexInterop.GetStringUtf8(i),
                    ValueWide = PexInterop.GetStringWide(i)
                });
            }
        }

        private void LoadDebugInfo()
        {
            DebugInfo.HasDebugInfo = PexInterop.C_HasDebugInfo() != 0;

            if (DebugInfo.HasDebugInfo)
            {
                DebugInfo.ModificationTime = PexInterop.C_GetDebugModificationTime();
                DebugInfo.FunctionCount = PexInterop.C_GetDebugFunctionCount();

                for (ushort i = 0; i < DebugInfo.FunctionCount; i++)
                {
                    ushort objectNameIndex;
                    ushort stateNameIndex;
                    ushort functionNameIndex;
                    byte functionType;
                    IntPtr lineNumbersPtr;
                    int lineCount;

                    if (PexInterop.C_GetDebugFunctionInfo(i,
                        out objectNameIndex, out stateNameIndex,
                        out functionNameIndex, out functionType,
                        out lineNumbersPtr, out lineCount) > 0)
                    {
                        var debugFunc = new PexDebugFunction
                        {
                            ObjectNameIndex = objectNameIndex,
                            StateNameIndex = stateNameIndex,
                            FunctionNameIndex = functionNameIndex,
                            FunctionType = functionType,
                            InstructionCount = (ushort)lineCount,
                            LineNumbers = PexInterop.GetDebugFunctionLineNumbers(lineNumbersPtr, lineCount)
                        };

                        DebugInfo.Functions.Add(debugFunc);

                        // Free the allocated line numbers buffer
                        if (lineNumbersPtr != IntPtr.Zero)
                        {
                            PexInterop.C_FreeBuffer(lineNumbersPtr);
                        }
                    }
                }
            }
        }

        private void LoadUserFlags()
        {
            UserFlags.Clear();
            ushort count = PexInterop.C_GetUserFlagCount();

            for (ushort i = 0; i < count; i++)
            {
                ushort flagNameIndex;
                byte flagIndex;
                if (PexInterop.C_GetUserFlagInfo(i, out flagNameIndex, out flagIndex) > 0)
                {
                    UserFlags.Add(new PexUserFlag
                    {
                        FlagNameIndex = flagNameIndex,
                        FlagIndex = flagIndex
                    });
                }
            }
        }

        private void LoadObjects()
        {
            Objects.Clear();
            ushort count = PexInterop.C_GetObjectCount();

            for (ushort i = 0; i < count; i++)
            {
                ushort nameIndex;
                uint size;
                ushort parentClassName;
                ushort docString;
                uint userFlags;
                ushort autoStateName;

                if (PexInterop.C_GetObjectInfo(i, out nameIndex, out size) > 0 &&
                    PexInterop.C_GetObjectData(i, out parentClassName, out docString,
                        out userFlags, out autoStateName) > 0)
                {
                    var obj = new PexObject
                    {
                        NameIndex = nameIndex,
                        Size = size,
                        ParentClassNameIndex = parentClassName,
                        DocStringIndex = docString,
                        UserFlags = userFlags,
                        AutoStateNameIndex = autoStateName
                    };

                    LoadObjectVariables(obj, i);
                    LoadObjectProperties(obj, i);
                    LoadObjectStates(obj, i);

                    Objects.Add(obj);
                }
            }
        }

        private void LoadObjectVariables(PexObject obj, ushort objectIndex)
        {
            ushort varCount = PexInterop.C_GetVariableCount(objectIndex);

            for (ushort j = 0; j < varCount; j++)
            {
                ushort name;
                ushort typeName;
                uint userFlags;
                byte dataType;

                if (PexInterop.C_GetVariableInfo(objectIndex, j,
                    out name, out typeName, out userFlags, out dataType, IntPtr.Zero) > 0)
                {
                    obj.Variables.Add(new PexVariable
                    {
                        NameIndex = name,
                        TypeNameIndex = typeName,
                        UserFlags = userFlags,
                        DataType = dataType,
                        DataValue = GetVariableDataValue(dataType, objectIndex, j)
                    });
                }
            }
        }

        private object GetVariableDataValue(byte dataType, ushort objectIndex, ushort varIndex)
        {
            try
            {
                switch (dataType)
                {
                    case 0: // null
                        return null;
                    case 1: // identifier
                    case 2: // string
                        IntPtr stringValue = Marshal.AllocHGlobal(sizeof(ushort));
                        try
                        {
                            ushort dummyName;
                            ushort dummyTypeName;
                            uint dummyUserFlags;
                            byte dummyDataType;

                            if (PexInterop.C_GetVariableInfo(objectIndex, varIndex,
                                out dummyName, out dummyTypeName, out dummyUserFlags, out dummyDataType, stringValue) > 0)
                            {
                                ushort stringIndex = (ushort)Marshal.ReadInt16(stringValue);
                                return GetString(stringIndex);
                            }
                        }
                        finally
                        {
                            Marshal.FreeHGlobal(stringValue);
                        }
                        return "";
                    case 3: // integer
                        IntPtr intValue = Marshal.AllocHGlobal(sizeof(int));
                        try
                        {
                            ushort dummyName;
                            ushort dummyTypeName;
                            uint dummyUserFlags;
                            byte dummyDataType;

                            if (PexInterop.C_GetVariableInfo(objectIndex, varIndex,
                                out dummyName, out dummyTypeName, out dummyUserFlags, out dummyDataType, intValue) > 0)
                            {
                                return Marshal.ReadInt32(intValue);
                            }
                        }
                        finally
                        {
                            Marshal.FreeHGlobal(intValue);
                        }
                        return 0;
                    case 4: // float
                        IntPtr floatValue = Marshal.AllocHGlobal(sizeof(float));
                        try
                        {
                            ushort dummyName;
                            ushort dummyTypeName;
                            uint dummyUserFlags;
                            byte dummyDataType;

                            if (PexInterop.C_GetVariableInfo(objectIndex, varIndex,
                                out dummyName, out dummyTypeName, out dummyUserFlags, out dummyDataType, floatValue) > 0)
                            {
                                byte[] bytes = new byte[sizeof(float)];
                                Marshal.Copy(floatValue, bytes, 0, sizeof(float));
                                return BitConverter.ToSingle(bytes, 0);
                            }
                        }
                        finally
                        {
                            Marshal.FreeHGlobal(floatValue);
                        }
                        return 0.0f;
                    case 5: // bool
                        IntPtr boolValue = Marshal.AllocHGlobal(sizeof(byte));
                        try
                        {
                            ushort dummyName;
                            ushort dummyTypeName;
                            uint dummyUserFlags;
                            byte dummyDataType;

                            if (PexInterop.C_GetVariableInfo(objectIndex, varIndex,
                                out dummyName, out dummyTypeName, out dummyUserFlags, out dummyDataType, boolValue) > 0)
                            {
                                return Marshal.ReadByte(boolValue) != 0;
                            }
                        }
                        finally
                        {
                            Marshal.FreeHGlobal(boolValue);
                        }
                        return false;
                    default:
                        return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void LoadObjectProperties(PexObject obj, ushort objectIndex)
        {
            ushort propCount = PexInterop.C_GetPropertyCount(objectIndex);

            for (ushort j = 0; j < propCount; j++)
            {
                ushort name;
                ushort type;
                ushort docstring;
                uint userFlags;
                byte flags;
                ushort autoVarName;

                if (PexInterop.C_GetPropertyInfo(objectIndex, j,
                    out name, out type, out docstring,
                    out userFlags, out flags, out autoVarName) > 0)
                {
                    obj.Properties.Add(new PexProperty
                    {
                        NameIndex = name,
                        TypeIndex = type,
                        DocstringIndex = docstring,
                        UserFlags = userFlags,
                        Flags = flags,
                        AutoVarNameIndex = autoVarName
                    });
                }
            }
        }

        private void LoadObjectStates(PexObject obj, ushort objectIndex)
        {
            ushort stateCount = PexInterop.C_GetStateCount(objectIndex);

            for (ushort j = 0; j < stateCount; j++)
            {
                ushort name;
                ushort numFunctions;

                if (PexInterop.C_GetStateInfo(objectIndex, j,
                    out name, out numFunctions) > 0)
                {
                    var state = new PexState
                    {
                        NameIndex = name,
                        NumFunctions = numFunctions
                    };

                    LoadStateFunctions(state, objectIndex, j);

                    obj.States.Add(state);
                }
            }
        }

        private void LoadFunctionParameters(PexFunction func, ushort objectIndex, ushort stateIndex, ushort funcIndex)
        {
            ushort paramCount = PexInterop.C_GetFunctionParamCount(objectIndex, stateIndex, funcIndex);

            for (ushort i = 0; i < paramCount; i++)
            {
                ushort nameIndex;
                ushort typeIndex;

                if (PexInterop.C_GetFunctionParamInfo(objectIndex, stateIndex, funcIndex, i,
                    out nameIndex, out typeIndex) > 0)
                {
                    func.Parameters.Add(new PexFunctionParam
                    {
                        NameIndex = nameIndex,
                        TypeIndex = typeIndex
                    });
                }
            }
        }

        private void LoadFunctionLocals(PexFunction func, ushort objectIndex, ushort stateIndex, ushort funcIndex)
        {
            ushort localCount = PexInterop.C_GetFunctionLocalCount(objectIndex, stateIndex, funcIndex);

            for (ushort i = 0; i < localCount; i++)
            {
                ushort nameIndex;
                ushort typeIndex;

                if (PexInterop.C_GetFunctionLocalInfo(objectIndex, stateIndex, funcIndex, i,
                    out nameIndex, out typeIndex) > 0)
                {
                    func.Locals.Add(new PexFunctionLocal
                    {
                        NameIndex = nameIndex,
                        TypeIndex = typeIndex
                    });
                }
            }
        }

        private void LoadFunctionInstructions(PexFunction func, ushort objectIndex, ushort stateIndex, ushort funcIndex)
        {
            for (ushort i = 0; i < func.NumInstructions; i++)
            {
                byte opcode;
                ushort argCount;

                if (PexInterop.C_GetInstructionInfo(objectIndex, stateIndex, funcIndex, i,
                    out opcode, out argCount) > 0)
                {
                    var instruction = new PexInstruction
                    {
                        Opcode = opcode
                    };

                    for (ushort argIndex = 0; argIndex < argCount; argIndex++)
                    {
                        byte argType;
                        IntPtr argValuePtr = Marshal.AllocHGlobal(8); 

                        try
                        {
                            if (PexInterop.C_GetInstructionArgument(objectIndex, stateIndex, funcIndex, i, argIndex,
                                out argType, argValuePtr) > 0)
                            {
                                var argument = new PexInstructionArgument
                                {
                                    Type = argType
                                };

                                switch (argType)
                                {
                                    case 0: // null
                                        argument.Value = null;
                                        break;
                                    case 1: // identifier
                                    case 2: // string
                                        argument.Value = (ushort)Marshal.ReadInt16(argValuePtr);
                                        break;
                                    case 3: // integer
                                        argument.Value = Marshal.ReadInt32(argValuePtr);
                                        break;
                                    case 4: // float
                                        byte[] floatBytes = new byte[4];
                                        Marshal.Copy(argValuePtr, floatBytes, 0, 4);
                                        argument.Value = BitConverter.ToSingle(floatBytes, 0);
                                        break;
                                    case 5: // bool
                                        argument.Value = Marshal.ReadByte(argValuePtr) != 0;
                                        break;
                                    default:
                                        argument.Value = null;
                                        break;
                                }

                                instruction.Arguments.Add(argument);
                            }
                        }
                        finally
                        {
                            Marshal.FreeHGlobal(argValuePtr);
                        }
                    }

                    func.Instructions.Add(instruction);
                }
            }
        }

        private void LoadFunctionDetails(PexFunction func, ushort objectIndex, ushort stateIndex, ushort funcIndex)
        {
            LoadFunctionParameters(func, objectIndex, stateIndex, funcIndex);

            LoadFunctionLocals(func, objectIndex, stateIndex, funcIndex);

            LoadFunctionInstructions(func, objectIndex, stateIndex, funcIndex);
        }

        private void LoadStateFunctions(PexState state, ushort objectIndex, ushort stateIndex)
        {
            for (ushort k = 0; k < state.NumFunctions; k++)
            {
                ushort functionName;
                ushort returnType;
                ushort docString;
                uint userFlags;
                byte flags;
                ushort numParams;
                ushort numLocals;
                ushort numInstructions;

                if (PexInterop.C_GetStateFunctionInfo(objectIndex, stateIndex, k,
                    out functionName, out returnType, out docString,
                    out userFlags, out flags, out numParams,
                    out numLocals, out numInstructions) > 0)
                {
                    var func = new PexFunction
                    {
                        FunctionNameIndex = functionName,
                        ReturnTypeIndex = returnType,
                        DocStringIndex = docString,
                        UserFlags = userFlags,
                        Flags = flags,
                        NumParams = numParams,
                        NumLocals = numLocals,
                        NumInstructions = numInstructions
                    };

                    LoadFunctionDetails(func, objectIndex, stateIndex, k);

                    state.Functions.Add(func);
                }
            }
        }

        public string GetString(ushort index)
        {
            if (index < StringTable.Count)
            {
                return StringTable[(int)index].Value;
            }
            return "";
        }

        public string GetStringWide(ushort index)
        {
            if (index < StringTable.Count)
            {
                return StringTable[(int)index].ValueWide;
            }
            return "";
        }

        public string GetVariableValueAsString(PexVariable variable)
        {
            if (variable.DataValue == null)
                return "null";

            switch (variable.DataType)
            {
                case 0:
                    return "null";
                case 1:
                case 2:
                    return variable.DataValue.ToString() ?? "";
                case 3:
                    return variable.DataValue.ToString() ?? "0";
                case 4:
                    try
                    {
                        return ((float)variable.DataValue).ToString("F6");
                    }
                    catch
                    {
                        return "0.000000";
                    }
                case 5:
                    try
                    {
                        return ((bool)variable.DataValue) ? "true" : "false";
                    }
                    catch
                    {
                        return "false";
                    }
                default:
                    return variable.DataValue.ToString() ?? "";
            }
        }

        public int SavePex(string outputPath)
        {
            return PexInterop.SavePex(outputPath);
        }

        public void Clear()
        {
            PexPath = "";
            Header = new PexHeader();
            StringTable.Clear();
            Objects.Clear();
            UserFlags.Clear();
            DebugInfo = new PexDebugInfo();
        }

        public void Close()
        {
            Clear();
            PexInterop.Close();
        }

        // Method to find object by name
        public PexObject FindObjectByName(string name)
        {
            foreach (var obj in Objects)
            {
                if (string.Equals(obj.GetName(this), name, StringComparison.OrdinalIgnoreCase))
                    return obj;
            }
            return null;
        }

     
    }
}