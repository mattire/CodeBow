using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodingHood
{
    class ClipRunner
    {
        public static ClipRunner Instance
        {
            get
            {
                if (clipRunner == null)
                {
                    clipRunner = new ClipRunner();
                }
                return clipRunner;
            }
        }
        private static ClipRunner clipRunner;

        public void Script2Clipboard(string txt)
        {
            var spl = txt.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            var lines = spl.Skip(1);
            lines = lines.Reverse();
            foreach (var line in lines)
            {
                Clipboard.SetText(line);
                Task.Delay(100);
            }
            CodeBow.Current.CloseWindow();
        }
    }
}
