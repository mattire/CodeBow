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
            public int FieldPlaceInd
            {
                get
                {
                    return CodeBow.Current.FieldPlaces.Where(p => p.FldName == this.FldName).ToList().IndexOf(this);
                }
            }
            public int FieldsPlaceInd
            {
                get
                {
                    return CodeBow.Current.FieldPlaces.IndexOf(this);
                }
            }



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
            SetEditControls();
            SetScriptControls();

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

            //FldText = "<#*Element*#>\n#*SurroundContent*#\n</#*Element*#>\n<#*Element*#>\n#*SurroundContent*#\n<//#*Element*#>";
            //ProcessText();

            richTextBox2.EnterPressed += RichTextBox2_EnterPressed;
        }

        private void RichTextBox2_EnterPressed(object sender, EventArgs e)
        {
            button1_Ok_Click(sender, e);
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
            WindowMode = WindowModeType.Active;
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
            if (e.KeyCode == Keys.V && e.Control)
            {
                HandleCopyPaste(e);
                e.Handled = true;
            }

            if (e.KeyCode == Keys.C && e.Control)
            {
                Clipboard.SetText(richTextBox2.SelectedText);
                e.Handled = true;
                return;
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

        private void HandleCopyPaste(KeyEventArgs e)
        {
            var fp = CurrentFieldPlace;
            
            if (fp != null)
            {
                var ind = fp.FieldPlaceInd;
                var inds = fp.FieldsPlaceInd;
                //fp.Paste(Clipboard.GetText());
                var txt = Clipboard.GetText();
                int rbStart = richTextBox2.SelectionStart;
                int rbLen   = richTextBox2.SelectionLength;

                var fld = Fields.FirstOrDefault(f => f.Name == fp.FldName);
                var fldPlaces = FieldPlaces.Where(fpl => fpl.FldName == fld.Name).ToList();

                int fldPos = rbStart - fp.OutPutTextStart;
                var val = (string)fp.FldValue.Clone();
                if (rbLen == 0)
                {
                    val = val.Insert(fldPos, txt);
                }
                else {
                    bool endsOutside = rbStart + rbLen > fp.OutPutTextEnd;
                    if (!endsOutside) { val = val.Remove(fldPos, rbLen); }
                    else { val = val.Substring(0, fldPos); }
                    val = val.Insert(fldPos, txt);
                }
                fld.Value = val;

                // Write to all FieldPlaces
                fldPlaces.ForEach(fpl => fpl.FldValue = val);
                RewriteFieldPlaces();
                //System.Diagnostics.Debug.WriteLine(ind);

                // get current fieldPlace
                var fpUpd = FieldPlaces.ElementAt(inds);
                richTextBox2.SelectionStart = fpUpd.OutPutTextEnd;
            }
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

            List<char> skipKeys = new List<char> { '\u001b', '\u0016', '\u0003' };

            if (skipKeys.Contains(e.KeyChar)) {
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
            var txt = richTextBox2.Text;
            bool res = PswdFieldManager.Instance.ContainsPswds(txt);
            bool ps = PswdFieldManager.Instance.PinSet();
            if (res)
            {
                Action<string> EndStuff = (str) =>
                {
                    str = PswdFieldManager.Instance.DecryptPassWordFields(str);
                    str = PswdFieldManager.Instance.CleanPswdTags(str);
                    ScriptEndActions(str);
                };

                if (ps)
                {
                    var inp = new Input("Enter your pin:", (result) =>
                    {
                        if (PswdFieldManager.Instance.CheckPin(result))
                        {
                            EndStuff(txt);
                        }
                    });
                    inp.TextBox.PasswordChar = '*';
                    inp.Show();
                }
                else {
                    EndStuff(txt);
                }   
            }
            else {
                ScriptEndActions(txt);
            }
        }

        private void ScriptEndActions(string txt) {
            Clipboard.SetText(txt);
            FieldHistoryMngr.Instance.StoreValues();
            //this.Close();
            this.WindowState = FormWindowState.Minimized;
            //WindowMode = WindowModeType.OkCancelSelected;
            RunScript("ahkep2.exe");

            //textBox1.Select();
            //this.Activate();
            WindowMode = WindowModeType.Finished;

            System.Diagnostics.Debug.WriteLine("SendToBack");
            this.SendToBack();

            //Task.Run(async () =>
            //{
            //    await Task.Delay(2000);
            //    //WindowMode = WindowModeType.Finished;
            //    //textBox1.Focus();
            //});
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            //Clipboard.SetText("");
            //this.Close();
            this.WindowState = FormWindowState.Minimized;
            this.SendToBack();
            //WindowMode = WindowModeType.OkCancelSelected;

            System.Diagnostics.Debug.WriteLine("SendToBack");
            this.SendToBack();

            //Task.Run(async () =>
            //{
            //    await Task.Delay(2000);
            //    //WindowMode =WindowModeType.Finished;
            //});
        }

        private void FormShown(object sender, EventArgs e)
        {
            PrintMethod();
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

        private WindowModeType WindowMode {
            get { return windowMode; }
            set {
                windowMode = value;
                System.Diagnostics.Debug.WriteLine($"WindowMode: {value}");
            }
        }

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
                SaveScript(richTextBox1.Text);
            }
        }

        private void SaveScript(string txt)
        {
            string SnipPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), SnippetFld);

            txt = PswdFieldManager.Instance.EncryptPassWordFields(txt);

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
                        sw.Write(txt);
                    }
                    saveStream.Close();
                }
                richTextBox2.Select();
                this.Dispose();
            }
        }

        List<Control> lstEditControls;
        List<Control> lstScriptControls;

        private void SetEditControls()
        {
            lstEditControls = new List<Control>() {
                btnToFields         ,
                quickPanel1         ,
                btnCoordsToClipboard,
                btnContentTag       ,
                btnToField          ,
                btnToPswd           ,
            };
        }

        private void SetScriptControls()
        {
            lstScriptControls = new List<Control>() {
                btnNew,
                quickPanel1
            };
        }

        private void SetupEditMode() {
            lstEditControls  .ForEach(ec => ec.Show());
            lstScriptControls.ForEach(lc => lc.Hide());
            //btnNew              .Hide();
            //btnToFields         .Show();
            //btnCoordsToClipboard.Show();
            //btnContentTag       .Show();
            //btnToField          .Show();
            //btnToPswd           .Show();
            richTextBox1.Enabled = true;
            richTextBox1.Visible = true;
            richTextBox2.Enabled = false;
            richTextBox2.Visible = false;
            btnEdit.Text = "&Save";
            richTextBox1.Select();
        }

        private void SetupScriptMode(bool selectScriptWindow=false)
        {
            lstEditControls.ForEach(ec =>   ec.Hide());
            lstScriptControls.ForEach(lc => lc.Show());
            //btnNew.Show();
            //btnToFields.Hide();
            //btnCoordsToClipboard.Hide();
            //btnContentTag.Hide();
            //btnToField.Hide();
            //btnToPswd.Hide();
            richTextBox1.Enabled = false;
            richTextBox1.Visible = false;
            richTextBox2.Enabled = true;
            richTextBox2.Visible = true;
            btnEdit.Text = "E&dit";
            if (selectScriptWindow) {
                richTextBox2.Select();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            
            SetupEditMode();
        }

        private void btnToField_Click(object sender, EventArgs e)
        {
            var selTxt = richTextBox1.SelectedText;
            var repTxt = 
                        FieldPlace.StartTag + 
                        richTextBox1.SelectedText + 
                        FieldPlace.EndTag;
            var newTxt = richTextBox1.Text.Replace(selTxt, repTxt);
            richTextBox1.Text = newTxt;
        }

        private void btnToField_Click_1(object sender, EventArgs e)
        {
            var selTxt = richTextBox1.SelectedText;
            var repTxt =
                        FieldPlace.StartTag +
                        richTextBox1.SelectedText +
                        FieldPlace.EndTag;
            richTextBox1.SelectedText = repTxt;
        }


        private void btnToPswd_Click_1(object sender, EventArgs e)
        {
            var selTxt = richTextBox1.SelectedText;
            var repTxt =
                        PswdFieldManager.PswdStartTag +
                        richTextBox1.SelectedText +
                        PswdFieldManager.PswdEndTag;
            richTextBox1.SelectedText = repTxt;

        }

        private void btnCoordsToClipboard_Click(object sender, EventArgs e)
        {
            if (RunScript("mouseGetPos2.exe")) {
                Clipboard.Clear();
            }
            //if (File.Exists("mouseGetPos2.exe"))
            //{
            //    Clipboard.Clear();
            //    System.Diagnostics.Process.Start("mouseGetPos2.exe");
            //    this.WindowState = FormWindowState.Minimized;
            //}
            //else {
            //    MessageBox.Show("mouseGetPos2.exe not found");
            //}
        }

        private void btnContentTag_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = "#*SurroundContent*#";
        }

        private void btnQuickSave_Click(object sender, EventArgs e)
        {
            var ac = new AhkCtrlCoords();
            ac.Process(richTextBox1.Text, richTextBox2.Text, this.FieldPlaces);


            string selItem = (string)comboBox1.SelectedItem;
            if (!string.IsNullOrWhiteSpace(selItem)) {
                var fn = selItem + ".txt";
                File.WriteAllText(fn, richTextBox2.Text);
            }
        }

        private void btnQuickRun_Click(object sender, EventArgs e)
        {
            RunScript("quickScript.exe");
        }

        private bool RunScript(string script)
        {
            if (File.Exists(script))
            {
                System.Diagnostics.Process.Start(script);
                this.WindowState = FormWindowState.Minimized;
                return true;
            }
            else
            {
                MessageBox.Show($"{script} not found");
                return false;
            }
        }

        private void CodeBow_SizeChanged(object sender, EventArgs e)
        {
            bool mousePointerNotOnTaskbar = Screen.GetWorkingArea(this).Contains(Cursor.Position);

            if (this.WindowState == FormWindowState.Minimized && mousePointerNotOnTaskbar) {
                notifyIcon1.Icon = SystemIcons.Application;
                //notifyIcon1.BalloonTipText = "CodeBow app";
                //notifyIcon1.ShowBalloonTip(1000);
                this.ShowInTaskbar = false;
                notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            if (this.WindowState == FormWindowState.Normal)
            {
                this.ShowInTaskbar = true;
                notifyIcon1.Visible = false;
            }
        }

        private void FormActivated(object sender, EventArgs e)
        {
            PrintMethod();
            //System.Diagnostics.Debug.WriteLine("LeaveMode1");
            System.Diagnostics.Debug.WriteLine(WindowMode);

            //if (WindowMode == WindowModeType.Finished)
            //{
            //    textBox1.Text = "";
            //    ClipText = Clipboard.GetText();
            //    textBox1.Select();
            //    WindowMode = WindowModeType.Active;
            //}
        }

        enum WindowModeType {
            Active,
            Finished,
        }
        WindowModeType windowMode = WindowModeType.Finished;

        private void FormDeactivate(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("LeaveMode2");
            //System.Diagnostics.Debug.WriteLine(WindowMode);
        }

        private void FormVisibleChanged(object sender, EventArgs e)
        {
            PrintMethod();
            System.Diagnostics.Debug.WriteLine( this.Visible);
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            textBox1.Select();
        }

        
        public void PrintMethod()
        {

            var st = new System.Diagnostics.StackTrace();
            var sf = st.GetFrame(1);
            System.Diagnostics.Debug.WriteLine("Method");
            System.Diagnostics.Debug.WriteLine(sf.GetMethod().Name);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0112) // WM_SYSCOMMAND
            {
                // Check your window state here
                if (m.WParam == new IntPtr(0xF030)) // Maximize event - SC_MAXIMIZE from Winuser.h
                {
                    // THe window is being maximized
                }
                if (m.WParam == new IntPtr(0xF120)) // Restore event - SC_RESTORE from Winuser.h
                {
                    System.Diagnostics.Debug.WriteLine("RESTORED !!");

                    System.Diagnostics.Debug.WriteLine($"WinMod: {WindowMode}");
                    //if (WindowMode == WindowModeType.Finished) {
                    Task.Run(async () =>
                    {
                        await Task.Delay(10);
                            this.Invoke((MethodInvoker)delegate
                            {
                                try
                                {
                                    System.Diagnostics.Debug.WriteLine("SELECT!!!");
                                    if (textBox1.CanSelect) { System.Diagnostics.Debug.WriteLine("Con select !!"); }
                                    textBox1.Select();
                                    if (!textBox1.Focused) { textBox1.Focus(); }
                                    System.Diagnostics.Debug.WriteLine("SELECT END!!!");
                                }
                                catch (Exception exp)
                                {
                                    System.Diagnostics.Debug.WriteLine(exp.ToString());
                                }
                            });
                    });
                        //textBox1.Select();
                        //WindowMode = WindowModeType.Active;
                    //}
                }

                //
            }
            base.WndProc(ref m);
        }

        public TextBox SearchBox { get { return textBox1; } }

        /// <summary>
        /// Hide from task switcher
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                /*/
                System.Diagnostics.Debug.WriteLine("CREATE PARAMS CALLED");
                return base.CreateParams;
                //
                if (WindowMode == WindowModeType.Finished)
                {
                    var Params = base.CreateParams;
                    Params.ExStyle |= 0x80;
                    return Params;
                }
                else {
                    return base.CreateParams;
                }
                //*/
                var Params = base.CreateParams;
                Params.ExStyle |= 0x80;
                return Params;
            }
        }
    }
}
