using Logic;

namespace Presenter.ViewModels
{
    /// <summary>
    /// Менеджер ViewModel - управляет созданием и жизненным циклом ViewModel
    /// </summary>
    public class VMManager
    {
        private readonly ISchoolService _schoolService;

        /// <summary>
        /// Событие запроса на создание View для ViewModel
        /// </summary>
        public event EventHandler<BaseViewModel>? RequestViewForViewModel;

        public VMManager(ISchoolService schoolService)
        {
            _schoolService = schoolService ?? throw new ArgumentNullException(nameof(schoolService));
        }

        /// <summary>
        /// Создает главную ViewModel для входа
        /// </summary>
        public void CreateLoginViewModel()
        {
            var loginViewModel = new LoginViewModel(_schoolService);
            loginViewModel.ViewModelReady += OnViewModelReady;
            loginViewModel.LoginSuccessful += OnLoginSuccessful;
            loginViewModel.RegisterRequested += OnRegisterRequested;

            // Инициализируем после подписки на события
            loginViewModel.Initialize();
        }

        /// <summary>
        /// Создает ViewModel для регистрации
        /// </summary>
        public void CreateRegistrationViewModel()
        {
            var registrationViewModel = new RegistrationViewModel(_schoolService);
            registrationViewModel.ViewModelReady += OnViewModelReady;
            registrationViewModel.RegistrationSuccessful += OnRegistrationSuccessful;
            registrationViewModel.BackToLoginRequested += OnBackToLoginRequested;

            // Инициализируем после подписки на события
            registrationViewModel.Initialize();
        }

        /// <summary>
        /// Создает главную ViewModel для пользователя
        /// </summary>
        public void CreateUserMainViewModel(int userId)
        {
            var userViewModel = new UserMainViewModel(_schoolService, userId);
            userViewModel.ViewModelReady += OnViewModelReady;

            // Инициализируем после подписки на события
            userViewModel.Initialize();
        }

        /// <summary>
        /// Создает главную ViewModel для администратора
        /// </summary>
        public void CreateAdminMainViewModel(int userId)
        {
            var adminViewModel = new AdminMainViewModel(_schoolService, userId);
            adminViewModel.ViewModelReady += OnViewModelReady;

            // Инициализируем после подписки на события
            adminViewModel.Initialize();
        }

        private void OnViewModelReady(object? sender, EventArgs e)
        {
            if (sender is BaseViewModel viewModel)
            {
                // Отправляем событие для создания View
                RequestViewForViewModel?.Invoke(this, viewModel);
            }
        }

        private void OnLoginSuccessful(object? sender, (int userId, int role) e)
        {
            // Создаем соответствующую главную форму в зависимости от роли
            if (e.role == 2) // User
            {
                CreateUserMainViewModel(e.userId);
            }
            else if (e.role == 1) // Admin
            {
                CreateAdminMainViewModel(e.userId);
            }
        }

        private void OnRegisterRequested(object? sender, EventArgs e)
        {
            CreateRegistrationViewModel();
        }

        private void OnRegistrationSuccessful(object? sender, EventArgs e)
        {
            // После успешной регистрации возвращаемся к окну входа
            CreateLoginViewModel();
        }

        private void OnBackToLoginRequested(object? sender, EventArgs e)
        {
            CreateLoginViewModel();
        }
    }
}
