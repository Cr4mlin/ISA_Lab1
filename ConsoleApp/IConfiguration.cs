namespace ConsoleApp
{
    /// <summary>
    /// Интерфейс для конфигурации приложения
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Строка подключения к базе данных
        /// </summary>
        string ConnectionString { get; }
        
        /// <summary>
        /// Использовать Entity Framework (true) или Dapper (false)
        /// </summary>
        bool UseEntityFramework { get; }
    }
}
