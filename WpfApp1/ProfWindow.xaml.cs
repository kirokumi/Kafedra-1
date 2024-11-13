using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class ProfWindow : Window
    {
        public ProfWindow()
        {
            InitializeComponent();

            LoadData();
        }

        private void LoadData()
        {
            dg_Course.ItemsSource = App.Context.Final_Courses
                .Where(p => p.ProfessorID == App._Professors.ID)
                .ToList();

            tb_FIO.Text = App._Professors.LastName + " " + App._Professors.FirstName + " " + App._Professors.Patronimyc;
            tb_Email.Text = App._Professors.Email;
            tb_Phone.Text = App._Professors.Phone;
            tb_Depart.Text = App.Context.Final_Departments
                .Where(p => p.ID == App._Professors.DepartmentID)
                .Select(p => p.DepartmentName)
                .FirstOrDefault();
            tb_PhoneDep.Text = App.Context.Final_Departments
                .Where(p => p.ID == App._Professors.DepartmentID)
                .Select(p => p.Phone)
                .FirstOrDefault();
        }

        private void cb_Sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cb_Sort.SelectedItem is ComboBoxItem selectedItem)
            {
                string sortBy = selectedItem.Content.ToString();

                switch (sortBy)
                {
                    case "Название курса":
                        dg_Course.ItemsSource = App.Context.Final_Courses
                            .Where(p => p.ProfessorID == App._Professors.ID)
                            .OrderBy(p => p.CourseName)
                            .ToList();
                        break;
                    case "Количество часов":
                        dg_Course.ItemsSource = App.Context.Final_Courses
                            .Where(p => p.ProfessorID == App._Professors.ID)
                            .OrderBy(p => p.MaxHours)
                            .ToList();
                        break;
                }
            }
        }

        private void tb_Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void SaveSchedule_Click(object sender, RoutedEventArgs e)
        {
            var scheduleItems = App.Context.Final_Schedule
                .Where(s => s.ID_Professors == App._Professors.ID)
                .Select(s => new
                {
                    CourseName = s.Final_Courses.CourseName,
                    DateOfClass = s.DateOfClass,
                    NumClass = s.NumClass,
                    NumRoom = s.NumRoom
                })
                .ToList();

            string fileName = $"Расписание_{App._Professors.LastName}.docx";
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(fileName, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body body = new Body();

                Paragraph title = new Paragraph(new Run(new Text("Расписание занятий")));
                title.ParagraphProperties = new ParagraphProperties(new Justification() { Val = JustificationValues.Center });
                title.Append(new RunProperties(new Bold(), new FontSize() { Val = "40" }));
                body.Append(title);

                Table table = new Table();

                TableRow headerRow = new TableRow();
                headerRow.Append(new TableCell(new Paragraph(new Run(new Text("Название курса")))));
                headerRow.Append(new TableCell(new Paragraph(new Run(new Text("Дата занятия")))));
                headerRow.Append(new TableCell(new Paragraph(new Run(new Text("Номер класса")))));
                headerRow.Append(new TableCell(new Paragraph(new Run(new Text("Номер аудитории")))));
                table.Append(headerRow);

                foreach (var item in scheduleItems)
                {
                    TableRow row = new TableRow();
                    row.Append(new TableCell(new Paragraph(new Run(new Text(item.CourseName)))));
                    row.Append(new TableCell(new Paragraph(new Run(new Text(item.DateOfClass?.ToString("dd.MM.yyyy") ?? "N/A")))));
                    row.Append(new TableCell(new Paragraph(new Run(new Text(item.NumClass?.ToString() ?? "N/A")))));
                    row.Append(new TableCell(new Paragraph(new Run(new Text(item.NumRoom?.ToString() ?? "N/A")))));
                    table.Append(row);
                }

                body.Append(table);
                mainPart.Document.Append(body);
                mainPart.Document.Save();
            }

            MessageBox.Show("Расписание успешно сохранено!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}