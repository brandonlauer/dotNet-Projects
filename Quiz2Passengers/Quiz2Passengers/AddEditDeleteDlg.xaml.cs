using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Quiz2Passengers
{
    /// <summary>
    /// Interaction logic for AddEditDeleteDlg.xaml
    /// </summary>
    public partial class AddEditDeleteDlg : Window
    {
        private Passenger currentPass;
        public AddEditDeleteDlg(Passenger pass)
        {
            currentPass = pass;
            InitializeComponent ();
            if (currentPass == null)
            {
                btSave.Content = "Add";
                btDelete.Visibility = Visibility.Hidden;
            }
            else
            {
                lblId.Content = pass.Id;
                tbname.Text = pass.Name;
                tbPassport.Text = pass.Passport;
                tbDestination.Text = pass.Destination;
                dpDepDate.SelectedDate = pass.DepartureDateTime;
                cbDepTime.Text = pass.DepartureDateTime.ToString ("HH:mm");
                btSave.Content = "Update";
            }
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            Passenger p = currentPass == null ? new Passenger () : currentPass;
            p.Name = tbname.Text;
            if(p.Name.Length < 1 || p.Name.Length > 100)
            {
                MessageBox.Show ("Please enter a name between 1 and 100 characters.", "Input error.", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            p.Passport = tbPassport.Text;
            Regex regex = new Regex (@"(?i)[A-Z][A-Z][0-9][0-9][0-9][0-9][0-9][0-9]");
            Match match = regex.Match (p.Passport);
            if (!match.Success)
            {
                MessageBox.Show ("Please enter a valid passport no. (ex: AB123456)", "Input error.", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            p.Destination = tbDestination.Text;
            if (p.Destination.Length < 1 || p.Destination.Length > 100)
            {
                MessageBox.Show ("Please enter a destination between 1 and 100 characters.", "Input error.", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if(dpDepDate.SelectedDate == null)
            {
                MessageBox.Show ("Please select a departure date", "Input error.", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DateTime dt = (DateTime)dpDepDate.SelectedDate;
            p.DepartureDateTime = DateTime.ParseExact (dt.ToShortDateString() + " " + cbDepTime.Text, "M/d/yyyy HH:mm", CultureInfo.InvariantCulture);

            if(currentPass == null)
            {
                Globals.db.AddPassenger (p);
            }
            else
            {
                Globals.db.UpdatePassenger (p);
            }
            DialogResult = true;
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            Globals.db.DeletePassengerById (currentPass.Id);
            DialogResult = true;
        }
    }
}
