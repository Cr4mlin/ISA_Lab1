using System.Windows.Input;
using Logic;

namespace Presenter.ViewModels
{
    /// <summary>
    /// ViewModel для окна входа
    /// </summary>
    public class LoginViewModel : BaseViewModel
    {
        private readonly ISchoolService _schoolService;
        private string _login = string.Empty;
        private string _password = string.Empty;
        private string _errorMessage = string.Empty;

        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        /// <summary>
        /// Событие успешного входа (userId, role)
        /// </summary>
        public event EventHandler<(int userId, int role)>? LoginSuccessful;

        /// <summary>
        /// Событие запроса на показ окна регистрации
        /// </summary>
        public event EventHandler? RegisterRequested;

        public LoginViewModel(ISchoolService schoolService)
        {
            _schoolService = schoolService ?? throw new ArgumentNullException(nameof(schoolService));

            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
            RegisterCommand = new RelayCommand(ExecuteRegister);
        }

        private bool CanExecuteLogin()
        {
            return !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password);
        }

        private void ExecuteLogin()
        {
            try
            {
                ErrorMessage = string.Empty;

                var user = _schoolService.Login(Login, Password);
                if (user != null)
                {
                    // Успешный вход
                    LoginSuccessful?.Invoke(this, (user.Id, user.Role));
                }
                else
                {
                    ErrorMessage = "Неверный логин или пароль";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Ошибка входа: {ex.Message}";
            }
        }

        private void ExecuteRegister()
        {
            RegisterRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
