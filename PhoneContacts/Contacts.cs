using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneContacts
{
    public class Contacts
    {
        private ObservableCollection<Contact> contacts = new ObservableCollection<Contact>();

        public Contacts()
        {
            using (StreamReader sr = File.OpenText($@"{Directory.GetCurrentDirectory()}\contacts.txt"))
            {
                while (!sr.EndOfStream)
                {
                    contacts.Add(new Contact() {Name = sr.ReadLine(), Number = sr.ReadLine()});
                }
            }
        }
        public ObservableCollection<Contact> Get() => contacts;
    }

    public class Contact
    {
        public string Name { get; set; }
        public string Number { get; set; }
    }
}

