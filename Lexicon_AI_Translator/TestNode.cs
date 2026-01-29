using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoenixEngine.EngineManagement;
using PhoenixEngine.PlatformManagement;

namespace LexTranslator
{
    public class TestNode
    {

        public static void StartTest()
        {
            CustomReqCore NewCore = new CustomReqCore();

            PlatformConfig CustomNode = new PlatformConfig();
            CustomNode.CustomInFo = new CustomPlatformInFo();
            CustomNode.CustomInFo.CustomID = 111;

            CustomNode.CustomInFo.Url = "https://xxx.com?q=1&s=2";
            NewCore.SetUrl(CustomNode.CustomInFo.Url);
            var UrlValues = NewCore.GetUrlKeyValues();


            CustomNode.CustomInFo.Header = 
                "Content-Type: application/json\n" +
                "Authorization:\n";


            NewCore.SetHeader(CustomNode.CustomInFo.Header);
            var HeaderValues = NewCore.GetHeaderKeyValues();


            CustomNode.CustomInFo.PayLoad = "{\r\n " +
                "\"model\": \"deepseek-chat\",\r\n" +
                " \"messages\": [\r\n " +
                " {\"role\": \"system\", \"content\": \"You are a helpful assistant.\"},\r\n " +
                " {\"role\": \"user\", \"content\": \"Hello!\"}\r\n  ],\r\n \"stream\": false\r\n }";

            NewCore.SetPayLoad(CustomNode.CustomInFo.PayLoad);

            var GetPayLoadValues = NewCore.GetPayLoadKeyValues();

            List<ReqReplaceTag> GenTags = new List<ReqReplaceTag>();
            GenTags.Add(new ReqReplaceTag("model", "Test1"));
            GenTags.Add(new ReqReplaceTag("messages[0].content", "Test2"));
            GenTags.Add(new ReqReplaceTag("messages[1].content", "Test3"));

            string BuildPayload = NewCore.GenPayLoad(GenTags);
            //
            //Phoenix.Config.PlatformConfigs.Add();
        }
    }
}
