using System;
using System.ComponentModel;
//using System.Net;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Documents;
//using System.Windows.Ink;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Animation;
//using System.Windows.Shapes;


namespace WageCalculator.Model
{
    public class Hourly : INotifyPropertyChanged
    {
        // THe Base Hourly Rate - no overtime
        private decimal _baseHourlyRate;
        public decimal BaseHourlyRate
        {
            get { return _baseHourlyRate; }
            set
            {
                if (_baseHourlyRate == value)
                    return;
                PropertyChanged.ChangeAndNotify(ref _baseHourlyRate, value, () => BaseHourlyRate);

            }
        }

        private decimal _weeklyPay;
        public decimal WeeklyPay
        {
            get { return _weeklyPay * _baseHourlyRate; }
            set
            {
                PropertyChanged.ChangeAndNotify(ref _weeklyPay, value, () => WeeklyPay);
            }
        }


        private readonly decimal _overTimeRate;
        public decimal OverTimeRate
        {
            get { return _overTimeRate * _baseHourlyRate; }
        }



        private decimal _annualSalary;
        public decimal AnnualSalary
        {
            get { return _annualSalary; }
            set
            {
                PropertyChanged.ChangeAndNotify(ref _annualSalary, value, () => AnnualSalary);
            }
        }


        private decimal _deductionPercentage;
        public decimal DeductionPercentage
        {
            get { return _deductionPercentage; }
            set
            {
                if (_deductionPercentage == value)
                    return;
                PropertyChanged.ChangeAndNotify(ref _deductionPercentage, value, () => DeductionPercentage);
            }
        }

        // How much Gov't or Elected deductions to remove
        private decimal _postDeductionSalary;
        public decimal PostDeductionSalary
        {
            get { return _postDeductionSalary; }
            set
            {
                PropertyChanged.ChangeAndNotify(ref _postDeductionSalary, value, () => PostDeductionSalary);
            }
        }


        public Hourly()
        {
            _overTimeRate = 1.50M;
        }

        private void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }


        public event PropertyChangedEventHandler PropertyChanged = null;
        //public event PropertyChangingEventHandler PropertyChanging;


    }
}
