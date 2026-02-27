using diPasswords.Domain.Models;
using diPasswords.Application.Interfaces;
using diPasswords.Presentation.Managers;
using diPasswords.Domain.Enums;
using diPasswords.Presentation.Views;

namespace diPasswords
{
    // Common app form
    public partial class diPasswordsForm : Form
    {
        private CurrentState _currentState = CurrentState.Authorizating; // Value keeping current UI-state status

        private ILogger _logger; // Logging to separate element
        private IUserService _userService; // Database working responsible for user logins and passwords keeping
        private IValidator _validator; // Inputting symbols checking for local rules appropriating
        private IElementView _elementView; // Class containging elements sets, neccessary to editting at the same time
        private IDataValidator _dataValidator; // Inputting data validation
        private IDataService _dataService; // User data interaction
        private IDataListShower _dataListShower; // User data visualization
        private IEncrypter _encrypter; // Data encrypting and decrypting

        // Element enabling by neccessary and their parameter edditing
        // Objects linking to appropriate element
        private IElementController<Control> _showBasePasswordController;
        private IElementController<Control> _enterBaseAccountController;
        private IElementController<Control> _addBaseAccountController;
        private IElementController<Control> _deleteBaseAccountController;

        private IElementController<Control> _addDataController;
        private IElementController<Control> _editDataController;
        private IElementController<Control> _deleteDataController;
        private IElementController<Control> _showDataController;

        private IElementController<Control> _baseLoginController;
        private IElementController<Control> _basePasswordController;

