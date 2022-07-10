using System;
using System.Collections.Generic;
using System.IO;
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
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace PhoneContacts
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EditBtn_OnClick(object sender, RoutedEventArgs e)
        {

            if (EditRow.Height != new GridLength(100))
            {
                EditRow.Height = new GridLength(100);
                BttnsRow.Height = new GridLength(0);
            }
            else
            {
                EditRow.Height = new GridLength(0);
                BttnsRow.Height = GridLength.Auto;
            }
        }

        Regex regex = new Regex("[^0-9]+");
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = regex.IsMatch(e.Text);
        }

        private void AddBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (NameTBox.Text != string.Empty && NumberTBox.Text != string.Empty)
            {
                var tempContact = new Contact()
                {
                    Name = NameTBox.Text,
                    Number = NumberTBox.Text
                };
                List<Contact> temp = new List<Contact>(ContactBox.ItemsSource.Cast<Contact>()) { tempContact };
                ContactBox.ItemsSource = temp;
            }
        }

        private void RemoveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (ContactBox.SelectedItem != null)
            {
                List<Contact> temp = new List<Contact>(ContactBox.ItemsSource.Cast<Contact>());
                temp.Remove((Contact)ContactBox.SelectedItem);
                ContactBox.ItemsSource = temp;
            }
        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            using (StreamWriter sw = File.CreateText(@$"{Directory.GetCurrentDirectory()}\contacts.txt"))
            {
                var collection = ContactBox.ItemsSource.Cast<Contact>();
                foreach (var contact in collection)
                {
                    sw.WriteLine(contact.Name);
                    sw.WriteLine(contact.Number);
                }
            }
        }

        private void CallBtn_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("There is no mobile connection, try again later!", "Error", MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}
