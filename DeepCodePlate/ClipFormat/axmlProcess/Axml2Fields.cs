using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace CodingHood.ClipFormat.axmlProcess
{
    class Axml2Fields : IClipFormatter
    {
        
        public string Name { get { return "AxmlFormatter"; } }

        //public string FormatClip() {
        //    string clip = Clipboard.GetText();
        //    return Format(clip);
        //}

        private string IfPathConvert2File(string str)
        {
            if (!str.StartsWith("\"")) { return str; }
            if (str.Contains('\r') || str.Contains('\n')) { return str; }
            try
            {
                str = str.Replace("\"", "");
                var p = Path.GetFullPath(str);
                str = File.ReadAllText(p);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("not path or invalid path");
            }
            return str;
        }
        //Id Name VarName
        private class IdNode
        {
            public string Id { get; set; }
            public XmlNode Node { get; set; }
            //public string Name { get; set; }
            public string VarName { get; set; }
        }

        public string Format(string str)
        {
            str = IfPathConvert2File(str);

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(str);
            StringBuilder sbFinds = new StringBuilder();
            StringBuilder sbProps = new StringBuilder();
            List<IdNode> UITuples = new List<IdNode>();

            Action<XmlNode> AppendFindStrs = (xn) => {
                //xn.Name
                var idVal = GetIdValue(xn);
                if (idVal != null) {
                    string varName = UnderScores2CamelCase(idVal);
                    UITuples.Add(new IdNode { Id = idVal, Node = xn, VarName = varName });
                    //sbFinds.Append($"{varName} = view.FindViewById<{xn.Name}>(Resource.Id.{idVal});\n");
                    //sbProps.Append($"public {xn.Name} {varName} {{ get; set; }}\n");
                }
            };

            foreach (XmlNode xn in xmlDocument.ChildNodes) {
                RecurNodes(xn, AppendFindStrs);
            }

            var nameLen = UITuples.Select(t => t.Node.Name.Length).Max();
            var varNameLen = UITuples.Select(t => t.VarName.Length).Max();
            //string findsFormat = $"{{0, -{varNameLen}}} = view.FindViewById<{{1, -{nameLen}}}>(Resource.Id.{{2}});\n";
            string findsFormat = $"{{0, -{varNameLen}}} = view.FindViewById<{{1}}>(Resource.Id.{{2}});\n";
            string propsFormat = $"public {{0, -{nameLen}}} {{1, -{varNameLen}}} {{{{ get; set; }}}}\n";

            foreach (var t in UITuples)
            {
                sbFinds.Append(string.Format(findsFormat, t.VarName, t.Node.Name, t.Id));
                sbProps.Append(string.Format(propsFormat, t.Node.Name, t.VarName));
            }

            return sbProps.ToString() + sbFinds.ToString();
        }

        string UnderScores2CamelCase(string txt)
        {
            return String.Join("", txt.Split(new char[] { '_' }).Select(s => s.Substring(0, 1).ToUpper() + s.Substring(1)));
        }

        void RecurNodes(XmlNode xn, Action<XmlNode> nodeAction)
        {
            Console.WriteLine(xn.Name);
            nodeAction(xn);
            foreach (XmlNode cn in xn.ChildNodes) { RecurNodes(cn, nodeAction); }
        }

        string GetIdValue(XmlNode xn)
        {
            if (xn.Attributes == null) { return null; }
            var idAttr = FindAttribute("android:id", xn.Attributes);
            //"@+id/buttonCurrentValuesTab"

            return idAttr?.Value?.Substring(5) ?? null;
        }

        System.Xml.XmlAttribute FindAttribute(string name, System.Xml.XmlAttributeCollection coll) {
            foreach (XmlAttribute attr in coll) {
                if (attr.Name == name) {
                    return attr;
                }
            }
            return null;
        }
    }

}
