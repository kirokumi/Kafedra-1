using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.entities;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для CourseAdd.xaml
    /// </summary>
    public partial class CourseAdd : Window
    {
        public CourseAdd()
        {
            InitializeComponent();

            cb_Depart.ItemsSource = App.Context.Final_Departments.Select(p => p.DepartmentName).ToList();
            cb_Prof.ItemsSource = App.Context.Final_Professors.Select(r => r.LastName).ToList();
        }

        private void tb_Add_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tb_Hours.Text) && !string.IsNullOrWhiteSpace(tb_Name.Text) && cb_Depart.SelectedItem != null && cb_Prof.SelectedItem != null)
            {
                var DID = App.Context.Final_Departments
                    .Where(p => p.DepartmentName == cb_Depart.Text)
                    .Select(p => p.ID)
                    .FirstOrDefault();

                var PID = App.Context.Final_Professors
                    .Where(p => p.LastName == cb_Prof.Text)
                    .Select(p => p.ID)
                    .FirstOrDefault();

                var newCourse = new Final_Courses
                {
                    CourseName = tb_Name.Text,
                    MaxHours = int.Parse(tb_Hours.Text),
                    DepartmentID = DID,
                    ProfessorID = PID
                };

                App.Context.Final_Courses.Add(newCourse);
                App.Context.SaveChanges();
                MessageBox.Show("Новый курс добавлен");
                CoursesList coursesList = new CoursesList();
                coursesList.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все поля!");
            }
        }

        private void but_Return_Click(object sender, RoutedEventArgs e)
        {
            CoursesList coursesList = new CoursesList();
            coursesList.Show();
            this.Close();
        }
    }
}
