using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class SupportCase
    {
        public SupportCase()
        {

        }

        public SupportCase(string description, string title, string category)
        {
            Status = "Active";
            Description = description;
            Title = title;
            Category = category;
            Time = DateTime.Now;
        }
        public SupportCase(int caseNumber, int customerNumber, string status, string description, string title, string category, DateTime time)
        {
            CaseNumber = caseNumber;
            CustomerNumber = customerNumber;
            Status = status;
            Description = description;
            Title = title;
            Category = category;
            Time = time;
        }

        public int CaseNumber { get; set; }
        public int CustomerNumber { get; private set; }
        public string Status { get; private set; }
        public string Description { get; private set; }
        public string Title { get; private set; }
        public string Category { get; private set; }
        public DateTime Time { get; private set; }

        public string Summary => $"{CaseNumber}, {Title}, {Category}, {Time:d}";

    }
    //public class SupportCaseViewModel
    //{
    //    public ObservableCollection<SupportCase> Case { get; } = new ObservableCollection<SupportCase>();

    //    public SupportCaseViewModel()
    //    {
    //        Case.Add(new SupportCase("Ongoing", "Description text", "Title", "Category1", new DateTime(1996, 3, 12)));
    //        Case.Add(new SupportCase("Ongoing", "Description text", "Title", "Category1", new DateTime(1996, 3, 12)));
    //    }
    //}
}
