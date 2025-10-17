using DataAccessLayer;
using System.Data;
using System.Data.SqlClient;
using System;
using Logic;
using Microsoft.EntityFrameworkCore;
using Model;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.Identity.Client;
using Microsoft.Data.SqlClient;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("---СИСТЕМА УПРАВЛЕНИЯ КУРСАМИ---\n");
            Console.WriteLine("Выберите режим работы с БД:");
            Console.WriteLine("1. Entity Framework");
            Console.WriteLine("2. Dapper");
            Console.Write("Ваш выбор: ");

            string choiceRepository = Console.ReadLine() ?? "1";

            IRepository<Course> repository;

            // Entity Framework
            if (choiceRepository == "1")
            {
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlServer("Server=DESKTOP-VLCID23\\SQLEXPRESS02;Database=School;Trusted_Connection=True;TrustServerCertificate=True;")
                    .Options;

                var context = new ApplicationDbContext(options);
                repository = new EntityRepository<Course>(context);

                Console.WriteLine("\nИспользуется репозиторий Entity Framework.\n");
            }
            // Dapper
            else
            {
                IDbConnection connection = new SqlConnection("Server=DESKTOP-VLCID23\\SQLEXPRESS02;Database=School;Trusted_Connection=True;TrustServerCertificate=True;");
                repository = new DapperRepository<Course>(connection);

                //Заглушка
                //var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                //   .UseSqlServer("Data Source=courses.db")
                //   .Options;
                //var context = new ApplicationDbContext(options);
                //repository = new EntityRepository<Course>(context);

                Console.WriteLine("\nИспользуется репозиторий Dapper.\n");
            }

            var schoolService = new SchoolService(repository);
            var menuController = new MenuController(schoolService);

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
                                menuController.SearchCourse();
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
                    //catch (CourseIdExistsException ex)
                    //{
                    //    Console.WriteLine(ex.Message);
                    //}
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
                    catch (FormatException ex)
                    {
                        Console.WriteLine("Введены данные не того типа!");
                    }
                    catch (OverflowException ex)
                    {
                        Console.WriteLine("Введённое число слишком большое!");
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