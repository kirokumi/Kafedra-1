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
    /// Логика взаимодействия для ProfAdd.xaml
    /// </summary>
    public partial class ProfAdd : Window
    {
        public ProfAdd()
        {
            InitializeComponent();
            cb_Depart.ItemsSource = App.Context.Final_Departments.Select(p => p.DepartmentName).ToList();
        }

        private void b_Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tb_Email.Text != null && tb_FirstName.Text != null && tb_LastName.Text != null && tb_Password.Text != null && tb_Patronimyc.Text != null && tb_Phone.Text != null && cb_Depart.Text != null)
                {
                    var dep = App.Context.Final_Departments.Where(p => p.DepartmentName == cb_Depart.Text).Select(p => p.ID).First();

                    var newProf = new Final_Professors
                    {
                        FirstName = tb_FirstName.Text,
                        LastName = tb_LastName.Text,
                        Password = tb_Password.Text,
                        Patronimyc = tb_Patronimyc.Text,
                        Email = tb_Email.Text,
                        Phone = tb_Phone.Text,
                        DepartmentID = dep
                    };
                    if (newProf != null)
                    {
                        App.Context.Final_Professors.Add(newProf);
                        App.Context.SaveChanges();
                        MessageBox.Show("Преподаватель зарегистрирован");
                        ProfList profList = new ProfList();
                        profList.Show();
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
            ProfList profList = new ProfList();
            profList.Show();
            this.Close();
        }
    }
}
