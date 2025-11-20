namespace Logic.Exceptions
{
    /// <summary>
    /// Исключение для случая, когда указан неподдерживаемый формат экспорта.
    /// </summary>
    public class InvalidExportFormatException : ExportException
    {
        /// <summary>
        /// Инициализирует новый экземпляр исключения InvalidExportFormatException.
        /// </summary>
        /// <param name="format">Неподдерживаемый формат</param>
        public InvalidExportFormatException(string format)
            : base($"Неподдерживаемый формат экспорта: {format}") { }
    }
}
