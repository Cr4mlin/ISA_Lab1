using Shared;
using Shared.DTO;
using Logic;

namespace Presenter
{
    public class AdminPresenter
    {
        private readonly IAdminView _view;
        private readonly ISchoolService _schoolService;
        private int _currentUserId;

        public AdminPresenter(IAdminView view, ISchoolService schoolService)
        {
            _view = view;
            _schoolService = schoolService;

            // События курсов
            _view.FormLoaded += OnFormLoaded;
            _view.AddCourseRequested += OnAddCourseRequested;
            _view.EditCourseRequested += OnEditCourseRequested;
            _view.DeleteCourseRequested += OnDeleteCourseRequested;
            _view.ToggleCourseStatusRequested += OnToggleCourseStatusRequested;
            _view.SearchCoursesRequested += OnSearchCoursesRequested;

            // События пользователей
            _view.ManageUsersRequested += OnManageUsersRequested;
            _view.DeleteUserRequested += OnDeleteUserRequested;
            _view.ChangeUserRoleRequested += OnChangeUserRoleRequested;
            _view.SearchUsersRequested += OnSearchUsersRequested;

            // Экспорт
            _view.ExportRequested += OnExportRequested;

            // События аватара
            _view.LoadAvatarRequested += OnLoadAvatarRequested;
            _view.ChangeAvatarRequested += OnChangeAvatarRequested;
        }

        public void SetUser(int userId)
        {
            _currentUserId = userId;
            try
            {
                var user = _schoolService.GetUserById(userId);
                if (user != null)
                    _view.UpdateUserInfo(user.NickName);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка: {ex.Message}");
            }
        }

        private void OnFormLoaded(object? sender, EventArgs e)
        {
            try
            {
                var courses = _schoolService.GetAllCourses();
                var courseDtos = DtoMapper.ToDto(courses);
                _view.DisplayCourses(courseDtos);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка: {ex.Message}");
            }
        }

