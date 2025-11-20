namespace WinFormsApp
{
    public partial class DbConnectionForm : Form
    {
        public string SelectedConnectionType { get; private set; } = "";
        public DbConnectionForm()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (rbEF.Checked)
                SelectedConnectionType = "EntityFrameWork";
            else if (rbDapper.Checked)
                SelectedConnectionType = "Dapper";

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
