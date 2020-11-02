using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Appointments;
using Windows.UI.Text;

namespace SharedLibrary.Models
{
    public class CustomerCaseList
    {      
        public CustomerCaseList()
        {

        }
        public CustomerCaseList(Customer customer, SupportCase supportCase)
        {
            Customer = customer;
            SupportCase = supportCase;
        }

        public Customer Customer { get; set; }
        public SupportCase SupportCase { get; set; }

        public string Summary => $"Support Case: {SupportCase.CaseNumber}           {SupportCase.Title}         {SupportCase.Status}";
        
    }
   
    public class CustomerCaseListViewModel
    {
        public ObservableCollection<CustomerCaseList> Cases { get; } = new ObservableCollection<CustomerCaseList>();

        public CustomerCaseListViewModel(string status1, string status2)
        {
            var result = DataAccess.GetAll(status1, status2);
            foreach (CustomerCaseList r in result)
            {
                var customer = r.Customer;
                var supportCase = r.SupportCase;

                Cases.Add(new CustomerCaseList(customer, supportCase));

            }
        }
    }
}
