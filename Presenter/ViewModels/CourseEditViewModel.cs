using System;
using System.Windows.Input;
using Logic;

namespace Presenter.ViewModels
{
    /// <summary>
    /// ViewModel для добавления/редактирования курса
    /// </summary>
    public class CourseEditViewModel : BaseViewModel
    {
        private readonly ISchoolService _schoolService;
        private readonly int? _courseId;

        private string _name = string.Empty;
        private string _description = string.Empty;
        private int _duration;
        private decimal _price;
        private string _teacherName = string.Empty;
        private bool _isActive = true;
        private string _errorMessage = string.Empty;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public int Duration
        {
            get => _duration;
            set => SetProperty(ref _duration, value);
        }

        public decimal Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        public string TeacherName
        {
            get => _teacherName;
            set => SetProperty(ref _teacherName, value);
        }

        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public event EventHandler? CourseSaved;
        public event EventHandler? Cancelled;

        public CourseEditViewModel(ISchoolService schoolService, int? courseId = null)
        {
            _schoolService = schoolService ?? throw new ArgumentNullException(nameof(schoolService));
            _courseId = courseId;

            SaveCommand = new RelayCommand(Save, CanSave);
            CancelCommand = new RelayCommand(Cancel);

            if (_courseId.HasValue)
            {
                LoadCourse();
            }
        }

        private void LoadCourse()
        {
            try
            {
                var course = _schoolService.GetCourseById(_courseId!.Value);
                if (course != null)
                {
                    Name = course.Name;
                    Description = course.Description;
                    Duration = course.Duration;
                    Price = course.Price;
                    TeacherName = course.TeacherName;
                    IsActive = course.IsActive;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Ошибка загрузки курса: {ex.Message}";
            }
        }

        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(Name) &&
                   !string.IsNullOrWhiteSpace(TeacherName) &&
                   Duration > 0 &&
                   Price >= 0;
        }

        private void Save()
        {
            try
            {
                ErrorMessage = string.Empty;
                string status = IsActive ? "да" : "нет";

                if (_courseId.HasValue)
                {
                    // Редактирование
                    _schoolService.UpdateCourse(_courseId.Value, Name, Description, Duration, Price, TeacherName, status);
                }
                else
                {
                    // Добавление
                    _schoolService.CreateCourse(Name, Description, Duration, Price, TeacherName, status);
                }

                CourseSaved?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Ошибка сохранения: {ex.Message}";
            }
        }

        private void Cancel()
        {
            Cancelled?.Invoke(this, EventArgs.Empty);
        }
    }
}
