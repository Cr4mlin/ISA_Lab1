using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Построитель SQL-запросов для курсов
    /// </summary>
    public class CourseSqlQueryBuilder : SqlQueryBuilder<Course>
    {
        /// <summary>
        /// Строит SQL-запрос для вставки курса
        /// </summary>
        /// <returns>SQL-запрос для вставки</returns>
        public override string BuildInsertQuery()
        {
            var tableName = GetTableName();
            return $"INSERT INTO {tableName} (Name, Description, Duration, Price, TeacherName, IsActive) VALUES (@Name, @Description, @Duration, @Price, @TeacherName, @IsActive)";
        }

        /// <summary>
        /// Строит SQL-запрос для обновления курса
        /// </summary>
        /// <returns>SQL-запрос для обновления</returns>
        public override string BuildUpdateQuery()
        {
            var tableName = GetTableName();
            return $@"UPDATE {tableName} 
                     SET Name=@Name, Description=@Description, Duration=@Duration, 
                         Price=@Price, TeacherName=@TeacherName, IsActive=@IsActive 
                     WHERE Id=@Id";
        }
    }
}
