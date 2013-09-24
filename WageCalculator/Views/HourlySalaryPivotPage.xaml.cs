using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Diagnostics;
using Microsoft.Phone.Tasks;
using WageCalculator.ViewModel;

namespace WageCalculator.Views
{
    public partial class HourlySalaryPivotPage : PhoneApplicationPage
    {
        public HourlySalaryPivotPage()
        {
            InitializeComponent();
        }


        private void ContactHyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EmailComposeTask emailComposeTask = new EmailComposeTask();

                emailComposeTask.To = "jaseporter@gmail.com";

                emailComposeTask.Body = string.Empty;

                emailComposeTask.Subject = "The Wagenator (ver 1.5.0.2) - Support and Feedback";

                emailComposeTask.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
        }

        private void SMSContactHyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SmsComposeTask smsComposeTask = new SmsComposeTask();

                smsComposeTask.To = "jaseporter@gmail.com";
                smsComposeTask.Body = "RE: The Wagenator v1.5.0.2 - ";

                smsComposeTask.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
        }

        private void MarketplaceReviewButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();

                marketplaceReviewTask.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
        }

        private void MarketplaceSearchHyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MarketplaceSearchTask marketplaceSearchTask = new MarketplaceSearchTask();

                marketplaceSearchTask.SearchTerms = "J3nius";

                marketplaceSearchTask.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
        }

        private void PrivacyPolicyHyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Neither personal nor location services data is stored or shared. To disable the usage of location services, " +
                "turn off location services in your phone's settings.", "Privacy policy", MessageBoxButton.OK);
        }

    }
}