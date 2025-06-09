using SSELex.ConvertManager;
using SSELex.RequestManagement;
using SSELex.TranslateCore;
using SSELex.TranslateManage;
using static SSELex.TranslateManage.TransCore;
using System.Net;
using System.Text.Json;
using Cohere;
using HarmonyLib;

namespace SSELex.PlatformManagement
{
    public class CohereHelper
    {
        private readonly CohereClient _client;

        public CohereHelper(string apiKey)
        {
            _client = new CohereClient(apiKey);
        }

        public string GenerateText(string prompt)
        {
            var request = new GenerateRequest
            {
                Prompt = prompt
            };

            var response = _client.GenerateAsync(request).GetAwaiter().GetResult();

            if (response?.Generations != null && response.Generations.Count > 0)
            {
                return response.Generations[0].Text;
            }
            return string.Empty;
        }
    }
    public class CohereApi
    {
        //"Important: When translating, strictly keep any text inside angle brackets (< >) or square brackets ([ ]) unchanged. Do not modify, translate, or remove them.\n\n"
        public string QuickTrans(string TransSource, Languages FromLang, Languages ToLang, bool UseAIMemory, int AIMemoryCountLimit, string Param)
        {
            List<string> Related = new List<string>();
            if (DeFine.GlobalLocalSetting.UsingContext && UseAIMemory)
            {
                Related = EngineSelect.AIMemory.FindRelevantTranslations(FromLang, TransSource, AIMemoryCountLimit);
            }

            var GetTransSource = $"Translate the following text from {LanguageHelper.ToLanguageCode(FromLang)} to {LanguageHelper.ToLanguageCode(ToLang)}:\n\n";

            if (Param.Trim().Length > 0)
            {
                GetTransSource += Param;
            }

            if (ConvertHelper.ObjToStr(DeFine.GlobalLocalSetting.UserCustomAIPrompt).Trim().Length > 0)
            {
                GetTransSource += DeFine.GlobalLocalSetting.UserCustomAIPrompt + "\n\n";
            }

            if (Related.Count > 0)
            {
                GetTransSource += "Previous related translations:\n";
                foreach (var related in Related)
                {
                    GetTransSource += $"- {related}\n";
                }
                GetTransSource += "\n";
            }

            GetTransSource += $"\"\"\"\n{TransSource}\n\"\"\"\n\n";
            GetTransSource += "Respond in JSON format: {\"translation\": \"<translated_text>\"}";

            if (GetTransSource.EndsWith("\n"))
            {
                GetTransSource = GetTransSource.Substring(0, GetTransSource.Length - 1);
            }

            var GetResult = CallAI(GetTransSource);
            if (GetResult != null)
            {
                if (GetResult.Trim().Length > 0)
                {
                    if (DeFine.CurrentDashBoardView != null)
                    {
                        DeFine.CurrentDashBoardView.SetLogB(GetTransSource + "\r\n\r\n AI:\r\n" + GetResult);
                    }

                    if (GetResult.Trim().Equals("<translated_text>"))
                    {
                        return string.Empty;
                    }

                    return GetResult;
                }
            }
            return string.Empty;
        }

        public string CallAI(string Msg)
        {
            try
            {
                var Cohere = new CohereHelper(DeFine.GlobalLocalSetting.CohereKey);
                string Result = Cohere.GenerateText(Msg);
                return JsonGeter.GetValue(Result);
            }
            catch 
            {
                return string.Empty;
            }
        }
    }
}
