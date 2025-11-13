namespace Logic.Validators
{
    /// <summary>
    /// Интерфейс для валидации цены курса
    /// </summary>
    public interface IPriceValidator
    {
        /// <summary>
        /// Проверяет валидность цены курса
        /// </summary>
        /// <param name="price">Цена для проверки</param>
        /// <returns>true если цена валидна, иначе false</returns>
        bool IsValid(decimal price);
    }
}

