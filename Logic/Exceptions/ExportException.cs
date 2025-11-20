namespace Logic.Exceptions
{
    /// <summary>
    /// Базовое исключение для ошибок экспорта данных.
    /// </summary>
    public class ExportException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр исключения ExportException.
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        public ExportException(string message) : base(message) { }

        /// <summary>
        /// Инициализирует новый экземпляр исключения ExportException с внутренним исключением.
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="innerException">Внутреннее исключение</param>
        public ExportException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
