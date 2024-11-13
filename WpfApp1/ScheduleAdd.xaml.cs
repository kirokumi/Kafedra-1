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
    /// Логика взаимодействия для ScheduleAdd.xaml
    /// </summary>
    public partial class ScheduleAdd : Window
    {
        public ScheduleAdd()
        {
            InitializeComponent();

            cb_Sub.ItemsSource = App.Context.Final_Courses.Select(p => p.CourseName).ToList();
            cb_LastName.ItemsSource = App.Context.Final_Professors.Select(p => p.LastName).ToList();
        }

        private void but_Return_Click(object sender, RoutedEventArgs e)
        {
            ScheduleList schedule = new ScheduleList();
            schedule.Show();
            this.Close();
        }

        private void b_Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cb_LastName.SelectedItem != null && cb_Sub.SelectedItem != null &&
                    !string.IsNullOrWhiteSpace(tb_NumClass.Text) &&
                    !string.IsNullOrWhiteSpace(tb_NumRoom.Text) &&
                    dp_Date.SelectedDate != null)
                {
                    var LastName = App.Context.Final_Professors.Where(p => p.LastName == cb_LastName.Text).Select(p => p.ID).FirstOrDefault();
                    var Subject = App.Context.Final_Courses.Where(p => p.CourseName == cb_Sub.Text).Select(p => p.ID).FirstOrDefault();

                    if (LastName == 0 || Subject == 0)
                    {
                        MessageBox.Show("Выберите существующего преподавателя и предмет.");
                        return;
                    }

                    var HoursFrom = App.Context.Final_Courses.Where(p => p.ID == Subject).Select(p => p.MaxHours).FirstOrDefault();
                    var MaxClass = HoursFrom / 2;
                    var CountClass = App.Context.Final_Schedule.Where(p => p.ID_Professors == LastName && p.ID_Courses == Subject).Count();
                    if (CountClass != 0)
                    {
                        MaxClass -= CountClass;
                    }

                    if (MaxClass < 0)
                    {
                        MessageBox.Show("Достигнуто максимальное количество классов для данного преподавателя и предмета.");
                        return;
                    }

                    if (!int.TryParse(tb_NumClass.Text, out int numClass) || !int.TryParse(tb_NumRoom.Text, out int numRoom))
                    {
                        MessageBox.Show("Введите корректные значения для номера класса и номера комнаты.");
                        return;
                    }

                    if (numClass > 6)
                    {
                        MessageBox.Show("Максимальное количество пар за день не может превышать 6!");
                        return;
                    }

                    if (dp_Date.SelectedDate < DateTime.Today)
                    {
                        MessageBox.Show("Нельзя добавлять пары в прошедшие дни!");
                        return;
                    }

                    var newClass = new Final_Schedule
                    {
                        ID_Professors = LastName,
                        ID_Courses = Subject,
                        NumClass = numClass,
                        NumRoom = numRoom,
                        DateOfClass = dp_Date.SelectedDate.Value,
                        ClassLeft = MaxClass
                    };

                    App.Context.Final_Schedule.Add(newClass);
                    App.Context.SaveChanges();
                    MessageBox.Show("Новое отделение добавлено");
                    ScheduleList schedule = new ScheduleList();
                    schedule.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
