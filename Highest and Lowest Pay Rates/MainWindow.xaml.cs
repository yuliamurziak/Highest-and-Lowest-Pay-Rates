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

namespace Highest_and_Lowest_Pay_Rates
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainDB mdb = new MainDB();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = mdb;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mdb.Calc();
        }

        private void btnMax_Click(object sender, RoutedEventArgs e)
        {
            mdb.GetHighestPay();
        }

        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            mdb.GetLowestPay();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            mdb.Clear();          

        }
    }
}
