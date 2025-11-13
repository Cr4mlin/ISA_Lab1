namespace Logic.Validators
{
    /// <summary>
    /// Интерфейс для валидации статуса курса
    /// </summary>
    public interface IStatusValidator
    {
        /// <summary>
        /// Проверяет валидность статуса курса
        /// </summary>
        /// <param name="status">Статус для проверки</param>
        /// <returns>true если статус валиден, иначе false</returns>
        bool IsValid(string status);
    }
}

