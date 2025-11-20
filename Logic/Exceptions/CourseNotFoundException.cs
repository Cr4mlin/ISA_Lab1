namespace Logic.Exceptions
{
    /// <summary>
    /// Исключение для случая, когда курс не найден.
    /// </summary>
    public class CourseNotFoundException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр исключения CourseNotFoundException.
        /// </summary>
        /// <param name="courseId">ID ненайденного курса</param>
        public CourseNotFoundException(string courseId)
            : base($"Курс с кодом '{courseId}' не найден.") { }
    }
}
