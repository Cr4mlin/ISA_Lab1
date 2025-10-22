using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Базовый интерфейс для доменных объектов с идентификатором
    /// </summary>
    public interface IDomainObject
    {
        /// <summary>
        /// Уникальный идентификатор объекта
        /// </summary>
        int Id { get; set; }
    }
}
