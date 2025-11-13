using Model;

namespace Logic.Services
{
    /// <summary>
    /// Интерфейс для управления жизненным циклом курсов (создание, обновление, удаление)
    /// </summary>
    public interface ICourseManagementService
    {
        /// <summary>
        /// Создаёт новый курс с заданными параметрами
        /// </summary>
        /// <param name="courseName">Название курса</param>
        /// <param name="description">Описание курса</param>
        /// <param name="duration">Длительность курса в часах</param>
        /// <param name="price">Стоимость курса</param>
        /// <param name="teacherName">Имя преподавателя</param>
        /// <param name="status">Состояние курса (да/нет)</param>
        /// <returns>Созданный курс</returns>
        Course CreateCourse(string courseName, string description, int duration, decimal price, string teacherName, string status);

        /// <summary>
        /// Обновляет существующий курс с заданными параметрами
        /// </summary>
        /// <param name="courseId">ID курса для обновления</param>
        /// <param name="courseName">Название курса</param>
        /// <param name="description">Описание курса</param>
        /// <param name="duration">Длительность курса в часах</param>
        /// <param name="price">Стоимость курса</param>
        /// <param name="teacherName">Имя преподавателя</param>
        /// <param name="status">Состояние курса (да/нет)</param>
        /// <returns>Обновленный курс</returns>
        Course UpdateCourse(int courseId, string courseName, string description, int duration, decimal price, string teacherName, string status);

        /// <summary>
        /// Удаляет курс по указанному идентификатору
        /// </summary>
        /// <param name="courseId">Идентификатор курса для удаления</param>
        /// <returns>true если курс успешно удален, иначе false</returns>
        bool DeleteCourse(int courseId);

        /// <summary>
        /// Переключает статус активности курса (активный/неактивный)
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        void ToggleCourseStatus(int courseId);
    }
}
