namespace Logic.Exceptions
{
    /// <summary>
    /// Исключение для невалидного имени преподавателя.
    /// </summary>
    public class InvalidTeacherNameException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр исключения InvalidTeacherNameException.
        /// </summary>
        /// <param name="teacherName">Невалидное имя преподавателя</param>
        public InvalidTeacherNameException(string teacherName)
            : base($"Имя преподавателя может содержать только буквы, пробелы, дефисы и апострофы.") { }
    }
}
