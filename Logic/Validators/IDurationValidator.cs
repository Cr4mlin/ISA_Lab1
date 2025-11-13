namespace Logic.Validators
{
    /// <summary>
    /// Интерфейс для валидации длительности курса
    /// </summary>
    public interface IDurationValidator
    {
        /// <summary>
        /// Проверяет валидность длительности курса
        /// </summary>
        /// <param name="duration">Длительность для проверки</param>
        /// <returns>true если длительность валидна, иначе false</returns>
        bool IsValid(int duration);
    }
}

