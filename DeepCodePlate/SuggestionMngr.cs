using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodingHood
{
    // When starting to type, autoshow suggestions, if there is suggestion matches 
    // Esc hide suggestions, ctrl + space, reshow suggestions
    // Ctrl + h, show entries in history

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

        public ListBox SuggestBox {
            get {
                return CodeBow.Current.SuggestBox;
            }
        }

        internal void ShowSuggestions()
        {
            var bow = CodeBow.Current;
            mFldPlace = bow.CurrentFieldPlace;
            StoreFldInd();
            ind = -1;
            if (mFldPlace != null) {
                FieldHistoryMngr mngr = FieldHistoryMngr.Instance;

                var origName = CodeBow.Current.GetOriginalFieldName(mFldPlace);

                //var fld = bow.Fields.FirstOrDefault(f => f.Name == mFldPlace.FldName);
                //var ind = bow.Fields.IndexOf(fld);
                List<string> entries = new List<string>();
                if (mngr.SuggestionMap.ContainsKey(origName)) {
                     entries=  mngr.SuggestionMap[origName];
                }
                //var lst = mngr.SuggestionMap.ElementAt(ind).Value;

                //var lst = mngr.SuggestionMap[fp.FldName];
                var rtb = bow.RichTextBox;
                var selPos = rtb.GetPositionFromCharIndex(rtb.SelectionStart);
                //var offset = new Point() { X = 5, Y = 50 };
                var offset = new Point() { X = 5, Y = 18 };
                var p1 = Utils.Add(/*this.Location,*/ rtb.Location, selPos, offset);

                SuggestBox.Items.Clear();
                entries.ForEach(s => SuggestBox.Items.Add(s));

                SuggestBox.Location = p1;
                SuggestBox.Visible = true;
            }
        }

        private void StoreFldInd()
        {
            var bow = CodeBow.Current;
            var fld = bow.Fields.FirstOrDefault(fld1 => fld1.Name == mFldPlace.FldName);
            mFldInd = bow.Fields.IndexOf(fld);
        }

        private CodeBow.FieldPlace mFldPlace;
        private int ind = -1;
        private int mFldInd;

        internal void HideSuggestions() {
            SuggestBox.Visible = false;
        }

        internal void HandleUpKey() {
            int count = SuggestBox.Items.Count;
            if (ind == -1) { ind = count-1; } else if (ind > 0) { ind--; } else { ind = count - 1; }
            SuggestBox.SelectedIndex = ind;
        }

        internal void HandleDownKey() {
            int count = SuggestBox.Items.Count;
            if (ind == -1) { ind = 0; } else if (ind < count - 1) { ind++; } else { ind = 0; }
            SuggestBox.SelectedIndex = ind;
        }

        internal void HandleEnterKey()
        {
            
            var item = (string)SuggestBox.SelectedItem;
            //CodeBow.Current.CurrentFieldPlace.FldValue = item;
            var bow = CodeBow.Current;
            bow.Process(mFldPlace, item, mFldInd);
            bow.RewriteFieldPlaces();
            bow.RichTextBox.Select(mFldPlace.OutPutTextStart, mFldPlace.OutLength);
            SuggestBox.Visible = false;
        }
    }
}
