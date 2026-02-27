using diPasswords.Application.Interfaces;
using diPasswords.Presentation.Managers;

namespace diPasswords.Presentation.Views
{
    public partial class DataEditorForm : Form
    {
        private string _baseLogin; // Login of user, which edits data
        private bool _favorite = false; // "Favorite" flag
        private string? _existingData; // Do data exist or user adds new

        private ILogger _logger; // Logging to separate element
        private IDataValidator _dataValidator; // Inputting data validation
        private IDataService _dataService; // User data interaction
        private IDataListShower _dataListShower; // User data visualization

        // Element enabling by neccessary and their parameter edditing
        // Objects linking to appropriate element
        private IElementController<Control> _showPasswordController;

        public DataEditorForm(
            string baseLogin,            
            ILogger logger,
            IDataValidator dataValidator,
            IDataService dataService,
            IDataListShower dataListShower,
            string? isNewData = null)
        {
            InitializeComponent();

            // Dependencies injection
            _baseLogin = baseLogin;            
            _logger = logger;
            _dataValidator = dataValidator;
            _dataService = dataService;
            _dataListShower = dataListShower;
            _existingData = isNewData;

            _showPasswordController = new ElementController<Control>(ShowPasswordBttn, false, "Hide");
        }

        /// <summary>
        /// Load event on DataEditor form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataEditorForm_Load(object sender, EventArgs e)
        {
            // If data are added, focus on name TextBox, else block it
            if (_existingData == null) this.ActiveControl = NameTextBox;
            else
            {
                this.Text = "Edit data";
                NameTextBox.ReadOnly = true;
                this.ActiveControl = LoginTextBox;

                diPasswords.Domain.Models.Data data = _dataService.GetSelectedData(_existingData);
                NameTextBox.Text = data.Name;
                LoginTextBox.Text = data.Login;
                PasswordTextBox.Text = data.Password;
                EmailTextBox.Text = data.Email;
                PhoneTextBox.Text = data.Phone;
                DescriptionTextBox.Text = data.Description;
            }
        }

        /// <summary>
        /// Click event on "To favorite" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetFavoriteBttn_Click(object sender, EventArgs e)
        {
            if (SetFavoriteBttn.Text == "☆")
            {
                _favorite = true;
                SetFavoriteBttn.ForeColor = Color.DarkGoldenrod;
                SetFavoriteBttn.Text = "★";
            }
            else
            {
                _favorite = false;
                SetFavoriteBttn.ForeColor = Color.Black;
                SetFavoriteBttn.Text = "☆";
            }
        }

        /// <summary>
        /// KeyPress event on TextBox controls (switching to next field by [Enter] pressing)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (_dataValidator.IsNameUnique(_baseLogin, NameTextBox.Text))
                {
                    e.Handled = true;
                    this.ActiveControl = LoginTextBox;
                }
            }
        }
        /// <inheritdoc cref="DataEditorForm.NameTextBox_KeyPress(object, KeyPressEventArgs)"/>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (_dataValidator.IsLoginAndPasswordNotEmpty(LoginTextBox.Text, " "))
                {
                    e.Handled = true;
                    this.ActiveControl = PasswordTextBox;
                }
            }
        }
        /// <inheritdoc cref="DataEditorForm.NameTextBox_KeyPress(object, KeyPressEventArgs)"/>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasswordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (_dataValidator.IsLoginAndPasswordNotEmpty(" ", PasswordTextBox.Text))
                {
                    e.Handled = true;
                    this.ActiveControl = EmailTextBox;
                }
            }
        }
        /// <inheritdoc cref="DataEditorForm.NameTextBox_KeyPress(object, KeyPressEventArgs)"/>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmailTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (_dataValidator.IsEmailCorrect(EmailTextBox.Text))
                {
                    e.Handled = true;
                    this.ActiveControl = PhoneTextBox;
                }
            }
        }
        /// <inheritdoc cref="DataEditorForm.NameTextBox_KeyPress(object, KeyPressEventArgs)"/>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PhoneTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (_dataValidator.IsPhoneCorrect(PhoneTextBox.Text))
                {
                    e.Handled = true;
                    this.ActiveControl = DescriptionTextBox;
                }
            }
        }
        /// <inheritdoc cref="DataEditorForm.NameTextBox_KeyPress(object, KeyPressEventArgs)"/>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DescriptionTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                AcceptBttn.PerformClick();
            }
        }

        /// <summary>
        /// Click event on "Show password" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowPasswordBttn_Click(object sender, EventArgs e)
        {
            if (ShowPasswordBttn.Text == "Show")
            {
                PasswordTextBox.PasswordChar = '\0';
                _showPasswordController.Retext(true);
            }
            else
            {
                PasswordTextBox.PasswordChar = '•';
                _showPasswordController.Retext(false);
            }
        }
        /// <summary>
        /// TextChanged event on password field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            if (PasswordTextBox.Text.Length > 0) _showPasswordController.Switch(true);
            else _showPasswordController.Switch(false);
        }

        /// <summary>
        /// Click event on "Accept" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AcceptBttn_Click(object sender, EventArgs e)
        {
            if (_existingData == null)
            {
                if (_dataValidator.IsNameUnique(_baseLogin, NameTextBox.Text) && _dataValidator.IsLoginAndPasswordNotEmpty(LoginTextBox.Text, PasswordTextBox.Text) && _dataValidator.IsEmailCorrect(EmailTextBox.Text) && _dataValidator.IsPhoneCorrect(PhoneTextBox.Text))
                {
                    _dataService.SetCurrentUser(_baseLogin);
                    _dataService.AddData(
                        NameTextBox.Text,
                        _favorite,
                        LoginTextBox.Text,
                        PasswordTextBox.Text,
                        EmailTextBox.Text,
                        PhoneTextBox.Text,
                        DescriptionTextBox.Text);

                    _logger.Info("Data is added");
                    _dataListShower.UpdateList(_dataService.GetData());
                    _dataListShower.SetDataCursor((!_favorite) ? NameTextBox.Text : "★ " + NameTextBox.Text);
                    this.Close();
                }
            }
            else
            {
                if (_dataValidator.IsLoginAndPasswordNotEmpty(LoginTextBox.Text, PasswordTextBox.Text) && _dataValidator.IsEmailCorrect(EmailTextBox.Text) && _dataValidator.IsPhoneCorrect(PhoneTextBox.Text))
                {
                    _dataService.SetCurrentUser(_baseLogin);
                    _dataService.EditData(
                        NameTextBox.Text,
                        _favorite,
                        LoginTextBox.Text,
                        PasswordTextBox.Text,
                        EmailTextBox.Text,
                        PhoneTextBox.Text,
                        DescriptionTextBox.Text);

                    _logger.Info("Data is editted");
                    _dataListShower.UpdateList(_dataService.GetData());
                    _dataListShower.SetDataCursor((!_favorite) ? NameTextBox.Text : "★ " + NameTextBox.Text);
                    this.Close();
                }
            }
        }
    }
}
