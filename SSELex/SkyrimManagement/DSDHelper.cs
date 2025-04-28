using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSELex.SkyrimManage
{
    public class DSDItem
    {
        public string editor_id { get; set; } = "";
        public string form_id { get; set; } = "";
        public int index { get; set; } = 1;
        public string type { get; set; } = "";
        public string original { get; set; } = "";
        public string @string { get; set; } = "";
    }

    public class DSDFile
    {
        public List<DSDItem> DSDItems = new List<DSDItem>();
    }
}
