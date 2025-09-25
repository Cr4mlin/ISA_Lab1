using Logic;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main()
        {
            SchoolService schoolService = new SchoolService();
            MenuController menuController = new MenuController(schoolService);

            while (true)
            {
                menuController.DisplayMenu();
                Console.Write("Выберите действие: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    try
                    {
                        Console.WriteLine();
                        switch (choice)
                        {
                            case 1:
                                menuController.CreateCourse();
                                break;
                            case 2:
                                menuController.ShowAllCourses();
                                break;
                            case 3:
                                menuController.FindCourseById();
                                break;
                            case 4:
                                menuController.UpdateCourse();
                                break;
                            case 5:
                                menuController.DeleteCourse();
                                break;
                            case 6:
                                menuController.ShowActiveCourses();
                                break;
                            case 7:
                                menuController.ShowCoursesByPriceRange();
                                break;
                            case 8:
                                menuController.ToggleCourseStatus();
                                break;
                            case 9:
                                Console.WriteLine("Выход из программы...");
                                return;
                            default:
                                Console.WriteLine("Неверный выбор. Попробуйте снова.");
                                break;
                        }
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
                else
                {
                    Console.WriteLine("Неверный ввод. Введите число от 1 до 9.");
                }

                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}