using System;
using GalaSoft.MvvmLight;
using WageCalculator.Model;
using System.ComponentModel;

namespace WageCalculator.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// </summary>
    public class HourlySalaryViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the HourlySalaryViewModel class.
        /// </summary>
        public HourlySalaryViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real": Connect to service, etc...
            ////}

            _hourly = new Hourly();  // Initialize Hourly Model to ViewModel
            _dFontSize = 22D; // Default font size
            _IsNotFullTime = false;
            _IsHoursDisplayValueEnabled = false;
            _HoursDisplayValue = 40.0M;  // hours worked
            _hoursContentValue = "Full Time";
            _IsNotFullYear = false;
            _IsWeeksDisplayValueEnabled = false;
            _WeeksDisplayValue = 52;  // 1 year
            _weeksContentValue = "Full Year";


            _salary = new Salary();  // Initialize Salary Model to ViewModel
            _IsSalNotFullTime = false;
            _IsSalHoursDisplayValueEnabled = false;
            _SalHoursDisplayValue = 40.0M;  // hours worked
            _SalHoursContentValue = "Full Time";
            _IsSalNotFullYear = false;
            _IsSalWeeksDisplayValueEnabled = false;
            _SalWeeksDisplayValue = 52;  // 1 year
            _SalWeeksContentValue = "Full Year";

            // Cori Code
            Recalculate();
            SalRecalculate();
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean own resources if needed

        ////    base.Cleanup();
        ////}



        #region Hourly

        // Hourly

        private readonly Hourly _hourly;
        public Hourly Hourly
        {
            get { return _hourly; }
        }

        private double _dFontSize;
        public double dFontSize
        {
            get { return _dFontSize; }
            set
            {
                if (_dFontSize == value)
                    return;

                _dFontSize = value;

                RaisePropertyChanged("dFontSize");
                Recalculate();
            }
        }


        private bool _IsNotFullTime;
        public bool IsNotFullTime
        {
            get { return _IsNotFullTime; }
            set
            {
                if (_IsNotFullTime == value)
                    return;
                _IsNotFullTime = value;
                RaisePropertyChanged("IsNotFullTime");
                //Recalculate();

                //Cori Code
                RaisePropertyChanged("IsFullTime");
                Recalculate();
            }
        }

        //Cori Code
        public bool IsFullTime
        {
            get { return !_IsNotFullTime; }
            set
            {
                if (!_IsNotFullTime == value)
                    return;
                _IsNotFullTime = !value;
                RaisePropertyChanged("IsNotFullTime");
                RaisePropertyChanged("IsFullTime");
                Recalculate();
            }
        }


        private string _hoursContentValue;
        public string HoursContentValue
        {
            get { return _hoursContentValue; }
            set
            {
                if (_hoursContentValue == value)
                    return;
                _hoursContentValue = value;
                RaisePropertyChanged("HoursContentValue");
                Recalculate();
            }
        }

        private bool _IsNotFullYear;
        public bool IsNotFullYear
        {
            get { return _IsNotFullYear; }
            set
            {
                if (_IsNotFullYear == value)
                    return;
                _IsNotFullYear = value;
                RaisePropertyChanged("IsNotFullYear");
                RaisePropertyChanged("IsFullYear"); // Cori Code
                Recalculate();
            }
        }

        // Cori Code
        public bool IsFullYear
        {
            get { return !IsNotFullYear; }
            set
            {
                if (!_IsNotFullYear == value)
                    return;
                _IsNotFullYear = !value;
                RaisePropertyChanged("IsNotFullYear");
                RaisePropertyChanged("IsFullYear");
                Recalculate();
            }
        }



        private string _weeksContentValue;
        public string WeeksContentValue
        {
            get { return _weeksContentValue; }
            set
            {
                if (_weeksContentValue == value)
                    return;
                _weeksContentValue = value;
                RaisePropertyChanged("WeeksContentValue");
                Recalculate();
            }
        }


        private bool _IsHoursDisplayValueEnabled;
        public bool IsHoursDisplayValueEnabled
        {
            get { return _IsHoursDisplayValueEnabled; }
            set
            {
                if (_IsHoursDisplayValueEnabled == value)
                    return;
                _IsHoursDisplayValueEnabled = value;
                RaisePropertyChanged("IsHoursDisplayValueEnabled");
                Recalculate();
            }
        }

        private bool _IsWeeksDisplayValueEnabled;
        public bool IsWeeksDisplayValueEnabled
        {
            get { return _IsWeeksDisplayValueEnabled; }
            set
            {
                if (_IsWeeksDisplayValueEnabled == value)
                    return;
                _IsWeeksDisplayValueEnabled = value;
                RaisePropertyChanged("IsWeeksDisplayValueEnabled");
                Recalculate();
            }
        }

        private decimal _HoursDisplayValue;
        public decimal HoursDisplayValue
        {
            get { return _HoursDisplayValue; }
            set
            {
                if (_HoursDisplayValue == value)
                    return;

                // Verify that the number of hours entered are in range of 0 and 40 (enhance for overtime)
                if (value < 0M)
                    value = 0M;
                if (value > 40M)
                    value = 40M;

                _HoursDisplayValue = value;
                RaisePropertyChanged("HoursDisplayValue");
                Recalculate();
            }
        }

        private int _WeeksDisplayValue;
        public int WeeksDisplayValue
        {
            get { return _WeeksDisplayValue; }
            set
            {
                if (_WeeksDisplayValue == value)
                    return;

                // Verify that the number of weeks entered are in range of 0 and 52
                if (value < 0)
                    value = 0;
                if (value > 52)
                    value = 52;

                _WeeksDisplayValue = value;
                RaisePropertyChanged("WeeksDisplayValue");
                Recalculate();
            }
        }

        public decimal BaseHourlyRate
        {
            get { return _hourly.BaseHourlyRate; }
            set
            {
                if (_hourly.BaseHourlyRate == value)
                    return;

                // Verify that the rate entered isn't less than 0 
                // Verify that the hourly rate entered isn't less than 0 nor greater than 480,769.23
                if (value < 0)
                    value = 0;

                if (value > 480769.23M)
                    value = 480769.23M;

                _hourly.BaseHourlyRate = value;

                // Reduce the FontSize if the Base Hourly Rate is larger than $9995.
                dFontSize = _hourly.BaseHourlyRate < 9995.0M ? 22D : 20.5D;

                Recalculate();
            }
        }

        public decimal DeductionPercentage
        {
            get { return _hourly.DeductionPercentage; }
            set
            {
                if (_hourly.DeductionPercentage == value)
                    return;

                // Verify that the deduction percentage is in range of 0 and 100
                if (value < 0M)
                    value = 0M;
                if (value > 100M)
                    value = 100M;
                _hourly.DeductionPercentage = value;
                RaisePropertyChanged("DeductionPercentage");
                Recalculate();
            }
        }



        public string BaseHourlyRateText
        {
            get { return String.Format("{0:C2}", BaseHourlyRate); }
        }


        public decimal BaseHourlyRateDeduction
        {
            get { return BaseHourlyRate - (BaseHourlyRate * (DeductionPercentage / 100)); }
        }

        public string BaseHourlyRateDeductionText
        {
            get { return String.Format("{0:C2}", BaseHourlyRateDeduction); }
        }


        public decimal OverTimeHourlyRate
        {
            get { return _hourly.OverTimeRate; }
        }

        public string OverTimeHourlyRateText
        {
            get { return String.Format("{0:C2}", OverTimeHourlyRate); }
        }


        public decimal OverTimeHourlyRateDeduction
        {
            get { return OverTimeHourlyRate - (OverTimeHourlyRate * (DeductionPercentage / 100)); }
        }

        public string OverTimeHourlyRateDeductionText
        {
            get { return String.Format("{0:C2}", OverTimeHourlyRateDeduction); }
        }


        public decimal WeeklyPay
        {
            get { return _hourly.BaseHourlyRate * _HoursDisplayValue; }
        }
        public string WeeklyPayText
        {
            get { return String.Format("{0:C2}", WeeklyPay); }
        }


        public decimal WeeklyPayDeduction
        {
            get { return WeeklyPay - (WeeklyPay * (DeductionPercentage / 100)); }
        }

        public string WeeklyPayDeductionText
        {
            get { return String.Format("{0:C2}", WeeklyPayDeduction); }
        }


        public decimal BiWeeklyPay
        {
            get { return _hourly.BaseHourlyRate * _HoursDisplayValue * 2; }
        }
        public string BiWeeklyPayText
        {
            get { return String.Format("{0:C2}", BiWeeklyPay); }
        }


        public decimal BiWeeklyPayDeduction
        {
            get { return BiWeeklyPay - (BiWeeklyPay * (DeductionPercentage / 100)); }
        }

        public string BiWeeklyPayDeductionText
        {
            get { return String.Format("{0:C2}", BiWeeklyPayDeduction); }
        }


        public decimal AnnualSalary
        {
            get { return _hourly.BaseHourlyRate * _HoursDisplayValue * _WeeksDisplayValue; }
        }

        public string AnnualSalaryText
        {
            get { return String.Format("{0:C2}", AnnualSalary); }
        }


        public decimal AnnualSalaryDeduction
        {
            get { return AnnualSalary - (AnnualSalary * (DeductionPercentage / 100)); }
        }

        public string AnnualSalaryDeductionText
        {
            get { return String.Format("{0:C2}", AnnualSalaryDeduction); }
        }


        private void Recalculate()  // check for changes
        {
            RecalculateHours(IsNotFullTime);
            RecalculateWeeks(IsNotFullYear);
        }

        private void RecalculateWeeks(bool fullYear)
        {
            IsWeeksDisplayValueEnabled = fullYear;

            if (!fullYear)
            {
                WeeksDisplayValue = 52;
                WeeksContentValue = "Full Year";

            }
            else
            {
                WeeksContentValue = "Custom Year";
            }

            UpdateProperties();
        }



        private void RecalculateHours(bool fullTime)
        {
            IsHoursDisplayValueEnabled = fullTime;

            if (!fullTime)
            {
                HoursDisplayValue = 40.0M;
                HoursContentValue = "Full Time";
            }
            else
            {
                HoursContentValue = "Custom Time";
            }

            UpdateProperties();
        }

        private void UpdateProperties()
        {
            RaisePropertyChanged("BaseHourlyRate");
            RaisePropertyChanged("BaseHourlyRateText");
            RaisePropertyChanged("BaseHourlyRateDeductionText");
            RaisePropertyChanged("dFontSize");

            RaisePropertyChanged("DeductionPercentage");

            RaisePropertyChanged("HoursDisplayValue");
            RaisePropertyChanged("WeeksDisplayValue");
            RaisePropertyChanged("HoursContentValue");
            RaisePropertyChanged("WeeksContentValue");

            RaisePropertyChanged("OverTimeHourlyRate");
            RaisePropertyChanged("OverTimeHourlyRateText");
            RaisePropertyChanged("OverTimeHourlyRateDeductionText");

            RaisePropertyChanged("WeeklyPay");
            RaisePropertyChanged("WeeklyPayText");
            RaisePropertyChanged("WeeklyPayDeductionText");

            RaisePropertyChanged("BiWeeklyPay");
            RaisePropertyChanged("BiWeeklyPayText");
            RaisePropertyChanged("BiWeeklyPayDeductionText");

            RaisePropertyChanged("AnnualSalary");
            RaisePropertyChanged("AnnualSalaryText");
            RaisePropertyChanged("AnnualSalaryDeductionText");
        }


        #endregion


        #region Salary

        // Salary

        private readonly Salary _salary;
        public Salary Salary
        {
            get { return _salary; }
        }

        private bool _IsSalNotFullTime;
        public bool IsSalNotFullTime
        {
            get { return _IsSalNotFullTime; }
            set
            {
                if (_IsSalNotFullTime == value)
                    return;
                _IsSalNotFullTime = value;
                RaisePropertyChanged("IsSalNotFullTime");
                RaisePropertyChanged("IsSalFullTime"); // Cori Code
                SalRecalculate();
            }
        }

        // Cori Code
        public bool IsSalFullTime
        {
            get { return !_IsSalNotFullTime; }
            set
            {
                if (!_IsSalNotFullTime == value)
                    return;
                _IsSalNotFullTime = !value;
                RaisePropertyChanged("IsSalNotFullTime");
                RaisePropertyChanged("IsSalFullTime");
                SalRecalculate();
            }
        }

        private bool _IsSalHoursDisplayValueEnabled;
        public bool IsSalHoursDisplayValueEnabled
        {
            get { return _IsSalHoursDisplayValueEnabled; }
            set
            {
                if (_IsSalHoursDisplayValueEnabled == value)
                    return;
                _IsSalHoursDisplayValueEnabled = value;
                RaisePropertyChanged("IsSalHoursDisplayValueEnabled");
                SalRecalculate();
            }
        }

        private decimal _SalHoursDisplayValue;
        public decimal SalHoursDisplayValue
        {
            get { return _SalHoursDisplayValue; }
            set
            {
                if (_SalHoursDisplayValue == value)
                    return;

                // Verify that the number of hours entered are in range of 0 and 100
                // If you are working more than 100 hours a week, you need a new job.
                if (value < 0)
                    value = 0;
                if (value > 100)
                    value = 100;

                _SalHoursDisplayValue = value;
                RaisePropertyChanged("SalHoursDisplayValue");
                SalRecalculate();
            }
        }


        private string _SalWeeksContentValue;
        public string SalWeeksContentValue
        {
            get { return _SalWeeksContentValue; }
            set
            {
                if (_SalWeeksContentValue == value)
                    return;
                _SalWeeksContentValue = value;
                RaisePropertyChanged("SalWeeksContentValue");
                SalRecalculate();
            }
        }

        private string _SalHoursContentValue;
        public string SalHoursContentValue
        {
            get { return _SalHoursContentValue; }
            set
            {
                if (_SalHoursContentValue == value)
                    return;
                _SalHoursContentValue = value;
                RaisePropertyChanged("SalHoursContentValue");
                SalRecalculate();
            }
        }



        public decimal BaseAnnualSalary
        {
            get { return _salary.BaseSalary; }
            set
            {
                if (_salary.BaseSalary == value)
                    return;

                // Verify that the salary entered isn't less than 0 nor greater than 999,999,999.99
                if (value < 0)
                    value = 0;

                if (value > 999999999.99M)
                    value = 999999999.99M;

                _salary.BaseSalary = value;
                RaisePropertyChanged("BaseAnnualSalary");
                SalRecalculate();
            }
        }

        public decimal SalDeductionPercentage
        {
            get { return _salary.DeductionPercentage; }
            set
            {
                if (_salary.DeductionPercentage == value)
                    return;

                // Verify that the deduction percentage is in range of 0 and 100
                if (value < 0)
                    value = 0;
                if (value > 100)
                    value = 100;

                _salary.DeductionPercentage = value;
                RaisePropertyChanged("SalDeductionPercentage");
                SalRecalculate();
            }
        }

        private bool _IsSalWeeksDisplayValueEnabled;
        public bool IsSalWeeksDisplayValueEnabled
        {
            get { return _IsSalWeeksDisplayValueEnabled; }
            set
            {
                if (_IsSalWeeksDisplayValueEnabled == value)
                    return;
                _IsSalWeeksDisplayValueEnabled = value;
                RaisePropertyChanged("IsSalWeeksDisplayValueEnabled");
                SalRecalculate();
            }
        }

        //private bool _IsSalNotFullYear;
        //public bool IsSalNotFullYear
        //{
        //    get { return _IsSalNotFullYear; }
        //    set
        //    {
        //        if (_IsSalNotFullYear == value)
        //            return;
        //        _IsSalNotFullYear = value;
        //        RaisePropertyChanged("IsSalNotFullYear");
        //        SalRecalculate();
        //    }
        //}

        // Cori Code
        private bool _IsSalNotFullYear;
        public bool IsSalNotFullYear
        {
            get { return _IsSalNotFullYear; }
            set
            {
                if (_IsSalNotFullYear == value)
                    return;
                _IsSalNotFullYear = value;
                RaisePropertyChanged("IsSalNotFullYear");
                RaisePropertyChanged("IsSalFullYear");
                SalRecalculate();
            }
        }
        public bool IsSalFullYear
        {
            get { return !_IsSalNotFullYear; }
            set
            {
                if (!_IsSalNotFullYear == value)
                    return;
                _IsSalNotFullYear = !value;
                RaisePropertyChanged("IsSalNotFullYear");
                RaisePropertyChanged("IsSalFullYear");
                SalRecalculate();
            }
        }

        private int _SalWeeksDisplayValue;
        public int SalWeeksDisplayValue
        {
            get { return _SalWeeksDisplayValue; }
            set
            {
                if (_SalWeeksDisplayValue == value)
                    return;

                // Verify that the number of weeks entered are in range of 0 and 52
                if (value < 0)
                    value = 0;
                if (value > 52)
                    value = 52;

                _SalWeeksDisplayValue = value;
                RaisePropertyChanged("SalWeeksDisplayValue");
                SalRecalculate();
            }
        }

        private void SalRecalculate()  // check for changes
        {
            RecalculateSalHours(IsSalNotFullTime);
            RecalculateSalWeeks(IsSalNotFullYear);
        }

        private void RecalculateSalHours(bool fullTime)
        {
            IsSalHoursDisplayValueEnabled = fullTime;

            if (!fullTime)
            {
                SalHoursDisplayValue = 40.0M;
                SalHoursContentValue = "Full Time";
            }
            else
            {
                SalHoursContentValue = "Custom Time";
            }

            UpdateSalaryProperties();
        }

        private void RecalculateSalWeeks(bool fullYear)
        {
            IsSalWeeksDisplayValueEnabled = fullYear;

            if (!fullYear)  // Custom Time is selected
            {
                SalWeeksDisplayValue = 52;
                SalWeeksContentValue = "Full Year";
            }
            else
            {
                SalWeeksContentValue = "Custom Year";
            }

            UpdateSalaryProperties();
        }

        private void UpdateSalaryProperties()
        {
            RaisePropertyChanged("BaseAnnualSalary");
            RaisePropertyChanged("BaseAnnualSalaryText");
            RaisePropertyChanged("SalDeductionPercentage");

            RaisePropertyChanged("SalHoursDisplayValue");
            RaisePropertyChanged("SalHoursContentValue");
            RaisePropertyChanged("SalWeeksDisplayValue");
            RaisePropertyChanged("SalWeeksContentValue");

            RaisePropertyChanged("SalaryPayDeduction");
            RaisePropertyChanged("SalaryPayDeductionText");

            RaisePropertyChanged("SalBiWeeklyPay");
            RaisePropertyChanged("SalBiWeeklyPayText");
            RaisePropertyChanged("SalBiWeeklyPayDeductionText");

            RaisePropertyChanged("SalaryWeeklyPay");
            RaisePropertyChanged("SalaryWeeklyPayText");
            RaisePropertyChanged("SalaryWeeklyPayDeductionText");

            RaisePropertyChanged("BaseHourlyPay");
            RaisePropertyChanged("BaseHourlyPayText");
            RaisePropertyChanged("BaseHourlyPayDeductionText");
        }

        public string BaseAnnualSalaryText
        {
            get { return String.Format("{0:C2}", BaseAnnualSalary); }
        }

        public decimal SalaryPayDeduction
        {
            get { return _salary.BaseSalary - (_salary.BaseSalary * (SalDeductionPercentage / 100)); }
        }

        public string SalaryPayDeductionText
        {
            get { return String.Format("{0:C2}", SalaryPayDeduction); }
        }

        public decimal SalBiWeeklyPay
        {
            get { return (_salary.BaseSalary / _SalWeeksDisplayValue) * 2; }
        }
        public string SalBiWeeklyPayText
        {
            get { return String.Format("{0:C2}", SalBiWeeklyPay); }
        }


        public decimal SalBiWeeklyPayDeduction
        {
            get { return SalBiWeeklyPay - (SalBiWeeklyPay * (SalDeductionPercentage / 100)); }
        }

        public string SalBiWeeklyPayDeductionText
        {
            get { return String.Format("{0:C2}", SalBiWeeklyPayDeduction); }
        }

        public decimal SalaryWeeklyPay
        {
            get { return _salary.BaseSalary / _SalWeeksDisplayValue; }

        }

        public string SalaryWeeklyPayText
        {
            get { return String.Format("{0:C2}", SalaryWeeklyPay); }
        }

        public decimal SalaryWeeklyPayDeduction
        {
            get { return SalaryWeeklyPay - (SalaryWeeklyPay * (SalDeductionPercentage / 100)); }
        }

        public string SalaryWeeklyPayDeductionText
        {
            get { return String.Format("{0:C2}", SalaryWeeklyPayDeduction); }
        }

        public decimal BaseHourlyPay
        {
            get { return _salary.BaseSalary / _SalWeeksDisplayValue / _SalHoursDisplayValue; }
        }

        public string BaseHourlyPayText
        {
            get { return String.Format("{0:C2}", BaseHourlyPay); }
        }

        public decimal BaseHourlyPayDeduction
        {
            get { return BaseHourlyPay - (BaseHourlyPay * (SalDeductionPercentage / 100)); }
        }

        public string BaseHourlyPayDeductionText
        {
            get { return String.Format("{0:C2}", BaseHourlyPayDeduction); }
        }
    }
        #endregion
}