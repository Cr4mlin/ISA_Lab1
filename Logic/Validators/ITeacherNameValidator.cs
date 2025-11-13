namespace Logic.Validators
{
    /// <summary>
    /// Интерфейс для валидации имени преподавателя
    /// </summary>
    public interface ITeacherNameValidator
    {
        /// <summary>
        /// Проверяет валидность имени преподавателя
        /// </summary>
        /// <param name="teacherName">Имя преподавателя для проверки</param>
        /// <returns>true если имя валидно, иначе false</returns>
        bool IsValid(string teacherName);
    }
}

