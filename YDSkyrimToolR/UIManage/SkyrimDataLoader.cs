using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YDSkyrimToolR.ConvertManager;
using YDSkyrimToolR.SkyrimManage;

namespace YDSkyrimToolR.UIManage
{
    public class SkyrimDataLoader
    {
        public static void LoadArmor(EspReader Reader, YDListView View)
        {
            for (int i = 0; i < Reader.Armor.Count; i++)
            {
                var GetHashKey = Reader.Armor.ElementAt(i).Key;
                var GetArmor = Reader.Armor[GetHashKey];

                var GetNameStr = ConvertHelper.ObjToStr(GetArmor.Name);
                if (GetNameStr.Length > 0)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        View.AddRowR(UIHelper.CreatLine("ArmorNames", GetHashKey.ToString(), GetArmor.EditorID, GetNameStr, ""));
                    }));
                }

                var GetDescriptionStr = ConvertHelper.ObjToStr(GetArmor.Description);
                if (GetDescriptionStr.Length > 0)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        View.AddRowR(UIHelper.CreatLine("ArmorDescription", GetHashKey.ToString(), GetArmor.EditorID, GetDescriptionStr, ""));
                    }));
                }
            }
        }
    }
}
