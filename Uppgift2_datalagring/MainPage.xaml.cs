using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Uppgift2_datalagring
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void btnShowForm_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(typeof(SharedLibrary.Views.FormView));
        }
        
        private void btnShowListActive_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(typeof(SharedLibrary.Views.OpenCasesView));
        }

        private void btnShowListClosed_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(typeof(SharedLibrary.Views.ClosedCasesView));
        }
    }
}