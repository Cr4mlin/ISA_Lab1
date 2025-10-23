using System.Text.RegularExpressions;

namespace Logic.Validation
{
    /// <summary>
    /// Валидатор для статуса курса
    /// </summary>
    public class StatusValidator : IValidator<string>
    {
        /// <summary>
        /// Валидирует статус курса
        /// </summary>
        /// <param name="status">Статус курса</param>
        /// <returns>true если статус валиден, иначе false</returns>
        public bool IsValid(string status)
        {
            if (string.IsNullOrEmpty(status))
                return false;

            string pattern = @"да|нет";
            return Regex.IsMatch(status, pattern);
        }

        /// <summary>
        /// Получает сообщение об ошибке валидации статуса
        /// </summary>
        /// <param name="status">Статус курса</param>
        /// <returns>Сообщение об ошибке</returns>
        public string GetErrorMessage(string status)
        {
            return "Введено неверное состояние курса";
        }
    }
}
