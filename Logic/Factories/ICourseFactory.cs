using Model;

namespace Logic.Factories
{
    /// <summary>
    /// Интерфейс фабрики для создания объектов курсов
    /// </summary>
    public interface ICourseFactory
    {
        /// <summary>
        /// Создает новый объект курса с указанными параметрами
        /// </summary>
        /// <param name="courseName">Название курса</param>
        /// <param name="description">Описание курса</param>
        /// <param name="duration">Длительность курса в часах</param>
        /// <param name="price">Стоимость курса</param>
        /// <param name="teacherName">Имя преподавателя</param>
        /// <param name="status">Состояние курса (да/нет)</param>
        /// <returns>Созданный объект курса</returns>
        Course CreateCourse(string courseName, string description, int duration, decimal price, string teacherName, string status);

        /// <summary>
        /// Обновляет существующий объект курса новыми данными
        /// </summary>
        /// <param name="existingCourse">Существующий курс для обновления</param>
        /// <param name="courseName">Название курса</param>
        /// <param name="description">Описание курса</param>
        /// <param name="duration">Длительность курса в часах</param>
        /// <param name="price">Стоимость курса</param>
        /// <param name="teacherName">Имя преподавателя</param>
        /// <param name="status">Состояние курса (да/нет)</param>
        void UpdateCourse(Course existingCourse, string courseName, string description, int duration, decimal price, string teacherName, string status);
    }
}
