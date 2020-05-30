using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodingHood
{
    public partial class CodeBow : Form
    {
        //https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.control.previewkeydown?view=netcore-3.1

        /*\
         * \
         *  =============
         *  DEEPCODEPLATE
         *  CODECYCLER
         *  =============
         * / 
        */

        private const string SnippetFld = ".\\Snippets";

        public List<string> Snippets { get; set; }

        private bool SnippetInited { get; set; }

        private void RefreshSnippets()
        {
            System.Diagnostics.Debug.WriteLine(Directory.GetCurrentDirectory());

            //List<string> fls = Directory.GetFiles(".\\Snippets").ToList();
            List<string> fls = Directory.GetFiles(SnippetFld).ToList();
            List<string> trimmed = new List<string>();
            fls.ForEach(s =>
            {
                var tr = s.Replace('.', ' ').Replace('\\', ' ').Trim().Split(' ')[1];
                trimmed.Add(tr);
            });

            Snippets = trimmed;
            //this.textBox1.AutoCompleteCustomSource.Clear();
            //this.textBox1.AutoCompleteCustomSource.AddRange(trimmed.ToArray());
        }


        public class Field
        {
            private string name;
            public string Name { get { return name; } set { name = value; } }
            public string Value { get; set; }
        }

        public class FieldPlace
        {
            public FieldPlace(int occurrenceStart, int occurrenceEnd, int order, string fldText)
            {
                FldOuterStart = occurrenceStart;
                FldOuterEnd = occurrenceEnd + EndTag.Length;
                FldInnerStart = occurrenceStart + StartTag.Length;
                FldInnerEnd = occurrenceEnd;
                Order = order;
                OutPutTextStart = FldOuterStart - Order * (StartTag.Length + EndTag.Length);
                OutPutTextEnd = FldOuterEnd - (Order + 1) * (StartTag.Length + EndTag.Length);
                FldName = fldText.Substring(FldInnerStart, FldInnerEnd - FldInnerStart);
                // initial value is fld name
                FldValue = (string)FldName.Clone();
            }

            public static string StartTag { get; set; } = "#*";
            public static string EndTag { get; set; } = "*#";

            public int FldOuterStart { get; set; }
            public int FldOuterEnd { get; set; }

            public int FldInnerStart { get; set; }
            public int FldInnerEnd { get; set; }

            public int OutPutTextStart { get; set; }
            public int OutPutTextEnd { get; set; }

            public string FldName { get; set; }

            public string OrigFldName { get; set; }

            private string fldValue { get; set; }
            public string FldValue
            {
                get { return fldValue; }
                set {
                    fldValue = value;
                    if (CodeBow.Current.SnippetInited) {
                        if (CodeBow.Current.CurrentFieldPlace == this) {
                            if (fldValue.Length >= 3) {                            
                                SuggestionMngr.Instance.CheckHistoryEntries(this);
                            }
                        }
                    }
                }
            }
            public string TagName { get { return StartTag + FldName + EndTag; } }
            public string TagValue { get { return StartTag + FldValue + EndTag; } }

            public int OutLength { get { return OutPutTextEnd - OutPutTextStart; } }

            public int Order { get; set; }

            internal void Select()
            {
                var box = (CodeRichTextBox)CodeBow.Current.RichTextBox;
                box.Select(this.OutPutTextStart, OutLength);
            }
        }

        public class OutPlace
        {
            public int Start { get; set; }
            public int End { get; set; }
            public int OutputStart { get; set; }
            public int OutputEnd { get; set; }
            public int Order { get; set; }
            public string Value { get; set; }
            public OutPlace(int start, int end, int order, string txt)
            {
                Start = start;
                End = end;
                Order = order;
                var diff = order * (FieldPlace.StartTag.Length + FieldPlace.EndTag.Length);
                OutputStart = start + diff;
                OutputEnd = end + diff;
                Value = txt.SubWithStartEndPoints(Start, End);
            }
            //public string GetOutTxt(string txt) {
            //    return txt.SubWithStartEndPoints(OutputStart, OutputEnd);
            //}
            public string GetOrigTxt(string txt)
            {
                return txt.SubWithStartEndPoints(Start, End);
            }

        }

        public List<FieldPlace> FieldPlaces { get; set; }
        public List<OutPlace> OutsideFldPlaces { get; set; } = new List<OutPlace>();


        public List<Field> Fields { get; set; }

        private SearchTxtMngr mSearchTxtMngr;

        public string FldText { get; set; }
        public string OutputText { get; set; }

        public List<string> PartsBetween { get; set; } = new List<string>();

        private Highlighter mHighlighter;
        private TabHandler mTabHandler;

        public RichTextBox RichTextBox { get { return richTextBox2; } }

        private string ClipText = null;

        public static CodeBow Current;

        public CodeBow()
        {

            SnippetInited = false;
            RefreshSnippets();


            Current = this;
            InitializeComponent();

            SetupScriptMode();
            


            listBox2.Visible = false;
            mHighlighter = new Highlighter(this);
            mTabHandler = new TabHandler(this);
            ClipText = Clipboard.GetText();
            richTextBox2.KeyPress += RichTextBox2_KeyPress;
            richTextBox2.SelectionChanged += RichTextBox2_SelectionChanged;
            richTextBox2.KeyDown += RichTextBox2_KeyDown1;
            richTextBox1.KeyDown += RichTextBox1_KeyDown;

            mSearchTxtMngr =
                new 
                SearchTxtMngr(textBox1, listBox1, Snippets,
                    listBoxSelectionChangeHandling: (sel) => { PreviewSnippet(sel); },
                    handleChosenOne: (sel) => {
                        SelectSnippet(sel); }
                );

            //textBox1.Focus();

            FldText = "<#*Element*#>\n#*SurroundContent*#\n</#*Element*#>\n<#*Element*#>\n#*SurroundContent*#\n</#*Element*#>";
            //FldText = "<#*Element*#>\r\n#*SurroundContent*#\r\n</#*Element*#>\r\n<#*Element*#>\r\n#*SurroundContent*#\r\n</#*Element*#>";

            ProcessText();
            //label2.Focus();
            //textBox1.Select();
        }

        private string HandleLineFeeds(string txt) {
            txt = txt.Replace("\n\r", "\n");
            txt = txt.Replace("\r\n", "\n");
            return txt;
        }


        private void SelectSnippet(string sel)
        {
            SelectedTxt = null;
            richTextBox2.Focus();
            //FieldHistoryMngr.Instance.LoadHistory();
        }

        public string CurrentFileName { get; set; }

        private void PreviewSnippet(string sel)
        {
            var fn = sel + ".txt";
            CurrentFileName = fn;
            SetupScriptMode();
                 
            var path = Path.Combine(SnippetFld, fn);
            var txt = File.ReadAllText(path);
            FldText = HandleLineFeeds(txt);
            ProcessText();
            OriginalFields = Fields.Select(f => new Field() { Name = f.Name, Value = f.Value }).ToList();
        }

        private void RichTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S && e.Alt)
            {
                //textBox1.Focus();
            }
        }

        int SelectionLenghtOnKeyDown = 0;
        private void RichTextBox2_KeyDown1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab) {
                mTabHandler.NextFld();
                e.Handled = true;
                return;
            }
            var sgstMngr = SuggestionMngr.Instance;
            if (e.KeyCode == Keys.H && e.Control) {
                var fp = CurrentFieldPlace;
                sgstMngr.ShowSuggestions();
                e.Handled = true;
            }
            if (SuggestBox.Visible == true)
            {
                if (e.KeyCode == Keys.Escape) {
                    sgstMngr.HideSuggestions();
                    e.Handled = true;
                }
                if (e.KeyCode == Keys.Up) {
                    sgstMngr.HandleUpKey();
                    e.Handled = true;
                }
                if (e.KeyCode == Keys.Down) {
                    sgstMngr.HandleDownKey();
                    e.Handled = true;
                }
                if (e.KeyCode == Keys.Enter)
                {
                    sgstMngr.HandleEnterKey();
                    e.Handled = true;
                }
            }

            //if (e.KeyCode == Keys.S && e.Alt)
            //{
            //    //textBox1.Focus();
            //    //textBox2.Select();
            //}

            System.Diagnostics.Debug.WriteLine("KeyDown" + richTextBox2.SelectionLength);
            SelectionLenghtOnKeyDown = richTextBox2.SelectionLength;
        }

        public bool HandleSelectionChanged { get; set; } = true;
        private void RichTextBox2_SelectionChanged(object sender, EventArgs e)
        {

        }

        List<char> specialChars = new List<char>() { '!', '"', '#', '¤', '%', '&', '/', '(', ')', '=', '?', '+', '´', '`', '@', '£', '$', '€', '{', '}', '[', ']', '\\', '^', '¨', '~', '*', '\'', ',', '.', ';', ':', '-', '_', ' ', '½', '§', '<', '>', '|' };

        private void RichTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Control) && e.KeyChar == 'h') { e.Handled = true; return; }

            var sgstMngr = SuggestionMngr.Instance;
            if (sgstMngr.SuggestBox.Visible) {
                //System.Diagnostics.Debug.WriteLine(e.KeyChar=='\r');
                if(e.KeyChar == '\r' || e.KeyChar == '\n'){
                    sgstMngr.HandleEnterKey();
                    e.Handled = true;
                    return;
                }
            }

            if (e.KeyChar == '\u001b') {
                e.Handled= true;
                return;
            }

            System.Diagnostics.Debug.WriteLine("KeyPress" + richTextBox2.SelectionLength);
            int selLen = SelectionLenghtOnKeyDown;
            if (char.IsLetterOrDigit(e.KeyChar) || e.KeyChar == '\b' || specialChars.Contains(e.KeyChar))
            {
                e.Handled = true;
            }
            int selStart;
            if (e.KeyChar == '\b' && selLen == 0) { selStart = richTextBox2.SelectionStart + 1; }
            else { selStart = richTextBox2.SelectionStart; }

            FieldPlace fp;
            int fieldsInd;
            int fieldInd;
            if (selLen == 0)
            {
                //var fp = InsideFieldPlace(selStart);
                InsideFieldPlace2(selStart, out fp, out fieldInd, out fieldsInd);
                var outFp = InsideOutFieldPlace(selStart);
                if (fp != null)
                {
                    Process(fp, selStart, e.KeyChar, richTextBox2.Text);
                    RewriteFieldPlaces();
                    

                    if (e.KeyChar != '\b')
                    {
                        richTextBox2.SelectionStart = selStart + fieldInd + 1;
                    }
                    else
                    { richTextBox2.SelectionStart = selStart - 1 - fieldInd; }
                }
                if (outFp != null)
                {
                }
            }
            else // Selection Length > 0
            {
                SelectionInsideFieldPlace(selStart, selLen, out fp, out fieldInd, out fieldsInd);
                if (fp != null)
                {
                    Process(fp, selStart, selLen, e.KeyChar, richTextBox2.Text);
                    RewriteFieldPlaces();
                    

                    if (e.KeyChar != '\b')
                    {
                        //richTextBox2.SelectionStart = selStart + fieldInd + 1;
                        richTextBox2.SelectionStart = selStart - fieldInd * (selLen - 1) + 1;
                    }
                    else
                    {
                        richTextBox2.SelectionStart = selStart - fieldInd * selLen;
                    }
                }
            }
            //throw new NotImplementedException();
        }

        public void RewriteFieldPlaces()
        {
            string newOrigTxt;
            string newOutTxt;
            AssembleText(out newOrigTxt, out newOutTxt);
            richTextBox1.Text = newOrigTxt;
            richTextBox2.Text = newOutTxt;
            FldText = newOrigTxt;
            ProcessText();
        }

        private int GetFieldPlaceCount(FieldPlace fp)
        {
            return FieldPlaces.Where(fplace => fplace.FldName == fp.FldName).Count();
        }

        private void Process(FieldPlace fp, int selStart, int selLen, char keyChar, string text)
        {
            int fldPos = selStart - fp.OutPutTextStart;
            string value = (string)fp.FldValue.Clone();
            if (fldPos + selLen > value.Length) { value = ""; }
            else { value = value.Remove(fldPos, selLen); }

            if (keyChar == '\b') { } // nothing to do
            else
            {
                value = value.Insert(fldPos, keyChar.ToString());
            }
            var fld = Fields.FirstOrDefault(f => f.Name == fp.FldName);
            fld.Value = value;
            FieldPlaces.Where(fpl => fpl.FldName == fld.Name).ToList().ForEach(fpl => fpl.FldValue = value);
        }

        private void Process(FieldPlace fp, int selStart, char keyChar, string text)
        {
            int fldPos = selStart - fp.OutPutTextStart;
            if (keyChar == '\b' && fldPos == 0) { return; }

            string value = (string)fp.FldValue.Clone();
            if (keyChar != '\b')
            {
                value = value.Insert(fldPos, keyChar.ToString());
            }
            else
            {
                value = value.Remove(fldPos - 1, 1);
            }
            var fld = Fields.FirstOrDefault(f => f.Name == fp.FldName);
            fld.Value = value;
            // Write to all FieldPlaces
            FieldPlaces.Where(fpl => fpl.FldName == fld.Name).ToList().ForEach(fpl => fpl.FldValue = value);
        }

        public void Process(FieldPlace fp, string newVal, int fldInd) {
            fp.FldValue = newVal;
            var fld = Fields.ElementAt(fldInd);
            //var fld = Fields.FirstOrDefault(f => f.Name == fp.FldName);
            fld.Value = newVal;
            FieldPlaces.Where(fpl => fpl.FldName == fld.Name).ToList().ForEach(fpl => fpl.FldValue = newVal);
        }

        private void RichTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            System.Windows.Input.KeyConverter keyConverter = new System.Windows.Input.KeyConverter();
            var str = keyConverter.ConvertToString(e.KeyCode);

            if (!IsNavigationEA(e))
            {
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Back)
            {
                e.Handled = false;
            }
        }

        List<Keys> navKeys = new List<Keys>() { Keys.Left, Keys.Right, Keys.Up, Keys.Down, Keys.PageUp, Keys.Tab, Keys.RButton, Keys.LButton, Keys.Escape, Keys.Return };
        private bool IsNavigationEA(KeyEventArgs e)
        {
            return navKeys.Contains(e.KeyCode);
        }

        //public FieldPlace InsideFieldPlace(int pos) {
        //    return FieldPlaces.FirstOrDefault(fld => fld.OutPutTextStart <= pos && pos <= fld.OutPutTextEnd);
        //}

        public void InsideFieldPlace2(int pos, out FieldPlace fp, out int fldInd, out int fldsInd)
        {
            fp = FieldPlaces.FirstOrDefault(fp1 => fp1.OutPutTextStart <= pos && pos <= fp1.OutPutTextEnd);
            if (fp == null) { fldInd = -1; fldsInd = -1; return; }
            fldsInd = FieldPlaces.IndexOf(fp);
            var fn = fp.FldName;
            fldInd = FieldPlaces.Where(fp1 => fp1.FldName == fn).ToList().IndexOf(fp);
        }

        public void SelectionInsideFieldPlace(int start, int selLength, out FieldPlace fp, out int fldInd, out int fldsInd)
        {
            // check start and end points are both inside fieldplace
            int end = start + selLength;

            FieldPlace fp1; int fldInd1; int fldsInd1;
            FieldPlace fp2; int fldInd2; int fldsInd2;

            InsideFieldPlace2(start, out fp1, out fldInd1, out fldsInd1);
            InsideFieldPlace2(start, out fp2, out fldInd2, out fldsInd2);

            if (Object.ReferenceEquals(fp1, fp2))
            {
                fp = fp1;
                fldInd = fldInd1;
                fldsInd = fldsInd1;
            }
            else
            {
                fp = null;
                fldInd = -1;
                fldsInd = -1;
            }
        }


        public OutPlace InsideOutFieldPlace(int pos)
        {
            return this.OutsideFldPlaces.FirstOrDefault(pl => pl.OutputStart <= pos && pos <= pl.OutputEnd);
        }


        public void ProcessText()
        {
            OutsideFldPlaces = new List<OutPlace>();
            FieldPlaces = new List<FieldPlace>();
            Fields = new List<Field>();
            PartsBetween = new List<string>();

            HandleSurroundContent();

            richTextBox1.Text = FldText;
            var starts = FldText.AllIndexesOf(FieldPlace.StartTag);
            var stops = FldText.AllIndexesOf(FieldPlace.EndTag);

            FieldPlaces = starts.Select((val, ind) => new { start = val, stop = stops[ind], order = ind }).Select(t => new FieldPlace(t.start, t.stop, t.order, FldText)).ToList();

            var outPlacesPoints = new List<int>() { 0 };
            FieldPlaces.ForEach(fp => { outPlacesPoints.Add(fp.FldOuterStart); outPlacesPoints.Add(fp.FldOuterEnd); });
            outPlacesPoints.Add(FldText.Length);
            for (int i = 0; i < outPlacesPoints.Count; i += 2)
            {
                var op = new OutPlace(outPlacesPoints[i], outPlacesPoints[i + 1], i / 2, FldText);
                System.Diagnostics.Debug.WriteLine(op.Value);
                OutsideFldPlaces.Add(op);
            }

            Fields = FieldPlaces.GroupBy(fld => fld.FldName).Select(g => g.First()).Select(fld => new Field() { Name = fld.FldName, Value = fld.FldName }).ToList();

            List<int> OutPutPoints = new List<int>() { 0 };
            FieldPlaces.ForEach(fld => { OutPutPoints.Add(fld.FldOuterStart); OutPutPoints.Add(fld.FldOuterEnd); });
            OutPutPoints.Add(FldText.Length);

            for (int i = 0; i < OutPutPoints.Count; i += 2)
            {
                var start = OutPutPoints.ElementAt(i);
                var end = OutPutPoints.ElementAt(i + 1);
                PartsBetween.Add(FldText.SubWithStartEndPoints(start, end));
                //OutFieldPlaces.Add()
            }
            //OutPutPoints.Aggregate((p1, p2) => { PartsBetween.Add(FldText.SubWithStartEndPoints(p1, p2)); return p2; });

            StringBuilder sb = new StringBuilder();
            int ind1 = 0;
            foreach (var item in PartsBetween)
            {
                sb.Append(item);
                if (ind1 < FieldPlaces.Count)
                {
                    sb.Append(FieldPlaces.ElementAt(ind1).FldName);
                }
                ind1++;
            }
            OutputText = sb.ToString();

            richTextBox2.Text = OutputText;
        }

        private void HandleSurroundContent()
        {
            string searchStr = FieldPlace.StartTag + "SurroundContent" + FieldPlace.EndTag;
            if (ClipText != null &&
                FldText.Contains(searchStr))
            {
                FldText = FldText.Replace(searchStr, ClipText);
            }
        }

        private void AssembleText(out string newOrigTxt, out string newOutTxt)
        {
            var outTxt = richTextBox2.Text;
            var origTxt = richTextBox1.Text;

            StringBuilder outSb = new StringBuilder();
            StringBuilder origSb = new StringBuilder();

            for (int i = 0; i < FieldPlaces.Count; i++)
            {
                //outSb.Append( OutsideFldPlaces.ElementAt(i).GetOutTxt(outTxt));
                outSb.Append(OutsideFldPlaces.ElementAt(i).Value);
                outSb.Append(FieldPlaces.ElementAt(i).FldValue);

                //origSb.Append(OutsideFldPlaces.ElementAt(i).GetOutTxt(origTxt));
                origSb.Append(OutsideFldPlaces.ElementAt(i).Value);
                origSb.Append(FieldPlaces.ElementAt(i).TagValue);
            }
            var lstPiece = OutsideFldPlaces.Last();
            //outSb.Append(lstPiece.GetOutTxt(outTxt));
            outSb.Append(lstPiece.Value);
            //origSb.Append(lstPiece.GetOutTxt(origTxt));
            origSb.Append(lstPiece.Value);
            newOrigTxt = origSb.ToString();
            newOutTxt = outSb.ToString();
        }


        private void SearchLabelEnter(object sender, EventArgs e)
        {
            //textBox1.Focus();
        }

        private void button1_Ok_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox2.Text);
            FieldHistoryMngr.Instance.StoreValues();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(" ");
            this.Close();
        }

        private void FormShown(object sender, EventArgs e)
        {
            textBox1.Select();
        }

        public string CurrentScript { get { return textBox1.Text; } }

        public FieldPlace CurrentFieldPlace {
            get {
                var pos = richTextBox2.SelectionStart;
                var fp = FieldPlaces.FirstOrDefault(fp1 => fp1.OutPutTextStart <= pos && pos <= fp1.OutPutTextEnd);
                return fp;
            }
        }

        public ListBox SuggestBox {
            get {
                return this.listBox2;
            } }

        public List<Field> OriginalFields { get; private set; }
        public TabHandler MTabHandler { get => mTabHandler; set => mTabHandler = value; }

        public string GetOriginalFieldName(FieldPlace fieldPlace)
        {
            var fld = Fields.FirstOrDefault(f => f.Name == fieldPlace.FldName);
            
            var ind = Fields.IndexOf(fld);
            if (ind != -1)
            {
                return OriginalFields[ind].Name;
            }
            return "";
        }

        private void SearchEntered(object sender, EventArgs e)
        {
            SnippetInited = false;
        }

        public string SelectedTxt { get; set; }

        private void RichTextBoxEnter(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null) { SelectedTxt = (string)listBox1.SelectedItem; }
            else if (Snippets.Contains(textBox1.Text))
            {
                SelectedTxt = textBox1.Text;
            }
            else if (SelectedTxt == null && listBox1.Items.Count > 0) {
                //if (listBox1.SelectedItem != null) { SelectedTxt = (string)listBox1.SelectedItem; }
                //else {
                    SelectedTxt = (string)listBox1.Items[0];
                //}
            }
            if (SelectedTxt == null) { return; }
            PreviewSnippet( SelectedTxt);
            textBox1.Text = SelectedTxt;
            FieldHistoryMngr.Instance.LoadHistory();
            SnippetInited = true;
            mTabHandler.SelectFirstFld();
        }

        private void SearchLeave(object sender, EventArgs e)
        {
            SelectedTxt = null; // set to null, so logic above can decide what is selected
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "E&dit")
            {
                SetupEditMode();
            }
            else
            {
                //SetScriptupMode();
                SaveScript();
            }
        }

        private void SaveScript()
        {
            string SnipPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), SnippetFld);

            saveFileDialog1.InitialDirectory = SnipPath;
            saveFileDialog1.FileName = CurrentFileName;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SetupScriptMode(selectScriptWindow:true);
                Stream saveStream;
                if ((saveStream = saveFileDialog1.OpenFile()) != null)
                {
                    // Code to write the stream goes here.
                    using (var sw = new StreamWriter(saveStream))
                    {
                        sw.Write(richTextBox1.Text);
                    }
                    saveStream.Close();
                }
                richTextBox2.Select();
            }
        }

        private void SetupEditMode() {
            richTextBox1.Enabled = true;
            richTextBox1.Visible = true;
            richTextBox2.Enabled = false;
            richTextBox2.Visible = false;
            btnEdit.Text = "&Save";
            richTextBox1.Select();
        }

        private void SetupScriptMode(bool selectScriptWindow=false)
        {
            richTextBox1.Enabled = false;
            richTextBox1.Visible = false;
            richTextBox2.Enabled = true;
            richTextBox2.Visible = true;
            btnEdit.Text = "E&dit";
            if (selectScriptWindow) {
                richTextBox2.Select();
            }
        }
    }
}
