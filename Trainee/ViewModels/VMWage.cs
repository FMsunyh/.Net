using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainee.Models;

namespace Trainee.ViewModels
{
    /// <summary>
    ///     VMWage
    /// </summary>
    public class VMWage : GalaSoft.MvvmLight.ViewModelBase
    {
        #region Fields

        private readonly Staff _staff;

        #endregion

        #region Constructors

        /// <summary>
        ///     VMWage
        /// </summary>
        /// <param name="staff"></param>
        public VMWage(Staff staff)
        {
            _staff = staff;
            if (_staff.Wage == null)
                _staff.Wage = new Wage();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Salary
        /// </summary>
        public double Salary
        {
            get
            {
                return _staff.Wage.Salary;
            }
            set
            {
                if (Math.Abs(_staff.Wage.Salary - value) < 0.0001)
                {
                    _staff.Wage.Salary = value;
                    RaisePropertyChanged(() => Salary);
                }
            }
        }

        /// <summary>
        ///     Bonus
        /// </summary>
        public double Bonus
        {
            get
            {
                return _staff.Wage.Bonus;
            }
            set
            {
                if (Math.Abs(_staff.Wage.Bonus - value) < 0.0001)
                {
                    _staff.Wage.Bonus = value;
                    RaisePropertyChanged(() => Bonus);
                }
            }
        }

        /// <summary>
        ///     Subsidy
        /// </summary>
        public double Subsidy
        {
            get
            {
                return _staff.Wage.Subsidy;
            }
            set
            {
                if (Math.Abs(_staff.Wage.Subsidy - value) < 0.0001)
                {
                    _staff.Wage.Subsidy = value;
                    RaisePropertyChanged(() => Subsidy);
                }
            }
        }

        /// <summary>
        ///     Tax
        /// </summary>
        public double TaxRate
        {
            get
            {
                return _staff.Wage.Tax;
            }
            set
            {
                if (Math.Abs(_staff.Wage.Tax - value) < 0.0001)
                {
                    _staff.Wage.Tax = value / 100;
                    RaisePropertyChanged(() => TaxRate);
                }
            }
        }

        /// <summary>
        ///     Fund
        /// </summary>
        public double FundRate
        {
            get
            {
                return _staff.Wage.Fund;
            }
            set
            {
                if (Math.Abs(_staff.Wage.Fund - value) < 0.0001)
                {
                    _staff.Wage.Fund = value / 100;
                    RaisePropertyChanged(() => FundRate);
                }
            }
        }

        /// <summary>
        ///     Insurance
        /// </summary>
        public double InsuranceRate
        {
            get
            {
                return _staff.Wage.Insurance;
            }
            set
            {
                if (Math.Abs(_staff.Wage.Insurance - value) < 0.0001)
                {
                    _staff.Wage.Insurance = value / 100;
                    RaisePropertyChanged(() => InsuranceRate);
                }
            }
        }

        /// <summary>
        ///     Wage
        /// </summary>
        public double Wage
        {
            get
            {
                return _staff.Wage.GetWage();
            }
        }

        /// <summary>
        ///     Tax
        /// </summary>
        public double Tax
        {
            get
            {
                return  _staff.Wage.GetTax();
            }
        }

        /// <summary>
        ///     Fund
        /// </summary>
        public double Fund
        {
            get { return _staff.Wage.GetFund(); }
        }

        /// <summary>
        ///     Insurance
        /// </summary>
        public double Insurance
        {
            get { return _staff.Wage.GetInsurance(); }
        }

        /// <summary>
        ///     Deserved
        /// </summary>
        public double Deserved
        {
            get { return _staff.Wage.GetDeserved(); }
        }

        /// <summary>
        ///     Cost
        /// </summary>
        public double Cost
        {
            get { return _staff.Wage.GetCost(); }
        }

        #endregion
    }
}
