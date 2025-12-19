using System.Collections.ObjectModel;
using System.Windows.Input;
using Logic;
using Shared.DTO;

namespace Presenter.ViewModels
{
    /// <summary>
    /// ViewModel для главного окна администратора
    /// </summary>
    public class AdminMainViewModel : BaseViewModel
    {
        private readonly ISchoolService _schoolService;
        private readonly int _currentUserId;

        private string _nickName = string.Empty;
        private ObservableCollection<ObservableCourseDto> _courses;
        private ObservableCollection<ObservableUserDto> _users;
        private ObservableCourseDto? _selectedCourse;
        private ObservableUserDto? _selectedUser;
        private string _searchText = string.Empty;

        public event EventHandler<CourseEditViewModel>? CourseAddRequested;

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

        public ObservableCollection<ObservableUserDto> Users
        {
            get => _users;
            set => SetProperty(ref _users, value);
        }

        public ObservableCourseDto? SelectedCourse
        {
            get => _selectedCourse;
            set => SetProperty(ref _selectedCourse, value);
        }

        public ObservableUserDto? SelectedUser
        {
            get => _selectedUser;
            set => SetProperty(ref _selectedUser, value);
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

        public ICommand LoadCoursesCommand { get; }
        public ICommand LoadUsersCommand { get; }
        public ICommand DeleteCourseCommand { get; }
        public ICommand EditCourseCommand { get; }
        public ICommand ToggleCourseStatusCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand AddCourseCommand { get; }
        public ICommand ExportCommand { get; }
        public ICommand ChangeAvatarCommand { get; }

        public int CurrentUserId => _currentUserId;

        public Image? GetAvatar()
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

        public AdminMainViewModel(ISchoolService schoolService, int userId)
        {
            _schoolService = schoolService ?? throw new ArgumentNullException(nameof(schoolService));
            _currentUserId = userId;

            _courses = new ObservableCollection<ObservableCourseDto>();
            _users = new ObservableCollection<ObservableUserDto>();

            LoadCoursesCommand = new RelayCommand(LoadCourses);
            LoadUsersCommand = new RelayCommand(LoadUsers);
            DeleteCourseCommand = new RelayCommand(DeleteCourse, CanDeleteCourse);
            EditCourseCommand = new RelayCommand(EditCourse, CanEditCourse);
            ToggleCourseStatusCommand = new RelayCommand(ToggleCourseStatus, CanToggleCourseStatus);
            DeleteUserCommand = new RelayCommand(DeleteUser, CanDeleteUser);
            AddCourseCommand = new RelayCommand(AddCourse);
            ExportCommand = new RelayCommand(Export);
            ChangeAvatarCommand = new RelayCommand(param => ChangeAvatar(param as string));

            LoadUserInfo();
            LoadCourses();
        }

        private void LoadUserInfo()
        {
            try
            {
                var user = _schoolService.GetUserById(_currentUserId);
                if (user != null)
                {
                    NickName = user.NickName;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка загрузки информации о пользователе: {ex.Message}");
            }
        }

        private void LoadCourses()
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
                System.Diagnostics.Debug.WriteLine($"Ошибка загрузки курсов: {ex.Message}");
            }
        }

        private void LoadUsers()
        {
            try
            {
                var users = _schoolService.GetAllUsers();
                var userDtos = DtoMapper.ToDto(users);

                Users.Clear();
                foreach (var dto in userDtos)
                {
                    Users.Add(ObservableUserDto.FromDto(dto));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка загрузки пользователей: {ex.Message}");
            }
        }

        private bool CanDeleteCourse()
        {
            return SelectedCourse != null;
        }

        private void DeleteCourse()
        {
            if (SelectedCourse == null) return;

            var result = System.Windows.MessageBox.Show(
                $"Вы уверены, что хотите удалить курс \"{SelectedCourse.Name}\"?",
                "Подтверждение удаления",
                System.Windows.MessageBoxButton.YesNo,
                System.Windows.MessageBoxImage.Warning);

            if (result != System.Windows.MessageBoxResult.Yes) return;

            try
            {
                _schoolService.DeleteCourse(SelectedCourse.Id);
                LoadCourses();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(
                    $"Ошибка удаления курса: {ex.Message}",
                    "Ошибка",
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Error);
            }
        }

        private bool CanToggleCourseStatus()
        {
            return SelectedCourse != null;
        }

        private void ToggleCourseStatus()
        {
            if (SelectedCourse == null) return;

            try
            {
                _schoolService.ToggleCourseStatus(SelectedCourse.Id);
                LoadCourses();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка изменения статуса курса: {ex.Message}");
            }
        }

        private bool CanDeleteUser()
        {
            return SelectedUser != null && SelectedUser.Id != _currentUserId;
        }

        private void DeleteUser()
        {
            if (SelectedUser == null) return;

            var result = System.Windows.MessageBox.Show(
                $"Вы уверены, что хотите удалить пользователя \"{SelectedUser.NickName}\" (логин: {SelectedUser.Login})?",
                "Подтверждение удаления",
                System.Windows.MessageBoxButton.YesNo,
                System.Windows.MessageBoxImage.Warning);

            if (result != System.Windows.MessageBoxResult.Yes) return;

            try
            {
                _schoolService.DeleteUser(SelectedUser.Id, _currentUserId);
                LoadUsers();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(
                    $"Ошибка удаления пользователя: {ex.Message}",
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
                    LoadCourses();
                    return;
                }

                var searchProperties = new List<string>
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

        private void AddCourse()
        {
            var courseEditViewModel = new CourseEditViewModel(_schoolService);

            courseEditViewModel.CourseSaved += (s, e) =>
            {
                LoadCourses();
                System.Windows.MessageBox.Show(
                    "Курс успешно добавлен",
                    "Успех",
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Information);
            };

            CourseAddRequested?.Invoke(this, courseEditViewModel);
        }

        private bool CanEditCourse()
        {
            return SelectedCourse != null;
        }

        private void EditCourse()
        {
            if (SelectedCourse == null) return;

            var courseEditViewModel = new CourseEditViewModel(_schoolService, SelectedCourse.Id);

            courseEditViewModel.CourseSaved += (s, e) =>
            {
                LoadCourses();
                System.Windows.MessageBox.Show(
                    "Курс успешно обновлен",
                    "Успех",
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Information);
            };

            CourseAddRequested?.Invoke(this, courseEditViewModel);
        }

        private void Export()
        {
            try
            {
                var saveFileDialog = new Microsoft.Win32.SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx|PDF files (*.pdf)|*.pdf",
                    Title = "Выберите файл для экспорта"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    var format = saveFileDialog.FilterIndex == 1 ?
                        Logic.Services.ExportFormat.Excel :
                        Logic.Services.ExportFormat.PDF;

                    var courses = _schoolService.GetAllCourses();
                    _schoolService.ExportCourses(courses.Cast<object>().ToList(), saveFileDialog.FileName, format);

                    System.Windows.MessageBox.Show(
                        "Данные успешно экспортированы",
                        "Успех",
                        System.Windows.MessageBoxButton.OK,
                        System.Windows.MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(
                    $"Ошибка экспорта: {ex.Message}",
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
                var image = Image.FromFile(imagePath);
                _schoolService.SaveAvatar(_currentUserId, image);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка изменения аватара: {ex.Message}");
            }
        }
    }
}
