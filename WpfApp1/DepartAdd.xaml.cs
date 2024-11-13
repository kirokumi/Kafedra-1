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
    /// Логика взаимодействия для DepartAdd.xaml
    /// </summary>
    public partial class DepartAdd : Window
    {
        public DepartAdd()
        {
            InitializeComponent();
            cb_Head.ItemsSource = App.Context.Final_Professors.Select(p => p.LastName).ToList();
        }

        private void but_Return_Click(object sender, RoutedEventArgs e)
        {
            DepartList departList = new DepartList();
            departList.Show();
            this.Close();
        }

        private void b_Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tb_Address.Text != null && tb_Name.Text != null && tb_Phone.Text != null && cb_Head.SelectedItem != null)
                {
                    var Head = App.Context.Final_Professors.Where(p => p.LastName == cb_Head.Text).Select(p => p.ID).FirstOrDefault();

                    var newUser = new Final_Departments
                    {
                        DepartmentName = tb_Name.Text,
                        HeadOfDepartment = Head,
                        PlaceOfDepartment = tb_Address.Text,
                        Phone = tb_Phone.Text
                    };

                    if (newUser != null)
                    {
                        App.Context.Final_Departments.Add(newUser);
                        App.Context.SaveChanges();
                        MessageBox.Show("Новое отделение добавлено");
                        DepartList departList = new DepartList();
                        departList.Show();
                        this.Close();
                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
