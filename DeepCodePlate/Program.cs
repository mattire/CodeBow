using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodingHood
{
    static class Program
    {
        private static CodeBow mCb;
        private static ObjectStudy mObjStudy;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mCb = new CodeBow();
            mObjStudy = new ObjectStudy();
            mObjStudy.StudyFormEvents(mCb);
            mObjStudy.StudyTextBox(mCb.SearchBox);


            mCb.Activated += (s, e) => { mCb.ClipText = Clipboard.GetText(); };
            Application.Run(mCb);
        }

        //private static void Cb_Activated(object sender, EventArgs e)
        //{            
        //    mCb.ClipText = Clipboard.GetText();
        //}
    }
}
