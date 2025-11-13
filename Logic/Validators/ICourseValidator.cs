namespace Logic.Validators
{
    /// <summary>
    /// Интерфейс для валидации данных курса
    /// </summary>
    public interface ICourseValidator
    {
        /// <summary>
        /// Проверяет валидность данных курса
        /// </summary>
        /// <param name="price">Цена курса</param>
        /// <param name="duration">Длительность курса</param>
        /// <param name="teacherName">Имя преподавателя</param>
        /// <param name="status">Статус курса</param>
        void Validate(decimal price, int duration, string teacherName, string status);
    }
}
