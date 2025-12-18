using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Presenter.ViewModels;

namespace WpfView.Views
{
    /// <summary>
    /// Логика взаимодействия для AdminMainView.xaml
    /// </summary>
    public partial class AdminMainView : BaseView
    {
        public AdminMainView()
        {
            InitializeComponent();
            Loaded += AdminMainView_Loaded;
        }

        private void AdminMainView_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is AdminMainViewModel viewModel)
            {
                LoadAvatar(viewModel.CurrentUserId);
                viewModel.CourseAddRequested += OnCourseAddRequested;
            }
        }

        private void TabControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.Source is System.Windows.Controls.TabControl && DataContext is AdminMainViewModel viewModel)
            {
                var tabControl = (System.Windows.Controls.TabControl)sender;
                var selectedTab = tabControl.SelectedItem as System.Windows.Controls.TabItem;

                if (selectedTab?.Name == "UsersTab")
                {
                    viewModel.LoadUsersCommand?.Execute(null);
                }
            }
        }

        private void OnCourseAddRequested(object? sender, Presenter.ViewModels.CourseEditViewModel courseEditViewModel)
        {
            var courseEditView = new CourseEditView
            {
                DataContext = courseEditViewModel,
                Owner = this
            };

            courseEditViewModel.CourseSaved += (s, e) => courseEditView.Close();
            courseEditViewModel.Cancelled += (s, e) => courseEditView.Close();

            courseEditView.ShowDialog();
        }

        private void ChangeAvatar_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*",
                Title = "Выберите изображение"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                if (DataContext is AdminMainViewModel viewModel)
                {
                    viewModel.ChangeAvatarCommand?.Execute(openFileDialog.FileName);
                    LoadAvatar(viewModel.CurrentUserId);
                }
            }
        }

        private void LoadAvatar(int userId)
        {
            if (DataContext is AdminMainViewModel viewModel)
            {
                try
                {
                    var avatar = viewModel.GetAvatar();
                    if (avatar != null)
                    {
                        AvatarImage.Source = ConvertToWpfImage(avatar);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Ошибка загрузки аватара: {ex.Message}");
                }
            }
        }

        private BitmapImage ConvertToWpfImage(System.Drawing.Image image)
        {
            using (var ms = new System.IO.MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.Position = 0;
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = ms;
                bitmap.EndInit();
                bitmap.Freeze();
                return bitmap;
            }
        }
    }
}
