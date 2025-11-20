using Model;

namespace Logic.Services
{
    public interface ICourseExportService
    {
        void ExportCourses(List<object> courses, string filePath, ExportFormat format);
    }

    public enum ExportFormat
    {
        PDF,
        Excel
    }
}
