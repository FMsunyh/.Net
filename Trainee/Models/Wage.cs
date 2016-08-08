using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trainee.Models
{
    /// <summary>
    ///     Wage
    /// </summary>
    public class Wage
    {
        /// <summary>
        ///     Salary
        /// </summary>
        public double Salary { get; set; }

        /// <summary>
        ///     Bonus
        /// </summary>
        public double Bonus { get; set; }

        /// <summary>
        ///     Subsidy
        /// </summary>
        public double Subsidy { get; set; }

        /// <summary>
        ///     Insurance 
        /// </summary>
        public double Insurance { get; set; }

        /// <summary>
        ///     Tax
        /// </summary>
        public double Tax { get; set; }

        /// <summary>
        ///     Fund
        /// </summary>
        public double Fund { get; set; }

        /// <summary>
        ///     GetWage
        /// </summary>
        /// <returns></returns>
        public double GetWage()
        {
            return Salary + Bonus + Subsidy;
        }

        /// <summary>
        ///     GetTax
        /// </summary>
        /// <returns></returns>
        public double GetTax()
        {
            return (Salary + Bonus)*Tax;
        }

        /// <summary>
        ///     GetFund
        /// </summary>
        /// <returns></returns>
        public double GetFund()
        {
            return (Salary + Bonus)*Fund;
        }

        /// <summary>
        ///     GetFund
        /// </summary>
        /// <returns></returns>
        public double GetInsurance()
        {
            return (Salary + Bonus)*Insurance;
        }

        /// <summary>
        ///     GetCost
        /// </summary>
        /// <returns></returns>
        public double GetCost()
        {
            return GetWage() + GetFund() + GetInsurance();
        }

        /// <summary>
        ///     GetDeserved
        /// </summary>
        /// <returns></returns>
        public double GetDeserved()
        {
            return GetWage() - GetTax() - GetFund() - GetInsurance();
        }
    }
}
