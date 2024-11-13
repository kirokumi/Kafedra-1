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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для UserEdit.xaml
    /// </summary>
    public partial class UserEdit : Window
    {
        public entities.Final_Students students;
        public UserEdit(entities.Final_Students selectedStudent)
        {
            InitializeComponent();
            students = selectedStudent;

            tb_Email.Text = students.Email;
            tb_FirstName.Text = students.FirstName;
            tb_LastName.Text = students.LastName;
            dp_Date.SelectedDate = students.EnrollmentDate;
            tb_Patronimyc.Text = students.Patronimyc;

            cb_Depart.ItemsSource = App.Context.Final_Departments.Select(p => p.DepartmentName).ToList();

            cb_Depart.SelectedItem = App.Context.Final_Departments.FirstOrDefault(d => d.ID == students.DepartmentID)?.DepartmentName;
        }

        private void but_Return_Click(object sender, RoutedEventArgs e)
        {
            UserList user = new UserList();
            user.Show();
            this.Close();
        }

        private void b_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tb_Email.Text) || string.IsNullOrWhiteSpace(tb_FirstName.Text) || string.IsNullOrWhiteSpace(tb_LastName.Text) || !dp_Date.SelectedDate.HasValue 
                || string.IsNullOrWhiteSpace(tb_Patronimyc.Text) || string.IsNullOrWhiteSpace(cb_Depart.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            var depart = App.Context.Final_Departments.FirstOrDefault(p => p.DepartmentName == cb_Depart.Text);
            if (depart == null)
            {
                MessageBox.Show("Выбранный департамент не найден.");
                return;
            }

            students.FirstName = tb_FirstName.Text;
            students.Email = tb_Email.Text;
            students.Patronimyc = tb_Patronimyc.Text;
            students.DepartmentID = depart.ID;
            students.LastName = tb_LastName.Text;
            students.EnrollmentDate = (DateTime)dp_Date.SelectedDate;

            try
            {
                App.Context.SaveChanges();
                MessageBox.Show("Изменения внесены");
                UserList user = new UserList();
                user.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении изменений: {ex.Message}");
            }
        }
    }
}
