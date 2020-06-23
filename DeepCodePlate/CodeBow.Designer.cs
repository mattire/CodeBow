namespace CodingHood
{
    partial class CodeBow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.panelEdit = new System.Windows.Forms.Panel();
            this.btnCoordsToClipboard = new System.Windows.Forms.Button();
            this.btnToPswd = new System.Windows.Forms.Button();
            this.btnToField = new System.Windows.Forms.Button();
            this.btnToFields = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.richTextBox2 = new CodingHood.CodeRichTextBox();
            this.panelEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Font = new System.Drawing.Font("Lucida Console", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(129, 10);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(333, 319);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 11);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "S&earch";
            this.label2.Enter += new System.EventHandler(this.SearchLabelEnter);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(9, 27);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(116, 20);
            this.textBox1.TabIndex = 7;
            this.textBox1.Enter += new System.EventHandler(this.SearchEntered);
            this.textBox1.Leave += new System.EventHandler(this.SearchLeave);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(9, 50);
            this.listBox1.Margin = new System.Windows.Forms.Padding(2);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(116, 303);
            this.listBox1.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(241, 331);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "&Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Ok_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(301, 331);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(56, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "&Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(27, 75);
            this.listBox2.Margin = new System.Windows.Forms.Padding(2);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(125, 225);
            this.listBox2.TabIndex = 11;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(2, 13);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(56, 21);
            this.btnEdit.TabIndex = 12;
            this.btnEdit.Text = "&Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // panelEdit
            // 
            this.panelEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEdit.Controls.Add(this.btnCoordsToClipboard);
            this.panelEdit.Controls.Add(this.btnToPswd);
            this.panelEdit.Controls.Add(this.btnToField);
            this.panelEdit.Controls.Add(this.btnToFields);
            this.panelEdit.Controls.Add(this.btnNew);
            this.panelEdit.Controls.Add(this.btnEdit);
            this.panelEdit.Location = new System.Drawing.Point(465, 10);
            this.panelEdit.Margin = new System.Windows.Forms.Padding(2);
            this.panelEdit.Name = "panelEdit";
            this.panelEdit.Size = new System.Drawing.Size(130, 354);
            this.panelEdit.TabIndex = 13;
            // 
            // btnCoordsToClipboard
            // 
            this.btnCoordsToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCoordsToClipboard.Location = new System.Drawing.Point(2, 156);
            this.btnCoordsToClipboard.Margin = new System.Windows.Forms.Padding(2);
            this.btnCoordsToClipboard.Name = "btnCoordsToClipboard";
            this.btnCoordsToClipboard.Size = new System.Drawing.Size(62, 44);
            this.btnCoordsToClipboard.TabIndex = 17;
            this.btnCoordsToClipboard.Text = "Coor&ds to Clipboard";
            this.btnCoordsToClipboard.UseVisualStyleBackColor = true;
            this.btnCoordsToClipboard.Visible = false;
            this.btnCoordsToClipboard.Click += new System.EventHandler(this.btnCoordsToClipboard_Click);
            // 
            // btnToPswd
            // 
            this.btnToPswd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToPswd.Location = new System.Drawing.Point(2, 126);
            this.btnToPswd.Margin = new System.Windows.Forms.Padding(2);
            this.btnToPswd.Name = "btnToPswd";
            this.btnToPswd.Size = new System.Drawing.Size(56, 26);
            this.btnToPswd.TabIndex = 16;
            this.btnToPswd.Text = "To&Pswd";
            this.btnToPswd.UseVisualStyleBackColor = true;
            this.btnToPswd.Visible = false;
            this.btnToPswd.Click += new System.EventHandler(this.btnToPswd_Click_1);
            // 
            // btnToField
            // 
            this.btnToField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToField.Location = new System.Drawing.Point(2, 96);
            this.btnToField.Margin = new System.Windows.Forms.Padding(2);
            this.btnToField.Name = "btnToField";
            this.btnToField.Size = new System.Drawing.Size(56, 26);
            this.btnToField.TabIndex = 15;
            this.btnToField.Text = "ToF&ield";
            this.btnToField.UseVisualStyleBackColor = true;
            this.btnToField.Visible = false;
            this.btnToField.Click += new System.EventHandler(this.btnToField_Click_1);
            // 
            // btnToFields
            // 
            this.btnToFields.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToFields.Location = new System.Drawing.Point(2, 65);
            this.btnToFields.Margin = new System.Windows.Forms.Padding(2);
            this.btnToFields.Name = "btnToFields";
            this.btnToFields.Size = new System.Drawing.Size(56, 26);
            this.btnToFields.TabIndex = 14;
            this.btnToFields.Text = "&ToFields";
            this.btnToFields.UseVisualStyleBackColor = true;
            this.btnToFields.Visible = false;
            this.btnToFields.Click += new System.EventHandler(this.btnToField_Click);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNew.Location = new System.Drawing.Point(2, 39);
            this.btnNew.Margin = new System.Windows.Forms.Padding(2);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(56, 21);
            this.btnNew.TabIndex = 13;
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // richTextBox2
            // 
            this.richTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox2.Font = new System.Drawing.Font("Lucida Console", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox2.Location = new System.Drawing.Point(129, 27);
            this.richTextBox2.Margin = new System.Windows.Forms.Padding(2);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(333, 301);
            this.richTextBox2.TabIndex = 8;
            this.richTextBox2.Text = "";
            this.richTextBox2.Enter += new System.EventHandler(this.RichTextBoxEnter);
            // 
            // CodeBow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 395);
            this.Controls.Add(this.panelEdit);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CodeBow";
            this.Text = "CodeBow";
            this.Shown += new System.EventHandler(this.FormShown);
            this.panelEdit.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private CodingHood.CodeRichTextBox richTextBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Panel panelEdit;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnToFields;
        private System.Windows.Forms.Button btnToField;
        private System.Windows.Forms.Button btnToPswd;
        private System.Windows.Forms.Button btnCoordsToClipboard;
    }
}

