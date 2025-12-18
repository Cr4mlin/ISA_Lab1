using System.Windows;

namespace WpfView.Views
{
    /// <summary>
    /// Базовый класс для всех окон WPF
    /// </summary>
    public abstract class BaseView : Window
    {
        protected BaseView()
        {
            // Общая инициализация для всех окон
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
    }
}
