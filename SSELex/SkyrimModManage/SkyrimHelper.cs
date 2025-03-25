using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSELex.SkyrimModManager
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/

    public class SkyrimHelper
    {
        public static bool FindPapyrusCompilerPath(ref string CompilerPathPtr)
        {
            if (File.Exists(DeFine.GetFullPath(@"Tool\Original Compiler\PapyrusAssembler.exe")))
            {
                CompilerPathPtr = DeFine.GetFullPath(@"Tool\Original Compiler\PapyrusAssembler.exe");
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
