using Model;
using ClosedXML.Excel;

namespace Logic.Export
{
    public class ExcelExportStrategy : IExportStrategy
    {
        public string FileExtension => "xlsx";
        public string FormatDescription => "Excel файлы (*.xlsx)";

        public void Export(List<object> courses, string filePath)
        {
            if (courses == null)
                throw new ArgumentNullException(nameof(courses));

            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Путь к файлу не может быть пустым", nameof(filePath));

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Курсы");

            // Заголовки
            worksheet.Cell(1, 1).Value = "ID";
            worksheet.Cell(1, 2).Value = "Название";
            worksheet.Cell(1, 3).Value = "Описание";
            worksheet.Cell(1, 4).Value = "Длительность (ч)";
            worksheet.Cell(1, 5).Value = "Цена (руб)";
            worksheet.Cell(1, 6).Value = "Преподаватель";
            worksheet.Cell(1, 7).Value = "Активен";

            // Стилизация заголовков
            var headerRange = worksheet.Range(1, 1, 1, 7);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
            headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            // Данные
            int row = 2;
            foreach (var courseObj in courses)
            {
                var type = courseObj.GetType();
                var id = type.GetProperty("Id")?.GetValue(courseObj);
                var name = type.GetProperty("Name")?.GetValue(courseObj);
                var description = type.GetProperty("Description")?.GetValue(courseObj);
                var duration = type.GetProperty("Duration")?.GetValue(courseObj);
                var price = type.GetProperty("Price")?.GetValue(courseObj);
                var teacherName = type.GetProperty("TeacherName")?.GetValue(courseObj);
                var isActive = type.GetProperty("IsActive")?.GetValue(courseObj);

                worksheet.Cell(row, 1).Value = id?.ToString() ?? "";
                worksheet.Cell(row, 2).Value = name?.ToString() ?? "";
                worksheet.Cell(row, 3).Value = description?.ToString() ?? "";
                worksheet.Cell(row, 4).Value = duration?.ToString() ?? "";
                worksheet.Cell(row, 5).Value = price?.ToString() ?? "";
                worksheet.Cell(row, 6).Value = teacherName?.ToString() ?? "";
                worksheet.Cell(row, 7).Value = isActive is bool b && b ? "Да" : "Нет";
                row++;
            }

            // Автоподбор ширины колонок
            worksheet.Columns().AdjustToContents();

            workbook.SaveAs(filePath);
        }
    }
}