namespace diPasswords
{
    partial class diPasswordsForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            AuthorizationBox = new GroupBox();
            AddBaseAccountBttn = new Button();
            DeleteBaseAccountBttn = new Button();
            EnterBaseAccountBttn = new Button();
            ShowBasePasswordBttn = new Button();
            BaseLoginLabel = new Label();
            BasePasswordLabel = new Label();
            BasePasswordTextBox = new TextBox();
            BaseLoginTextBox = new TextBox();
            DataListBox = new ListBox();
            AddDataBttn = new Button();
            EditDataBttn = new Button();
            DeleteDataBttn = new Button();
            SelectedDataBox = new GroupBox();
            DescriptionLbl = new Label();
            DataDescriptionTextBox = new TextBox();
            EmailLbl = new Label();
            DataEmailTextBox = new TextBox();
            PhoneLbl = new Label();
            DataPhoneTextBox = new TextBox();
            LoginLbl = new Label();
            DataLoginTextBox = new TextBox();
            PasswordLbl = new Label();
            DataNameLbl = new Label();
            DataPasswordTextBox = new TextBox();
            LoggerListView = new ListView();
            ShowDataBttn = new Button();
            AuthorizationBox.SuspendLayout();
            SelectedDataBox.SuspendLayout();
            SuspendLayout();
            // 
            // AuthorizationBox
            // 
            AuthorizationBox.Controls.Add(AddBaseAccountBttn);
            AuthorizationBox.Controls.Add(DeleteBaseAccountBttn);
            AuthorizationBox.Controls.Add(EnterBaseAccountBttn);
            AuthorizationBox.Controls.Add(ShowBasePasswordBttn);
            AuthorizationBox.Controls.Add(BaseLoginLabel);
            AuthorizationBox.Controls.Add(BasePasswordLabel);
            AuthorizationBox.Controls.Add(BasePasswordTextBox);
            AuthorizationBox.Controls.Add(BaseLoginTextBox);
            AuthorizationBox.Location = new Point(398, 374);
            AuthorizationBox.Name = "AuthorizationBox";
            AuthorizationBox.Size = new Size(383, 184);
            AuthorizationBox.TabIndex = 0;
            AuthorizationBox.TabStop = false;
            AuthorizationBox.Text = "Enter your key here";
            // 
            // AddBaseAccountBttn
            // 
            AddBaseAccountBttn.Enabled = false;
            AddBaseAccountBttn.Location = new Point(182, 136);
            AddBaseAccountBttn.Name = "AddBaseAccountBttn";
            AddBaseAccountBttn.Size = new Size(88, 34);
            AddBaseAccountBttn.TabIndex = 4;
            AddBaseAccountBttn.Text = "Add";
            AddBaseAccountBttn.UseVisualStyleBackColor = true;
            AddBaseAccountBttn.Click += AddBaseAccountBttn_Click;
            // 
            // DeleteBaseAccountBttn
            // 
            DeleteBaseAccountBttn.Enabled = false;
            DeleteBaseAccountBttn.Location = new Point(276, 136);
            DeleteBaseAccountBttn.Name = "DeleteBaseAccountBttn";
            DeleteBaseAccountBttn.Size = new Size(88, 34);
            DeleteBaseAccountBttn.TabIndex = 5;
            DeleteBaseAccountBttn.Text = "Delete";
            DeleteBaseAccountBttn.UseVisualStyleBackColor = true;
            DeleteBaseAccountBttn.Click += DeleteBaseAccountBttn_Click;
            // 
            // EnterBaseAccountBttn
            // 
            EnterBaseAccountBttn.Enabled = false;
            EnterBaseAccountBttn.Location = new Point(15, 136);
            EnterBaseAccountBttn.Name = "EnterBaseAccountBttn";
            EnterBaseAccountBttn.Size = new Size(110, 34);
            EnterBaseAccountBttn.TabIndex = 3;
            EnterBaseAccountBttn.Text = "Enter";
            EnterBaseAccountBttn.UseVisualStyleBackColor = true;
            EnterBaseAccountBttn.Click += EnterBaseAccountBttn_Click;
            // 
            // ShowBasePasswordBttn
            // 
            ShowBasePasswordBttn.Enabled = false;
            ShowBasePasswordBttn.Location = new Point(297, 99);
            ShowBasePasswordBttn.Name = "ShowBasePasswordBttn";
            ShowBasePasswordBttn.Size = new Size(67, 31);
            ShowBasePasswordBttn.TabIndex = 2;
            ShowBasePasswordBttn.Text = "Show";
            ShowBasePasswordBttn.UseVisualStyleBackColor = true;
            ShowBasePasswordBttn.Click += ShowBasePasswordBttn_Click;
            // 
            // BaseLoginLabel
            // 
            BaseLoginLabel.AutoSize = true;
            BaseLoginLabel.Location = new Point(15, 29);
            BaseLoginLabel.Name = "BaseLoginLabel";
            BaseLoginLabel.Size = new Size(92, 25);
            BaseLoginLabel.TabIndex = 3;
            BaseLoginLabel.Text = "User login";
            // 
            // BasePasswordLabel
            // 
            BasePasswordLabel.AutoSize = true;
            BasePasswordLabel.Location = new Point(15, 80);
            BasePasswordLabel.Name = "BasePasswordLabel";
            BasePasswordLabel.Size = new Size(129, 25);
            BasePasswordLabel.TabIndex = 2;
            BasePasswordLabel.Text = "User password";
            // 
            // BasePasswordTextBox
            // 
            BasePasswordTextBox.Location = new Point(15, 99);
            BasePasswordTextBox.MaxLength = 30;
            BasePasswordTextBox.Name = "BasePasswordTextBox";
            BasePasswordTextBox.PasswordChar = '•';
            BasePasswordTextBox.Size = new Size(276, 31);
            BasePasswordTextBox.TabIndex = 1;
            BasePasswordTextBox.TextChanged += BasePasswordTextBox_TextChanged;
            BasePasswordTextBox.KeyPress += BasePasswordTextBox_KeyPress;
            // 
            // BaseLoginTextBox
            // 
            BaseLoginTextBox.Location = new Point(15, 45);
            BaseLoginTextBox.MaxLength = 30;
            BaseLoginTextBox.Name = "BaseLoginTextBox";
            BaseLoginTextBox.Size = new Size(349, 31);
            BaseLoginTextBox.TabIndex = 0;
            BaseLoginTextBox.TextChanged += BaseLoginTextBox_TextChanged;
            BaseLoginTextBox.KeyPress += BaseLoginTextBox_KeyPress;
            // 
            // DataListBox
            // 
            DataListBox.FormattingEnabled = true;
            DataListBox.ItemHeight = 25;
            DataListBox.Location = new Point(12, 12);
            DataListBox.Name = "DataListBox";
            DataListBox.Size = new Size(202, 304);
            DataListBox.TabIndex = 6;
            DataListBox.SelectedIndexChanged += DataListBox_SelectedIndexChanged;
            DataListBox.DoubleClick += DataListBox_DoubleClick;
            DataListBox.KeyPress += DataListBox_KeyPress;
            // 
            // AddDataBttn
            // 
            AddDataBttn.Enabled = false;
            AddDataBttn.Location = new Point(241, 12);
            AddDataBttn.Name = "AddDataBttn";
            AddDataBttn.Size = new Size(169, 34);
            AddDataBttn.TabIndex = 8;
            AddDataBttn.Text = "Add new";
            AddDataBttn.UseVisualStyleBackColor = true;
            AddDataBttn.Click += AddDataBttn_Click;
            // 
            // EditDataBttn
            // 
            EditDataBttn.Enabled = false;
            EditDataBttn.Location = new Point(426, 12);
            EditDataBttn.Name = "EditDataBttn";
            EditDataBttn.Size = new Size(169, 34);
            EditDataBttn.TabIndex = 9;
            EditDataBttn.Text = "Edit current";
            EditDataBttn.UseVisualStyleBackColor = true;
            EditDataBttn.Click += EditDataBttn_Click;
            // 
            // DeleteDataBttn
            // 
            DeleteDataBttn.Enabled = false;
            DeleteDataBttn.Location = new Point(612, 12);
            DeleteDataBttn.Name = "DeleteDataBttn";
            DeleteDataBttn.Size = new Size(169, 34);
            DeleteDataBttn.TabIndex = 10;
            DeleteDataBttn.Text = "Delete";
            DeleteDataBttn.UseVisualStyleBackColor = true;
            DeleteDataBttn.Click += DeleteDataBttn_Click;
            // 
            // SelectedDataBox
            // 
            SelectedDataBox.Controls.Add(DescriptionLbl);
            SelectedDataBox.Controls.Add(DataDescriptionTextBox);
            SelectedDataBox.Controls.Add(EmailLbl);
            SelectedDataBox.Controls.Add(DataEmailTextBox);
            SelectedDataBox.Controls.Add(PhoneLbl);
            SelectedDataBox.Controls.Add(DataPhoneTextBox);
            SelectedDataBox.Controls.Add(LoginLbl);
            SelectedDataBox.Controls.Add(DataLoginTextBox);
            SelectedDataBox.Controls.Add(PasswordLbl);
            SelectedDataBox.Controls.Add(DataNameLbl);
            SelectedDataBox.Controls.Add(DataPasswordTextBox);
            SelectedDataBox.Location = new Point(241, 52);
            SelectedDataBox.Name = "SelectedDataBox";
            SelectedDataBox.Size = new Size(540, 304);
            SelectedDataBox.TabIndex = 5;
            SelectedDataBox.TabStop = false;
            SelectedDataBox.Text = "Selected data";
            // 
            // DescriptionLbl
            // 
            DescriptionLbl.AutoSize = true;
            DescriptionLbl.Location = new Point(25, 176);
            DescriptionLbl.Name = "DescriptionLbl";
            DescriptionLbl.Size = new Size(102, 25);
            DescriptionLbl.TabIndex = 15;
            DescriptionLbl.Text = "Description";
            // 
            // DataDescriptionTextBox
            // 
            DataDescriptionTextBox.Location = new Point(25, 192);
            DataDescriptionTextBox.MaxLength = 30;
            DataDescriptionTextBox.Multiline = true;
            DataDescriptionTextBox.Name = "DataDescriptionTextBox";
            DataDescriptionTextBox.ReadOnly = true;
            DataDescriptionTextBox.ScrollBars = ScrollBars.Vertical;
            DataDescriptionTextBox.Size = new Size(496, 92);
            DataDescriptionTextBox.TabIndex = 14;
            DataDescriptionTextBox.TabStop = false;
            // 
            // EmailLbl
            // 
            EmailLbl.AutoSize = true;
            EmailLbl.Location = new Point(25, 116);
            EmailLbl.Name = "EmailLbl";
            EmailLbl.Size = new Size(61, 25);
            EmailLbl.TabIndex = 13;
            EmailLbl.Text = "E-mail";
            // 
            // DataEmailTextBox
            // 
            DataEmailTextBox.Location = new Point(25, 132);
            DataEmailTextBox.MaxLength = 30;
            DataEmailTextBox.Name = "DataEmailTextBox";
            DataEmailTextBox.ReadOnly = true;
            DataEmailTextBox.Size = new Size(245, 31);
            DataEmailTextBox.TabIndex = 12;
            DataEmailTextBox.TabStop = false;
            DataEmailTextBox.MouseDown += DataEmailTextBox_MouseDown;
            // 
            // PhoneLbl
            // 
            PhoneLbl.AutoSize = true;
            PhoneLbl.Location = new Point(459, 116);
            PhoneLbl.Name = "PhoneLbl";
            PhoneLbl.Size = new Size(62, 25);
            PhoneLbl.TabIndex = 11;
            PhoneLbl.Text = "Phone";
            // 
            // DataPhoneTextBox
            // 
            DataPhoneTextBox.Location = new Point(276, 132);
            DataPhoneTextBox.MaxLength = 30;
            DataPhoneTextBox.Name = "DataPhoneTextBox";
            DataPhoneTextBox.ReadOnly = true;
            DataPhoneTextBox.Size = new Size(245, 31);
            DataPhoneTextBox.TabIndex = 10;
            DataPhoneTextBox.TabStop = false;
            DataPhoneTextBox.MouseDown += DataPhoneTextBox_MouseDown;
            // 
            // LoginLbl
            // 
            LoginLbl.AutoSize = true;
            LoginLbl.Location = new Point(25, 60);
            LoginLbl.Name = "LoginLbl";
            LoginLbl.Size = new Size(56, 25);
            LoginLbl.TabIndex = 9;
            LoginLbl.Text = "Login";
            // 
            // DataLoginTextBox
            // 
            DataLoginTextBox.Location = new Point(25, 76);
            DataLoginTextBox.MaxLength = 30;
            DataLoginTextBox.Name = "DataLoginTextBox";
            DataLoginTextBox.ReadOnly = true;
            DataLoginTextBox.Size = new Size(245, 31);
            DataLoginTextBox.TabIndex = 8;
            DataLoginTextBox.TabStop = false;
            DataLoginTextBox.MouseDown += DataLoginTextBox_MouseDown;
            // 
            // PasswordLbl
            // 
            PasswordLbl.AutoSize = true;
            PasswordLbl.Location = new Point(434, 60);
            PasswordLbl.Name = "PasswordLbl";
            PasswordLbl.Size = new Size(87, 25);
            PasswordLbl.TabIndex = 7;
            PasswordLbl.Text = "Password";
            // 
            // DataNameLbl
            // 
            DataNameLbl.AutoSize = true;
            DataNameLbl.Location = new Point(339, 27);
            DataNameLbl.Name = "DataNameLbl";
            DataNameLbl.Size = new Size(129, 25);
            DataNameLbl.TabIndex = 0;
            DataNameLbl.Text = "<No selected>";
            // 
            // DataPasswordTextBox
            // 
            DataPasswordTextBox.Location = new Point(276, 76);
            DataPasswordTextBox.MaxLength = 30;
            DataPasswordTextBox.Name = "DataPasswordTextBox";
            DataPasswordTextBox.ReadOnly = true;
            DataPasswordTextBox.Size = new Size(245, 31);
            DataPasswordTextBox.TabIndex = 6;
            DataPasswordTextBox.TabStop = false;
            DataPasswordTextBox.MouseDown += DataPasswordTextBox_MouseDown;
            // 
            // LoggerListView
            // 
            LoggerListView.Font = new Font("Segoe UI", 8.5F);
            LoggerListView.Location = new Point(12, 374);
            LoggerListView.Name = "LoggerListView";
            LoggerListView.Size = new Size(368, 184);
            LoggerListView.TabIndex = 0;
            LoggerListView.TabStop = false;
            LoggerListView.TileSize = new Size(342, 50);
            LoggerListView.UseCompatibleStateImageBehavior = false;
            LoggerListView.View = View.Tile;
            // 
            // ShowDataBttn
            // 
            ShowDataBttn.Enabled = false;
            ShowDataBttn.Location = new Point(12, 322);
            ShowDataBttn.Name = "ShowDataBttn";
            ShowDataBttn.Size = new Size(202, 34);
            ShowDataBttn.TabIndex = 7;
            ShowDataBttn.Text = "Show data";
            ShowDataBttn.UseVisualStyleBackColor = true;
            ShowDataBttn.Click += ShowDataBttn_Click;
            // 
            // diPasswordsForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(793, 570);
            Controls.Add(ShowDataBttn);
            Controls.Add(LoggerListView);
            Controls.Add(SelectedDataBox);
            Controls.Add(DeleteDataBttn);
            Controls.Add(EditDataBttn);
            Controls.Add(AddDataBttn);
            Controls.Add(DataListBox);
            Controls.Add(AuthorizationBox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "diPasswordsForm";
            Text = "diPasswords";
            Load += diPasswords_Load;
            AuthorizationBox.ResumeLayout(false);
            AuthorizationBox.PerformLayout();
            SelectedDataBox.ResumeLayout(false);
            SelectedDataBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox AuthorizationBox;
        private Label BasePasswordLabel;
        private TextBox BasePasswordTextBox;
        private TextBox BaseLoginTextBox;
        private Label BaseLoginLabel;
        private Button ShowBasePasswordBttn;
        private ListBox DataListBox;
        private Button AddBaseAccountBttn;
        private Button DeleteBaseAccountBttn;
        private Button EnterBaseAccountBttn;
        private Button AddDataBttn;
        private Button EditDataBttn;
        private Button DeleteDataBttn;
        private GroupBox SelectedDataBox;
        private ListView LoggerListView;
        private Label PasswordLbl;
        private Label DataNameLbl;
        private TextBox DataPasswordTextBox;
        private Label LoginLbl;
        private TextBox DataLoginTextBox;
        private Label DescriptionLbl;
        private TextBox DataDescriptionTextBox;
        private Label EmailLbl;
        private TextBox DataEmailTextBox;
        private Label PhoneLbl;
        private TextBox DataPhoneTextBox;
        private Button ShowDataBttn;
    }
}
