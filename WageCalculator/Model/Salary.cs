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
    public class Salary : INotifyPropertyChanged
    {
        private decimal _baseSalary;
        public decimal BaseSalary
        {
            get { return _baseSalary; }
            set
            {
                PropertyChanged.ChangeAndNotify(ref _baseSalary, value, () => BaseSalary);
                // _baseSalary = value;
            }
        }


        //  Possible interface
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

        private decimal _weeklyWages;
        public decimal WeeklyWages
        {
            get { return _weeklyWages; }
            set
            {
                value = _baseSalary / 52;
                PropertyChanged.ChangeAndNotify(ref _weeklyWages, value, () => WeeklyWages);
                // _weeklyWages = value;
            }
        }

        private decimal _hourlyRate;
        public decimal HourlyRate
        {
            get { return _hourlyRate; }
            set
            {
                value = _weeklyWages / 40;
                PropertyChanged.ChangeAndNotify(ref _hourlyRate, value, () => HourlyRate);
                // _hourlyRate = value;
            }
        }

        public Salary()
        {

        }




        public event PropertyChangedEventHandler PropertyChanged = null;

        // public event PropertyChangingEventHandler PropertyChanging;
    }
}
