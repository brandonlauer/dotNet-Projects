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

namespace Quiz1Flights
{
    /// <summary>
    /// Interaction logic for AddEditDlg.xaml
    /// </summary>
    public partial class AddEditDlg : Window
    {
        private Flight currentFlight;
        public AddEditDlg(Flight flight)
        {
            currentFlight = flight;
            InitializeComponent ();
            {
                if(currentFlight == null)
                {
                    btSave.Content = "Add";
                }
                else
                {
                    lblId.Content = flight.Id;
                    dpOuDate.SelectedDate = flight.OuDay;
                    tbFromCode.Text = flight.FromCode;
                    tbToCode.Text = flight.ToCode;
                    cbType.Text = flight.Type.ToString();
                    slPassengers.Value = flight.Passengers;
                    btSave.Content = "Update";
                }
            }
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            Flight f = currentFlight == null ? new Flight () : currentFlight;
            if(dpOuDate.SelectedDate == null)
            {
                MessageBox.Show ("Please select a date", "Input error.", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            f.OuDay = (DateTime) dpOuDate.SelectedDate;
            f.FromCode = tbFromCode.Text;
            if (f.FromCode.Length < 3 || f.FromCode.Length > 5)
            {
                MessageBox.Show ("From code must be 3-5 characters", "Input error.", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            f.ToCode = tbToCode.Text;
            if (f.ToCode.Length < 3 || f.ToCode.Length > 5)
            {
                MessageBox.Show ("To code must be 3-5 characters", "Input error.", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if(cbType.Text == "Domestic")
            {
                f.Type = Flight.FlightType.Domestic;
            }else if (cbType.Text == "International")
            {
                f.Type = Flight.FlightType.International;
            }else if (cbType.Text == "Private")
            {
                f.Type = Flight.FlightType.Private;
            }

            f.Passengers = (int)slPassengers.Value;

            if(currentFlight == null)
            {// Add
                try
                {
                    Globals.ctx.Flights.Add (f);
                    Globals.ctx.SaveChanges ();
                }
                catch (System.IO.InvalidDataException ex)
                {
                    MessageBox.Show (ex.Message);
                }
            }
            else
            {// Update
                var flights = (from r in Globals.ctx.Flights where r.Id == f.Id select r).ToList ();
                Flight flight = flights[0];
                flight.OuDay = f.OuDay;
                flight.FromCode = f.FromCode;
                flight.ToCode = f.ToCode;
                flight.Type = f.Type;
                flight.Passengers = f.Passengers;
                Globals.ctx.SaveChanges ();
            }
            DialogResult = true;
        }
    }
}
