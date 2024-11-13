using System;
using System.Linq;
using System.Windows;

namespace WpfApp1
{
    public partial class ScheduleEdit : Window
    {
        private entities.Final_Schedule schedule;

        public ScheduleEdit(entities.Final_Schedule selectedDay)
        {
            InitializeComponent();
            schedule = selectedDay;

            tb_ClassLeft.Text = schedule.ClassLeft.ToString();
            tb_NumClass.Text = schedule.NumClass.ToString();
            tb_NumRoom.Text = schedule.NumRoom.ToString();
            dp_Date.SelectedDate = schedule.DateOfClass;

            cb_Sub.ItemsSource = App.Context.Final_Courses.Select(p => p.CourseName).ToList();
            cb_LastName.ItemsSource = App.Context.Final_Professors.Select(p => p.LastName).ToList();

            var selectedCourse = App.Context.Final_Courses.FirstOrDefault(p => p.ID == schedule.ID_Courses);
            if (selectedCourse != null)
            {
                cb_Sub.SelectedItem = selectedCourse.CourseName;
            }

            var selectedProf = App.Context.Final_Professors.FirstOrDefault(p => p.ID == schedule.ID_Professors);
            if (selectedProf != null)
            {
                cb_LastName.SelectedItem = selectedProf.LastName;
            }
        }

        private void b_Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(tb_ClassLeft.Text, out int classLeft) &&
                    int.TryParse(tb_NumClass.Text, out int numClass) &&
                    int.TryParse(tb_NumRoom.Text, out int numRoom) &&
                    dp_Date.SelectedDate != null &&
                    cb_LastName.SelectedItem != null &&
                    cb_Sub.SelectedItem != null)
                {
                    var profId = App.Context.Final_Professors
                        .Where(p => p.LastName == cb_LastName.Text)
                        .Select(p => p.ID)
                        .FirstOrDefault();

                    var courseId = App.Context.Final_Courses
                        .Where(p => p.CourseName == cb_Sub.Text)
                        .Select(p => p.ID)
                        .FirstOrDefault();

                    // Обновление расписания
                    schedule.ClassLeft = classLeft;
                    schedule.NumClass = numClass;
                    schedule.NumRoom = numRoom;
                    schedule.DateOfClass = dp_Date.SelectedDate.Value; // Убедитесь, что значение не null
                    schedule.ID_Professors = profId;
                    schedule.ID_Courses = courseId;

                    App.Context.SaveChanges();
                    MessageBox.Show("Данные о паре изменились");

                    // Открытие нового окна
                    ScheduleList scheduleList = new ScheduleList();
                    scheduleList.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Пожалуйста, заполните все поля корректно.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        private void but_Return_Click(object sender, RoutedEventArgs e)
        {
            ScheduleList scheduleList = new ScheduleList();
            scheduleList.Show();
            this.Close();
        }
    }
}