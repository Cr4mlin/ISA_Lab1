namespace Logic.Exceptions
{
    /// <summary>
    /// Исключение для недопустимой стоимости курса.
    /// </summary>
    public class InvalidPriceException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр исключения InvalidPriceException.
        /// </summary>
        /// <param name="price">Недопустимая стоимость курса</param>
        public InvalidPriceException(decimal price)
            : base($"Стоимость курса не может быть отрицательной.") { }
    }
}
