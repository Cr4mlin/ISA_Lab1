using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DataAccessLayer
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр контекста базы данных с заданными параметрами
        /// </summary>
        /// <param name="options">Параметры конфигурации для DbContext</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        /// <summary>
        /// Настраивает параметры подключения к базе данных, если они не были настроены ранее
        /// </summary>
        /// <param name="optionsBuilder">Построитель параметров конфигурации</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("");
            }
        }
    }
}
