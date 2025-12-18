using System.ComponentModel;
using System.Runtime.CompilerServices;
using Shared.DTO;

namespace Shared.ObservableDTO
{
    public class ObservableCourseDto : INotifyPropertyChanged
    {
        private int _id;
        private string _name = string.Empty;
        private string _description = string.Empty;
        private int _duration;
        private decimal _price;
        private string _teacherName = string.Empty;
        private bool _isActive;

        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Duration
        {
            get => _duration;
            set
            {
                if (_duration != value)
                {
                    _duration = value;
                    OnPropertyChanged();
                }
            }
        }

        public decimal Price
        {
            get => _price;
            set
            {
                if (_price != value)
                {
                    _price = value;
                    OnPropertyChanged();
                }
            }
        }

        public string TeacherName
        {
            get => _teacherName;
            set
            {
                if (_teacherName != value)
                {
                    _teacherName = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Создает ObservableCourseDto из обычного CourseDto
        /// </summary>
        public static ObservableCourseDto FromDto(CourseDto dto)
        {
            return new ObservableCourseDto
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Duration = dto.Duration,
                Price = dto.Price,
                TeacherName = dto.TeacherName,
                IsActive = dto.IsActive
            };
        }

        /// <summary>
        /// Конвертирует обратно в обычный CourseDto
        /// </summary>
        public CourseDto ToDto()
        {
            return new CourseDto
            {
                Id = Id,
                Name = Name,
                Description = Description,
                Duration = Duration,
                Price = Price,
                TeacherName = TeacherName,
                IsActive = IsActive
            };
        }
    }
}
