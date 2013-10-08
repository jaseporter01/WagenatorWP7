using System;
using System.Windows;
using Microsoft.Phone.Controls;
using System.Windows.Interactivity;
//using System.Net;
//using System.Windows.Controls;
//using System.Windows.Documents;
//using System.Windows.Ink;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Animation;
//using System.Windows.Shapes;



namespace WageCalculator.Model
{
    public class SelectAllTextBoxBehavior : Behavior<PhoneTextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.GotFocus += new RoutedEventHandler(AssociatedObject_GotFocus);
        }

        void AssociatedObject_GotFocus(object sender, RoutedEventArgs e)
        {
            ((PhoneTextBox)sender).SelectAll();
        }
    }
}
