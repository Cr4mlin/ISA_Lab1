using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Logic;
using Shared.DTO;

namespace Presenter.ViewModels
{
    /// <summary>
    /// ViewModel для главного окна пользователя
    /// </summary>
    public class UserMainViewModel : BaseViewModel
    {
        private readonly ISchoolService _schoolService;
        private readonly int _currentUserId;

        private string _userName = string.Empty;
        private string _nickName = string.Empty;
        private ObservableCollection<ObservableCourseDto> _courses;
        private ObservableCollection<ObservableCourseDto> _purchasedCourses;
        private ObservableCourseDto? _selectedCourse;
        private string _searchText = string.Empty;

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        public string NickName
        {
            get => _nickName;
            set => SetProperty(ref _nickName, value);
        }

        public ObservableCollection<ObservableCourseDto> Courses
        {
            get => _courses;
            set => SetProperty(ref _courses, value);
        }

        public ObservableCollection<ObservableCourseDto> PurchasedCourses
        {
            get => _purchasedCourses;
            set => SetProperty(ref _purchasedCourses, value);
        }

        public ObservableCourseDto? SelectedCourse
        {
            get => _selectedCourse;
            set => SetProperty(ref _selectedCourse, value);
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    SearchCourses();
                }
            }
        }

        public ICommand LoadAllCoursesCommand { get; }
        public ICommand LoadPurchasedCoursesCommand { get; }
        public ICommand PurchaseCourseCommand { get; }
        public ICommand ChangeAvatarCommand { get; }

        public int CurrentUserId => _currentUserId;

        public System.Drawing.Image? GetAvatar()
        {
            try
            {
                return _schoolService.LoadAvatar(_currentUserId);
            }
            catch
            {
                return null;
            }
        }

        public UserMainViewModel(ISchoolService schoolService, int userId)
        {
            _schoolService = schoolService ?? throw new ArgumentNullException(nameof(schoolService));
            _currentUserId = userId;

            _courses = new ObservableCollection<ObservableCourseDto>();
            _purchasedCourses = new ObservableCollection<ObservableCourseDto>();

            LoadAllCoursesCommand = new RelayCommand(LoadAllCourses);
            LoadPurchasedCoursesCommand = new RelayCommand(LoadPurchasedCourses);
            PurchaseCourseCommand = new RelayCommand(PurchaseCourse, CanPurchaseCourse);
            ChangeAvatarCommand = new RelayCommand(param => ChangeAvatar(param as string));

            LoadUserInfo();
            LoadAllCourses();
        }

        private void LoadUserInfo()
        {
            try
            {
                var user = _schoolService.GetUserById(_currentUserId);
                if (user != null)
                {
                    UserName = user.Login;
                    NickName = user.NickName;
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine($"Ошибка загрузки информации о пользователе: {ex.Message}");
            }
        }

        private void LoadAllCourses()
        {
            try
            {
                var courses = _schoolService.GetAllCourses();
                var courseDtos = DtoMapper.ToDto(courses);

                Courses.Clear();
                foreach (var dto in courseDtos)
                {
                    Courses.Add(ObservableCourseDto.FromDto(dto));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки курсов: {ex.Message}");
            }
        }

        private void LoadPurchasedCourses()
        {
            try
            {
                var courses = _schoolService.GetPurchasedCourses(_currentUserId);
                var courseDtos = DtoMapper.ToDto(courses);

                PurchasedCourses.Clear();
                foreach (var dto in courseDtos)
                {
                    PurchasedCourses.Add(ObservableCourseDto.FromDto(dto));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки купленных курсов: {ex.Message}");
            }
        }

        private bool CanPurchaseCourse()
        {
            return SelectedCourse != null;
        }

        private void PurchaseCourse()
        {
            if (SelectedCourse == null) return;

            try
            {
                // Проверяем, не куплен ли уже курс
                if (_schoolService.HasPurchasedCourse(_currentUserId, SelectedCourse.Id))
                {
                    System.Windows.MessageBox.Show(
                        "Вы уже купили этот курс!",
                        "Информация",
                        System.Windows.MessageBoxButton.OK,
                        System.Windows.MessageBoxImage.Information);
                    return;
                }

                var result = System.Windows.MessageBox.Show(
                    $"Вы уверены, что хотите купить курс \"{SelectedCourse.Name}\" за {SelectedCourse.Price:N2}?",
                    "Подтверждение покупки",
                    System.Windows.MessageBoxButton.YesNo,
                    System.Windows.MessageBoxImage.Question);

                if (result == System.Windows.MessageBoxResult.Yes)
                {
                    _schoolService.PurchaseCourse(_currentUserId, SelectedCourse.Id);
                    System.Windows.MessageBox.Show(
                        "Курс успешно приобретен!",
                        "Успех",
                        System.Windows.MessageBoxButton.OK,
                        System.Windows.MessageBoxImage.Information);
                    LoadAllCourses();
                    LoadPurchasedCourses();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(
                    $"Ошибка покупки курса: {ex.Message}",
                    "Ошибка",
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Error);
            }
        }

        private void SearchCourses()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    LoadAllCourses();
                    return;
                }

                var searchProperties = new System.Collections.Generic.List<string>
                {
                    "Name", "Description", "TeacherName"
                };
                var courses = _schoolService.SearchCourses(SearchText, searchProperties);
                System.Diagnostics.Debug.WriteLine($"SearchCourses: найдено {courses.Count} курсов для запроса '{SearchText}'");

                var courseDtos = DtoMapper.ToDto(courses);

                Courses.Clear();
                foreach (var dto in courseDtos)
                {
                    Courses.Add(ObservableCourseDto.FromDto(dto));
                }

                System.Diagnostics.Debug.WriteLine($"SearchCourses: добавлено {Courses.Count} курсов в ObservableCollection");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(
                    $"Ошибка поиска курсов: {ex.Message}",
                    "Ошибка",
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Error);
            }
        }

        private void ChangeAvatar(string? imagePath)
        {
            if (string.IsNullOrEmpty(imagePath)) return;

            try
            {
                var image = System.Drawing.Image.FromFile(imagePath);
                _schoolService.SaveAvatar(_currentUserId, image);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка изменения аватара: {ex.Message}");
            }
        }
    }
}
