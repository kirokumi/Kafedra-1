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
    /// Логика взаимодействия для ScheduleList.xaml
    /// </summary>
    public partial class ScheduleList : Window
    {
        public ScheduleList()
        {
            InitializeComponent();
            FillTable();
        }

        private void FillTable()
        {
            dg_Schedule.ItemsSource = App.Context.Final_Schedule.ToList();
        }
        private void But_Edit_Click(object sender, RoutedEventArgs e)
        {
            var selectedDay = (sender as Button).DataContext as Final_Schedule;
            ScheduleEdit schedule = new ScheduleEdit(selectedDay);
            schedule.Show();
            this.Close();
        }

        private void But_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы хотите удалить пару?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Button button = sender as Button;
                if (button != null)
                {
                    var schedule = button.DataContext as Final_Schedule;
                    if (schedule != null)
                    {
                        App.Context.Final_Schedule.Remove(schedule);
                        App.Context.SaveChanges();

                        MessageBox.Show("Пара удалена");
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
            ScheduleAdd scheduleAdd = new ScheduleAdd();
            scheduleAdd.Show();
            this.Close();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterTable();
        }

        private void FilterTable()
        {
            string searchText = SearchTextBox.Text.ToLower();
            var filteredData = App.Context.Final_Schedule
                .Where(schedule => schedule.Final_Professors.LastName.ToLower().Contains(searchText) ||
                                 schedule.Final_Courses.CourseName.ToLower().Contains(searchText) ||
                                 schedule.NumClass.ToString().Contains(searchText) ||
                                 schedule.NumRoom.ToString().Contains(searchText) ||
                                 schedule.DateOfClass.ToString().Contains(searchText) || 
                                 schedule.ClassLeft.ToString().Contains(searchText))
                .ToList();

            dg_Schedule.ItemsSource = filteredData;
        }
    }
}
