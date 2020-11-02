using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SharedLibrary.Views
{
    public sealed partial class ClosedCasesView : Page
    {
        public CustomerCaseListViewModel ViewModel { get; set; }
        public ClosedCasesView()
        {
            this.InitializeComponent();

            ViewModel = new CustomerCaseListViewModel("Closed", "");
        }
    }
}
