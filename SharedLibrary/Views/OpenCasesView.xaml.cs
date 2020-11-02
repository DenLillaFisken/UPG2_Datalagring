using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Import;
using Windows.Storage;
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
    public sealed partial class OpenCasesView : Page
    {
        public CustomerCaseListViewModel ViewModel { get; set; }
           
        public OpenCasesView()
        {
           this.InitializeComponent();

            var task = Task.Run(async () => await DataAccess.ReadJson());
            
            string statusWaiting = task.Result.StatusWaiting;
            string statusActive = task.Result.StatusActive;
            string statusClosed = task.Result.StatusClosed;

            cbStatus.Items.Add(statusWaiting);
            cbStatus.Items.Add(statusActive);
            cbStatus.Items.Add(statusClosed);

            ViewModel = new CustomerCaseListViewModel(statusWaiting, statusActive);
        }

        public async void UpdateStatus_Click(object sender, RoutedEventArgs e)
        {
            Object selectedItem = cbStatus.SelectedItem;
            string status = Convert.ToString(selectedItem);

            await DataAccess.UpdateAsync(Convert.ToInt32(tbCaseNumber.Text), status);

            Frame.Navigate(Frame.CurrentSourcePageType);
        }

        public void CbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
