using Model;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Logic.Export
{
    public class PdfExportStrategy: IExportStrategy
    {
        public string FileExtension => "pdf";
        public string FormatDescription => "PDF файлы (*.pdf)";

        public void Export(List<object> courses, string filePath)
        {
            if (courses == null)
                throw new ArgumentNullException(nameof(courses));

            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Путь к файлу не может быть пустым", nameof(filePath));

            // Настройка лицензии QuestPDF (Community License)
            QuestPDF.Settings.License = LicenseType.Community;

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4.Landscape());
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header()
                        .Text("Список курсов")
                        .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Table(table =>
                        {
                            // Определение колонок
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(40);  // ID
                                columns.RelativeColumn(2);    // Название
                                columns.RelativeColumn(3);    // Описание
                                columns.ConstantColumn(70);   // Длительность
                                columns.ConstantColumn(80);   // Цена
                                columns.RelativeColumn(2);    // Преподаватель
                                columns.ConstantColumn(60);   // Активен
                            });

                            // Заголовок таблицы
                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("ID");
                                header.Cell().Element(CellStyle).Text("Название");
                                header.Cell().Element(CellStyle).Text("Описание");
                                header.Cell().Element(CellStyle).Text("Длительность (ч)");
                                header.Cell().Element(CellStyle).Text("Цена (руб)");
                                header.Cell().Element(CellStyle).Text("Преподаватель");
                                header.Cell().Element(CellStyle).Text("Активен");

                                static IContainer CellStyle(IContainer container)
                                {
                                    return container
                                        .Border(1)
                                        .BorderColor(Colors.Grey.Lighten1)
                                        .Background(Colors.Grey.Lighten3)
                                        .PaddingVertical(5)
                                        .PaddingHorizontal(10)
                                        .AlignCenter()
                                        .AlignMiddle();
                                }
                            });

                            // Данные
                            foreach (var courseObj in courses)
                            {
                                var type = courseObj.GetType();
                                var id = type.GetProperty("Id")?.GetValue(courseObj)?.ToString() ?? "";
                                var name = type.GetProperty("Name")?.GetValue(courseObj)?.ToString() ?? "";
                                var description = type.GetProperty("Description")?.GetValue(courseObj)?.ToString() ?? "";
                                var duration = type.GetProperty("Duration")?.GetValue(courseObj)?.ToString() ?? "";
                                var price = type.GetProperty("Price")?.GetValue(courseObj);
                                var priceStr = price != null ? $"{price:F2}" : "0.00";
                                var teacherName = type.GetProperty("TeacherName")?.GetValue(courseObj)?.ToString() ?? "";
                                var isActive = type.GetProperty("IsActive")?.GetValue(courseObj);
                                var isActiveStr = isActive is bool b && b ? "Да" : "Нет";

                                table.Cell().Element(CellStyle).Text(id);
                                table.Cell().Element(CellStyle).Text(name);
                                table.Cell().Element(CellStyle).Text(description);
                                table.Cell().Element(CellStyle).Text(duration);
                                table.Cell().Element(CellStyle).Text(priceStr);
                                table.Cell().Element(CellStyle).Text(teacherName);
                                table.Cell().Element(CellStyle).Text(isActiveStr);

                                static IContainer CellStyle(IContainer container)
                                {
                                    return container
                                        .Border(1)
                                        .BorderColor(Colors.Grey.Lighten2)
                                        .PaddingVertical(5)
                                        .PaddingHorizontal(10);
                                }
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Страница ");
                            x.CurrentPageNumber();
                            x.Span(" из ");
                            x.TotalPages();
                        });
                });
            })
            .GeneratePdf(filePath);
        }
    }
}
