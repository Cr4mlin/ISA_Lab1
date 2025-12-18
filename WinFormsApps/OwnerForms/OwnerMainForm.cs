using Shared;
using Shared.DTO;
using FontAwesome.Sharp;

namespace WinFormsApps
{
    public partial class OwnerMainForm : Form, IAdminView
    {
        private bool _dragging = false;
        private Point _dragCursorPoint;
        private Point _dragFormPoint;
        private const int RESIZE_HANDLE_SIZE = 10;
        private bool _resizing = false;
        private Point _resizeStartPoint;
        private Size _resizeStartSize;
        private Form? _currentChildForm;
        private ICoursesView? _currentCoursesView;
        private IUsersView? _currentUsersView;
        private IProfileView? _currentProfileView;
        private int _currentUserId;

        public OwnerMainForm()
        {
            InitializeComponent();

            panelLogo.MouseDown += Panel_MouseDown;
            panelLogo.MouseMove += Panel_MouseMove;
            panelLogo.MouseUp += Panel_MouseUp;
            panelTitleBar.MouseDown += Panel_MouseDown;
            panelTitleBar.MouseMove += Panel_MouseMove;
            panelTitleBar.MouseUp += Panel_MouseUp;

            btnClose.Click += (s, e) => Application.Exit();
            btnFullWindow.Click += BtnFullWindow_Click;
            btnWIndowRestore.Click += BtnWindowRestore_Click;
            btnRollUp.Click += (s, e) => this.WindowState = FormWindowState.Minimized;

            btnCourses.Click += (s, e) => OpenCoursesForm();
            btnUsers.Click += (s, e) => OpenUsersForm();
            btnDbConnection.Click += (s, e) => OpenChildForm(new DbConnectionForm(), btnDbConnection.IconChar, btnDbConnection.Text);
            btnExport.Click += BtnExport_Click;

            Logo.Click += (s, e) => CloseCurrentChildForm();
            panelUser.Click += (s, e) => OpenProfile();
            lblUsername.Click += (s, e) => OpenProfile();
            avatarPictureBox.Click += (s, e) => OpenProfile();

            this.MouseDown += Form_MouseDown;
            this.MouseMove += Form_MouseMove;
            this.MouseUp += Form_MouseUp;

            panelChildForm.MouseDown += Form_MouseDown;
            panelChildForm.MouseMove += Form_MouseMove;
            panelChildForm.MouseUp += Form_MouseUp;
        }

        // События IAdminView
        public event EventHandler? FormLoaded;
        public event EventHandler? AddCourseRequested;
        public event EventHandler<int>? EditCourseRequested;
        public event EventHandler<int>? DeleteCourseRequested;
        public event EventHandler<int>? ToggleCourseStatusRequested;
        public event EventHandler<string>? SearchCoursesRequested;
        public event EventHandler? ManageUsersRequested;
        public event EventHandler<int>? DeleteUserRequested;
        public event EventHandler<(int userId, int newRoleId)>? ChangeUserRoleRequested;
        public event EventHandler<string>? SearchUsersRequested;
        public event EventHandler? ExportRequested;
        public event EventHandler? LoadAvatarRequested;
        public event EventHandler ChangeAvatarRequested;

        // Методы IAdminView
        public void DisplayCourses(IEnumerable<CourseDto> courses)
        {
            _currentCoursesView?.DisplayCourses(courses);
        }

