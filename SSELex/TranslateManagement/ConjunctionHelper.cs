using System.Data;
using SSELex.ConvertManager;

namespace SSELex.TranslateCore
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/

    public class ConjunctionHelper
    {
        public static List<ConjunctItem> ConjunctionItems = new List<ConjunctItem>();

        public static void Init()
        {
            ConjunctionItems.Clear();

            string SqlOrder = "Select *,Rowid From Conjunction Where 1=1";

            DataTable NTable = DeFine.GlobalDB.ExecuteQuery(string.Format(SqlOrder));

            for (int i = 0; i < NTable.Rows.Count; i++)
            {
                ConjunctionItems.Add(new ConjunctItem(NTable.Rows[i]["StrExist"], NTable.Rows[i]["FrontExist"], NTable.Rows[i]["BehindExist"], NTable.Rows[i]["Text"], NTable.Rows[i]["Result"], NTable.Rows[i]["IgnoreCase"], NTable.Rows[i]["DefaultResult"], NTable.Rows[i]["TIndex"]));
            }

            ConjunctionItems.Sort((x, y) => x.CompareTo(y));
        }

        public int GetKeyCount(string Str, string Key, int IgnoreCase)
        {
            int FindsCount = 0;

            string TempLine = Str;

            while (true)
            {
                string GetKey = Key.ToLower();

                if (TempLine.Contains(GetKey))
                {
                    int Index = TempLine.IndexOf(GetKey);

                    FindsCount++;

                    TempLine = TempLine.Substring(Index + GetKey.Length);
                }
                else
                if (IgnoreCase > 0)
                {
                    GetKey = GetKey.Substring(0, 1).ToUpper() + GetKey.Substring(1).ToLower();

                    if (TempLine.Contains(GetKey))
                    {
                        int Index = TempLine.IndexOf(GetKey);

                        FindsCount++;

                        TempLine = TempLine.Substring(Index + GetKey.Length);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            return FindsCount;
        }

        public List<ConjunctResult> ConjunctResults = new List<ConjunctResult>();
        public int CheckKey(ref int ReturnOffset,string Str,string Key, int IgnoreCase)
        {
            bool FindStr = false;

            string GetKey = Key.ToLower();

            if (Str.Contains(GetKey))
            {
                FindStr = true;
            }
            else
            if (IgnoreCase > 0)
            {
                GetKey = GetKey.Substring(0, 1).ToUpper() + GetKey.Substring(1).ToLower();

                if (Str.Contains(GetKey))
                {
                    FindStr = true;
                }
            }

            if (FindStr)
            {
                int Offset = Str.IndexOf(GetKey);
                ReturnOffset = Offset;
                return Offset;
            }
            else
            {
                ReturnOffset = -1;
                return -1;
            }
        }

        public string ProcessStr(ref List<EngineProcessItem> EngineMsgs,string Str)
        {
            int CodeCount = 0;

            string TempContent = Str;

            foreach (var Get in ConjunctionHelper.ConjunctionItems)
            {
                int ReturnOffset = 0;

                int KeyCount = GetKeyCount(Str, Get.Text, Get.IgnoreCase);

                while (CheckKey(ref ReturnOffset, TempContent, Get.Text, Get.IgnoreCase) >= 0)
                {
                    bool CanUsing = true;

                    if (Get.StrExist.Trim().Length > 0)
                    {
                        if (!UsingStr(TempContent).Trim().ToLower().Contains(Get.StrExist.Trim().ToLower()))
                        {
                            CanUsing = false;
                        }
                    }

                    if (CanUsing)
                    { 
                        string LeftStr = (TempContent.Substring(0, ReturnOffset)).Trim();
                        string RightStr = (TempContent.Substring(ReturnOffset + Get.Text.Length, TempContent.Length - (ReturnOffset + Get.Text.Length))).Trim();

                        if (LeftStr.Contains(" "))
                        {
                            LeftStr = LeftStr.Substring(LeftStr.LastIndexOf(" ") +" ".Length);
                        }
                        if (RightStr.Contains(" "))
                        {
                            RightStr = RightStr.Substring(0,RightStr.LastIndexOf(" "));
                        }

                        if (RightStr.Contains("(")&&RightStr.Contains(")"))
                        {
                            int GetAddress = ConvertHelper.ObjToInt(ConvertHelper.StringDivision(RightStr,"(",")"));
                            if (GetAddress > 0)
                            {
                                foreach (var GetAdd in ConjunctResults)
                                {
                                    if (GetAdd.HashNo.Equals(GetAddress.ToString()))
                                    {
                                        RightStr = GetAdd.Text;
                                        break;
                                    }
                                }
                            }
                        }

                        if (Get.FrontExist.Trim().Length > 0)
                        {
                            if (!UsingStr(LeftStr).ToLower().Contains(Get.FrontExist.Trim().ToLower()))
                            {
                                CanUsing = false;
                            }
                        }

                        if (Get.BehindExist.Trim().Length > 0)
                        {
                            if (!UsingStr(RightStr).ToLower().Contains(Get.BehindExist.Trim().ToLower()))
                            {
                                CanUsing = false;
                            }
                        }
                    }

                    CodeCount++;

                    string TempLeftStr = TempContent.Substring(0, ReturnOffset);
                    string TempRightStr = TempContent.Substring(ReturnOffset + Get.Text.Length, TempContent.Length - (ReturnOffset + Get.Text.Length));

                    string SignName = "60" + CodeCount.ToString() + "06";

                    if (CanUsing)
                    {
                        ConjunctResults.Add(new ConjunctResult(SignName,Get.Text,Get.Result));
                        EngineMsgs.Add(new EngineProcessItem("Conjunction", Get.Text, Get.Result, 1));

                        TempContent = TempLeftStr + "(" + SignName + ")" + TempRightStr;
                    }
                    else
                    {
                        if (Get.DefaultResult.Trim().Length > 0)
                        {
                            ConjunctResults.Add(new ConjunctResult(SignName, Get.Text,Get.DefaultResult));
                            EngineMsgs.Add(new EngineProcessItem("Conjunction", Get.Text, Get.DefaultResult, 0));

                            TempContent = TempLeftStr + "(" + SignName + ")" + TempRightStr;
                        }
                        else
                        {
                            if (KeyCount > 0)
                            {
                                KeyCount--;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }

            return TempContent;
        }

        public string GetRemainingText(string Text)
        {
            string TempLine = Text;

            foreach (var Get in this.ConjunctResults)
            {
                TempLine = TempLine.Replace("(" + Get.HashNo + ")", string.Empty);
                TempLine = TempLine.Replace("（" + Get.HashNo + "）", string.Empty);
                TempLine = TempLine.Replace(Get.HashNo, string.Empty);
            }

            return TempLine;
        }
        public string GetDefStr(string Text)
        {
            string TempLine = Text;

            foreach (var Get in this.ConjunctResults)
            {
                TempLine = TempLine.Replace(" (" + Get.HashNo + ") ", Get.EN);
                TempLine = TempLine.Replace("(" + Get.HashNo + ")", Get.EN);
                TempLine = TempLine.Replace(" （" + Get.HashNo + "） ", Get.EN);
                TempLine = TempLine.Replace("（" + Get.HashNo + "）", Get.EN);
                TempLine = TempLine.Replace(Get.HashNo, Get.EN);
            }

            return TempLine;
        }
        public string UsingStr(string Text)
        {
            string TempLine = Text;

            foreach (var Get in this.ConjunctResults)
            {
                TempLine = TempLine.Replace(" (" + Get.HashNo + ") ", Get.Text);
                TempLine = TempLine.Replace("(" + Get.HashNo + ")",Get.Text);
                TempLine = TempLine.Replace(" （" + Get.HashNo + "） ", Get.Text);
                TempLine = TempLine.Replace("（" + Get.HashNo + "）",Get.Text);
                TempLine = TempLine.Replace(Get.HashNo,Get.Text);
            }

            return TempLine;
        }

    }

    public class ConjunctResult
    {
        public string HashNo = "";
        public string EN = "";
        public string Text = "";

        public ConjunctResult(string HashNo,string EN,string Text)
        {
            this.HashNo = HashNo;
            this.EN = EN;
            this.Text = Text;
        }
    }

    public class ConjunctItem :IComparable<ConjunctItem>
    {
        public string StrExist = "";
        public string FrontExist = "";
        public string BehindExist = "";
        public string Text = "";
        public string Result = "";
        public int IgnoreCase = 0;
        public string DefaultResult = "";
        public int TIndex = 0;

        public int CompareTo(ConjunctItem p)
        {
            if (this.TIndex > p.TIndex)
                return 1;
            else
                return -1;
        }


        public ConjunctItem(object StrExist, object FrontExist, object BehindExist, object Text,object Result, object IgnoreCase, object DefaultResult,object TIndex)
        {
            this.StrExist = ConvertHelper.ObjToStr(StrExist);
            this.FrontExist = ConvertHelper.ObjToStr(FrontExist);
            this.BehindExist = ConvertHelper.ObjToStr(BehindExist);
            this.Text = ConvertHelper.ObjToStr(Text);
            this.Result = ConvertHelper.ObjToStr(Result);
            this.IgnoreCase = ConvertHelper.ObjToInt(IgnoreCase);
            this.DefaultResult = ConvertHelper.ObjToStr(DefaultResult);
            this.TIndex = ConvertHelper.ObjToInt(TIndex);
        }
    }

}
