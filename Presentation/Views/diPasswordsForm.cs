using diPasswords.Domain.Models;
using diPasswords.Application.Interfaces;
using diPasswords.Presentation.Managers;
using diPasswords.Domain.Enums;
using diPasswords.Presentation.Views;

namespace diPasswords
{
    // Основная форма приложения (открывается при запуске)
    public partial class diPasswordsForm : Form
    {
        private CurrentState _currentState = CurrentState.Authorizating; // Переменная хранения текущего статуса состояния UI

        private ILogger _logger; // Логгирование работы программы в отдельный элемент
        private IUserService _userService; // Взаимодействие с БД, отвечающей за существующих пользователей
        private IValidator _validator; // Проверка вводимых символов на соответствие локальным правилам
        private IElementView _elementView; // Включение/отключение пользовательских групп элементов
        private IDataValidator _dataValidator; // Проверка вводимых данных
        private IDataService _dataService; // Взаимодействие с данными пользователя
        private IDataListShower _dataListShower; // Отображение данных для данного пользователя
        private IEncrypter _encrypter; // Шифрование и дешифрование данных

        // Отключение элементов при необходимости и изменение их параметров
        // Привязка объектов к соответствующему элементу
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

        // Изменения состояний в зависимости от текущего сценария
        private void Confirmition(CurrentState type)
        {
            _currentState = type;
            switch (type)
            {
                case CurrentState.AddConfirming: // Подтверждение пароля при добавлении пользователя
                case CurrentState.DeleteConfirming: // Подтверждение пароля при удалении пользователя
                    ClearPasswordField();
                    Deactivate();

                    _elementView.Switch((type == CurrentState.AddConfirming) ? ElementMode.Adding : ElementMode.Deleting, false);
                    _elementView.CurrentPool((type == CurrentState.AddConfirming) ? ElementMode.Adding : ElementMode.Deleting);
                    BasePasswordTextBox.PlaceholderText = "Confirm password";

                    break;
                case CurrentState.Authorizating: // Сценарий авторизации пользователя
                case CurrentState.LoggedIn: // Сценарий входа в аккаунт пользователя
                    Deactivate();

                    if (type == CurrentState.Authorizating) _elementView.Switch(ElementMode.Authorization, false);
                    else _elementView.Switch(ElementMode.Using, true);
                    _elementView.CurrentPool(ElementMode.None);
                    BasePasswordTextBox.PlaceholderText = "";

                    if (type == CurrentState.Authorizating) ClearPasswordField();

                    break;
                case CurrentState.BackToAuthorization: // Переключение с подтверждения пароля на авторизацию
                    _currentState = CurrentState.Authorizating;

                    _elementView.Switch(ElementMode.Using, true);
                    _elementView.CurrentPool(ElementMode.None);
                    BasePasswordTextBox.PlaceholderText = "";

                    break;
            }
        }
        // Вывод логов в ListView
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
        // Обновление DataTextBox
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
        // Деактивировация кнопки "Показать пароль"
        public void Deactivate()
        {
            if (BasePasswordTextBox.PasswordChar == '\0') ShowBasePasswordBttn.PerformClick();
            ShowBasePasswordBttn.Enabled = false;
        }
        // Установка фокуса ListBox на данные
        private void ListBoxFocus(string name) => DataListBox.SelectedIndex = DataListBox.Items.IndexOf(name);
        // Очистка поля ввода пароля
        private void ClearPasswordField() => BasePasswordTextBox.Text = "";
        // Отображение выбранных данных
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
        // Преобразование выбранного элемента из ListBox в строку
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

            // Передача зависимостей
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

            // Добавление групп элементов            
            _elementView.AddPool
            (
                ElementMode.Authorization, new ElementController<Control>(EnterBaseAccountBttn, false),
                _addBaseAccountController,
                _deleteBaseAccountController
            ); // Изменяет состояние при непустых полях ввода логина и пароля
            _elementView.AddPool
            (
                ElementMode.Using, _enterBaseAccountController,
                new ElementController<Control>(AddBaseAccountBttn, true),
                new ElementController<Control>(DeleteBaseAccountBttn, true),
                _addDataController,
                _baseLoginController,
                _basePasswordController
            ); // Изменяет состояние при успешной авторизации
            _elementView.AddPool
            (
                ElementMode.Adding, _addBaseAccountController
            ); // Изменяет состояние во время подтверждения пароля при добавлении пользователя
            _elementView.AddPool
            (
                ElementMode.Deleting, _deleteBaseAccountController
            ); // Изменяет состояние во время подтверждения пароля при удалении пользователя
            _elementView.AddPool
            (
                ElementMode.DataSelected, new ElementController<Control>(ShowDataBttn, false),
                _editDataController,
                _deleteDataController
            ); // Изменяет состояние при выбранном элементе в TextBox

            // Подписки на события объектов
            _logger.OnLog += ListViewLog;
            _dataListShower.OnList += ListBoxUpdate;
            _dataListShower.OnListCursor += ListBoxFocus;

            _logger.Info("Programm start");

            _userService.CheckExisting(); // Создаем БД в случае, если это первый запуск программы
            _logger.Info("Data base connection is successful");
        }

        // При запуске данной формы
        private void diPasswords_Load(object sender, EventArgs e)
        {
            // Если сохранен последний вошедший пользователь            
            if (Properties.Settings.Default.LastUsedLogin != null && Properties.Settings.Default.LastUsedLogin != "")
            {
                BaseLoginTextBox.Text = Properties.Settings.Default.LastUsedLogin;
                this.ActiveControl = BasePasswordTextBox;

                _logger.Info("Last user is saved. Enter a password");
            }
            else this.ActiveControl = BaseLoginTextBox;
        }