        public void ShowError(string message) => MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        public void ShowInfo(string message) => MessageBox.Show(message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        public void UpdateUserInfo(string userName) => lblUsername.Text = userName;
        void IView.Show() => this.Show();
        void IView.Hide() => this.Hide();
        void IView.Close() => this.Close();

        public CourseDto? RequestNewCourseData()
        {
            return _currentCoursesView?.RequestNewCourseData();
        }

        public CourseDto? RequestCourseUpdate(CourseDto course)
        {
            return _currentCoursesView?.RequestCourseUpdate(course);
        }

        public void DisplayUsers(IEnumerable<UserDto> users)
        {
            _currentUsersView?.DisplayUsers(users);
        }

        public bool ConfirmDelete(string itemName)
        {
            var result = MessageBox.Show($"Вы уверены, что хотите удалить '{itemName}'?",
                "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            return result == DialogResult.Yes;
        }

        public CourseDto? GetSelectedCourse()
        {
            return _currentCoursesView?.GetSelectedCourse();
        }

        public UserDto? GetSelectedUser()
        {
            return _currentUsersView?.GetSelectedUser();
        }

        public int? RequestNewRole(string userName, int currentRole)
        {
            return _currentUsersView?.RequestNewRole(userName, currentRole);
        }

        public bool? RequestExportFormat()
        {
            var result = MessageBox.Show("Экспортировать в PDF?\n\nДа - PDF\nНет - Excel",
                "Выбор формата экспорта", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (result == DialogResult.Cancel)
                return null;

            return result == DialogResult.Yes;
        }

        public string? RequestExportFilePath(string defaultFileName, bool isPdf)
        {
            string filter = isPdf ? "PDF файлы (*.pdf)|*.pdf" : "Excel файлы (*.xlsx)|*.xlsx";

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = filter;
                saveFileDialog.Title = "Сохранить файл экспорта";
                saveFileDialog.FileName = defaultFileName;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    return saveFileDialog.FileName;

                return null;
            }
        }

        public void ShowExportSuccess(string filePath)
        {
            MessageBox.Show($"Данные успешно экспортированы в файл:\n{filePath}",
                "Экспорт выполнен", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void SetUserAvatar(Image? avatar)
        {
            // Освобождаем предыдущее изображение, чтобы избежать утечки памяти
            var oldImage = avatarPictureBox.Image;
            avatarPictureBox.Image = avatar;
            if (oldImage != null && oldImage != avatar)
            {
                oldImage.Dispose();
            }

            // Передаем аватар в ProfileForm, если он открыт
            _currentProfileView?.DisplayAvatar(avatar);
        }

        public string? RequestImageFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Изображения (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.Title = "Выберите изображение для аватара";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                    return openFileDialog.FileName;

                return null;
            }
        }

        private void OpenCoursesForm()
        {
            var coursesForm = new OwnerCoursesForm();

            // Подписываемся на события дочерней формы и пробрасываем их в Presenter
            coursesForm.AddRequested += (s, e) => AddCourseRequested?.Invoke(this, EventArgs.Empty);
            coursesForm.EditRequested += (s, e) => {
                var selected = coursesForm.GetSelectedCourse();
                if (selected != null)
                    EditCourseRequested?.Invoke(this, selected.Id);
            };
            coursesForm.DeleteRequested += (s, e) => {
                var selected = coursesForm.GetSelectedCourse();
                if (selected != null)
                    DeleteCourseRequested?.Invoke(this, selected.Id);
            };
            coursesForm.ChangeStatusRequested += (s, e) => {
                var selected = coursesForm.GetSelectedCourse();
                if (selected != null)
                    ToggleCourseStatusRequested?.Invoke(this, selected.Id);
            };
            coursesForm.SearchRequested += (s, searchText) => SearchCoursesRequested?.Invoke(this, searchText);

            OpenChildForm(coursesForm, btnCourses.IconChar, btnCourses.Text);

            // Устанавливаем _currentCoursesView ПОСЛЕ OpenChildForm, чтобы не сбросилось в NULL
            _currentCoursesView = coursesForm;

            // Загружаем курсы при открытии формы
            FormLoaded?.Invoke(this, EventArgs.Empty);
        }

        private void OpenUsersForm()
        {
            var usersForm = new OwnerUsersViewForm();

            // Подписываемся на события дочерней формы и пробрасываем их в Presenter
            usersForm.DeleteRequested += (s, e) => {
                var selected = usersForm.GetSelectedUser();
                if (selected != null)
                    DeleteUserRequested?.Invoke(this, selected.Id);
            };
            usersForm.ChangeRoleRequested += (s, e) => {
                var selected = usersForm.GetSelectedUser();
                if (selected != null)
                {
                    var newRole = usersForm.RequestNewRole(selected.NickName, selected.Role);
                    if (newRole.HasValue)
                        ChangeUserRoleRequested?.Invoke(this, (selected.Id, newRole.Value));
                }
            };
            usersForm.SearchRequested += (s, searchText) => SearchUsersRequested?.Invoke(this, searchText);

            OpenChildForm(usersForm, btnUsers.IconChar, btnUsers.Text);

            // Устанавливаем _currentUsersView ПОСЛЕ OpenChildForm, чтобы не сбросилось в NULL
            _currentUsersView = usersForm;

            ManageUsersRequested?.Invoke(this, EventArgs.Empty);
        }

        private void OpenChildForm(Form childForm, IconChar icon, string title)
        {
            CloseCurrentChildForm();
            _currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            childForm.BringToFront();
            childForm.Show();
            iconCurrentChildForm.IconChar = icon;
            labelCurrentChildForm.Text = title;
        }

        private void CloseCurrentChildForm()
        {
            if (_currentChildForm != null) { _currentChildForm.Close(); _currentChildForm = null; }
            _currentCoursesView = null;
            _currentUsersView = null;
            panelChildForm.Controls.Clear();
            iconCurrentChildForm.IconChar = IconChar.House;
            labelCurrentChildForm.Text = "Главная";
        }

        private void OpenProfile()
        {
            if (_currentUserId > 0)
            {
                var profileForm = new ProfileForm();
                _currentProfileView = profileForm;

                profileForm.SetUserInfo(lblUsername.Text, "Администратор");

                profileForm.ChangeAvatarRequested += (s, e) => ChangeAvatarRequested?.Invoke(this, EventArgs.Empty);

                // Загружаем аватар после показа формы

                profileForm.DisplayAvatar(avatarPictureBox.Image);

                OpenChildForm(profileForm, IconChar.User, "Профиль");
            }
        }

        public void SetCurrentUser(int userId, string nickName)
        {
            _currentUserId = userId;
            lblUsername.Text = nickName;

            // Запрашиваем загрузку аватара через Presenter
            LoadAvatarRequested?.Invoke(this, EventArgs.Empty);

            FormLoaded?.Invoke(this, EventArgs.Empty);
        }

        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) { _dragging = true; _dragCursorPoint = Cursor.Position; _dragFormPoint = this.Location; }
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging) this.Location = Point.Add(_dragFormPoint, new Size(Point.Subtract(Cursor.Position, new Size(_dragCursorPoint))));
        }

