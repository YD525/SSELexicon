using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SSELex.TranslateCore
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/

    public class CodeProcess
    {
        public List<CodeItem> CodeItems = new List<CodeItem>();

        public string ProcessCode(string Text)
        {
            int CodeCount = 0;

            if (Text.Contains("<") && Text.Contains(">"))
            {
                string RichText = "";
                string TempSignValue = "";
                bool IsCode = false;
                for (int i = 0; i < Text.Length; i++)
                {
                    string GetChar = Text.Substring(i,1);

                    if (GetChar != "<" && GetChar != ">")
                    {
                        if (!IsCode)
                        {
                            RichText += GetChar;
                        }
                        else
                        {
                            TempSignValue += GetChar;
                        }
                    }
                    else
                    {
                        if (GetChar == "<")
                        {
                            IsCode = true;
                        }
                        if (GetChar == ">")
                        {
                            CodeCount++;
                            string NewName = "90" + CodeCount.ToString() + "09";
                            this.CodeItems.Add(new CodeItem(NewName,TempSignValue));
                            RichText += "(" + NewName + ")";

                            TempSignValue = string.Empty;

                            IsCode = false;
                        }
                    }
                }

                if (TempSignValue.Length > 0)
                {
                    RichText += TempSignValue;
                }

                return RichText;
            }
            else
            {
                return Text;
            }
        }

        public string UsingCode(string Text)
        {
            string TempLine = Text;

            foreach (var Get in this.CodeItems)
            {
                TempLine = TempLine.Replace("(" + Get.HashID + ")", "<" + Get.CodeStr + ">");
                TempLine = TempLine.Replace("（" + Get.HashID + "）", "<" + Get.CodeStr + ">");
                TempLine = TempLine.Replace(Get.HashID, "<" + Get.CodeStr + ">");
            }

            return TempLine;
        }

        public string GetRemainingText(string Text)
        {
            string TempLine = Text;

            foreach (var Get in this.CodeItems)
            {
                TempLine = TempLine.Replace("(" + Get.HashID + ")", string.Empty);
                TempLine = TempLine.Replace("（" + Get.HashID + "）", string.Empty);
                TempLine = TempLine.Replace(Get.HashID, string.Empty);
            }

            return TempLine;
        }
    }

    public class CodeItem
    {
        public string HashID = "";
        public string CodeStr = "";
        public CodeItem(string HashID, string CodeStr)
        {
            this.HashID = HashID;
            this.CodeStr = CodeStr;
        }
    }
        
}
