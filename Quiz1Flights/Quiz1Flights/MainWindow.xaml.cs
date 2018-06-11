using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Quiz1Flights
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static ObservableCollection<Flight> flightList;
        public MainWindow()
        {
            InitializeComponent ();

            try
            {
                Globals.ctx = new FlightsDBContext ();
                var v = (from f in Globals.ctx.Flights select f).ToList ();
                flightList = new ObservableCollection<Flight> (v);
                lvFlights.DataContext = flightList;
                lblTotalFlights.Content = "Total flights: " + flightList.Count;
            }
            catch (System.IO.InvalidDataException ex)
            {
                MessageBox.Show (ex.Message);
            }
        }

        private void miAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditDlg dlg = new AddEditDlg (null);
            if (dlg.ShowDialog () == true)
            {
                RefreshList ();
            }
        }

        private void miSave_Click(object sender, RoutedEventArgs e)
        {
            if(lvFlights.SelectedItems.Count == 0)
            {
                MessageBox.Show ("Please select flights first.", "Invalid selection", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog ();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";
            if(dlg.ShowDialog() == true)
            {
                string filename = dlg.FileName;
                foreach (var item in lvFlights.SelectedItems)
                {
                    Flight f = (Flight)item;
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter (filename, true))
                    {
                        string flightLog = string.Format ("Id:{0};{1};From:{2};To:{3};{4};Passengers:{5}", 
                            f.Id, f.OuDay.ToShortDateString(), f.FromCode, f.ToCode, f.Type.ToString(), f.Passengers);
                        file.WriteLine (flightLog);
                        MessageBox.Show ("Selection saved to file.");
                    }
                }
            }
        }

        private void miExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit (1);
        }

        private void lvFlights_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Flight f = (Flight)lvFlights.SelectedItem;
            if(f == null)
            {
                return;
            }
            AddEditDlg dlg = new AddEditDlg (f);
            if(dlg.ShowDialog() == true)
            {
                RefreshList ();
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Flight f = (Flight)lvFlights.SelectedItem;
            if (f == null)
            {
                return;
            }
            var flights = (from r in Globals.ctx.Flights where r.Id == f.Id select r).ToList ();
            Flight flight = flights[0];
            Globals.ctx.Flights.Remove (flight);
            Globals.ctx.SaveChanges ();
            RefreshList ();
        }

        void RefreshList()
        {
            flightList = new ObservableCollection<Flight> ((from r in Globals.ctx.Flights select r).ToList ());
            lvFlights.DataContext = flightList;
            lblTotalFlights.Content = "Total flights: " + flightList.Count;
        }
    }
}
