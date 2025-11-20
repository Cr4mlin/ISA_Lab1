namespace Logic.Exceptions
{
    /// <summary>
    /// Исключение для случая, когда указан некорректный путь к файлу.
    /// </summary>
    public class InvalidFilePathException : ExportException
    {
        /// <summary>
        /// Инициализирует новый экземпляр исключения InvalidFilePathException.
        /// </summary>
        public InvalidFilePathException()
            : base("Путь к файлу не может быть пустым") { }

        /// <summary>
        /// Инициализирует новый экземпляр исключения InvalidFilePathException с дополнительным сообщением.
        /// </summary>
        /// <param name="message">Дополнительное сообщение об ошибке</param>
        public InvalidFilePathException(string message)
            : base(message) { }
    }
}
