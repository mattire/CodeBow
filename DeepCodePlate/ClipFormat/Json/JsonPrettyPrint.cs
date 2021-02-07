using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingHood.ClipFormat.Generic
{
    class JsonPrettyPrint : IClipFormatter
    {
        
        public string Name { get { return "JsonPrettyPrintFormatter"; } }

        public string Format(string str)
        {
            dynamic parsedJson = JsonConvert.DeserializeObject(str);
            return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
        }
    }
}
