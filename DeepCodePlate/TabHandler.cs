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
        public CodeBow mDeepCodePlate { get; }

        private int mTabInd;

        public TabHandler(CodeBow DeepCodePlate)
        {
            mDeepCodePlate = DeepCodePlate;
            mTabInd = 0;
        }

        public void SelectCurrent() {
            var fld = mDeepCodePlate.Fields[mTabInd];
            var fldPlace = mDeepCodePlate.FieldPlaces.FirstOrDefault(fp => fp.FldName == fld.Name);
            mDeepCodePlate.RichTextBox.Select(fldPlace.FldOuterStart, fldPlace.OutLength);
        }

        public void SelectFirst() {
            mTabInd = 0;
            SelectCurrent();
        }

        public void Next()
        {
            if (mTabInd < mDeepCodePlate.Fields.Count)
            {
                mTabInd++;
            }
            else {
                mTabInd = 0;
            }
            SelectCurrent();
        }
    }
}
