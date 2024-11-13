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
    /// Логика взаимодействия для ProfEdit.xaml
    /// </summary>
    public partial class ProfEdit : Window
    {
        public entities.Final_Professors professors;
        public ProfEdit(entities.Final_Professors selectedProf)
        {
            InitializeComponent();
            professors = selectedProf;

            tb_Email.Text = professors.Email;
            tb_FirstName.Text = professors.FirstName;
            tb_LastName.Text = professors.LastName;
            tb_Password.Text = professors.Password;
            tb_Patronimyc.Text = professors.Patronimyc;
            tb_Phone.Text = professors.Phone;

            cb_Depart.ItemsSource = App.Context.Final_Departments.Select(p => p.DepartmentName).ToList();

            cb_Depart.SelectedItem = App.Context.Final_Departments.FirstOrDefault(d => d.ID == professors.DepartmentID)?.DepartmentName;

        }

        private void but_Return_Click(object sender, RoutedEventArgs e)
        {
            ProfList profList = new ProfList();
            profList.Show();
            this.Close();
        }

        private void b_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tb_Email.Text) || string.IsNullOrWhiteSpace(tb_FirstName.Text) || string.IsNullOrWhiteSpace(tb_LastName.Text) || string.IsNullOrWhiteSpace(tb_Password.Text) ||
                string.IsNullOrWhiteSpace(tb_Patronimyc.Text) || string.IsNullOrWhiteSpace(tb_Phone.Text) || string.IsNullOrWhiteSpace(cb_Depart.Text))
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

            professors.FirstName = tb_FirstName.Text;
            professors.Email = tb_Email.Text;
            professors.Patronimyc = tb_Patronimyc.Text;
            professors.DepartmentID = depart.ID;
            professors.LastName = tb_LastName.Text;
            professors.Phone = tb_Phone.Text;
            professors.Password = tb_Password.Text;

            try
            {
                App.Context.SaveChanges();
                MessageBox.Show("Изменения внесены");
                ProfList profList = new ProfList();
                profList.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении изменений: {ex.Message}");
            }
        }
    }
}
