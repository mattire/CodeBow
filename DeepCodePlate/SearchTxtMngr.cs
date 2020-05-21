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
        private readonly Action<string> listBoxSelectionChangeHandling;
        private readonly Action<string> handleChosen;

        public SearchTxtMngr(TextBox searchBox, 
            ListBox listBox, 
            List<string> snippets,
            Action<string> listBoxSelectionChangeHandling,
            Action<string> handleChosenOne
            )
        {
            TextBox = searchBox;
            ListBox = listBox;
            Snippets = snippets;
            this.listBoxSelectionChangeHandling = listBoxSelectionChangeHandling;
            this.handleChosen = handleChosenOne;
            TextBox.KeyPress += TextBox_KeyPress;
            TextBox.KeyDown += TextBox_KeyDown;
            TextBox.GotFocus += (s, e) => { ListBox.Visible = true; };
            TextBox.LostFocus += (s, e) => { ListBox.Visible = false; };
            TextBox.TextChanged += new System.EventHandler(this.SearchTextChanged);
            ListBox.SelectedIndexChanged += (s, e) =>
            {
                var sel = ListBox.SelectedItem.ToString();
                this.listBoxSelectionChangeHandling(sel);
            };
        }

        private void SearchTextChanged(object sender, EventArgs e)
        {
            //textBox1.TextChanged
            var search = TextBox.Text;
            var results = Snippets.Where(t => t.ToUpper().Contains(search.ToUpper())).ToList();
            ListBox.Items.Clear();
            results.ForEach(s => { ListBox.Items.Add(s); });
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            var ind = ListBox.SelectedIndex;
            var count = ListBox.Items.Count;

            if (ListBox.Visible == true) {
                if (e.KeyCode == Keys.Up) {
                    if (ind == -1) { ind = count; } else if (ind > 0) { ind--; } else { ind = count - 1; }
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Down) {
                    if (ind == -1) { ind = 0; } else if (ind < count - 1) { ind++; } else { ind = 0; }
                    e.Handled = true;
                }
            }
            if (ind != -1) {
                if (ind > ListBox.Items.Count - 1) { ind = ListBox.Items.Count - 1; }
                ListBox.SelectedIndex = ind;
            };


            if (e.KeyCode == Keys.Enter)
            {
                string sel=null;
                if (ListBox.SelectedItem != null)
                {
                    sel = ListBox.SelectedItem.ToString();
                }
                else {
                    if (ListBox.Items.Count > 0) {
                        sel = ListBox.Items[0].ToString();
                    }
                }
                if (sel != null) {
                    System.Diagnostics.Debug.WriteLine(sel);
                    ListBox.Visible = false;

                    handleChosen(sel);
                    e.Handled = true;
                    CodeBow.Current.MTabHandler.SelectFirstFld();
                }
            }
        }

        public TextBox TextBox { get; }
        public ListBox ListBox { get; }
        public List<string> Snippets { get; }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
    }
}
