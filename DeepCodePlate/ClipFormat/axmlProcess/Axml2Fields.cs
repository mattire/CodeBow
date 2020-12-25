using System;
using System.Collections.Generic;
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

        public string Format(string str)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(str);
            StringBuilder sb = new StringBuilder();

            Action<XmlNode> AppendFindStrs = (xn) => {
                //xn.Name
                var idVal = GetIdValue(xn);
                if (idVal != null) {
                    //varName = view.FindViewById<Type>(Resource.Id.resourceId);
                    string varName = UnderScores2CamelCase(idVal);
                    sb.Append($"var {varName} = view.FindViewById<{xn.Name}>(Resource.Id.{idVal});\n");
                }
            };

            foreach (XmlNode xn in xmlDocument.ChildNodes)
            {
                RecurNodes(xn, AppendFindStrs);
            }

            return sb.ToString();
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
