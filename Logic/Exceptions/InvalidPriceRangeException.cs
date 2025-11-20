namespace Logic.Exceptions
{
    /// <summary>
    /// Исключение для неверного ценового диапазона.
    /// </summary>
    public class InvalidPriceRangeException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр исключения InvalidPriceRangeException.
        /// </summary>
        /// <param name="minPrice">Минимальная цена</param>
        /// <param name="maxPrice">Максимальная цена</param>
        public InvalidPriceRangeException(decimal minPrice, decimal maxPrice)
            : base($"Неверный ценовой диапазон") { }
    }
}
