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
            var bow = CodeBow.Current;
            var fp = bow.CurrentFieldPlace;
            ind = -1;
            if (fp != null) {
                FieldHistoryMngr mngr = FieldHistoryMngr.Instance;

                //bow.OriginalFields.ForEach(f =>
                //{
                //    System.Diagnostics.Debug.WriteLine(f.Name);
                //    System.Diagnostics.Debug.WriteLine(f.Value);
                //});

                var fld = bow.Fields.FirstOrDefault(f => f.Name == fp.FldName);
                var ind = bow.Fields.IndexOf(fld);
                var lst = mngr.SuggestionMap.ElementAt(ind).Value;

                //var lst = mngr.SuggestionMap[fp.FldName];
                var rtb = bow.RichTextBox;
                var selPos = rtb.GetPositionFromCharIndex(rtb.SelectionStart);
                //var offset = new Point() { X = 5, Y = 50 };
                var offset = new Point() { X = 5, Y = 18 };
                var p1 = Utils.Add(/*this.Location,*/ rtb.Location, selPos, offset);

                lst.ForEach(s => SuggestBox.Items.Add(s));

                SuggestBox.Location = p1;
                SuggestBox.Visible = true;
            }
        }

        private int ind = -1;

        internal void HideSuggestions() {
            SuggestBox.Visible = false;
        }

        internal void HandleUpKey() {
            int count = SuggestBox.Items.Count;
            if (ind == -1) { ind = count; } else if (ind > 0) { ind--; } else { ind = count - 1; }
            SuggestBox.SelectedIndex = ind;
        }

        internal void HandleDownKey() {
            int count = SuggestBox.Items.Count;
            if (ind == -1) { ind = 0; } else if (ind < count - 1) { ind++; } else { ind = 0; }
            SuggestBox.SelectedIndex = ind;
        }
    }
}
