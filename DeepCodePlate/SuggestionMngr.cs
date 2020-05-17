using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodingHood
{
    class SuggestionMngr
    {
        private static SuggestionMngr instance;
        public static SuggestionMngr Instance
        {
            get {
                if (instance == null) {
                    instance = new SuggestionMngr();
                }
                return instance;
            }
        }

        public ListBox SuggestBox { get {
                return CodeBow.Current.SuggestBox;
            }
        }

        internal void ShowSuggestions()
        {
            var fp = CodeBow.Current.CurrentFieldPlace;
            if (fp != null) {
                FieldHistoryMngr mngr = FieldHistoryMngr.Instance;
                //var lst = mngr.SuggestionMap[fp.FldName];
                var rtb = CodeBow.Current.RichTextBox;
                var selPos = rtb.GetPositionFromCharIndex(rtb.SelectionStart);
                //var offset = new Point() { X = 5, Y = 50 };
                var offset = new Point() { X = 5, Y = 14 };
                var p1 = Utils.Add(/*this.Location,*/ rtb.Location, selPos, offset);

                SuggestBox.Location = p1;
                SuggestBox.Visible = true;
            }
        }

        internal void HideSuggestions() {
            SuggestBox.Visible = false;
        }
    }
}
