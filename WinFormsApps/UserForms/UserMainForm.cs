using Shared;
using Shared.DTO;
using FontAwesome.Sharp;

namespace WinFormsApps
{
    public partial class UserMainForm : Form, IUserView
    {
        private bool _dragging = false;
        private Point _dragCursorPoint;
        private Point _dragFormPoint;
        private const int RESIZE_HANDLE_SIZE = 10;
        private bool _resizing = false;
        private Point _resizeStartPoint;
        private Size _resizeStartSize;
        private Form? _currentChildForm;
        private IUserCoursesView? _currentCoursesView;
        private IProfileView? _currentProfileView;
        private int _currentUserId;

        public UserMainForm()
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
            btnDbConnection.Click += (s, e) => OpenChildForm(new DbConnectionForm(), btnDbConnection.IconChar, btnDbConnection.Text);

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

        public event EventHandler? FormLoaded;
        public event EventHandler<int>? PurchaseCourseRequested;
        public event EventHandler? ViewPurchasedCoursesRequested;
        public event EventHandler<string>? SearchCoursesRequested;
        public event EventHandler? ViewAllCoursesRequested;
        public event EventHandler? ChangeAvatarRequested;
        public event EventHandler? LoadAvatarRequested;
        public event EventHandler<Image>? AvatarImageProvided;

        public void DisplayCourses(IEnumerable<CourseDto> courses)
        {
            _currentCoursesView?.DisplayCourses(courses);
        }

        public void DisplayPurchasedCourses(IEnumerable<CourseDto> purchasedCourses)
        {
            _currentCoursesView?.DisplayCourses(purchasedCourses);
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

            _currentProfileView?.DisplayAvatar(avatar);
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowInfo(string message)
        {
            MessageBox.Show(message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void UpdateUserInfo(string userName, string nickName)
        {
            lblUsername.Text = nickName;
        }

        public string? RequestImageFile()
        {
            return _currentProfileView?.RequestImageFile();
        }

        public CourseDto? GetSelectedCourse()
        {
            return _currentCoursesView?.GetSelectedCourse();
        }

        public bool ConfirmPurchase(string courseName)
        {
            return _currentCoursesView?.ConfirmPurchase(courseName) ?? false;
        }

        void IView.Show() => this.Show();
        void IView.Hide() => this.Hide();
        void IView.Close() => this.Close();

        private void OpenChildForm(Form childForm, IconChar icon, string title)
        {
            CloseCurrentChildForm();

            _currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

            iconCurrentChildForm.IconChar = icon;
            labelCurrentChildForm.Text = title;
        }

        private void CloseCurrentChildForm()
        {
            if (_currentChildForm != null)
            {
                _currentChildForm.Close();
                _currentChildForm = null;
            }

            panelChildForm.Controls.Clear();

            iconCurrentChildForm.IconChar = IconChar.House;
            labelCurrentChildForm.Text = "Главная";
        }

        private void OpenCoursesForm()
        {
            var coursesForm = new UserCoursesForm();
            _currentCoursesView = coursesForm;

            coursesForm.ViewAllCoursesRequested += (s, e) => ViewAllCoursesRequested?.Invoke(this, EventArgs.Empty);
            coursesForm.ViewPurchasedCoursesRequested += (s, e) => ViewPurchasedCoursesRequested?.Invoke(this, EventArgs.Empty);
            coursesForm.SearchRequested += (s, searchText) => SearchCoursesRequested?.Invoke(this, searchText);
            coursesForm.PurchaseCourseRequested += (s, e) =>
            {
                var selected = coursesForm.GetSelectedCourse();
                if (selected != null)
                    PurchaseCourseRequested?.Invoke(this, selected.Id);
            };

            OpenChildForm(coursesForm, btnCourses.IconChar, btnCourses.Text);

            ViewAllCoursesRequested?.Invoke(this, EventArgs.Empty);
        }

        private void OpenProfile()
        {
            if (_currentUserId > 0)
            {
                var profileForm = new ProfileForm();
                _currentProfileView = profileForm;

                profileForm.SetUserInfo(lblUsername.Text, "Пользователь");

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

            LoadAvatarRequested?.Invoke(this, EventArgs.Empty);

            FormLoaded?.Invoke(this, EventArgs.Empty);
        }

        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _dragging = true;
                _dragCursorPoint = Cursor.Position;
                _dragFormPoint = this.Location;
            }
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(_dragCursorPoint));
                this.Location = Point.Add(_dragFormPoint, new Size(diff));
            }
        }

        private void Panel_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

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
            if (e.Button == MouseButtons.Left && this.WindowState == FormWindowState.Normal)
            {
                if (IsInResizeArea(e.Location))
                {
                    _resizing = true;
                    _resizeStartPoint = Cursor.Position;
                    _resizeStartSize = this.Size;
                }
            }
        }

        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (_resizing)
            {
                int xDiff = Cursor.Position.X - _resizeStartPoint.X;
                int yDiff = Cursor.Position.Y - _resizeStartPoint.Y;

                this.Size = new Size(
                    Math.Max(this.MinimumSize.Width, _resizeStartSize.Width + xDiff),
                    Math.Max(this.MinimumSize.Height, _resizeStartSize.Height + yDiff)
                );
            }
            else if (this.WindowState == FormWindowState.Normal)
            {
                if (IsInResizeArea(e.Location))
                {
                    this.Cursor = Cursors.SizeNWSE;
                }
                else
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void Form_MouseUp(object sender, MouseEventArgs e)
        {
            _resizing = false;
        }

        private bool IsInResizeArea(Point location)
        {
            return location.X >= this.Width - RESIZE_HANDLE_SIZE &&
                   location.Y >= this.Height - RESIZE_HANDLE_SIZE;
        }
    }
}
