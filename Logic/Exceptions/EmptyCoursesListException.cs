namespace Logic.Exceptions
{
    /// <summary>
    /// Исключение для случая, когда список курсов пуст или не указан.
    /// </summary>
    public class EmptyCoursesListException : ExportException
    {
        /// <summary>
        /// Инициализирует новый экземпляр исключения EmptyCoursesListException.
        /// </summary>
        public EmptyCoursesListException()
            : base("Список курсов для экспорта не может быть пустым") { }
    }
}
