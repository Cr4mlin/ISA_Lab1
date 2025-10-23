namespace Logic.Validation
{
    /// <summary>
    /// Базовый интерфейс для валидаторов
    /// </summary>
    /// <typeparam name="T">Тип объекта для валидации</typeparam>
    public interface IValidator<T>
    {
        /// <summary>
        /// Валидирует объект
        /// </summary>
        /// <param name="item">Объект для валидации</param>
        /// <returns>true если объект валиден, иначе false</returns>
        bool IsValid(T item);
        
        /// <summary>
        /// Получает сообщение об ошибке валидации
        /// </summary>
        /// <param name="item">Объект для валидации</param>
        /// <returns>Сообщение об ошибке</returns>
        string GetErrorMessage(T item);
    }
}
