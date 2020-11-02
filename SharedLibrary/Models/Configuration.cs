using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class Configuration
    {
        public Configuration()
        {

        }
        public Configuration(int numberOfItems, string statusWaiting, string statusActive, string statusClosed)
        {
            NumberOfItems = numberOfItems;
            StatusWaiting = statusWaiting;
            StatusActive = statusActive;
            StatusClosed = statusClosed;
        }

        public int NumberOfItems { get; set; }
        public string StatusWaiting { get; set; }
        public string StatusActive { get; set; }
        public string StatusClosed { get; set; }

    }
}
