using System.Text.RegularExpressions;

namespace Logic.Validators
{
    /// <summary>
    /// Реализация валидатора имени преподавателя
    /// </summary>
    public class TeacherNameValidator : ITeacherNameValidator
    {
        /// <summary>
        /// Проверяет валидность имени преподавателя
        /// </summary>
        /// <param name="teacherName">Имя преподавателя для проверки</param>
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
    }
}

