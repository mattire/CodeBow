using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingHood
{
    class CodeBowState
    {
        //Dictionary<string, List<(bool, Action)>> namedActions = new Dictionary<string, List<(bool, Action)>>();
        
        public CodeBowState(CodeBow cb)
        {
            SendToBackOnActivated = false;
            Cbow = cb;
        }

        public bool SendToBackOnActivated { get; set; }
        public CodeBow Cbow { get; }

        public void Activated() {
            if (SendToBackOnActivated) {
                Cbow.SendToBack();
            }
        }
    }
}
