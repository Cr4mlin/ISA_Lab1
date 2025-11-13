namespace Logic.Validators
{
    /// <summary>
    /// Реализация валидатора длительности курса
    /// </summary>
    public class DurationValidator : IDurationValidator
    {
        /// <summary>
        /// Проверяет валидность длительности курса
        /// </summary>
        /// <param name="duration">Длительность для проверки</param>
        /// <returns>true если длительность валидна, иначе false</returns>
        public bool IsValid(int duration)
        {
            return duration >= 0;
        }
    }
}

