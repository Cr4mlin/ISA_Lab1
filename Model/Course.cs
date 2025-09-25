namespace Model
{
    public class Course
    {
        // Свойства курса
        public string CourseName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CourseId { get; set; } = string.Empty;
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public string Teacher { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Переопределяет вывод сущности (Добавлено на всякий случай)
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            string courseStatus = IsActive ? "Да" : "Нет";
            return $"ID: {CourseId}, Название: {CourseName} (Преп.: {Teacher}, Дл.: {Duration} ч., Стоимость: {Price} руб., Активен: {courseStatus})";
        }
    }
}
