namespace Logic.Converters
{
    /// <summary>
    /// Реализация конвертера статуса курса
    /// </summary>
    public class StatusConverter : IStatusConverter
    {
        /// <summary>
        /// Конвертирует строковое представление статуса в булево значение
        /// </summary>
        /// <param name="status">Строковое представление статуса ("да" или "нет")</param>
        /// <returns>true если статус "да", false если "нет"</returns>
        public bool ConvertToBool(string status)
        {
            return status?.ToLower() == "да";
        }
    }
}

