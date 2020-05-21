using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodingHood
{
    public class Highlighter
    {
        private CodeBow mCodeBow;
        private RichTextBox mRichTextBox;

        public Highlighter(CodeBow codeBow)
        {
            this.mCodeBow = codeBow;
            mRichTextBox = mCodeBow.RichTextBox;
            mCodeBow.RichTextBox.SelectionChanged += RichTextBox_SelectionChanged;
            //mCodeBow.ProcessAndRewriteDone += (s, e) => { RichTextBox_SelectionChanged(s,e); };
        }

        bool handling = false;
        private void RichTextBox_SelectionChanged(object sender, EventArgs e)
        {
            if (handling) { return; }
            handling = true;
            var pos = mRichTextBox.SelectionStart;
            Unhighlight();
            var fldPlace = mCodeBow.FieldPlaces.FirstOrDefault(fp => 
                                    pos >= fp.OutPutTextStart && 
                                    pos <= fp.OutPutTextEnd);
            if (fldPlace != null) { Highlight(fldPlace); }
            handling = false;
        }

        internal void Highlight(CodeBow.FieldPlace fpl)
        {
            var selCol = mCodeBow.RichTextBox.SelectionColor;
            var selStart = mCodeBow.RichTextBox.SelectionStart;
            var selLen = mCodeBow.RichTextBox.SelectionLength;

            mCodeBow.RichTextBox.SelectionColor = Color.LightGray;

            mCodeBow.FieldPlaces.Where(fp1 => fp1.FldName == fpl.FldName).ToList().ForEach(fp =>
            {
                int len = fp.OutPutTextEnd - fp.OutPutTextStart;
                mCodeBow.RichTextBox.Select(fp.OutPutTextStart, len);
                //mCodeBow.RichTextBox.SelectionBackColor = Color.LightSalmon;
                //mCodeBow.RichTextBox.SelectionBackColor = Color.LightGoldenrodYellow;
                mCodeBow.RichTextBox.SelectionBackColor = Color.LightGray;
            });
            //mCodeBow.RichTextBox

            mCodeBow.RichTextBox.SelectionStart = selStart;
            mCodeBow.RichTextBox.SelectionLength = selLen;
            mCodeBow.RichTextBox.SelectionColor = selCol;
        }

        internal void Unhighlight()
        {
            var selCol = mCodeBow.RichTextBox.SelectionColor;
            var selStart = mCodeBow.RichTextBox.SelectionStart;
            var selLen = mCodeBow.RichTextBox.SelectionLength;

            mCodeBow.RichTextBox.SelectionColor = Color.White;

            mCodeBow.FieldPlaces.ForEach(fp =>
            {
                int len = fp.OutPutTextEnd - fp.OutPutTextStart;
                mCodeBow.RichTextBox.Select(fp.OutPutTextStart, len);
                //mCodeBow.RichTextBox.SelectionBackColor = Color.LightSalmon;
                //mCodeBow.RichTextBox.SelectionBackColor = Color.LightGoldenrodYellow;
                mCodeBow.RichTextBox.SelectionBackColor = Color.White;
            });
            //mCodeBow.RichTextBox

            mCodeBow.RichTextBox.SelectionStart = selStart;
            mCodeBow.RichTextBox.SelectionLength = selLen;
            mCodeBow.RichTextBox.SelectionColor = selCol;
        }
    }
}
