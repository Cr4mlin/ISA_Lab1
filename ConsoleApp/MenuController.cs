using Logic;

namespace ConsoleApp
{
    internal class MenuController
    {
        private readonly SchoolService _schoolService;

        public MenuController(SchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        /// <summary>
        /// Функция для отображения меню консоли
        /// </summary>
        public void DisplayMenu()
        {
            Console.WriteLine("---СИСТЕМА УПРАВЛЕНИЯ КУРСАМИ---\n");
            Console.WriteLine("МЕНЮ:");
            Console.WriteLine("1. Создать новый курс");
            Console.WriteLine("2. Показать все курсы");
            Console.WriteLine("3. Найти курс по ID");
            Console.WriteLine("4. Обновить курс");
            Console.WriteLine("5. Удалить курс");
            Console.WriteLine("6. Показать активные курсы");
            Console.WriteLine("7. Поиск курсов по ценовому диапазону");
            Console.WriteLine("8. Изменить статус курса (активный/неактивный)");
            Console.WriteLine("9. Выход");
        }

        /// <summary>
        /// Функция для создания курса через консоль
        /// </summary>
        public void CreateCourse()
        {
            Console.Clear();
            Console.WriteLine("---СОЗДАНИЕ НОВОГО КУРСА---\n");

            Console.Write("Название курса: ");
            string courseName = Console.ReadLine();

            Console.Write("Описание: ");
            string courseDescription = Console.ReadLine();

            Console.Write("ID курса: ");
            string courseId = Console.ReadLine();

            Console.Write("Длительность (часов): ");
            string inputCourseDuration = Console.ReadLine();
            if (string.IsNullOrEmpty(inputCourseDuration)) { inputCourseDuration = "0"; }
            if (!int.TryParse(inputCourseDuration, out int courseDuration))
            {
                Console.WriteLine("Необходимо вводить число");
                return;
            }

            Console.Write("Стоимость: ");
            string inputCoursePrice = Console.ReadLine();
            if (string.IsNullOrEmpty(inputCoursePrice)) { inputCoursePrice = "0"; }
            if (!decimal.TryParse(inputCoursePrice, out decimal coursePrice))
            {
                Console.WriteLine("Необходимо вводить число");
                return;
            }

            Console.Write("Преподаватель: ");
            string teacherName = Console.ReadLine();

            Console.Write("Активный(Да/Нет): ");
            string courseStatus = Console.ReadLine().ToLower();

            var course = _schoolService.CreateCourse(courseName, courseDescription, courseId, 
                courseDuration, coursePrice, teacherName, courseStatus);
            Console.WriteLine($"Курс создан успешно");
            Console.WriteLine(course);
        }

        /// <summary>
        /// Функция для отображения всех курсов в консоли
        /// </summary>
        public void ShowAllCourses()
        {
            Console.Clear();
            Console.WriteLine("---ВСЕ КУРСЫ---\n");

            var courses = _schoolService.GetAllCourses();

            if (courses.Count == 0)
            {
                Console.WriteLine("Курсов нет.");
                return;
            }

            foreach (var course in courses)
            {
                Console.WriteLine(course);
            }
        }

        /// <summary>
        /// Функция для отображения в консоли найденного курса
        /// </summary>
        public void FindCourseById()
        {
            Console.Clear();
            Console.WriteLine("---ПОИСК КУРСА ПО ID---\n");

            var courses = _schoolService.GetAllCourses();

            if (courses.Count == 0) 
            {
                Console.WriteLine("Курсов нет.");
                return; 
            } //Проверка наличия курсов

            Console.WriteLine("Введите ID курса:");

            string courseId = Console.ReadLine();

            var course = _schoolService.GetCourseById(courseId);
            Console.WriteLine($"Найденный курс: {course}");
        }

        /// <summary>
        /// Функция для обновления курса через консоль
        /// </summary>
        public void UpdateCourse()
        {
            Console.Clear();
            Console.WriteLine("---ОБНОВЛЕНИЕ ДАННЫХ О КУРСЕ---\n");

            var courses = _schoolService.GetAllCourses();

            if (courses.Count == 0) 
            {
                Console.WriteLine("Курсов нет");
                return; 
            } //Проверка наличия курсов

            foreach (var course in courses)
            {
                Console.WriteLine(course);
            }

            Console.WriteLine("Введите ID курса, который хотите обновить: ");
            string oldCourseId = Console.ReadLine();

            var excisitingCourse = _schoolService.GetCourseById(oldCourseId);
            Console.WriteLine($"Текущие данные: {excisitingCourse}");

            Console.WriteLine("Можете оставлять поля пустыми для того, чтобы не изменять параметры.");

            Console.Write("Название курса: ");
            string newCourseName = ReadLineWithDefault(Console.ReadLine(), excisitingCourse.Name);

            Console.Write("Описание: ");
            string newCourseDescription = ReadLineWithDefault(Console.ReadLine(), excisitingCourse.Description);

            Console.Write("ID курса: ");
            string newCourseId = ReadLineWithDefault(Console.ReadLine(), excisitingCourse.Id);

            Console.Write("Длительность (часов): ");
            string newDuration = ReadLineWithDefault(Console.ReadLine(), Convert.ToString(excisitingCourse.Duration));
            if (!int.TryParse(newDuration, out int newCourseDuration))
            {
                Console.WriteLine("Введите число");
                return;
            }

            Console.Write("Стоимость: ");
            string newPrice = ReadLineWithDefault(Console.ReadLine(), Convert.ToString(excisitingCourse.Price));
            if (!decimal.TryParse(newPrice, out decimal newCoursePrice))
            {
                Console.WriteLine("Введите число");
                return;
            }

            Console.Write("Преподаватель: ");
            string newTeacherName = ReadLineWithDefault(Console.ReadLine(), excisitingCourse.TeacherName);

            Console.Write("Активный(Да/Нет): ");
            string courseStatus = "нет";
            if (excisitingCourse.IsActive) { courseStatus = "да"; }
            string newCourseStatus = ReadLineWithDefault(Console.ReadLine(), courseStatus);

            var updateCourse = _schoolService.UpdateCourse(oldCourseId, newCourseName, newCourseDescription, newCourseId, 
                newCourseDuration, newCoursePrice, newTeacherName, newCourseStatus);
            Console.WriteLine($"Курс успешно изменён");
            Console.WriteLine(updateCourse);
        }

        /// <summary>
        /// Функция для удаления курса по ID
        /// </summary>
        public void DeleteCourse()
        {
            Console.Clear();
            Console.WriteLine("---Удаление курсов---\n");

            Console.WriteLine("Все курсы:");

            var courses = _schoolService.GetAllCourses();

            if (courses.Count == 0) 
            {
                Console.WriteLine("Курсов нет.");
                return; 
            } //Проверка наличия курсов

            foreach (var course in courses)
            {
                Console.WriteLine(course);
            }

            Console.Write("Введите ID курса который хотите удалить: ");
            string courseId = Console.ReadLine();

            _schoolService.DeleteCourse(courseId);
            Console.WriteLine("Курс успешно удалён");
        }

        /// <summary>
        /// Функция для отображения всех активных курсов в консоли
        /// </summary>
        public void ShowActiveCourses()
        {
            Console.Clear();
            Console.WriteLine("Все активные курсы:");

            var activeCourses = _schoolService.GetActiveCourses();

            if (activeCourses.Count == 0)
            {
                Console.WriteLine("Активных курсов нет.");
                return;
            }

            foreach( var course in activeCourses ) 
            { 
                Console.WriteLine(course); 
            }
        }

        /// <summary>
        /// Функция для отображения в консоли курсов входящих в указанный ценовой диапазон
        /// </summary>
        public void ShowCoursesByPriceRange()
        {
            Console.Clear();
            Console.WriteLine("---ПОИСК В ЦЕНОВОМ ДИАПАЗОНЕ---\n");

            var courses = _schoolService.GetAllCourses();

            if (courses.Count == 0)
            {
                Console.WriteLine("Курсов нет.");
                return; 
            } //Проверка наличия курсов

            Console.Write("Введите минимальную цену: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal minCoursePrice))
            {
                Console.WriteLine("Необходимо вводить число");
                return;
            }

            Console.Write("Введите максимальную цену: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal maxCoursePrice))
            {
                Console.WriteLine("Необходимо вводить число");
                return;
            }

            Console.WriteLine($"\nКурсы в ценовом диапазоне {minCoursePrice}-{maxCoursePrice} руб.:");
            var courseInPriceRange = _schoolService.GetCoursesInPriceRange(minCoursePrice, maxCoursePrice);

            if (courseInPriceRange.Count == 0)
            {
                Console.WriteLine("Курсов в заданном ценовом диапазоне нет.");
            }

            foreach (var course in courseInPriceRange)
            {
                Console.WriteLine(course);
            }
        }

        /// <summary>
        /// Функция для изменения статуса курса через консоль
        /// </summary>
        public void ToggleCourseStatus()
        {
            Console.Clear();
            Console.WriteLine("---ИЗМЕНИТЬ СТАТУС КУРСА---\n");

            Console.WriteLine("Все курсы:");

            var courses = _schoolService.GetAllCourses();

            if (courses.Count == 0) 
            {
                Console.WriteLine("Курсов нет.");
                return; 
            } //Проверка наличия курсов

            foreach (var course in courses)
            {
                Console.WriteLine(course);
            }

            Console.Write("Введите ID курса статус которого вы хотите изменить: ");
            string courseId = Console.ReadLine();

            _schoolService.ToggleCourseStatus(courseId);
            Console.WriteLine("Статус успешно изменён.");
        }

        public string ReadLineWithDefault(string line, string oldLine)
        {
            if (string.IsNullOrEmpty(line))
            {
                return oldLine;
            }
            else
            {
                return line;
            }
        }
    }
}
