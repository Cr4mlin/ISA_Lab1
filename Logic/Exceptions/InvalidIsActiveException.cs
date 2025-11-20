namespace Logic.Exceptions
{
    /// <summary>
    /// Исключение для случая, если введено неверное состояние для курса.
    /// </summary>
    public class InvalidIsActiveException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр исключения InvalidIsActiveException.
        /// </summary>
        /// <param name="status">Невалидное состояние курса</param>
        public InvalidIsActiveException(string status)
            : base("Введено неверное состояние курса") { }
    }
}
