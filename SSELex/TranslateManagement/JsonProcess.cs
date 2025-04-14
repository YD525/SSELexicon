using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SSELex.TranslateManage
{
    public class JsonProcess
    {
        public static string GetValue(string Json)
        {
            if (Json.Contains(":"))
            {
                if (Json.Contains("{"))
                {
                    Json = Json.Substring(Json.IndexOf("{"));
                }

                Json = Json.Substring(Json.IndexOf(":") + ":".Length);

                if (Json.Contains("}"))
                {
                    Json = Json.Substring(0,Json.LastIndexOf("}"));
                }
                Json = Json.Trim();
                Json = Json.Trim('\"');
                return Json;
            }
            return string.Empty;
        }
    }
}