        // Если изменен текст в поле базового логина
        private void BaseLoginTextBox_TextChanged(object sender, EventArgs e)
        {
            if (_currentState != CurrentState.Authorizating) // Если активировано подтверждение пароля, деактивируем
            {
                BasePasswordTextBox.PlaceholderText = "";
                _currentState = CurrentState.Authorizating;
                _userService.Confirm();

                _logger.Info("Password confirming is off");
            }

            if (BaseLoginTextBox.Text.Length > 0)
            {
                if (BasePasswordTextBox.Text.Length > 0) // Если поля с логином и паролем одновременно не являются пустыми
                {
                    _elementView.Switch(ElementMode.Authorization, (_currentState == CurrentState.Authorizating) ? true : false);
                    _elementView.Switch(true);
                }
            }
            else _elementView.Switch(ElementMode.Authorization, false);
        }
        // Если нажата клавиша в поле базового логина
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

        // Если изменен текст в поле базового пароля
        private void BasePasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            if (BasePasswordTextBox.Text.Length > 0)
            {
                if (BaseLoginTextBox.Text.Length > 0) // Если поля с логином и паролем одновременно не являются пустыми
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
        // Если нажата клавиша в поле базового пароля
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

        // Если нажата кнопка "Показать пароль"
        private void ShowBasePasswordBttn_Click(object sender, EventArgs e)
        {
            if (ShowBasePasswordBttn.Text == "Show") // Если в данный момент пароль скрыт
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

        // Если нажата кнопка "Ввести"
        private void EnterBaseAccountBttn_Click(object sender, EventArgs e)
        {
            if (_currentState == CurrentState.Authorizating) // Если пользователь еще не авторизовался
            {
                if (_userService.IsPasswordCorrect(BaseLoginTextBox.Text, BasePasswordTextBox.Text)) // Проводится попытка подключения к соответствующей таблицу
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

        // Если нажата кнопка "Добавить пользователя"
        private void AddBaseAccountBttn_Click(object sender, EventArgs e)
        {
            if (!_userService.IsUserExists(BaseLoginTextBox.Text))
            {
                ConfirmitionState confirmer = _userService.Confirm(BasePasswordTextBox.Text); // Проверка на активированный режим подтверждения пароля
                if (confirmer == ConfirmitionState.NotStarted) // Если подтверждение пароля неактивно, активируем
                {
                    Confirmition(CurrentState.AddConfirming);

                    _logger.Info("You need to confirm the password");
                    _logger.Info("You also can turn confirmition mode off by login editting");
                }
                else if (confirmer == ConfirmitionState.Confirmed) // Если пароль подтвержден
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

        // Если нажата кнопка "Удалить пользователя"
        private void DeleteBaseAccountBttn_Click(object sender, EventArgs e)
        {
            if (_userService.IsPasswordCorrect(BaseLoginTextBox.Text, BasePasswordTextBox.Text))
            {
                ConfirmitionState confirmer = _userService.Confirm(BasePasswordTextBox.Text); // Проверка на активированный режим подтверждения пароля
                if (confirmer == ConfirmitionState.NotStarted) // Если подтверждение пароля неактивно, активируем
                {
                    Confirmition(CurrentState.DeleteConfirming);

                    _logger.Info("You need to confirm the password");
                    _logger.Info("You also can turn confirmition mode off by login editting");
                }
                else if (confirmer == ConfirmitionState.Confirmed) // Если пароль подтвержден
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
        // Начиная с этой строки и далее идут методы для работы с данными конкретного введенного пользователя
        // (авторизация уже выполнена)
        // ----------------------------------------------------------------------------------------------------

        // Если нажата кнопка "Добавить данные"
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
        // Если нажата кнопка "Изменить данные"
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
        // Если нажата кнопка "Удалить данные"
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

        // Если выбран элемент в DataListBox
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
        // Если нажата кнопка "Показать данные"
        private void ShowDataBttn_Click(object sender, EventArgs e)
        {
            if (ShowDataBttn.Text == "Show data") ShowSelectedData(true);
            else ShowSelectedData(false);
        }
        // Если пользователь нажимает Enter во время фокуса на элементе ListBox
        private void DataListBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (DataListBox.SelectedItem != null && e.KeyChar == '\r') ShowDataBttn.PerformClick();
        }
        // Если пользователь дважды нажимает на ListBox (делает то же, что и предыдущий метод)
        private void DataListBox_DoubleClick(object sender, EventArgs e)
        {
            if (DataListBox.SelectedItem != null) ShowDataBttn.PerformClick();
            else _elementView.Switch(ElementMode.DataSelected, false);
        }

        // Копируем текст с соответствующих TextBox, если они не пустые
        private void DataLoginTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (DataLoginTextBox.Text != "")
            {
                Clipboard.SetText(DataLoginTextBox.Text);

                _logger.Info("Login is copied");
            }
        }
        private void DataPasswordTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (DataPasswordTextBox.Text != "")
            {
                Clipboard.SetText(DataPasswordTextBox.Text);

                _logger.Info("Password is copied");
            }
        }
        private void DataEmailTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (DataEmailTextBox.Text != "")
            {
                Clipboard.SetText(DataEmailTextBox.Text);

                _logger.Info("Email is copied");
            }
        }
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
