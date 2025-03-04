using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Copyright (C) 2025 YD525
// Licensed under the GNU GPLv3
// See LICENSE for details
//https://github.com/YD525/YDSkyrimToolR/
namespace YDSkyrimToolR.TranslateManage
{
    public class Translator
    {
        public static Dictionary<int, string> TransData = new Dictionary<int, string>();

        public static void ClearCache()
        {
            TransData.Clear();
        }
    }
}
