using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Quiz2Passengers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            try
            {
                Globals.db = new Database ();
                InitializeComponent ();
                refreshPassengerList ();
            }
            catch (SqlException e)
            {
                Console.WriteLine (e.StackTrace);
                MessageBox.Show ("Error opening database connection." + e.Message);
                Environment.Exit (1);
            }
            
        }

        private void refreshPassengerList()
        {
            lvPassengers.ItemsSource = Globals.db.GetAllPassengers ();
        }

        private void lvPassengers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Passenger p = (Passenger)lvPassengers.SelectedItem;
            if(p == null)
            {
                return;
            }
            AddEditDeleteDlg dlg = new AddEditDeleteDlg (p);
            if (dlg.ShowDialog () == true)
            {
                lvPassengers.ItemsSource = Globals.db.GetAllPassengers ();
            }
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditDeleteDlg dlg = new AddEditDeleteDlg (null);
            if (dlg.ShowDialog () == true)
            {
                lvPassengers.ItemsSource = Globals.db.GetAllPassengers ();
            }
        }

        private void btSort_Click(object sender, RoutedEventArgs e)
        {
            SortingDlg dlg = new SortingDlg ();
            if(dlg.ShowDialog() == true)
            {
                lvPassengers.ItemsSource = Globals.db.GetAllPassengers ();
            }
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Passenger> passList = Globals.db.GetAllPassengers ();
            string word = tbSearch.Text.ToLower ();
            if(word != "")
            {
                var result = from p in passList where p.Name.ToLower ().Contains (word) || p.Destination.ToLower ().Contains (word) select p;
                passList = result.ToList ();
            }
            lvPassengers.ItemsSource = passList;
        }
    }
}
