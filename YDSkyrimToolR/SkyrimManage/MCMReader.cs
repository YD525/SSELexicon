using Mutagen.Bethesda.Fallout4;
using Mutagen.Bethesda.Oblivion;
using Mutagen.Bethesda.Skyrim;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YDSkyrimToolR.SkyrimModManager;
using YDSkyrimToolR.TranslateManage;
using YDSkyrimToolR.UIManage;

namespace YDSkyrimToolR.SkyrimManage
{
    /*
* @Author: YD525
* @GitHub: https://github.com/YD525/YDSkyrimToolR
* @Date: 2025-02-06
*/

    //string Type,string EditorID,string Key, string SourceText,string TransText
    public class MCMItem
    {
        public string Type = "";
        public string EditorID = "";
        public string Key = "";
        public string SourceText = "";
        public string TransText = "";

        public MCMItem(string EditorID,string SourceText)
        {
            this.Type = "MCM";
            this.EditorID = EditorID;
            if (this.EditorID.Contains("$"))
            {
                this.EditorID = EditorID.Substring(1);
            }
            this.Key = SkyrimDataLoader.GenUniqueKey(this.EditorID, this.Type);
            this.SourceText = SourceText;
            this.TransText = string.Empty;
        }

        public string GetTextIfTrans()
        {
            if (this.TransText.Trim().Length > 0)
            {
                return this.TransText;
            }
            int GetKey = SkyrimDataLoader.GenUniqueKey(this.EditorID, this.Type).GetHashCode();
            if (Translator.TransData.ContainsKey(GetKey))
            {
                this.TransText = Translator.TransData[GetKey];
                if (this.TransText.Length > 0)
                {
                    return this.TransText;
                }
                else
                {
                    return this.SourceText;
                }
            }

            return this.SourceText;
        }


        public string GetTextIfTransR()
        {
            int GetKey = SkyrimDataLoader.GenUniqueKey(this.EditorID, this.Type).GetHashCode();
            if (Translator.TransData.ContainsKey(GetKey))
            {
                this.TransText = Translator.TransData[GetKey];
                if (this.TransText.Length > 0)
                {
                    return this.TransText;
                }
                else
                {
                    return "";
                }
            }

            return "";
        }
    }
    public class MCMReader
    {
        public List<string> Lines = new List<string>();
        public List<MCMItem> MCMItems = new List<MCMItem>();
        public Encoding CurrentEncoding = null;
        public bool CheckIsMCM()
        {
            foreach (var Get in Lines)
            {
                if (Get.StartsWith("$") && (Get.Contains("\t") || Get.Contains(" ")))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public void Close()
        {
            Lines.Clear();
            MCMItems.Clear();
        }

        public void LoadMCM(string Path)
        {
            YDSkyrimToolR.TranslateManage.Translator.ClearCache();
            Lines.Clear();
            MCMItems.Clear();

            Encoding Encoder = DataHelper.GetFileEncodeType(Path);

            var GetData = DataHelper.GetBytesByFilePath(Path);
            var FileStr = Encoder.GetString(GetData);

            foreach (var GetLine in FileStr.Split(new char[2] { '\r', '\n' }))
            {
                if (GetLine.Trim().Length > 0)
                { 
                   this.Lines.Add(GetLine.Trim());
                }
            }

            if (CheckIsMCM())
            {
                ReadMCMConfig();
            }
        }
        //SystemDataWriter.PreFormatStr

        public void ReadMCMConfig()
        {
            for (int i = 0; i < Lines.Count; i++)
            {
                string GetLine = Lines[i];

                if (GetLine.StartsWith("$") && (GetLine.Contains("\t") || GetLine.Contains(" ")))
                {
                    string AutoSplictChar = "";

                    if (GetLine.Contains("\t"))
                    {
                        AutoSplictChar = "\t";
                    }
                    else
                    if (GetLine.Contains(" "))
                    {
                        AutoSplictChar = " ";
                    }

                    string GetEditorID = GetLine.Substring(0, GetLine.IndexOf(AutoSplictChar));
                    string GetSourceValue = GetLine.Substring(GetEditorID.Length);
                    GetSourceValue = GetSourceValue.Trim();

                    MCMItem NMCMItem = new MCMItem(GetEditorID,GetSourceValue);
                    this.MCMItems.Add(NMCMItem);
                }
            }
        }

        public void SaveMCMConfig(string OutPutPath)
        {
            if (File.Exists(OutPutPath))
            {
                return;
            }
            string RichText = "";
            foreach (var GetMCMItem in this.MCMItems)
            {
                RichText += string.Format("${0}\t{1}\r\n", GetMCMItem.EditorID, SystemDataWriter.PreFormatStr(GetMCMItem.GetTextIfTrans()));
            }
            DataHelper.WriteFile(OutPutPath,Encoding.UTF8.GetBytes(RichText));

            Close();
        }
    }
}
