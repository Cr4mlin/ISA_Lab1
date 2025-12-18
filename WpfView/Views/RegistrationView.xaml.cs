using System.Windows;
using System.Windows.Controls;
using Presenter.ViewModels;

namespace WpfView.Views
{
    /// <summary>
    /// Логика взаимодействия для RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : BaseView
    {
        public RegistrationView()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegistrationViewModel viewModel)
            {
                viewModel.Password = PasswordBox.Password;
            }
        }
    }
}
