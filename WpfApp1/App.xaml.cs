using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static entities.user88_dbEntities2 Context { get; } = new entities.user88_dbEntities2();
        public static entities.Final_Professors _Professors = new entities.Final_Professors();
    }
}
