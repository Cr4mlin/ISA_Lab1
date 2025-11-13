namespace Logic.Validators
{
    /// <summary>
    /// Реализация валидатора цены курса
    /// </summary>
    public class PriceValidator : IPriceValidator
    {
        /// <summary>
        /// Проверяет валидность цены курса
        /// </summary>
        /// <param name="price">Цена для проверки</param>
        /// <returns>true если цена валидна, иначе false</returns>
        public bool IsValid(decimal price)
        {
            return price >= 0;
        }
    }
}

