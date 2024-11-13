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
    /// Логика взаимодействия для CoursesList.xaml
    /// </summary>
    public partial class CoursesList : Window
    {
        public CoursesList()
        {
            InitializeComponent();

            FillTable();
        }

        private void FillTable()
        {
            dg_Course.ItemsSource = App.Context.Final_Courses.ToList();
        }

        private void But_Edit_Click(object sender, RoutedEventArgs e)
        {
            var selectedCourse = (sender as Button).DataContext as Final_Courses;
            CourseEdit courseEdit = new CourseEdit(selectedCourse);
            courseEdit.Show();
            this.Close();
        }

        private void But_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы хотите удалить курс?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Button button = sender as Button;
                if (button != null)
                {
                    var course = button.DataContext as Final_Courses;
                    if (course != null)
                    {
                        App.Context.Final_Courses.Remove(course);
                        App.Context.SaveChanges();

                        MessageBox.Show("Курс удален");
                        FillTable();
                    }
                }
            }
        }

        private void But_Return_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
            this.Close();
        }

        private void But_Add_Click(object sender, RoutedEventArgs e)
        {
            CourseAdd courseAdd = new CourseAdd();
            courseAdd.Show();
            this.Close();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterTable();
        }

        private void FilterTable()
        {
            string searchText = SearchTextBox.Text.ToLower();
            var filteredData = App.Context.Final_Courses
                .Where(course => course.CourseName.ToLower().Contains(searchText) ||
                                 course.MaxHours.ToString().Contains(searchText) ||
                                 course.Final_Departments.DepartmentName.ToLower().Contains(searchText) ||
                                 course.Final_Professors.LastName.ToLower().Contains(searchText))
                .ToList();

            dg_Course.ItemsSource = filteredData;
        }
    }
}
