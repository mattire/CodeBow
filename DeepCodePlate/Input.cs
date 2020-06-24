using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodingHood
{
    public partial class Input : Form
    {
        public Input(string labelTxt, Action<string> callback)
        {
            InitializeComponent();
            label1.Text = labelTxt;
            Callback = callback;
        }


        public Action<string> Callback { get; }

        public TextBox TextBox { get { return textBox1; }  }


        private void btnOk_Click(object sender, EventArgs e)
        {
            Callback(textBox1.Text);
        }

        private void InputKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOk_Click(sender, e);
            }
        }
    }
}
