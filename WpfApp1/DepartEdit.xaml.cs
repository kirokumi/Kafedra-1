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
    /// Логика взаимодействия для DepartEdit.xaml
    /// </summary>
    public partial class DepartEdit : Window
    {
        public entities.Final_Departments _Departments;
        public DepartEdit(entities.Final_Departments selectedDep)
        {
            InitializeComponent();
            _Departments = selectedDep;

            tb_Name.Text = _Departments.DepartmentName;
            tb_Address.Text = _Departments.PlaceOfDepartment;
            tb_Phone.Text = _Departments.Phone;

            cb_Head.ItemsSource = App.Context.Final_Professors.Select(p => p.LastName).ToList();

            cb_Head.SelectedItem = App.Context.Final_Professors.FirstOrDefault(d => d.ID == _Departments.HeadOfDepartment)?.LastName;
        }

        private void b_Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(tb_Name.Text) && !string.IsNullOrWhiteSpace(tb_Address.Text) && cb_Head.SelectedItem != null && !string.IsNullOrWhiteSpace(tb_Phone.Text))
                {
                    var professor = App.Context.Final_Professors.FirstOrDefault(p => p.LastName == cb_Head.SelectedItem.ToString());

                    if (professor != null)
                    {
                        _Departments.DepartmentName = tb_Name.Text;
                        _Departments.PlaceOfDepartment = tb_Address.Text;
                        _Departments.Phone = tb_Phone.Text;
                        _Departments.HeadOfDepartment = professor.ID;

                        App.Context.SaveChanges();
                        MessageBox.Show("Отделение успешно обновлено!");
                        DepartList depart = new DepartList();
                        depart.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Не удалось найти преподавателя.");
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, заполните все поля корректно!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void but_Return_Click(object sender, RoutedEventArgs e)
        {
            DepartList depart = new DepartList();
            depart.Show();
            this.Close();
        }
    }
}
