using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneContacts
{
    [Serializable]
    public class Contacts
    {
        private ObservableCollection<Contact> contacts;
        public ObservableCollection<Contact> Get() => contacts;
    }

    public class Contact
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public string SecondNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}

