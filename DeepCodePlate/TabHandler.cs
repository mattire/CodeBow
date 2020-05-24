using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingHood
{

    // Need inherit and DeepCodePlate Richtextbox to work
    public class TabHandler
    {
        public CodeBow mCodeBow { get; }
        public List<CodeBow.FieldPlace> MFieldPlaces {
            get {
                return CodeBow.Current.FieldPlaces;
            }
        }

        private CodeRichTextBox mRCodeBox;
        private int mTabFieldInd;
        private int mTabFieldPlaceInd;

        public TabHandler(CodeBow codeBow)
        {
            mCodeBow = codeBow;
            mRCodeBox = (CodeRichTextBox)mCodeBow.RichTextBox;
            //MFieldPlaces = mCodeBow.FieldPlaces;
            mRCodeBox.TabForward += MCodeBox_TabForward;
            mRCodeBox.TabBackward += MCodeBox_TabBackward;
            mTabFieldInd = 0;
        }

        public void SelectFirstFieldPlace() {
            var fst = MFieldPlaces.FirstOrDefault();
            if (fst != null) {
                fst.Select();
                //mRCodeBox.Select(fst.OutPutTextStart, fst.OutLength);
            }
        }

        private void DetermineSelectedFieldPlace()
        {
            var startPos = mRCodeBox.SelectionStart;
            var fldPlc = MFieldPlaces.FirstOrDefault(fp =>
                                    fp.OutPutTextStart <= startPos &&
                                    fp.OutPutTextEnd >= startPos
                                    );
            if (fldPlc != null) { mTabFieldPlaceInd = MFieldPlaces.IndexOf(fldPlc); }
            else { mTabFieldPlaceInd = 0; }
        }


        private void MCodeBox_TabForward(object sender, EventArgs e)
        {
            SuggestionMngr.Instance.HandleTabForward();
            DetermineSelectedFieldPlace();

            if (mTabFieldPlaceInd < MFieldPlaces.Count-1) {
                mTabFieldPlaceInd++;
            }
            else {
                mTabFieldPlaceInd = 0;
            }
            var fp = MFieldPlaces.ElementAt(mTabFieldPlaceInd);
            fp.Select();
        }

        private void MCodeBox_TabBackward(object sender, EventArgs e)
        {
            DetermineSelectedFieldPlace();

            if (mTabFieldPlaceInd > 0) {
                mTabFieldPlaceInd--;
            }
            else {
                mTabFieldPlaceInd = MFieldPlaces.Count - 1;
            }
            var fp = MFieldPlaces.ElementAt(mTabFieldPlaceInd);
            fp.Select();
        }


        public void SelectCurrentFld() {
            if (mCodeBow.Fields.Count > 0) {
                var fld = mCodeBow.Fields[mTabFieldInd];
                if (fld == null) { return; }
                var fldPlace = mCodeBow.FieldPlaces.FirstOrDefault(fp => fp.FldName == fld.Name);
                mCodeBow.RichTextBox.Select(fldPlace.FldOuterStart, fldPlace.OutLength);
            }
        }

        public void SelectFirstFld() {
            mTabFieldInd = 0;
            SelectCurrentFld();
        }

        public void NextFld()
        {
            if (mTabFieldInd < mCodeBow.Fields.Count)
            {
                mTabFieldInd++;
            }
            else {
                mTabFieldInd = 0;
            }
            SelectCurrentFld();
        }
    }
}
