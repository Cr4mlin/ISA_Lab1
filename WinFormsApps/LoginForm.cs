using Shared;

namespace WinFormsApps
{
    public partial class LoginForm : Form, ILoginView
    {
        private bool _dragging = false;
        private Point _dragCursorPoint;
        private Point _dragFormPoint;
        public LoginForm()
        {
            InitializeComponent();

            // Подписка на события элементов управления
            btnLogin.Click += (s, e) => LoginClicked?.Invoke(this, EventArgs.Empty);
            labelRegistration.Click += (s, e) => RegisterClicked?.Invoke(this, EventArgs.Empty);

            panelTop.MouseDown += Panel_MouseDown;
            panelTop.MouseMove += Panel_MouseMove;
            panelTop.MouseUp += Panel_MouseUp;

            // Кнопки управления окном
            btnClose.Click += (s, e) =>
            {
                CancelClicked?.Invoke(this, EventArgs.Empty);
            };
            btnRollUp.Click += (s, e) => this.WindowState = FormWindowState.Minimized;

            // Скрываем ошибку по умолчанию
            labelLoginError.Visible = false;
        }

        // Реализация ILoginView
        public string Login => textBoxLogin.Text;
        public string Password => textBoxPassword.Text;

        public event EventHandler? LoginClicked;
        public event EventHandler? RegisterClicked;
        public event EventHandler? CancelClicked;

        public void ShowError(string message)
        {
            labelLoginError.Text = message;
            labelLoginError.Visible = true;
        }

        public void ShowInfo(string message)
        {
            MessageBox.Show(message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ClearFields()
        {
            textBoxLogin.Text = string.Empty;
            textBoxPassword.Text = string.Empty;
            labelLoginError.Visible = false;
        }

        public void SetDialogResultOk()
        {
            this.DialogResult = DialogResult.OK;
        }

        public void SetDialogResultCancel()
        {
            this.DialogResult = DialogResult.Cancel;
        }

        public void SetDialogResultAbort()
        {
            this.DialogResult = DialogResult.Abort;
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

        // Реализация IView
        void IView.Show()
        {
            this.Show();
        }

        void IView.Hide()
        {
            this.Hide();
        }

        void IView.Close()
        {
            this.Close();
        }
    }
}
