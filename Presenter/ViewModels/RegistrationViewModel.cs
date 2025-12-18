using System;
using System.Windows.Input;
using Logic;

namespace Presenter.ViewModels
{
    /// <summary>
    /// ViewModel для окна регистрации
    /// </summary>
    public class RegistrationViewModel : BaseViewModel
    {
        private readonly ISchoolService _schoolService;
        private string _nickName = string.Empty;
        private string _login = string.Empty;
        private string _password = string.Empty;
        private string _errorMessage = string.Empty;

        public string NickName
        {
            get => _nickName;
            set => SetProperty(ref _nickName, value);
        }

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

        public ICommand RegisterCommand { get; }
        public ICommand BackToLoginCommand { get; }

        /// <summary>
        /// Событие успешной регистрации
        /// </summary>
        public event EventHandler? RegistrationSuccessful;

        /// <summary>
        /// Событие возврата к окну входа
        /// </summary>
        public event EventHandler? BackToLoginRequested;

        public RegistrationViewModel(ISchoolService schoolService)
        {
            _schoolService = schoolService ?? throw new ArgumentNullException(nameof(schoolService));

            RegisterCommand = new RelayCommand(ExecuteRegister, CanExecuteRegister);
            BackToLoginCommand = new RelayCommand(ExecuteBackToLogin);
        }

        private bool CanExecuteRegister()
        {
            return !string.IsNullOrWhiteSpace(NickName) &&
                   !string.IsNullOrWhiteSpace(Login) &&
                   !string.IsNullOrWhiteSpace(Password);
        }

        private void ExecuteRegister()
        {
            try
            {
                ErrorMessage = string.Empty;

                var user = _schoolService.Register(Login, Password, NickName);
                if (user != null)
                {
                    RegistrationSuccessful?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    ErrorMessage = "Ошибка регистрации";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Ошибка: {ex.Message}";
            }
        }

        private void ExecuteBackToLogin()
        {
            BackToLoginRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
