using System.Windows;
using Logic;
using Ninject;
using Presenter.ViewModels;

namespace WpfView;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private ViewManager? _viewManager;
    private IKernel? _kernel;

    private void Application_Startup(object sender, StartupEventArgs e)
    {
        // Загружаем тип подключения из настроек
        string connectionType = LoadConnectionType();

        // Создание IoC-контейнера с выбранным типом подключения
        _kernel = new StandardKernel(new SimpleConfigModule(connectionType));

        // Получаем SchoolService
        var schoolService = _kernel.Get<ISchoolService>();

        // Создаем VMManager
        var vmManager = new VMManager(schoolService);

        // Создаем ViewManager
        _viewManager = new ViewManager(vmManager);

        // Устанавливаем режим завершения приложения
        ShutdownMode = ShutdownMode.OnLastWindowClose;

        // Запускаем приложение
        _viewManager.Run();
    }

    private string LoadConnectionType()
    {
        try
        {
            string settingsFile = "appsettings.txt";
            if (System.IO.File.Exists(settingsFile))
            {
                return System.IO.File.ReadAllText(settingsFile).Trim();
            }
        }
        catch
        {
            // Игнорируем ошибки чтения настроек
        }

        return "1"; // По умолчанию EntityFramework
    }
}
