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
    /// Логика взаимодействия для ProfList.xaml
    /// </summary>
    public partial class ProfList : Window
    {
        public ProfList()
        {
            InitializeComponent();
            FillTable();
        }

        private void FillTable()
        {
            dg_Prof.ItemsSource = App.Context.Final_Professors.ToList();
        }

        private void But_Return_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
            this.Close();
        }

        private void But_Add_Click(object sender, RoutedEventArgs e)
        {
            ProfAdd profAdd = new ProfAdd();
            profAdd.Show();
            this.Close();
        }

        private void But_Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы точно хотите удалить этого преподавателя из базы?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var deleteThis = (sender as Button).DataContext as Final_Professors;
                    if (deleteThis != null)
                    {
                        App.Context.Final_Professors.Remove(deleteThis);
                        App.Context.SaveChanges();
                        MessageBox.Show("Преподаватель удален");
                        FillTable();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void But_Edit_Click(object sender, RoutedEventArgs e)
        {
            var selectedProf = (sender as Button).DataContext as Final_Professors;
            if (selectedProf != null)
            {
                ProfEdit profEdit = new ProfEdit(selectedProf);
                profEdit.Show();
                this.Close();
            }
        }

        private void filterProf()
        {
            string searchText = SearchTextBox.Text.ToLower();

            var allProfessors = App.Context.Final_Professors.ToList();

            var filteredProfessors = allProfessors.Where(professor =>
                professor.FirstName.ToLower().Contains(searchText) ||
                professor.LastName.ToLower().Contains(searchText) ||
                professor.Patronimyc.ToLower().Contains(searchText) ||
                professor.Email.ToLower().Contains(searchText) ||
                professor.Phone.ToLower().Contains(searchText) ||
                professor.Password.ToLower().Contains(searchText) ||
                professor.Final_Departments1.DepartmentName.ToLower().Contains(searchText)).ToList();

            dg_Prof.ItemsSource = filteredProfessors;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            filterProf();
        }
    }
}
