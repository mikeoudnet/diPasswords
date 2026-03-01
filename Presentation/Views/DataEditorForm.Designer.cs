namespace diPasswords.Presentation.Views
{
    partial class DataEditorForm
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
            NameTextBox = new TextBox();
            NameLbl = new Label();
            PhoneLbl = new Label();
            PhoneTextBox = new TextBox();
            EmailLbl = new Label();
            EmailTextBox = new TextBox();
            LoginLbl = new Label();
            LoginTextBox = new TextBox();
            PasswordLbl = new Label();
            PasswordTextBox = new TextBox();
            DescriptionLbl = new Label();
            DescriptionTextBox = new TextBox();
            Neccessary1Lbl = new Label();
            Neccessary2Lbl = new Label();
            SetFavoriteBttn = new Button();
            AcceptBttn = new Button();
            ShowPasswordBttn = new Button();
            DescriptionLengthLbl = new Label();
            SuspendLayout();
            // 
            // NameTextBox
            // 
            NameTextBox.Location = new Point(77, 15);
            NameTextBox.MaxLength = 30;
            NameTextBox.Name = "NameTextBox";
            NameTextBox.Size = new Size(263, 31);
            NameTextBox.TabIndex = 0;
            NameTextBox.KeyPress += NameTextBox_KeyPress;
            // 
            // NameLbl
            // 
            NameLbl.AutoSize = true;
            NameLbl.Location = new Point(12, 15);
            NameLbl.Name = "NameLbl";
            NameLbl.Size = new Size(59, 25);
            NameLbl.TabIndex = 1;
            NameLbl.Text = "Name";
            // 
            // PhoneLbl
            // 
            PhoneLbl.AutoSize = true;
            PhoneLbl.Location = new Point(12, 199);
            PhoneLbl.Name = "PhoneLbl";
            PhoneLbl.Size = new Size(62, 25);
            PhoneLbl.TabIndex = 3;
            PhoneLbl.Text = "Phone";
            // 
            // PhoneTextBox
            // 
            PhoneTextBox.Location = new Point(108, 196);
            PhoneTextBox.MaxLength = 30;
            PhoneTextBox.Name = "PhoneTextBox";
            PhoneTextBox.Size = new Size(275, 31);
            PhoneTextBox.TabIndex = 6;
            PhoneTextBox.KeyPress += PhoneTextBox_KeyPress;
            // 
            // EmailLbl
            // 
            EmailLbl.AutoSize = true;
            EmailLbl.Location = new Point(12, 162);
            EmailLbl.Name = "EmailLbl";
            EmailLbl.Size = new Size(61, 25);
            EmailLbl.TabIndex = 5;
            EmailLbl.Text = "E-mail";
            // 
            // EmailTextBox
            // 
            EmailTextBox.Location = new Point(108, 159);
            EmailTextBox.MaxLength = 40;
            EmailTextBox.Name = "EmailTextBox";
            EmailTextBox.Size = new Size(275, 31);
            EmailTextBox.TabIndex = 5;
            EmailTextBox.KeyPress += EmailTextBox_KeyPress;
            // 
            // LoginLbl
            // 
            LoginLbl.AutoSize = true;
            LoginLbl.Location = new Point(324, 75);
            LoginLbl.Name = "LoginLbl";
            LoginLbl.Size = new Size(56, 25);
            LoginLbl.TabIndex = 7;
            LoginLbl.Text = "Login";
            // 
            // LoginTextBox
            // 
            LoginTextBox.Location = new Point(12, 72);
            LoginTextBox.MaxLength = 30;
            LoginTextBox.Name = "LoginTextBox";
            LoginTextBox.Size = new Size(263, 31);
            LoginTextBox.TabIndex = 2;
            LoginTextBox.KeyPress += LoginTextBox_KeyPress;
            // 
            // PasswordLbl
            // 
            PasswordLbl.AutoSize = true;
            PasswordLbl.Location = new Point(293, 112);
            PasswordLbl.Name = "PasswordLbl";
            PasswordLbl.Size = new Size(87, 25);
            PasswordLbl.TabIndex = 9;
            PasswordLbl.Text = "Password";
            // 
            // PasswordTextBox
            // 
            PasswordTextBox.Location = new Point(92, 109);
            PasswordTextBox.MaxLength = 30;
            PasswordTextBox.Name = "PasswordTextBox";
            PasswordTextBox.PasswordChar = '•';
            PasswordTextBox.Size = new Size(183, 31);
            PasswordTextBox.TabIndex = 3;
            PasswordTextBox.TextChanged += PasswordTextBox_TextChanged;
            PasswordTextBox.KeyPress += PasswordTextBox_KeyPress;
            // 
            // DescriptionLbl
            // 
            DescriptionLbl.AutoSize = true;
            DescriptionLbl.Location = new Point(278, 233);
            DescriptionLbl.Name = "DescriptionLbl";
            DescriptionLbl.Size = new Size(102, 25);
            DescriptionLbl.TabIndex = 11;
            DescriptionLbl.Text = "Description";
            // 
            // DescriptionTextBox
            // 
            DescriptionTextBox.Location = new Point(12, 250);
            DescriptionTextBox.MaxLength = 200;
            DescriptionTextBox.Multiline = true;
            DescriptionTextBox.Name = "DescriptionTextBox";
            DescriptionTextBox.ScrollBars = ScrollBars.Vertical;
            DescriptionTextBox.Size = new Size(371, 101);
            DescriptionTextBox.TabIndex = 7;
            DescriptionTextBox.TextChanged += DescriptionTextBox_TextChanged;
            DescriptionTextBox.KeyPress += DescriptionTextBox_KeyPress;
            // 
            // Neccessary1Lbl
            // 
            Neccessary1Lbl.AutoSize = true;
            Neccessary1Lbl.Font = new Font("Segoe UI", 9F);
            Neccessary1Lbl.ForeColor = Color.DarkRed;
            Neccessary1Lbl.Location = new Point(275, 109);
            Neccessary1Lbl.Name = "Neccessary1Lbl";
            Neccessary1Lbl.Size = new Size(20, 25);
            Neccessary1Lbl.TabIndex = 12;
            Neccessary1Lbl.Text = "*";
            // 
            // Neccessary2Lbl
            // 
            Neccessary2Lbl.AutoSize = true;
            Neccessary2Lbl.Font = new Font("Segoe UI", 9F);
            Neccessary2Lbl.ForeColor = Color.DarkRed;
            Neccessary2Lbl.Location = new Point(275, 72);
            Neccessary2Lbl.Name = "Neccessary2Lbl";
            Neccessary2Lbl.Size = new Size(20, 25);
            Neccessary2Lbl.TabIndex = 13;
            Neccessary2Lbl.Text = "*";
            // 
            // SetFavoriteBttn
            // 
            SetFavoriteBttn.Location = new Point(346, 13);
            SetFavoriteBttn.Name = "SetFavoriteBttn";
            SetFavoriteBttn.Size = new Size(34, 34);
            SetFavoriteBttn.TabIndex = 1;
            SetFavoriteBttn.Text = "☆";
            SetFavoriteBttn.UseVisualStyleBackColor = true;
            SetFavoriteBttn.Click += SetFavoriteBttn_Click;
            // 
            // AcceptBttn
            // 
            AcceptBttn.Location = new Point(271, 357);
            AcceptBttn.Name = "AcceptBttn";
            AcceptBttn.Size = new Size(112, 34);
            AcceptBttn.TabIndex = 8;
            AcceptBttn.Text = "Accept";
            AcceptBttn.UseVisualStyleBackColor = true;
            AcceptBttn.Click += AcceptBttn_Click;
            // 
            // ShowPasswordBttn
            // 
            ShowPasswordBttn.Enabled = false;
            ShowPasswordBttn.Location = new Point(12, 109);
            ShowPasswordBttn.Name = "ShowPasswordBttn";
            ShowPasswordBttn.Size = new Size(74, 34);
            ShowPasswordBttn.TabIndex = 4;
            ShowPasswordBttn.Text = "Show";
            ShowPasswordBttn.UseVisualStyleBackColor = true;
            ShowPasswordBttn.Click += ShowPasswordBttn_Click;
            // 
            // DescriptionLengthLbl
            // 
            DescriptionLengthLbl.AutoSize = true;
            DescriptionLengthLbl.ForeColor = SystemColors.ControlDark;
            DescriptionLengthLbl.Location = new Point(14, 233);
            DescriptionLengthLbl.Name = "DescriptionLengthLbl";
            DescriptionLengthLbl.Size = new Size(79, 25);
            DescriptionLengthLbl.TabIndex = 14;
            DescriptionLengthLbl.Text = "000/200";
            // 
            // DataEditorForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(395, 403);
            Controls.Add(DescriptionLengthLbl);
            Controls.Add(ShowPasswordBttn);
            Controls.Add(AcceptBttn);
            Controls.Add(SetFavoriteBttn);
            Controls.Add(Neccessary2Lbl);
            Controls.Add(Neccessary1Lbl);
            Controls.Add(DescriptionLbl);
            Controls.Add(DescriptionTextBox);
            Controls.Add(PasswordLbl);
            Controls.Add(PasswordTextBox);
            Controls.Add(LoginLbl);
            Controls.Add(LoginTextBox);
            Controls.Add(EmailLbl);
            Controls.Add(EmailTextBox);
            Controls.Add(PhoneLbl);
            Controls.Add(PhoneTextBox);
            Controls.Add(NameLbl);
            Controls.Add(NameTextBox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "DataEditorForm";
            Text = "Add data";
            Load += DataEditorForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox NameTextBox;
        private Label NameLbl;
        private Label PhoneLbl;
        private TextBox PhoneTextBox;
        private Label EmailLbl;
        private TextBox EmailTextBox;
        private Label LoginLbl;
        private TextBox LoginTextBox;
        private Label PasswordLbl;
        private TextBox PasswordTextBox;
        private Label DescriptionLbl;
        private TextBox DescriptionTextBox;
        private Label Neccessary1Lbl;
        private Label Neccessary2Lbl;
        private Button SetFavoriteBttn;
        private Button AcceptBttn;
        private Button ShowPasswordBttn;
        private Label DescriptionLengthLbl;
    }
}