namespace Logic.Validators
{
    /// <summary>
    /// Интерфейс для валидации ценового диапазона
    /// </summary>
    public interface IPriceRangeValidator
    {
        /// <summary>
        /// Проверяет валидность ценового диапазона
        /// </summary>
        /// <param name="minPrice">Минимальная цена</param>
        /// <param name="maxPrice">Максимальная цена</param>
        /// <returns>true если диапазон валиден, иначе false</returns>
        bool IsValid(decimal minPrice, decimal maxPrice);
    }
}

