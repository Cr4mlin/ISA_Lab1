namespace Model
{
    public class Course : IDomainObject
    {
        // Свойства курса
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public string TeacherName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Переопределяет вывод сущности (Добавлено на всякий случай)
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            string courseStatus = IsActive ? "Да" : "Нет";
            return $"ID: {Id}, Название: {Name.Trim()} (Преп.: {TeacherName.Trim()}, Опис.: {Description.Trim()}, Дл.: {Duration} ч., Стоимость: {Price} руб., Активен: {courseStatus})";
        }
    }
}
