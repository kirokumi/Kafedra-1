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
    /// Логика взаимодействия для UserList.xaml
    /// </summary>
    public partial class UserList : Window
    {
        public UserList()
        {
            InitializeComponent();
            FillTable();
        }

        private void FillTable()
        {
            dg_Stud.ItemsSource = App.Context.Final_Students.ToList();
        }

        private void But_Edit_Click(object sender, RoutedEventArgs e)
        {
            var selectedStudent = (sender as Button).DataContext as Final_Students;
            if (selectedStudent != null)
            {
                UserEdit userEdit = new UserEdit(selectedStudent);
                userEdit.Show();
                this.Close();
            }
        }

        private void But_Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы точно хотите удалить студента?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var deleteThis = (sender as Button).DataContext as Final_Students;
                    if (deleteThis != null)
                    {
                        App.Context.Final_Students.Remove(deleteThis);
                        App.Context.SaveChanges();
                        MessageBox.Show("Студент удален из базы данных");
                        FillTable();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void But_Add_Click(object sender, RoutedEventArgs e)
        {
            UserAdd userAdd = new UserAdd();
            userAdd.Show();
            this.Close();
        }

        private void But_Return_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
            this.Close();
        }

        private void FilterStudent()
        {
            string searchText = SearchTextBox.Text.ToLower();

            var allStudents = App.Context.Final_Students.ToList();

            var filteredStudents = allStudents.Where(student =>
                student.FirstName.ToLower().Contains(searchText) ||
                student.LastName.ToLower().Contains(searchText) ||
                student.Patronimyc.ToLower().Contains(searchText) ||
                student.Email.ToLower().Contains(searchText) ||
                student.EnrollmentDate.ToString("dd.MM.yyyy").Contains(searchText) ||
                student.Final_Departments.DepartmentName.ToLower().Contains(searchText)).ToList();

            dg_Stud.ItemsSource = filteredStudents;
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterStudent();
        }
    }
}
