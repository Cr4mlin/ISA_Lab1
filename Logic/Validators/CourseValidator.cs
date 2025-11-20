using Logic.Exceptions;

namespace Logic.Validators
{
    /// <summary>
    /// Композитный валидатор для проверки данных курса
    /// Объединяет несколько специализированных валидаторов
    /// </summary>
    public class CourseValidator : ICourseValidator
    {
        private readonly IPriceValidator _priceValidator;
        private readonly IDurationValidator _durationValidator;
        private readonly ITeacherNameValidator _teacherNameValidator;
        private readonly IStatusValidator _statusValidator;

        /// <summary>
        /// Инициализирует новый экземпляр композитного валидатора курсов
        /// </summary>
        /// <param name="priceValidator">Валидатор цены</param>
        /// <param name="durationValidator">Валидатор длительности</param>
        /// <param name="teacherNameValidator">Валидатор имени преподавателя</param>
        /// <param name="statusValidator">Валидатор статуса</param>
        public CourseValidator(
            IPriceValidator priceValidator,
            IDurationValidator durationValidator,
            ITeacherNameValidator teacherNameValidator,
            IStatusValidator statusValidator)
        {
            _priceValidator = priceValidator ?? throw new ArgumentNullException(nameof(priceValidator));
            _durationValidator = durationValidator ?? throw new ArgumentNullException(nameof(durationValidator));
            _teacherNameValidator = teacherNameValidator ?? throw new ArgumentNullException(nameof(teacherNameValidator));
            _statusValidator = statusValidator ?? throw new ArgumentNullException(nameof(statusValidator));
        }

        /// <summary>
        /// Проверяет валидность всех данных курса
        /// </summary>
        /// <param name="price">Цена курса</param>
        /// <param name="duration">Длительность курса</param>
        /// <param name="teacherName">Имя преподавателя</param>
        /// <param name="status">Статус курса</param>
        /// <exception cref="InvalidPriceException">Выбрасывается при отрицательном значении цены</exception>
        /// <exception cref="InvalidDurationException">Выбрасывается при отрицательном значении продолжительности</exception>
        /// <exception cref="InvalidTeacherNameException">Выбрасывается при невалидном имени преподавателя</exception>
        /// <exception cref="InvalidIsActiveException">Выбрасывается при неверном указании состояния</exception>
        public void Validate(decimal price, int duration, string teacherName, string status)
        {
            if (!_priceValidator.IsValid(price))
            {
                throw new InvalidPriceException(price);
            }

            if (!_durationValidator.IsValid(duration))
            {
                throw new InvalidDurationException(duration);
            }

            if (!_teacherNameValidator.IsValid(teacherName))
            {
                throw new InvalidTeacherNameException(teacherName);
            }

            if (!_statusValidator.IsValid(status))
            {
                throw new InvalidIsActiveException(status);
            }
        }
    }
}