        private void OnAddCourseRequested(object? sender, EventArgs e)
        {
            try
            {
                var courseDto = _view.RequestNewCourseData();
                if (courseDto == null)
                    return;

                var statusText = courseDto.IsActive ? "да" : "нет";
                _schoolService.CreateCourse(
                    courseDto.Name,
                    courseDto.Description,
                    courseDto.Duration,
                    courseDto.Price,
                    courseDto.TeacherName,
                    statusText
                );

                _view.ShowInfo("Курс успешно добавлен!");
                OnFormLoaded(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка при добавлении курса: {ex.Message}");
            }
        }

        private void OnEditCourseRequested(object? sender, int courseId)
        {
            try
            {
                var course = _schoolService.GetCourseById(courseId);
                if (course == null)
                {
                    _view.ShowError("Курс не найден");
                    return;
                }

                var courseDto = DtoMapper.ToDto(course);
                var updatedDto = _view.RequestCourseUpdate(courseDto);
                if (updatedDto == null)
                    return;

                var statusText = updatedDto.IsActive ? "да" : "нет";
                _schoolService.UpdateCourse(
                    courseId,
                    updatedDto.Name,
                    updatedDto.Description,
                    updatedDto.Duration,
                    updatedDto.Price,
                    updatedDto.TeacherName,
                    statusText
                );

                _view.ShowInfo("Курс успешно обновлен!");
                OnFormLoaded(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка при редактировании курса: {ex.Message}");
            }
        }

        private void OnDeleteCourseRequested(object? sender, int courseId)
        {
            try
            {
                var course = _schoolService.GetCourseById(courseId);
                if (course == null)
                {
                    _view.ShowError("Курс не найден");
                    return;
                }

                if (!_view.ConfirmDelete(course.Name))
                    return;

                _schoolService.DeleteCourse(courseId);
                _view.ShowInfo("Курс удален!");
                OnFormLoaded(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка при удалении курса: {ex.Message}");
            }
        }

        private void OnManageUsersRequested(object? sender, EventArgs e)
        {
            try
            {
                var users = _schoolService.GetAllUsers();
                var userDtos = DtoMapper.ToDto(users);
                _view.DisplayUsers(userDtos);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка при загрузке пользователей: {ex.Message}");
            }
        }

        private void OnToggleCourseStatusRequested(object? sender, int courseId)
        {
            try
            {
                _schoolService.ToggleCourseStatus(courseId);
                _view.ShowInfo("Статус курса изменен!");
                OnFormLoaded(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка при изменении статуса: {ex.Message}");
            }
        }

        private void OnSearchCoursesRequested(object? sender, string searchText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    OnFormLoaded(null, EventArgs.Empty);
                    return;
                }

                var searchProperties = new List<string> { "Name", "Description", "TeacherName" };
                var filteredCourses = _schoolService.SearchCourses(searchText, searchProperties);
                var courseDtos = DtoMapper.ToDto(filteredCourses);
                _view.DisplayCourses(courseDtos);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка при поиске курсов: {ex.Message}");
            }
        }

        private void OnDeleteUserRequested(object? sender, int userId)
        {
            try
            {
                var user = _schoolService.GetUserById(userId);
                if (user == null)
                {
                    _view.ShowError("Пользователь не найден");
                    return;
                }

                if (!_view.ConfirmDelete(user.NickName))
                    return;

                _schoolService.DeleteUser(userId, _currentUserId);
                _view.ShowInfo("Пользователь удален!");
                OnManageUsersRequested(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка при удалении пользователя: {ex.Message}");
            }
        }

        private void OnChangeUserRoleRequested(object? sender, (int userId, int newRoleId) args)
        {
            try
            {
                _schoolService.ChangeUserRole(args.userId, args.newRoleId, _currentUserId);
                _view.ShowInfo("Роль успешно изменена!");
                OnManageUsersRequested(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка при изменении роли: {ex.Message}");
            }
        }

        private void OnSearchUsersRequested(object? sender, string searchText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    OnManageUsersRequested(null, EventArgs.Empty);
                    return;
                }

                var searchProperties = new List<string> { "Login", "NickName" };
                var filteredUsers = _schoolService.SearchUsers(searchText, searchProperties);
                var userDtos = DtoMapper.ToDto(filteredUsers);
                _view.DisplayUsers(userDtos);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка при поиске пользователей: {ex.Message}");
            }
        }

        private void OnExportRequested(object? sender, EventArgs e)
        {
            try
            {
                var courses = _schoolService.GetAllCourses();

                if (courses == null || courses.Count == 0)
                {
                    _view.ShowInfo("Нет данных для экспорта");
                    return;
                }

                var exportFormat = _view.RequestExportFormat();
                if (!exportFormat.HasValue)
                    return;

                bool isPdf = exportFormat.Value;
                string defaultFileName = $"Courses_Export_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.{(isPdf ? "pdf" : "xlsx")}";

                string? filePath = _view.RequestExportFilePath(defaultFileName, isPdf);
                if (string.IsNullOrEmpty(filePath))
                    return;

                var coursesAsObjects = courses.Cast<object>().ToList();
                var format = isPdf ? Logic.Services.ExportFormat.PDF : Logic.Services.ExportFormat.Excel;

                _schoolService.ExportCourses(coursesAsObjects, filePath, format);

                _view.ShowExportSuccess(filePath);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка при экспорте: {ex.Message}\n\nStack trace: {ex.StackTrace}");
            }
        }

        private void OnLoadAvatarRequested(object? sender, EventArgs e)
        {
            try
            {
                var avatar = _schoolService.LoadAvatar(_currentUserId);
                if (avatar != null)
                {
                    _view.SetUserAvatar(avatar);
                }
                else
                {
                    _view.SetUserAvatar(Properties.Resources.DefaultAvatar);
                }
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка при загрузке аватара: {ex.Message}");
            }
        }

        private void OnChangeAvatarRequested(object? sender, EventArgs e)
        {
            try
            {
                var imagePath = _view.RequestImageFile();
                if (!string.IsNullOrEmpty(imagePath))
                {
                    // Загружаем изображение и создаем копию, чтобы освободить файл
                    Image image;
                    using (var originalImage = Image.FromFile(imagePath))
                    {
                        image = new Bitmap(originalImage);
                    }

                    _schoolService.SaveAvatar(_currentUserId, image);
                    _view.SetUserAvatar(image);
                    _view.ShowInfo("Аватар успешно изменен!");
                }
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка при изменении аватара: {ex.Message}");
            }
        }
    }
}
