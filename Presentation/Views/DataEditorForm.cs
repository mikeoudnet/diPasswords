using diPasswords.Application.Interfaces;
using diPasswords.Presentation.Managers;

namespace diPasswords.Presentation.Views
{
    public partial class DataEditorForm : Form
    {
        private string _baseLogin; // Логин пользователя, который редактирует данные
        private bool _favorite = false; // Флажок "Избранное"
        private string? _existingData; // Существуют ли эти данные или пользователь добавляет новые

        private ILogger _logger; // Логгирование работы программы в отдельный элемент
        private IDataValidator _dataValidator; // Проверка вводимых данных
        private IDataService _dataService; // Взаимодействие с данными пользователя
        private IDataListShower _dataListShower; // Отображение данных для данного пользователя

        // Отключение элементов при необходимости и изменение их параметров
        // Привязка объектов к соответствующему элементу
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

            // Передача зависимостей
            _baseLogin = baseLogin;            
            _logger = logger;
            _dataValidator = dataValidator;
            _dataService = dataService;
            _dataListShower = dataListShower;
            _existingData = isNewData;

            _showPasswordController = new ElementController<Control>(ShowPasswordBttn, false, "Hide");
        }

        // Когда форма открывается
        private void DataEditorForm_Load(object sender, EventArgs e)
        {
            // Если данные добавляются, фокус на TextBox для имени, иначе блок на этом TextBox
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

        // Если нажата кнопка "Добавить в избранное"
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

        // Переключаем на следующее поле при нажатии на Enter
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
        private void DescriptionTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                AcceptBttn.PerformClick();
            }
        }

        // Если нажата кнопка "Показать пароль"
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
        // Если изменен текст в поле для пароля
        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            if (PasswordTextBox.Text.Length > 0) _showPasswordController.Switch(true);
            else _showPasswordController.Switch(false);
        }

        // Если нажата кнопка "Принять"
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
