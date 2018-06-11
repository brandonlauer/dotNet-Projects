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

namespace Quiz2Passengers
{
    /// <summary>
    /// Interaction logic for sortingDlg.xaml
    /// </summary>
    public partial class SortingDlg : Window
    {
        public SortingDlg()
        {
            InitializeComponent ();
            if (Globals.sortingMode == "Name")
            {
                rbName.IsChecked = true;
            }
            if (Globals.sortingMode == "Passport")
            {
                rbPassport.IsChecked = true;
            }
            if (Globals.sortingMode == "Destination")
            {
                rbDest.IsChecked = true;
            }
            if (Globals.sortingMode == "DepartureDateTime")
            {
                rbDepDateTime.IsChecked = true;
            }
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btApply_Click(object sender, RoutedEventArgs e)
        {
            if(rbName.IsChecked == true)
            {
                Globals.sortingMode = "Name";
            }
            if (rbPassport.IsChecked == true)
            {
                Globals.sortingMode = "Passport";
            }
            if (rbDest.IsChecked == true)
            {
                Globals.sortingMode = "Destination";
            }
            if (rbDepDateTime.IsChecked == true)
            {
                Globals.sortingMode = "DepartureDateTime";
            }
            DialogResult = true;
        }
    }
}
