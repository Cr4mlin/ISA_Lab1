namespace ConsoleApp
{
    /// <summary>
    /// Конфигурация приложения
    /// </summary>
    public class AppConfiguration : IConfiguration
    {
        /// <summary>
        /// Строка подключения к базе данных
        /// </summary>
        public string ConnectionString { get; }

        /// <summary>
        /// Использовать Entity Framework (true) или Dapper (false)
        /// </summary>
        public bool UseEntityFramework { get; }

        /// <summary>
        /// Инициализирует новый экземпляр конфигурации приложения
        /// </summary>
        /// <param name="connectionString">Строка подключения к базе данных</param>
        /// <param name="useEntityFramework">Использовать Entity Framework</param>
        public AppConfiguration(string connectionString, bool useEntityFramework)
        {
            ConnectionString = connectionString;
            UseEntityFramework = useEntityFramework;
        }
    }
}