        /// <summary>
        /// State changing depending on current script
        /// </summary>
        /// <param name="type"></param>
        private void Confirmition(CurrentState type)
        {
            _currentState = type;
            switch (type)
            {
                case CurrentState.AddConfirming: // Password confirming by user adding
                case CurrentState.DeleteConfirming: // Passord confirming by user deleting
                    ClearPasswordField();
                    Deactivate();

                    _elementView.Switch((type == CurrentState.AddConfirming) ? ElementMode.Adding : ElementMode.Deleting, false);
                    _elementView.CurrentPool((type == CurrentState.AddConfirming) ? ElementMode.Adding : ElementMode.Deleting);
                    BasePasswordTextBox.PlaceholderText = "Confirm password";

                    break;
                case CurrentState.Authorizating: // User authorization script
                case CurrentState.LoggedIn: // User logged in script
                    Deactivate();

                    if (type == CurrentState.Authorizating) _elementView.Switch(ElementMode.Authorization, false);
                    else _elementView.Switch(ElementMode.Using, true);
                    _elementView.CurrentPool(ElementMode.None);
                    BasePasswordTextBox.PlaceholderText = "";

                    if (type == CurrentState.Authorizating) ClearPasswordField();

                    break;
                case CurrentState.BackToAuthorization: // Password confirming switching to authorization
                    _currentState = CurrentState.Authorizating;

                    _elementView.Switch(ElementMode.Using, true);
                    _elementView.CurrentPool(ElementMode.None);
                    BasePasswordTextBox.PlaceholderText = "";

                    break;
            }
        }
        /// <summary>
        /// Log outputting to ListView
        /// </summary>
        /// <param name="logEntry"></param>
        private void ListViewLog(LogEntry logEntry)
        {
            ListViewItem item = new ListViewItem("[" + DateTime.Now.ToString() + "]: " + logEntry.message);

            switch (logEntry.level)
            {
                case LogLevel.Info:
                    item.ForeColor = Color.Black;

                    break;
                case LogLevel.Warn:
                    item.ForeColor = Color.DarkGoldenrod;

                    break;
                case LogLevel.Error:
                    item.ForeColor = Color.DarkRed;

                    break;
                case LogLevel.CurrentError:
                    item.ForeColor = Color.DarkRed;
                    MessageBox.Show(logEntry.message, "Inputting error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    break;
                case LogLevel.FatalError:
                    item.ForeColor = Color.DarkRed;
                    foreach (Control control in this.Controls) control.Enabled = false;

                    MessageBox.Show(logEntry.message, "Fatal error (program is finished)", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    break;
            }

            LoggerListView.Items.Add(item);
            item.EnsureVisible();
        }
        /// <summary>
        /// DataTextBox updating
        /// </summary>
        /// <param name="dataList"></param>
        private void ListBoxUpdate(List<Data> dataList)
        {
            DataListBox.Items.Clear();

            if (dataList != null)
            {
                foreach (Data data in dataList)
                {
                    string line = data.Name;
                    if (data.Favorite) line = "★ " + line;

                    DataListBox.Items.Add(line);
                }
            }
        }
        /// <summary>
        /// Button "Show password" deactivating
        /// </summary>
        public void Deactivate()
        {
            if (BasePasswordTextBox.PasswordChar == '\0') ShowBasePasswordBttn.PerformClick();
            ShowBasePasswordBttn.Enabled = false;
        }
        /// <summary>
        /// ListBox focus setting on the data
        /// </summary>
        /// <param name="name"></param>
        private void ListBoxFocus(string name) => DataListBox.SelectedIndex = DataListBox.Items.IndexOf(name);
        /// <summary>
        /// Password TextBox clearing
        /// </summary>
        private void ClearPasswordField() => BasePasswordTextBox.Text = "";
        /// <summary>
        /// Chosed data visualization
        /// </summary>
        /// <param name="activate"></param>
        private void ShowSelectedData(bool activate)
        {
            if (activate)
            {
                string name = ConvertSelected();
                Data data = _dataService.GetSelectedData(name);

                DataNameLbl.Text = data.Name;
                DataLoginTextBox.Text = data.Login;
                DataPasswordTextBox.Text = data.Password;
                DataEmailTextBox.Text = data.Email;
                DataPhoneTextBox.Text = data.Phone;
                DataDescriptionTextBox.Text = data.Description;

                _showDataController.Retext(true);
            }
            else
            {
                DataNameLbl.Text = "<No selected>";
                DataLoginTextBox.Text = "";
                DataPasswordTextBox.Text = "";
                DataEmailTextBox.Text = "";
                DataPhoneTextBox.Text = "";
                DataDescriptionTextBox.Text = "";

                _showDataController.Retext(false);
            }
        }
        /// <summary>
        /// Choosed ListBox element transformation to string
        /// </summary>
        /// <returns></returns>
        private string ConvertSelected()
        {
            if (DataListBox.SelectedItem != null)
            {
                if (DataListBox.SelectedItem.ToString().Contains("★")) return DataListBox.SelectedItem.ToString().Substring(2, DataListBox.SelectedItem.ToString().Length - 2);
                else return DataListBox.SelectedItem.ToString();
            }
            else return "";
        }

        public diPasswordsForm(
            ILogger logger,
            IUserService userService,
            IValidator validator,
            IElementView elementView,
            IDataValidator dataValidator,
            IDataService dataService,
            IDataListShower dataListShower,
            IEncrypter encrypter)
        {
            InitializeComponent();

            // Dependencies injection
            _logger = logger;
            _userService = userService;
            _validator = validator;
            _elementView = elementView;
            _dataValidator = dataValidator;
            _dataService = dataService;
            _dataListShower = dataListShower;
            _encrypter = encrypter;

            _showBasePasswordController = new ElementController<Control>(ShowBasePasswordBttn, false, "Hide");
            _enterBaseAccountController = new ElementController<Control>(EnterBaseAccountBttn, false, "Log out");
            _addBaseAccountController = new ElementController<Control>(AddBaseAccountBttn, false);
            _deleteBaseAccountController = new ElementController<Control>(DeleteBaseAccountBttn, false);

            _addDataController = new ElementController<Control>(AddDataBttn, false);
            _editDataController = new ElementController<Control>(EditDataBttn, false);
            _deleteDataController = new ElementController<Control>(DeleteDataBttn, false);
            _showDataController = new ElementController<Control>(ShowDataBttn, false, "Hide data");

            _baseLoginController = new ElementController<Control>(BaseLoginTextBox, true);
            _basePasswordController = new ElementController<Control>(BasePasswordTextBox, true);

            // Elements group adding
            _elementView.AddPool
            (
                ElementMode.Authorization, new ElementController<Control>(EnterBaseAccountBttn, false),
                _addBaseAccountController,
                _deleteBaseAccountController
            ); // Edits a state by non-empty login and password fields
            _elementView.AddPool
            (
                ElementMode.Using, _enterBaseAccountController,
                new ElementController<Control>(AddBaseAccountBttn, true),
                new ElementController<Control>(DeleteBaseAccountBttn, true),
                _addDataController,
                _baseLoginController,
                _basePasswordController
            ); // Edits a state by successful authorization
            _elementView.AddPool
            (
                ElementMode.Adding, _addBaseAccountController
            ); // Edits a state for password confirming by user adding time
            _elementView.AddPool
            (
                ElementMode.Deleting, _deleteBaseAccountController
            ); // Edits a state for password confirming by user deleting time
            _elementView.AddPool
            (
                ElementMode.DataSelected, new ElementController<Control>(ShowDataBttn, false),
                _editDataController,
                _deleteDataController
            ); // Edits a state by choosed element in TextBox

            // Objects events subscribings
            _logger.OnLog += ListViewLog;
            _dataListShower.OnList += ListBoxUpdate;
            _dataListShower.OnListCursor += ListBoxFocus;

            _logger.Info("Programm start");

            _userService.CreateDatabase(); // Creating database if this is first program start
            _logger.Info("Data base connection is successful");
        }

        /// <summary>
        /// Load event on diPassword form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void diPasswords_Load(object sender, EventArgs e)
        {
            // If last user was saved
            if (Properties.Settings.Default.LastUsedLogin != null && Properties.Settings.Default.LastUsedLogin != "")
            {
                BaseLoginTextBox.Text = Properties.Settings.Default.LastUsedLogin;
                this.ActiveControl = BasePasswordTextBox;

                _logger.Info("Last user is saved. Enter a password");
            }
            else this.ActiveControl = BaseLoginTextBox;
        }

        /// <summary>
        /// TextChanged event on base login field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseLoginTextBox_TextChanged(object sender, EventArgs e)
        {
            if (_currentState != CurrentState.Authorizating) // If password confirming is activating, deactivate it
            {
                BasePasswordTextBox.PlaceholderText = "";
                _currentState = CurrentState.Authorizating;
                _userService.Confirm();

                _logger.Info("Password confirming is off");
            }

            if (BaseLoginTextBox.Text.Length > 0)
            {
                if (BasePasswordTextBox.Text.Length > 0) // If login and password fields are non-empty at the same time
                {
                    _elementView.Switch(ElementMode.Authorization, (_currentState == CurrentState.Authorizating) ? true : false);
                    _elementView.Switch(true);
                }
            }
            else _elementView.Switch(ElementMode.Authorization, false);
        }
        /// <summary>
        /// KeyPress event on base login field (here is going text validation)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseLoginTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') this.ActiveControl = BasePasswordTextBox;
            if (!_validator.IsKeyValid(e.KeyChar))
            {
                e.Handled = true;

                _logger.CurrentError("Inputting symbol has not to use in the login box");
            }
            if (BaseLoginTextBox.Text == "" && !_validator.IsLoginCorrect(e.KeyChar))
            {
                e.Handled = true;

                _logger.CurrentError("The login cannot begin by digit");
            }
        }

        /// <summary>
        /// TextChanged event on base password field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BasePasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            if (BasePasswordTextBox.Text.Length > 0)
            {
                if (BaseLoginTextBox.Text.Length > 0) // If login and password fields are non-empty at the same time
                {
                    _elementView.Switch(ElementMode.Authorization, (_currentState == CurrentState.Authorizating) ? true : false);
                    _elementView.Switch(true);
                };

                _showBasePasswordController.Switch(true);
            }
            else
            {
                _elementView.Switch(ElementMode.Authorization, false);
                Deactivate();
            }
        }
        /// <summary>
        /// KeyPress event on pase password field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BasePasswordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                if (_currentState == CurrentState.Authorizating) EnterBaseAccountBttn.PerformClick();
                else if (_currentState == CurrentState.AddConfirming) AddBaseAccountBttn.PerformClick();
                else if (_currentState == CurrentState.DeleteConfirming) DeleteBaseAccountBttn.PerformClick();
            }
            if (!_validator.IsKeyValid(e.KeyChar))
            {
                e.Handled = true;

                _logger.CurrentError("Inputting symbol has not to use in the password box");
            }
        }

        /// <summary>
        /// Click event on "Show password" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowBasePasswordBttn_Click(object sender, EventArgs e)
        {
            if (ShowBasePasswordBttn.Text == "Show") // If the password is hidden now
            {
                BasePasswordTextBox.PasswordChar = '\0';
                _showBasePasswordController.Retext(true);
            }
            else
            {
                BasePasswordTextBox.PasswordChar = '•';
                _showBasePasswordController.Retext(false);
            }
        }

        /// <summary>
        /// Click event on "Enter" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterBaseAccountBttn_Click(object sender, EventArgs e)
        {
            if (_currentState == CurrentState.Authorizating) // If a user is yet not authorizated
            {
                if (_userService.IsPasswordCorrect(BaseLoginTextBox.Text, BasePasswordTextBox.Text)) // Trying to connect to appropriate table
                {
                    _dataService.SetCurrentUser(BaseLoginTextBox.Text);
                    _encrypter.PasswordToKey(BasePasswordTextBox.Text);

                    _dataListShower.UpdateList(_dataService.GetData());
                    Confirmition(CurrentState.LoggedIn);

                    Properties.Settings.Default.LastUsedLogin = BaseLoginTextBox.Text;
                    Properties.Settings.Default.Save();

                    _logger.Info($"Successed authorization by {BaseLoginTextBox.Text} login");
                }
                else
                {
                    BasePasswordTextBox.Text = "";

                    _logger.CurrentError("Incorrect login or password. Try again");
                }
            }
            else
            {
                DataListBox.Items.Clear();
                ShowSelectedData(false);
                _elementView.Switch(ElementMode.DataSelected, false);
                _showDataController.Retext(false);

                _elementView.Switch(ElementMode.Using, false);
                _currentState = CurrentState.Authorizating;
                ClearPasswordField();

                _logger.Info($"{BaseLoginTextBox.Text} logged out");
            }
        }

        /// <summary>
        /// Click event on "Add user" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBaseAccountBttn_Click(object sender, EventArgs e)
        {
            if (!_userService.IsUserExists(BaseLoginTextBox.Text))
            {
                ConfirmitionState confirmer = _userService.Confirm(BasePasswordTextBox.Text); // Activating password confirming mode checking
                if (confirmer == ConfirmitionState.NotStarted) // If password confirming is deactivated, activate it
                {
                    Confirmition(CurrentState.AddConfirming);

                    _logger.Info("You need to confirm the password");
                    _logger.Info("You also can turn confirmition mode off by login editting");
                }
                else if (confirmer == ConfirmitionState.Confirmed) // If the password is confirmed
                {
                    _dataService.SetCurrentUser(BaseLoginTextBox.Text);
                    _encrypter.PasswordToKey(BasePasswordTextBox.Text);

                    _userService.Add(BaseLoginTextBox.Text, BasePasswordTextBox.Text);                                        
                    Confirmition(CurrentState.LoggedIn);

                    Properties.Settings.Default.LastUsedLogin = BaseLoginTextBox.Text;
                    Properties.Settings.Default.Save();

                    _logger.Info("User is added");
                }
                else
                {
                    ClearPasswordField();

                    _logger.CurrentError("The passwords aren't same. Try again or enter other login");
                }
            }
            else
            {
                ClearPasswordField();

                _logger.CurrentError("The user is already exists. Try other login");
            }
        }

        /// <summary>
        /// Click event on "Delete user" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteBaseAccountBttn_Click(object sender, EventArgs e)
        {
            if (_userService.IsPasswordCorrect(BaseLoginTextBox.Text, BasePasswordTextBox.Text))
            {
                ConfirmitionState confirmer = _userService.Confirm(BasePasswordTextBox.Text); // Activating password confirming checking
                if (confirmer == ConfirmitionState.NotStarted) // If the password confirming is deactivated, activate it
                {
                    Confirmition(CurrentState.DeleteConfirming);

                    _logger.Info("You need to confirm the password");
                    _logger.Info("You also can turn confirmition mode off by login editting");
                }
                else if (confirmer == ConfirmitionState.Confirmed) // If the password is confirmed
                {
                    _userService.Delete(BaseLoginTextBox.Text, BasePasswordTextBox.Text);
                    Confirmition(CurrentState.Authorizating);

                    _logger.Info("The user is successfully deleted");
                }
                else
                {
                    ClearPasswordField();

                    _logger.CurrentError("Incorrect login or password. Try again");
                }
            }
            else
            {
                ClearPasswordField();

                _logger.CurrentError("Incorrect login or password. Try again");
            }
        }

        // ----------------------------------------------------------------------------------------------------
        // Beginning this line and further described the methods for certain user data
        // (authorization is already successful)
        // ----------------------------------------------------------------------------------------------------

        /// <summary>
        /// Click event on "Show data" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddDataBttn_Click(object sender, EventArgs e)
        {
            DataEditorForm dataEditorForm = new DataEditorForm(
                BaseLoginTextBox.Text,
                _logger,
                _dataValidator,
                _dataService,
                _dataListShower);
            dataEditorForm.Show();
        }
        /// <summary>
        /// Click event on "Edit data" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditDataBttn_Click(object sender, EventArgs e)
        {
            string name = ConvertSelected();
            DataEditorForm dataEditorForm = new DataEditorForm(BaseLoginTextBox.Text,
                _logger,
                _dataValidator,
                _dataService,
                _dataListShower,
                name);
            dataEditorForm.Show();
        }
        /// <summary>
        /// Click event on "Delete data" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteDataBttn_Click(object sender, EventArgs e)
        {
            string name = ConvertSelected();

            DialogResult confirmition = MessageBox.Show($"Are you sure you want to delete data about {name}?", "Confirm action", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (confirmition == DialogResult.Yes)
            {
                _dataService.DeleteData(name);

                ShowSelectedData(false);
                _dataListShower.UpdateList(_dataService.GetData());                
                _elementView.Switch(ElementMode.DataSelected, false);

                _logger.Info($"{name} is deleted");
            }
        }

        /// <summary>
        /// SelectedIndexChanged event on DataListBox element
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DataListBox.SelectedItem != null)
            {
                _elementView.Switch(ElementMode.DataSelected, true);
                ShowSelectedData(false);
            }
            else
            {
                _elementView.Switch(ElementMode.DataSelected, false);
                _showDataController.Retext(false);
            }
        }
        /// <summary>
        /// Click event on "Show data" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowDataBttn_Click(object sender, EventArgs e)
        {
            if (ShowDataBttn.Text == "Show data") ShowSelectedData(true);
            else ShowSelectedData(false);
        }
        /// <summary>
        /// KeyPress event on DataListBox element (by [Enter] shows choosed data)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataListBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (DataListBox.SelectedItem != null && e.KeyChar == '\r') ShowDataBttn.PerformClick();
        }
        /// <summary>
        /// DoubleClick event on DataListBox element (do same as prior method)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataListBox_DoubleClick(object sender, EventArgs e)
        {
            if (DataListBox.SelectedItem != null) ShowDataBttn.PerformClick();
            else _elementView.Switch(ElementMode.DataSelected, false);
        }

        /// <summary>
        /// MouseDown event on TextBox elements (copying putted text from approrriate field to the clipboard)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataLoginTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (DataLoginTextBox.Text != "")
            {
                Clipboard.SetText(DataLoginTextBox.Text);

                _logger.Info("Login is copied");
            }
        }
        /// <inheritdoc cref="diPasswordsForm.DataLoginTextBox_MouseDown(object, MouseEventArgs)"/>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataPasswordTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (DataPasswordTextBox.Text != "")
            {
                Clipboard.SetText(DataPasswordTextBox.Text);

                _logger.Info("Password is copied");
            }
        }
        /// <inheritdoc cref="diPasswordsForm.DataLoginTextBox_MouseDown(object, MouseEventArgs)"/>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataEmailTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (DataEmailTextBox.Text != "")
            {
                Clipboard.SetText(DataEmailTextBox.Text);

                _logger.Info("Email is copied");
            }
        }
        /// <inheritdoc cref="diPasswordsForm.DataLoginTextBox_MouseDown(object, MouseEventArgs)"/>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataPhoneTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (DataPhoneTextBox.Text != "")
            {
                Clipboard.SetText(DataPhoneTextBox.Text);

                _logger.Info("Phone number is copied");
            }
        }
    }
}
