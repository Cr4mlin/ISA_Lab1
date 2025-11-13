namespace Logic.Converters
{
    /// <summary>
    /// Интерфейс для конвертации строкового статуса в булево значение
    /// </summary>
    public interface IStatusConverter
    {
        /// <summary>
        /// Конвертирует строковое представление статуса в булево значение
        /// </summary>
        /// <param name="status">Строковое представление статуса ("да" или "нет")</param>
        /// <returns>true если статус "да", false если "нет"</returns>
        bool ConvertToBool(string status);
    }
}

