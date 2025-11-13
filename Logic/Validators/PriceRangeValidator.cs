namespace Logic.Validators
{
    /// <summary>
    /// Реализация валидатора ценового диапазона
    /// </summary>
    public class PriceRangeValidator : IPriceRangeValidator
    {
        /// <summary>
        /// Проверяет валидность ценового диапазона
        /// </summary>
        /// <param name="minPrice">Минимальная цена</param>
        /// <param name="maxPrice">Максимальная цена</param>
        /// <returns>true если диапазон валиден, иначе false</returns>
        public bool IsValid(decimal minPrice, decimal maxPrice)
        {
            return minPrice >= 0 && maxPrice >= 0 && minPrice <= maxPrice;
        }
    }
}

