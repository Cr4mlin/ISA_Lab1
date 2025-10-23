namespace Logic.Validation
{
    /// <summary>
    /// Валидатор для цены курса
    /// </summary>
    public class PriceValidator : IValidator<decimal>
    {
        /// <summary>
        /// Валидирует цену курса
        /// </summary>
        /// <param name="price">Цена курса</param>
        /// <returns>true если цена валидна, иначе false</returns>
        public bool IsValid(decimal price)
        {
            return price >= 0;
        }

        /// <summary>
        /// Получает сообщение об ошибке валидации цены
        /// </summary>
        /// <param name="price">Цена курса</param>
        /// <returns>Сообщение об ошибке</returns>
        public string GetErrorMessage(decimal price)
        {
            return "Стоимость курса не может быть отрицательной.";
        }
    }
}
