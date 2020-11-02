using SharedLibrary.Models;
using System;
using System.Collections.Generic;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SharedLibrary.Views
{
    public sealed partial class FormView : Page
    {
        public FormView()
        {
            this.InitializeComponent();
        }

        private async void btnAddCase_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = new Customer(Convert.ToInt32(tbSSN.Text), tbName.Text, Convert.ToInt32(tbPhoneNumber.Text), tbEmail.Text);
            SupportCase supportCase = new SupportCase(tbDescription.Text, tbTitle.Text, tbCategory.Text);  

            await SharedLibrary.DataAccess.AddAsync(customer, supportCase);

            tbSSN.Text = String.Empty;
            tbName.Text = String.Empty;
            tbPhoneNumber.Text = String.Empty;
            tbEmail.Text = String.Empty;
            tbDescription.Text = String.Empty;
            tbTitle.Text = String.Empty;
            tbCategory.Text = String.Empty;

        }

    }
}


