using System.IO;

namespace LexTranslator.SkyrimModManager
{
    public class SkyrimHelper
    {
        /// <summary>
        /// Check if the specified directory contains a PapyrusCompiler
        /// </summary>
        /// <param name="CompilerPathPtr"></param>
        /// <returns></returns>
        public static bool FindPapyrusCompilerPath(ref string CompilerPathPtr)
        {
            if (File.Exists(DeFine.GetFullPath(@"\Tool\Original Compiler\PapyrusAssembler.exe")))
            {
                CompilerPathPtr = DeFine.GetFullPath(@"\Tool\Original Compiler\PapyrusAssembler.exe");
                return true;
            }
            if (Directory.Exists(DeFine.GlobalLocalSetting.SkyrimPath))
            {
                if (!DeFine.GlobalLocalSetting.SkyrimPath.EndsWith(@"\"))
                {
                    DeFine.GlobalLocalSetting.SkyrimPath += @"\";
                }
                string SetPapyrusAssemblerPath = DeFine.GlobalLocalSetting.SkyrimPath + "Papyrus Compiler" + @"\PapyrusAssembler.exe";
                if (File.Exists(SetPapyrusAssemblerPath))
                {
                    CompilerPathPtr = SetPapyrusAssemblerPath;
                    return true;
                }
            }
            return false;
        }
    }
}
