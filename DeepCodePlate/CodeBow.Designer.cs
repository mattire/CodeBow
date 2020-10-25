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
            this.components = new System.ComponentModel.Container();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnPaste = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.panelEdit = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.chkFullscreen = new System.Windows.Forms.CheckBox();
            this.btnContentTag = new System.Windows.Forms.Button();
            this.btnCoordsToClipboard = new System.Windows.Forms.Button();
            this.btnToPswd = new System.Windows.Forms.Button();
            this.btnToField = new System.Windows.Forms.Button();
            this.btnToFields = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnQuickSave = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnQuickRun = new System.Windows.Forms.Button();
            this.quickPanel1 = new System.Windows.Forms.Panel();
            this.lblQuick = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.richTextBox2 = new CodingHood.CodeRichTextBox();
            this.btn2Clipboard = new System.Windows.Forms.Button();
            this.panelEdit.SuspendLayout();
            this.quickPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Font = new System.Drawing.Font("Lucida Console", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(172, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(477, 359);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "S&earch";
            this.label2.Enter += new System.EventHandler(this.SearchLabelEnter);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 33);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(153, 22);
            this.textBox1.TabIndex = 7;
            this.textBox1.Enter += new System.EventHandler(this.SearchEntered);
            this.textBox1.Leave += new System.EventHandler(this.SearchLeave);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(12, 61);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(153, 372);
            this.listBox1.TabIndex = 8;
            // 
            // btnPaste
            // 
            this.btnPaste.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPaste.Location = new System.Drawing.Point(193, 413);
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(75, 28);
            this.btnPaste.TabIndex = 9;
            this.btnPaste.Text = "P&aste";
            this.btnPaste.UseVisualStyleBackColor = true;
            this.btnPaste.Click += new System.EventHandler(this.button1_Ok_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Location = new System.Drawing.Point(350, 413);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 28);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 16;
            this.listBox2.Location = new System.Drawing.Point(36, 92);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(165, 276);
            this.listBox2.TabIndex = 11;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(-1, 22);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(83, 26);
            this.btnEdit.TabIndex = 12;
            this.btnEdit.Text = "&Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // panelEdit
            // 
            this.panelEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelEdit.Controls.Add(this.button2);
            this.panelEdit.Controls.Add(this.chkFullscreen);
            this.panelEdit.Controls.Add(this.btnContentTag);
            this.panelEdit.Controls.Add(this.btnCoordsToClipboard);
            this.panelEdit.Controls.Add(this.btnToPswd);
            this.panelEdit.Controls.Add(this.btnToField);
            this.panelEdit.Controls.Add(this.btnToFields);
            this.panelEdit.Controls.Add(this.btnNew);
            this.panelEdit.Controls.Add(this.btnEdit);
            this.panelEdit.Location = new System.Drawing.Point(654, 13);
            this.panelEdit.Name = "panelEdit";
            this.panelEdit.Size = new System.Drawing.Size(99, 392);
            this.panelEdit.TabIndex = 13;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(0, 322);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 32);
            this.button2.TabIndex = 20;
            this.button2.Text = "SaveSize";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // chkFullscreen
            // 
            this.chkFullscreen.AutoSize = true;
            this.chkFullscreen.Location = new System.Drawing.Point(4, 294);
            this.chkFullscreen.Name = "chkFullscreen";
            this.chkFullscreen.Size = new System.Drawing.Size(95, 21);
            this.chkFullscreen.TabIndex = 19;
            this.chkFullscreen.Text = "Fullscreen";
            this.chkFullscreen.UseVisualStyleBackColor = true;
            this.chkFullscreen.Visible = false;
            this.chkFullscreen.CheckedChanged += new System.EventHandler(this.chkFullscreen_CheckedChanged);
            // 
            // btnContentTag
            // 
            this.btnContentTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnContentTag.Location = new System.Drawing.Point(0, 255);
            this.btnContentTag.Name = "btnContentTag";
            this.btnContentTag.Size = new System.Drawing.Size(83, 32);
            this.btnContentTag.TabIndex = 18;
            this.btnContentTag.Text = "CntntT&ag";
            this.btnContentTag.UseVisualStyleBackColor = true;
            this.btnContentTag.Visible = false;
            this.btnContentTag.Click += new System.EventHandler(this.btnContentTag_Click);
            // 
            // btnCoordsToClipboard
            // 
            this.btnCoordsToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCoordsToClipboard.Location = new System.Drawing.Point(0, 196);
            this.btnCoordsToClipboard.Name = "btnCoordsToClipboard";
            this.btnCoordsToClipboard.Size = new System.Drawing.Size(83, 54);
            this.btnCoordsToClipboard.TabIndex = 17;
            this.btnCoordsToClipboard.Text = "Coor&ds to Clipboard";
            this.btnCoordsToClipboard.UseVisualStyleBackColor = true;
            this.btnCoordsToClipboard.Visible = false;
            this.btnCoordsToClipboard.Click += new System.EventHandler(this.btnCoordsToClipboard_Click);
            // 
            // btnToPswd
            // 
            this.btnToPswd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToPswd.Location = new System.Drawing.Point(0, 159);
            this.btnToPswd.Name = "btnToPswd";
            this.btnToPswd.Size = new System.Drawing.Size(83, 32);
            this.btnToPswd.TabIndex = 16;
            this.btnToPswd.Text = "To&Pswd";
            this.btnToPswd.UseVisualStyleBackColor = true;
            this.btnToPswd.Visible = false;
            this.btnToPswd.Click += new System.EventHandler(this.btnToPswd_Click_1);
            // 
            // btnToField
            // 
            this.btnToField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToField.Location = new System.Drawing.Point(0, 122);
            this.btnToField.Name = "btnToField";
            this.btnToField.Size = new System.Drawing.Size(83, 32);
            this.btnToField.TabIndex = 15;
            this.btnToField.Text = "ToF&ield";
            this.btnToField.UseVisualStyleBackColor = true;
            this.btnToField.Visible = false;
            this.btnToField.Click += new System.EventHandler(this.btnToField_Click_1);
            // 
            // btnToFields
            // 
            this.btnToFields.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToFields.Location = new System.Drawing.Point(0, 84);
            this.btnToFields.Name = "btnToFields";
            this.btnToFields.Size = new System.Drawing.Size(83, 32);
            this.btnToFields.TabIndex = 14;
            this.btnToFields.Text = "&ToFields";
            this.btnToFields.UseVisualStyleBackColor = true;
            this.btnToFields.Visible = false;
            this.btnToFields.Click += new System.EventHandler(this.btnToField_Click);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNew.Location = new System.Drawing.Point(-1, 53);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(83, 26);
            this.btnNew.TabIndex = 13;
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnQuickSave
            // 
            this.btnQuickSave.Location = new System.Drawing.Point(189, 3);
            this.btnQuickSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnQuickSave.Name = "btnQuickSave";
            this.btnQuickSave.Size = new System.Drawing.Size(72, 28);
            this.btnQuickSave.TabIndex = 14;
            this.btnQuickSave.Text = "Sa&ve";
            this.btnQuickSave.UseVisualStyleBackColor = true;
            this.btnQuickSave.Click += new System.EventHandler(this.btnQuickSave_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "F1",
            "F2",
            "F3",
            "F4"});
            this.comboBox1.Location = new System.Drawing.Point(107, 4);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(72, 24);
            this.comboBox1.TabIndex = 15;
            // 
            // btnQuickRun
            // 
            this.btnQuickRun.Location = new System.Drawing.Point(261, 1);
            this.btnQuickRun.Margin = new System.Windows.Forms.Padding(4);
            this.btnQuickRun.Name = "btnQuickRun";
            this.btnQuickRun.Size = new System.Drawing.Size(72, 28);
            this.btnQuickRun.TabIndex = 16;
            this.btnQuickRun.Text = "&Run";
            this.btnQuickRun.UseVisualStyleBackColor = true;
            this.btnQuickRun.Click += new System.EventHandler(this.btnQuickRun_Click);
            // 
            // quickPanel1
            // 
            this.quickPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.quickPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.quickPanel1.Controls.Add(this.lblQuick);
            this.quickPanel1.Controls.Add(this.comboBox1);
            this.quickPanel1.Controls.Add(this.btnQuickRun);
            this.quickPanel1.Controls.Add(this.btnQuickSave);
            this.quickPanel1.Location = new System.Drawing.Point(431, 408);
            this.quickPanel1.Name = "quickPanel1";
            this.quickPanel1.Size = new System.Drawing.Size(339, 31);
            this.quickPanel1.TabIndex = 19;
            this.quickPanel1.Tag = "Quick";
            // 
            // lblQuick
            // 
            this.lblQuick.AutoSize = true;
            this.lblQuick.Location = new System.Drawing.Point(4, 7);
            this.lblQuick.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQuick.Name = "lblQuick";
            this.lblQuick.Size = new System.Drawing.Size(92, 17);
            this.lblQuick.TabIndex = 17;
            this.lblQuick.Text = "Assign # key:";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // richTextBox2
            // 
            this.richTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox2.Font = new System.Drawing.Font("Lucida Console", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox2.Location = new System.Drawing.Point(172, 33);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(477, 371);
            this.richTextBox2.TabIndex = 8;
            this.richTextBox2.Text = "";
            this.richTextBox2.Enter += new System.EventHandler(this.RichTextBoxEnter);
            // 
            // btn2Clipboard
            // 
            this.btn2Clipboard.Location = new System.Drawing.Point(269, 413);
            this.btn2Clipboard.Name = "btn2Clipboard";
            this.btn2Clipboard.Size = new System.Drawing.Size(75, 28);
            this.btn2Clipboard.TabIndex = 20;
            this.btn2Clipboard.Text = "&2Clipb";
            this.btn2Clipboard.UseVisualStyleBackColor = true;
            this.btn2Clipboard.Click += new System.EventHandler(this.btn2Clipboard_Click);
            // 
            // CodeBow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 453);
            this.Controls.Add(this.btn2Clipboard);
            this.Controls.Add(this.quickPanel1);
            this.Controls.Add(this.panelEdit);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPaste);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Name = "CodeBow";
            this.Text = "CodeBow";
            this.Activated += new System.EventHandler(this.FormActivated);
            this.Deactivate += new System.EventHandler(this.FormDeactivate);
            this.Shown += new System.EventHandler(this.FormShown);
            this.SizeChanged += new System.EventHandler(this.CodeBow_SizeChanged);
            this.VisibleChanged += new System.EventHandler(this.FormVisibleChanged);
            this.panelEdit.ResumeLayout(false);
            this.panelEdit.PerformLayout();
            this.quickPanel1.ResumeLayout(false);
            this.quickPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private CodingHood.CodeRichTextBox richTextBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnPaste;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Panel panelEdit;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnToFields;
        private System.Windows.Forms.Button btnToField;
        private System.Windows.Forms.Button btnToPswd;
        private System.Windows.Forms.Button btnCoordsToClipboard;
        private System.Windows.Forms.Button btnContentTag;
        private System.Windows.Forms.Button btnQuickSave;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnQuickRun;
        private System.Windows.Forms.Panel quickPanel1;
        private System.Windows.Forms.Label lblQuick;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.CheckBox chkFullscreen;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btn2Clipboard;
    }
}

