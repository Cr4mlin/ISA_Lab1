using System.Windows;
using System.Windows.Controls;
using Presenter.ViewModels;

namespace WpfView.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class LoginView : BaseView
    {
        public LoginView()
        {
            InitializeComponent();
            // DataContext устанавливается ViewManager'ом
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel viewModel && sender is PasswordBox passwordBox)
            {
                viewModel.Password = passwordBox.Password;
            }
        }
    }
}
