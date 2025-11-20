namespace Logic.Exceptions
{
    /// <summary>
    /// Исключение для недопустимой продолжительности курса.
    /// </summary>
    public class InvalidDurationException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр исключения InvalidDurationException.
        /// </summary>
        /// <param name="duration">Недопустимая продолжительность курса</param>
        public InvalidDurationException(int duration)
            : base($"Продолжительность курса должна быть положительной.") { }
    }
}
