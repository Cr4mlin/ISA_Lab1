namespace Logic
{
    /// <summary>
    /// Исключение для случая, когда курс с таким кодом уже существует.
    /// </summary>
    //public class CourseIdExistsException : Exception
    //{
    //    /// <summary>
    //    /// Исключение для случая, когда курс с таким кодом уже существует.
    //    /// </summary>
    //    public CourseIdExistsException(string courseId)
    //        : base($"Курс с ID '{courseId}' уже существует.") { }
    //}

    /// <summary>
    /// Исключение для недопустимой стоимости курса.
    /// </summary>
    public class InvalidPriceException : Exception
    {
        /// <summary>
        /// Исключение для недопустимой стоимости курса.
        /// </summary>
        public InvalidPriceException(decimal price)
            : base($"Стоимость курса не может быть отрицательной.") { }
    }

    /// <summary>
    /// Исключение для недопустимой продолжительности курса.
    /// </summary>
    public class InvalidDurationException : Exception
    {
        /// <summary>
        /// Исключение для недопустимой продолжительности курса.
        /// </summary>
        public InvalidDurationException(int duration)
            : base($"Продолжительность курса должна быть положительной.") { }
    }

    /// <summary>
    /// Исключение для невалидного имени преподавателя.
    /// </summary>
    public class InvalidTeacherNameException : Exception
    {
        /// <summary>
        /// Исключение для невалидного имени преподавателя.
        /// </summary>
        public InvalidTeacherNameException(string teacherName)
            : base($"Имя преподавателя может содержать только буквы, пробелы, дефисы и апострофы.") { }
    }

    /// <summary>
    /// Исключение для случая, когда курс не найден.
    /// </summary>
    public class CourseNotFoundException : Exception
    {
        /// <summary>
        /// Исключение для случая, когда курс не найден.
        /// </summary>
        public CourseNotFoundException(string courseId)
            : base($"Курс с кодом '{courseId}' не найден.") { }
    }

    /// <summary>
    /// Исключение для случая, если введено неверное состояние для курса
    /// </summary>
    public class InvalidIsActiveException : Exception
    {
        /// <summary>
        /// Исключение для случая, если введено неверное состояние для курса
        /// </summary>
        public InvalidIsActiveException(string status)
            : base("Введено неверное состояние курса") { }
    }

    /// <summary>
    /// Исключение для неверного ценового диапазона.
    /// </summary>
    public class InvalidPriceRangeException : Exception
    {
        /// <summary>
        /// Исключение для неверного ценового диапазона.
        /// </summary>
        public InvalidPriceRangeException(decimal minPrice, decimal maxPrice)
            : base($"Неверный ценовой диапазон") { }
    }

    /// <summary>
    /// Исключение для неверного свойства курса
    /// </summary>
    //public class PropertyNotFoundException : Exception
    //{
    //    public PropertyNotFoundException(string propertyName)
    //        : base($"Курсы не содержат свойства {propertyName}.") { }
    //}
}
