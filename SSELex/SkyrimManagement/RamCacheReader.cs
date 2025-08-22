using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SSELex.SkyrimModManager;
using static PhoenixEngine.SSELexiconBridge.NativeBridge;

namespace SSELex.SkyrimManagement
{
    public class RamCacheReader
    {
        public List<FakeGrid>? RamLines = new List<FakeGrid>();
        public void Load(string FilePath)
        {
            if (File.Exists(FilePath))
            {
                string GetRamCache = Encoding.UTF8.GetString(DataHelper.ReadFile(FilePath));
                this.RamLines = JsonSerializer.Deserialize<List<FakeGrid>>(GetRamCache);

                if (RamLines != null)
                {
                    foreach (var Get in RamLines)
                    {
                        TranslatorBridge.SetTransCache(Get.Key, Get.TransText);
                    }
                }
            }
        }
        public void Close()
        {
            RamLines?.Clear();
        }

        public bool Save(string OutPutPath)
        {
            try
            {
                if (RamLines != null)
                {
                    for (int i = 0; i < RamLines.Count; i++)
                    {
                        bool IsCloud = false;
                        RamLines[i].SyncData(ref IsCloud);
                    }


                    var JsonOptions = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                    };

                    string GetJson = JsonSerializer.Serialize(RamLines, JsonOptions);

                    if (OutPutPath != null)
                    {
                        if (OutPutPath.Trim().Length > 0)
                        {
                            if (File.Exists(OutPutPath))
                            {
                                File.Delete(OutPutPath);
                            }
                            DataHelper.WriteFile(OutPutPath, Encoding.UTF8.GetBytes(GetJson));
                        }
                    }
                }

                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}
