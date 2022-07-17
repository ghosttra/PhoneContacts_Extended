using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
using System.Xml.Serialization;
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
            try
            {
                using (FileStream fs = new FileStream("contacts.xml", FileMode.Open))
                {
                    var xmlSerializer = new XmlSerializer(typeof(ObservableCollection<Contact>));
                    ContactBox.ItemsSource = (ObservableCollection<Contact>)xmlSerializer.Deserialize(fs);
                }
            }
            catch (FileNotFoundException fileNotFoundException)
            {
                
            }
        }

        private void EditBtn_OnClick(object sender, RoutedEventArgs e)
        {

            if (EditRow.Height != new GridLength(180))
            {
                EditRow.Height = new GridLength(180);
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
            if (new EmptyPropertyValidationRule().Validate(NameTBox.Text, CultureInfo.CurrentCulture) == ValidationResult.ValidResult &&
                new EmptyPropertyValidationRule().Validate(NumberTBox.Text, CultureInfo.CurrentCulture) == ValidationResult.ValidResult)
            //Остальные поля можно добавить потом, поэтому проверки на последующие поля упущены
            {
                var tempContact = new Contact()
                {
                    Name = NameTBox.Text,
                    Number = NumberTBox.Text,
                    SecondNumber = SecondNumberTBox.Text,
                    Address = AddressTBox.Text,
                    Email = EmailTBox.Text
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
            using (FileStream fs = new FileStream("contacts.xml", FileMode.Create))
            {
                List<Contact> temp = ContactBox.ItemsSource.OfType<Contact>().ToList();
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Contact>));
                xmlSerializer.Serialize(fs, temp);
            }
        }

        private void CallBtn_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("There is no mobile connection, try again later!", "Error", MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
    public class EmptyPropertyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrWhiteSpace((string)value))
                return new ValidationResult(false, "Can not be empty!");
            return ValidationResult.ValidResult;
        }
    }
}