        private void Panel_MouseUp(object sender, MouseEventArgs e) => _dragging = false;

        private void BtnFullWindow_Click(object? sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnFullWindow.Visible = false;
            btnWIndowRestore.Visible = true;
        }

        private void BtnWindowRestore_Click(object? sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnFullWindow.Visible = true;
            btnWIndowRestore.Visible = false;
        }

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && this.WindowState == FormWindowState.Normal && IsInResizeArea(e.Location))
            {
                _resizing = true;
                _resizeStartPoint = Cursor.Position;
                _resizeStartSize = this.Size;
            }
        }

        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (_resizing)
            {
                int xDiff = Cursor.Position.X - _resizeStartPoint.X;
                int yDiff = Cursor.Position.Y - _resizeStartPoint.Y;
                this.Size = new Size(Math.Max(this.MinimumSize.Width, _resizeStartSize.Width + xDiff), Math.Max(this.MinimumSize.Height, _resizeStartSize.Height + yDiff));
            }
            else if (this.WindowState == FormWindowState.Normal)
                this.Cursor = IsInResizeArea(e.Location) ? Cursors.SizeNWSE : Cursors.Default;
        }

        private void Form_MouseUp(object sender, MouseEventArgs e) => _resizing = false;
        private bool IsInResizeArea(Point location) => location.X >= this.Width - RESIZE_HANDLE_SIZE && location.Y >= this.Height - RESIZE_HANDLE_SIZE;
        private void sideMenu_Paint(object sender, PaintEventArgs e) { }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            ExportRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
