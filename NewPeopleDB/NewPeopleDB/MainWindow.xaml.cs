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

namespace NewPeopleDB
{
    class Person
    {
        public int id;
        public string name;
        public int age;
        public double height;

        public Person(string name, int age, double height)
        {
            this.name = name;
            this.age = age;
            this.height = height;
        }

        public Person(int id, string name, int age, double height)
        {
            this.id = id;
            this.name = name;
            this.age = age;
            this.height = height;
        }

        override
        public String ToString()
        {
            return string.Format("{0}: {1}, {2}, {3}", id, name, age, height);
        }
    }
    public partial class MainWindow : Window
    {
        Database db;
        public MainWindow()
        {
            InitializeComponent();
            db = new Database();
            getAllPeople();
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            string name = tbName.Text;
            int age = 0;
            double height = slHeight.Value;
            if(name == "")
            {
                MessageBox.Show("Please enter a name.", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(!int.TryParse(tbAge.Text, out age))
            {
                MessageBox.Show("Please enter a valid age.", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(age < 1 || age > 150)
            {
                MessageBox.Show("Please enter a valid age.", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Person person = new Person(name, age, height);
            db.addPerson(person);
            MessageBox.Show("Succesfully added person.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            getAllPeople();
            clearInputFields();
        }

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            int id = 0;
            if(!int.TryParse(lblId.Content.ToString(), out id))
            {
                MessageBox.Show("Please select a person first.", "Could not update", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string name = tbName.Text;
            int age = 0;
            double height = slHeight.Value;
            if (name == "")
            {
                MessageBox.Show("Please enter a name.", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!int.TryParse(tbAge.Text, out age))
            {
                MessageBox.Show("Please enter a valid age.", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (age < 1 || age > 150)
            {
                MessageBox.Show("Please enter a valid age.", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Person person = new Person(id, name, age, height);
            db.updatePerson(person);
            getAllPeople();
            clearInputFields();
        }

        void getAllPeople()
        {
            lvPeople.Items.Clear();
            List<Person> people = db.getAllPeople();
            foreach (Person p in people)
            {
                lvPeople.Items.Add(p);
            }
        }

        private void lvPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lvPeople.SelectedIndex;
            if (index < 0)
            {
                lblId.Content = "...";
                return;
            }
            Person p = (Person) lvPeople.SelectedItem;
            lblId.Content = p.id + "";
            tbName.Text = p.name;
            tbAge.Text = p.age + "";
            slHeight.Value = p.height;
        }

        void clearInputFields()
        {
            tbAge.Text = "";
            tbName.Text = "";
            slHeight.Value = 165;
        }

        private void miDelete_Click(object sender, RoutedEventArgs e)
        {
            int index = lvPeople.SelectedIndex;
            if (index < 0)
            {
                return;
            }

            Person p = (Person)lvPeople.SelectedItem;
            db.deletePerson(p.id);
            getAllPeople();
            clearInputFields();
        }
    }
}
