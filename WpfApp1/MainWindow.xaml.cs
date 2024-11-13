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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void but_Auth_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tb_email.Text != null && pb_Pass.Password != null)
                {
                    var user = App.Context.Final_Professors.Where(p => p.Email == tb_email.Text && p.Password == pb_Pass.Password).FirstOrDefault();
                    if (user != null)
                    {
                        App._Professors = user;

                        MessageBox.Show("Вход от лица преподавателя");
                        ProfWindow profWindow = new ProfWindow();
                        profWindow.Show();
                        this.Close();

                    }
                    else if (tb_email.Text == "admin" && pb_Pass.Password == "admin")
                    {
                        MessageBox.Show("Вход от лица администратора");
                        AdminWindow adminWindow = new AdminWindow();
                        adminWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Произошла ошибка!");
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
