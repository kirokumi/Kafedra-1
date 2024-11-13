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
    /// Логика взаимодействия для DepartList.xaml
    /// </summary>
    public partial class DepartList : Window
    {
        public DepartList()
        {
            InitializeComponent();
            FillTable();
        }

        private void FillTable()
        {
            dg_Dep.ItemsSource = App.Context.Final_Departments.ToList();
        }

        private void But_Return_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
            this.Close();
        }

        private void But_Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы точно хотите удалить отделение из списка?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var deleteThis = (sender as Button).DataContext as Final_Departments;
                    if (deleteThis != null)
                    {
                        App.Context.Final_Departments.Remove(deleteThis);
                        App.Context.SaveChanges();
                        MessageBox.Show("Отделение удалено");
                        FillTable();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void But_Add_Click(object sender, RoutedEventArgs e)
        {
            DepartAdd departAdd = new DepartAdd();
            departAdd.Show();
            this.Close();
        }

        private void But_Edit_Click(object sender, RoutedEventArgs e)
        {
            var selectedDep = (sender as Button).DataContext as Final_Departments;
            DepartEdit departEdit = new DepartEdit(selectedDep);
            departEdit.Show();
            this.Close();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterTable();
        }

        private void FilterTable()
        {
            string searchText = SearchTextBox.Text.ToLower();
            var filteredData = App.Context.Final_Departments
                .Where(dep => dep.DepartmentName.ToLower().Contains(searchText) ||
                              dep.Final_Professors.LastName.ToLower().Contains(searchText) ||
                              dep.PlaceOfDepartment.ToLower().Contains(searchText) ||
                              dep.Phone.ToLower().Contains(searchText))
                .ToList();

            dg_Dep.ItemsSource = filteredData;
        }
    }
}
