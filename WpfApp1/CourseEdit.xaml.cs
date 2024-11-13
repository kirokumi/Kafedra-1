using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для CourseEdit.xaml
    /// </summary>
    public partial class CourseEdit : Window
    {
        private entities.Final_Courses _Courses;

        public CourseEdit(entities.Final_Courses selectedCourse)
        {
            InitializeComponent();
            _Courses = selectedCourse;

            tb_Name.Text = _Courses.CourseName;
            tb_Hours.Text = _Courses.MaxHours.ToString();
            cb_Depart.ItemsSource = App.Context.Final_Departments.Select(p => p.DepartmentName).ToList();
            cb_Prof.ItemsSource = App.Context.Final_Professors.Select(r => r.LastName).ToList();

            cb_Depart.SelectedItem = App.Context.Final_Departments.FirstOrDefault(d => d.ID == _Courses.DepartmentID)?.DepartmentName;
            cb_Prof.SelectedItem = App.Context.Final_Professors.FirstOrDefault(p => p.ID == _Courses.ProfessorID)?.LastName;
        }

        private void but_Return_Click(object sender, RoutedEventArgs e)
        {
            CoursesList coursesList = new CoursesList();
            coursesList.Show();
            this.Close();
        }

        private void tb_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tb_Name.Text) && int.TryParse(tb_Hours.Text, out int maxHours) && cb_Depart.SelectedItem != null && cb_Prof.SelectedItem != null)
            {
                var department = App.Context.Final_Departments.FirstOrDefault(d => d.DepartmentName == cb_Depart.SelectedItem.ToString());
                var professor = App.Context.Final_Professors.FirstOrDefault(p => p.LastName == cb_Prof.SelectedItem.ToString());

                if (department != null && professor != null)
                {
                    _Courses.CourseName = tb_Name.Text;
                    _Courses.MaxHours = maxHours;
                    _Courses.DepartmentID = department.ID;
                    _Courses.ProfessorID = professor.ID;

                    App.Context.SaveChanges();
                    MessageBox.Show("Курс успешно обновлен!");
                    CoursesList coursesList = new CoursesList();
                    coursesList.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Не удалось найти отделение или преподавателя.");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все поля корректно!");
            }
        }
    }
}