using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        /// <param name="schoolService"></param>
        public void CreateCourse()
        {
            Console.WriteLine("---СОЗДАНИЕ НОВОГО КУРСА---");

            try
            {
                Console.Write("Название курса: ");
                string name = Console.ReadLine();

                Console.Write("Описание: ");
                string description = Console.ReadLine();

                Console.Write("ID курса: ");
                string id = Console.ReadLine();

                Console.Write("Длительность (часов): ");
                if (!int.TryParse(Console.ReadLine(), out int duration))
                {
                    Console.WriteLine("Необходимо вводить число");
                    return;
                }

                Console.Write("Стоимость: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal price))
                {
                    Console.WriteLine("Необходимо вводить число");
                    return;
                }

                Console.Write("Преподаватель: ");
                string teacher = Console.ReadLine();

                Console.Write("Активный(Да/Нет): ");
                string status = Console.ReadLine().ToLower();

                var course = _schoolService.CreateCourse(name, description, id, duration, price, teacher, status);
                Console.WriteLine($"Курс создан успешно");
                Console.WriteLine(course);
            }
            catch (CourseIdExistsException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidDurationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidPriceException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidTeacherNameException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidIsActiveException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Функция для отображения всех курсов в консоли
        /// </summary>
        /// <param name="schoolService"></param>
        public void ShowAllCourses()
        {
            Console.Clear();
            Console.WriteLine("---ВСЕ КУРСЫ---");

            var courses = _schoolService.GetAllCourses();
            Console.WriteLine(courses);
        }

        /// <summary>
        /// Функция для отображения в консоли найденного курса
        /// </summary>
        /// <param name="schoolService"></param>
        public void FindCourseById()
        {
            Console.Clear();
            Console.WriteLine("---ПОИСК КУРСА ПО ID---");

            var courses = _schoolService.GetAllCourses();
            Console.WriteLine(courses);

            if (courses == null) 
            {
                Console.WriteLine("Курсов нет.");
                return; 
            } //Проверка наличия курсов

            Console.WriteLine("Введите ID курса:");

            string id = Console.ReadLine();

            try
            {
                var course = _schoolService.GetCourseById(id);
                Console.WriteLine($"Найденный курс: {course}");
            }
            catch (CourseNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Функция для обновления курса через консоль
        /// </summary>
        /// <param name="schoolService"></param>
        public void UpdateCourse()
        {
            Console.Clear();
            Console.WriteLine("---ОБНОВЛЕНИЕ ДАННЫХ О КУРСЕ---");

            var courses = _schoolService.GetAllCourses();
            Console.WriteLine(courses);

            if (courses == null) 
            {
                Console.WriteLine("Курсов нет");
                return; 
            } //Проверка наличия курсов

            Console.WriteLine("Введите ID курса, который хотите обновить: ");
            string id = Console.ReadLine();

            try
            {
                var excisitingCourse = _schoolService.GetCourseById(id);
                Console.WriteLine($"Текущие данные: {excisitingCourse}");

                Console.Write("Название курса: ");
                string newName = Console.ReadLine();

                Console.Write("Описание: ");
                string newDescription = Console.ReadLine();

                Console.Write("ID курса: ");
                string newId = Console.ReadLine();

                Console.Write("Длительность (часов): ");
                if (!int.TryParse(Console.ReadLine(), out int newDuration))
                {
                    Console.WriteLine("Введите число");
                    return;
                }

                Console.Write("Стоимость: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal newPrice))
                {
                    Console.WriteLine("Введите число");
                    return;
                }

                Console.Write("Преподаватель: ");
                string newTeacher = Console.ReadLine();

                Console.Write("Активный(Да/Нет): ");
                string newStatus = Console.ReadLine().ToLower();

                var course = _schoolService.UpdateCourse(id, newName, newDescription, newId, newDuration, newPrice, newTeacher, newStatus);
                Console.WriteLine($"Курс успешно изменён");
                Console.WriteLine(course);
            }
            catch (CourseIdExistsException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidDurationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidPriceException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidTeacherNameException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidIsActiveException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Функция для удаления курса по ID
        /// </summary>
        /// <param name="schoolService"></param>
        public void DeleteCourse()
        {
            Console.WriteLine("---Удаление курсов---\n");

            Console.WriteLine("Все курсы:");

            var courses = _schoolService.GetAllCourses();
            Console.WriteLine(courses);

            if (courses == null) 
            {
                Console.WriteLine("Курсов нет.");
                return; 
            } //Проверка наличия курсов

            Console.Write("Введите ID курса который хотите удалить: ");
            string id = Console.ReadLine();

            try
            {
                _schoolService.DeleteCourse(id);
                Console.WriteLine("Курс успешно удалён");
            }
            catch (CourseNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Функция для отображения всех активных курсов в консоли
        /// </summary>
        /// <param name="schoolService"></param>
        public void ShowActiveCourses()
        {
            Console.WriteLine("Все активные курсы:");

            Console.WriteLine(_schoolService.GetActiveCourses());
        }

        /// <summary>
        /// Функция для отображения в консоли курсов входящих в указанный ценовой диапазон
        /// </summary>
        /// <param name="schoolService"></param>
        public void ShowCoursesByPriceRange()
        {
            Console.WriteLine("---ПОИСК В ЦЕНОВОМ ДИАПАЗОНЕ");

            var courses = _schoolService.GetAllCourses();
            Console.WriteLine(courses);

            if (courses == null)
            {
                Console.WriteLine("Курсов нет.");
                return; 
            } //Проверка наличия курсов

            Console.Write("Введите минимальную цену: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal minPrice))
            {
                Console.WriteLine("Необходимо вводить число");
                return;
            }

            Console.Write("Введите максимальную цену: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal maxPrice))
            {
                Console.WriteLine("Необходимо вводить число");
                return;
            }

            try
            {
                Console.WriteLine($"Курсы в ценовом диапазоне {minPrice}-{maxPrice} руб.");
                Console.WriteLine(_schoolService.GetCoursesInPriceRange(minPrice, maxPrice));
            }
            catch (InvalidPriceRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Функция для изменения статуса курса через консоль
        /// </summary>
        /// <param name="schoolService"></param>
        public void ToggleCourseStatus()
        {
            Console.WriteLine("---ИЗМЕНИТЬ СТАТУС КУРСА---");

            Console.WriteLine("Все курсы:");

            var courses = _schoolService.GetAllCourses();
            Console.WriteLine(courses);

            if (courses == null) 
            {
                Console.WriteLine("Курсов нет.");
                return; 
            } //Проверка наличия курсов

            Console.Write("Введите ID курса статус которого вы хотите изменить: ");
            string id = Console.ReadLine();

            try
            {
                _schoolService.ToggleCourseStatus(id);
                Console.WriteLine("Статус успешно изменён.");
            }
            catch (CourseNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
