using System;
using System.Collections.Generic;
using System.Windows;
using Presenter.ViewModels;
using WpfView.Views;

namespace WpfView
{
    /// <summary>
    /// Менеджер View - управляет созданием и отображением окон
    /// </summary>
    public class ViewManager
    {
        private readonly Dictionary<Type, Type> _vmToViewMapping;
        private readonly VMManager _vmManager;
        private Window? _currentWindow;

        public ViewManager(VMManager vmManager)
        {
            _vmManager = vmManager ?? throw new ArgumentNullException(nameof(vmManager));

            // Регистрация соответствия ViewModel → View
            _vmToViewMapping = new Dictionary<Type, Type>
            {
                { typeof(LoginViewModel), typeof(LoginView) },
                { typeof(RegistrationViewModel), typeof(RegistrationView) },
                { typeof(UserMainViewModel), typeof(UserMainView) },
                { typeof(AdminMainViewModel), typeof(AdminMainView) },
                { typeof(CourseEditViewModel), typeof(CourseEditView) }
            };

            // Подписка на события от VMManager
            _vmManager.RequestViewForViewModel += OnRequestViewForViewModel;
        }

        /// <summary>
        /// Запускает приложение с окна входа
        /// </summary>
        public void Run()
        {
            _vmManager.CreateLoginViewModel();
        }

        private void OnRequestViewForViewModel(object? sender, BaseViewModel viewModel)
        {
            // Получаем тип ViewModel
            var vmType = viewModel.GetType();

            // Находим соответствующий View
            if (_vmToViewMapping.TryGetValue(vmType, out var viewType))
            {
                // Создаем новый View
                var view = Activator.CreateInstance(viewType) as Window;
                if (view != null)
                {
                    // Устанавливаем DataContext
                    view.DataContext = viewModel;

                    // Сохраняем ссылку на предыдущее окно
                    var previousWindow = _currentWindow;

                    // Сохраняем ссылку на новое текущее окно
                    _currentWindow = view;

                    // Показываем новое окно
                    view.Show();

                    // Закрываем предыдущее окно после показа нового
                    previousWindow?.Close();
                }
            }
            else
            {
                throw new InvalidOperationException($"Не найдено соответствие View для ViewModel типа {vmType.Name}");
            }
        }
    }
}
