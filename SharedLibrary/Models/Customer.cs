using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class Customer
    {
        public Customer()
        {

        }
        public Customer(int ssn, string name, int phoneNumber, string email)
        {
            SSN = ssn;
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public int SSN { get; set; }
        public string Name { get; private set; }
        public int PhoneNumber { get; private set; }
        public string Email { get; private set; }

    }
}
