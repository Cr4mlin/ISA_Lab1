using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DataAccessLayer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Logic
{
    public class ChooseConnection
    {
        public IRepository<Course> chooseRepository(string choiceRepository)
        {
            if (choiceRepository == "1" || choiceRepository == "EntityFrameWork")
            {
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlServer("Server=localhost\\SQLEXPRESS02;Database=School;Trusted_Connection=True;TrustServerCertificate=True;")
                    .Options;

                var context = new ApplicationDbContext(options);

                Console.WriteLine("\nИспользуется репозиторий Entity Framework.\n");

                return new EntityRepository<Course>(context);
            }

            else
            {
                IDbConnection connection = new SqlConnection("Server=localhost\\SQLEXPRESS02;Database=School;Trusted_Connection=True;TrustServerCertificate=True;");

                Console.WriteLine("\nИспользуется репозиторий Dapper.\n");

                return new DapperRepository<Course>(connection);
            }
        }

    }
}
