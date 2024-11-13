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
    /// Логика взаимодействия для UserAdd.xaml
    /// </summary>
    public partial class UserAdd : Window
    {
        public UserAdd()
        {
            InitializeComponent();
            cb_Depart.ItemsSource = App.Context.Final_Departments.Select(p => p.DepartmentName).ToList();
        }

        private void b_Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tb_Email.Text != null && tb_FirstName.Text != null && tb_LastName.Text != null && dp_Date.SelectedDate != null && tb_Patronimyc.Text != null && cb_Depart.Text != null)
                {
                    var dep = App.Context.Final_Departments.Where(p => p.DepartmentName == cb_Depart.Text).Select(p => p.ID).First();

                    var newStud = new Final_Students
                    {
                        FirstName = tb_FirstName.Text,
                        LastName = tb_LastName.Text,
                        EnrollmentDate = (DateTime)dp_Date.SelectedDate,
                        Patronimyc = tb_Patronimyc.Text,
                        Email = tb_Email.Text,
                        DepartmentID = dep
                    };
                    if (newStud != null)
                    {
                        App.Context.Final_Students.Add(newStud);
                        App.Context.SaveChanges();
                        MessageBox.Show("Студент зарегистрирован");
                        UserList user = new UserList();
                        user.Show();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void but_Return_Click(object sender, RoutedEventArgs e)
        {
            UserList user = new UserList();
            user.Show();
            this.Close();
        }
    }
}