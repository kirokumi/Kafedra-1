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
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void But_Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void tb_Users_Click(object sender, RoutedEventArgs e)
        {
            UserList userList = new UserList();
            userList.Show();
            this.Close();
        }

        private void tb_Prof_Click(object sender, RoutedEventArgs e)
        {
            ProfList profList = new ProfList();
            profList.Show();
            this.Close();
        }

        private void tb_Courses_Click(object sender, RoutedEventArgs e)
        {
            CoursesList coursesList = new CoursesList();
            coursesList.Show();
            this.Close();
        }

        private void tb_Depart_Click(object sender, RoutedEventArgs e)
        {
            DepartList departList = new DepartList();
            departList.Show();
            this.Close();
        }

        private void b_Shedule_Click(object sender, RoutedEventArgs e)
        {
            ScheduleList scheduleList = new ScheduleList();
            scheduleList.Show();
            this.Close();
        }
    }
}
