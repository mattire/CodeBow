using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodingHood
{
    class SearchTxtMngr
    {
        public SearchTxtMngr(TextBox textBox, ListBox listBox )
        {
            textBox.KeyPress += TextBox_KeyPress;
            textBox.KeyDown += TextBox_KeyDown;

            TextBox = textBox;
            ListBox = listBox;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) {
                
            }
            else if (e.KeyCode == Keys.Down) {

            }

        }

        public TextBox TextBox { get; }
        public ListBox ListBox { get; }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
    }
}
