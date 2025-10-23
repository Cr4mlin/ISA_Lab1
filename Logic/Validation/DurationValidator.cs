namespace Logic.Validation
{
    /// <summary>
    /// Валидатор для продолжительности курса
    /// </summary>
    public class DurationValidator : IValidator<int>
    {
        /// <summary>
        /// Валидирует продолжительность курса
        /// </summary>
        /// <param name="duration">Продолжительность курса</param>
        /// <returns>true если продолжительность валидна, иначе false</returns>
        public bool IsValid(int duration)
        {
            return duration >= 0;
        }

        /// <summary>
        /// Получает сообщение об ошибке валидации продолжительности
        /// </summary>
        /// <param name="duration">Продолжительность курса</param>
        /// <returns>Сообщение об ошибке</returns>
        public string GetErrorMessage(int duration)
        {
            return "Продолжительность курса должна быть положительной.";
        }
    }
}
