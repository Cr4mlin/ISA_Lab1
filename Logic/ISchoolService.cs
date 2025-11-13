using Model;

namespace Logic
{
    /// <summary>
    /// Интерфейс для сервиса управления курсами
    /// </summary>
    public interface ISchoolService
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
        /// <exception cref="InvalidPriceException">Выбрасывается при отрицательном значении цены</exception>
        /// <exception cref="InvalidDurationException">Выбрасывается при отрицательном значении продолжительности</exception>
        /// <exception cref="InvalidTeacherNameException">Выбрасывается при невалидном имени преподавателя</exception>
        /// <exception cref="InvalidIsActiveException">Выбрасывается при неверном указании состояния</exception>
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
        /// <exception cref="InvalidPriceException">Выбрасывается при отрицательном значении цены</exception>
        /// <exception cref="InvalidDurationException">Выбрасывается при отрицательном значении продолжительности</exception>
        /// <exception cref="InvalidTeacherNameException">Выбрасывается при невалидном имени преподавателя</exception>
        /// <exception cref="InvalidIsActiveException">Выбрасывается при неверном указании состояния</exception>
        Course UpdateCourse(int courseId, string courseName, string description, int duration, decimal price, string teacherName, string status);

        /// <summary>
        /// Выполняет поиск курсов по заданному тексту и свойствам
        /// </summary>
        /// <param name="searchText">Текст для поиска</param>
        /// <param name="searchProperties">Список свойств для поиска</param>
        /// <returns>Список найденных курсов</returns>
        /// <exception cref="ArgumentException">Выбрасывается если поле поиска пустое или не выбрано ни одного свойства</exception>
        List<Course> SearchCourses(string searchText, List<string> searchProperties);

        /// <summary>
        /// Удаляет курс по указанному идентификатору
        /// </summary>
        /// <param name="courseId">Идентификатор курса для удаления</param>
        /// <returns>true если курс успешно удален, иначе false</returns>
        bool DeleteCourse(int courseId);

        /// <summary>
        /// Возвращает список всех курсов
        /// </summary>
        /// <returns>Список всех курсов</returns>
        List<Course> GetAllCourses();

        /// <summary>
        /// Находит курс по указанному идентификатору
        /// </summary>
        /// <param name="id">Идентификатор курса</param>
        /// <returns>Найденный курс или null, если не найден</returns>
        Course GetCourseById(int id);

        /// <summary>
        /// Возвращает список активных курсов
        /// </summary>
        /// <returns>Список активных курсов</returns>
        List<Course> GetActiveCourses();

        /// <summary>
        /// Возвращает список курсов в указанном ценовом диапазоне
        /// </summary>
        /// <param name="minPrice">Минимальная цена</param>
        /// <param name="maxPrice">Максимальная цена</param>
        /// <returns>Список курсов в заданном ценовом диапазоне</returns>
        /// <exception cref="InvalidPriceRangeException">Выбрасывается при неверном ценовом диапазоне</exception>
        List<Course> GetCoursesInPriceRange(decimal minPrice, decimal maxPrice);

        /// <summary>
        /// Переключает статус активности курса (активный/неактивный)
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        void ToggleCourseStatus(int courseId);
    }
}

