using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodingHood.ClipFormat
{
    class ClipFormatMngr
    {
        List<Type> ClipFormatterTypes = new List<Type>();
        List<IClipFormatter> Formatters = new List<IClipFormatter>();

        public ClipFormatMngr()
        {
            Init();
        }

        void Init()
        {
            var type = typeof(IClipFormatter);
            var asm = Assembly.GetExecutingAssembly();
            
            ClipFormatterTypes =
                //AppDomain.CurrentDomain.GetAssemblies()
                //.SelectMany(s => s.GetTypes())
                GetTypesWithinNamespace(asm, "CodingHood.ClipFormat")
                .Where(p => type.IsAssignableFrom(p))
                .ToList();
            //Activator.CreateInstance()
            Formatters = ClipFormatterTypes.Select(t => (IClipFormatter)Activator.CreateInstance(t)).ToList();
        }

        private Type[] GetTypesWithinNamespace(Assembly assembly, string nameSpace)
        {
            return
              assembly.GetTypes()
                      //.Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                      .Where(t => t.Namespace!=null)
                      .Where(t => t.Namespace.StartsWith(nameSpace, StringComparison.Ordinal))
                      .Where(t => !t.IsInterface)
                      .ToArray();
        }

        public IClipFormatter TryFindClipFormatter(string scriptTxt)
        {
            using (var reader = new StringReader(scriptTxt))
            {
                var line1 = reader.ReadLine();

                if (line1.StartsWith("{Formatter:") && line1.EndsWith("}")) {
                    var name = line1.Substring(11, line1.IndexOf('}') - 11);
                    return Formatters.FirstOrDefault(f => f.Name == name);
                }
            }
            return null;
        }
    }
}
