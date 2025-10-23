using System.Text.RegularExpressions;

namespace Logic.Validation
{
    /// <summary>
    /// Валидатор для имени преподавателя
    /// </summary>
    public class TeacherNameValidator : IValidator<string>
    {
        /// <summary>
        /// Валидирует имя преподавателя
        /// </summary>
        /// <param name="teacherName">Имя преподавателя</param>
        /// <returns>true если имя валидно, иначе false</returns>
        public bool IsValid(string teacherName)
        {
            if (string.IsNullOrWhiteSpace(teacherName))
                return false;

            string pattern = @"^[a-zA-Zа-яА-ЯёЁ\s\-']+$";
            if (!Regex.IsMatch(teacherName, pattern))
                return false;

            if (teacherName.StartsWith("-") || teacherName.EndsWith("-") ||
                teacherName.StartsWith("'") || teacherName.EndsWith("'"))
                return false;

            if (teacherName.Contains("--") || teacherName.Contains("  "))
                return false;

            return true;
        }

        /// <summary>
        /// Получает сообщение об ошибке валидации имени преподавателя
        /// </summary>
        /// <param name="teacherName">Имя преподавателя</param>
        /// <returns>Сообщение об ошибке</returns>
        public string GetErrorMessage(string teacherName)
        {
            return "Имя преподавателя может содержать только буквы, пробелы, дефисы и апострофы.";
        }
    }
}
