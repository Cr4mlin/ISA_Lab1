using System.Text.RegularExpressions;

namespace Logic.Validators
{
    /// <summary>
    /// Реализация валидатора статуса курса
    /// </summary>
    public class StatusValidator : IStatusValidator
    {
        /// <summary>
        /// Проверяет валидность статуса курса
        /// </summary>
        /// <param name="status">Статус для проверки</param>
        /// <returns>true если статус валиден, иначе false</returns>
        public bool IsValid(string status)
        {
            if (string.IsNullOrEmpty(status))
                return false;

            string pattern = @"да|нет";
            return Regex.IsMatch(status, pattern);
        }
    }
}

