using Model;
using Logic.Export;

namespace Logic.Services
{
    public class CourseExportService: ICourseExportService
    {
        private readonly Dictionary<ExportFormat, IExportStrategy> _strategies;

        public CourseExportService(PdfExportStrategy pdfStrategy, ExcelExportStrategy excelStrategy)
        {
            _strategies = new Dictionary<ExportFormat, IExportStrategy>
            {
                { ExportFormat.PDF, pdfStrategy ?? throw new ArgumentNullException(nameof(pdfStrategy)) },
                { ExportFormat.Excel, excelStrategy ?? throw new ArgumentNullException(nameof(excelStrategy)) }
            };
        }

        public void ExportCourses(List<object> courses, string filePath, ExportFormat format)
        {
            if (courses == null)
                throw new ArgumentNullException(nameof(courses));

            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Путь к файлу не может быть пустым", nameof(filePath));

            if (!_strategies.TryGetValue(format, out var strategy))
                throw new ArgumentException($"Неподдерживаемый формат: {format}", nameof(format));

            strategy.Export(courses, filePath);
        }
    }
}
