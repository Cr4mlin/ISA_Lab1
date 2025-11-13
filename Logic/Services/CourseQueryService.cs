using DataAccessLayer;
using Model;

namespace Logic.Services
{
    /// <summary>
    /// Сервис для выполнения запросов и получения информации о курсах
    /// Отвечает за чтение данных без изменений
    /// </summary>
    public class CourseQueryService : ICourseQueryService
    {
        private readonly IRepository<Course> _repository;

        /// <summary>
        /// Инициализирует новый экземпляр сервиса запросов курсов
        /// </summary>
        /// <param name="repository">Репозиторий для работы с курсами</param>
        public CourseQueryService(IRepository<Course> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Возвращает список всех курсов
        /// </summary>
        /// <returns>Список всех курсов</returns>
        public List<Course> GetAllCourses() => _repository.ReadAll();

        /// <summary>
        /// Находит курс по указанному идентификатору
        /// </summary>
        /// <param name="id">Идентификатор курса</param>
        /// <returns>Найденный курс или null, если не найден</returns>
        public Course GetCourseById(int id) => _repository.ReadById(id);
    }
}
