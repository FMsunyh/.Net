using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Trainee.Annotations;

namespace Trainee.Models
{
    /// <summary>
    ///     DepartmentId
    /// </summary>
    public enum DepartmentId
    {
        /// <summary>
        ///     Management
        /// </summary>
        Management,

        /// <summary>
        ///     HR
        /// </summary>
        HR,

        /// <summary>
        ///     Marked
        /// </summary>
        Market,

        /// <summary>
        ///     Consult
        /// </summary>
        Consult,

        /// <summary>
        ///     Develop
        /// </summary>
        Development,

        /// <summary>
        ///     Finance
        /// </summary>
        Finance,

        /// <summary>
        ///     Security
        /// </summary>
        Security,

        /// <summary>
        ///     Misc
        /// </summary>
        Misc
    }

    /// <summary>
    ///     Department
    /// </summary>
    public class Department : ModelBase, IEquatable<Department>
    {
        /// <summary>
        ///     Id
        /// </summary>
        public override int Id { get; set; }

        /// <summary>
        ///     Description
        /// </summary>
        public override string Description { get; set; }

        /// <summary>
        ///     Equals
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Department other)
        {
            return other != null &&
                   other.Id == Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as Department);
        }

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Id;
        }
    }

    /// <summary>
    ///     ManagementDepartment
    /// </summary>
    public class ManagementDepartment : Department
    {
        /// <summary>
        ///     Id
        /// </summary>
        public override int Id
        {
            get { return (int) DepartmentId.Management; }
            set { throw new NotSupportedException(); }
        }

        /// <summary>
        ///     Description
        /// </summary>
        public override string Description
        {
            get { return "Management"; }
            set { throw new NotSupportedException(); }
        }
    }

    /// <summary>
    ///     HRDepartment
    /// </summary>
    public class HRDepartment : Department
    {
        /// <summary>
        ///     Id
        /// </summary>
        public override int Id
        {
            get { return (int) DepartmentId.HR; }
            set { throw new NotSupportedException(); }
        }

        /// <summary>
        ///     Description
        /// </summary>
        public override string Description
        {
            get { return "HR"; }
            set { throw new NotSupportedException(); }
        }
    }

    /// <summary>
    ///     MarketDepartment
    /// </summary>
    public class MarketDepartment : Department
    {
        /// <summary>
        ///     Id
        /// </summary>
        public override int Id
        {
            get { return (int) DepartmentId.Market; }
            set { throw new NotSupportedException(); }
        }

        /// <summary>
        ///     Description
        /// </summary>
        public override string Description
        {
            get { return "Market"; }
            set { throw new NotSupportedException(); }
        }
    }

    /// <summary>
    ///     ConsultDepartment
    /// </summary>
    public class ConsultDepartment : Department
    {
        /// <summary>
        ///     Id
        /// </summary>
        public override int Id
        {
            get { return (int) DepartmentId.Consult; }
            set { throw new NotSupportedException(); }
        }

        /// <summary>
        ///     Description
        /// </summary>
        public override string Description
        {
            get { return "Consult"; }
            set { throw new NotSupportedException(); }
        }
    }

    /// <summary>
    ///     DevelopDepartment
    /// </summary>
    public class DevelopmentDepartment : Department
    {
        /// <summary>
        ///     Id
        /// </summary>
        public override int Id
        {
            get { return (int) DepartmentId.Development; }
            set { throw new NotSupportedException(); }
        }

        /// <summary>
        ///     Description
        /// </summary>
        public override string Description
        {
            get { return "Development"; }
            set { throw new NotSupportedException(); }
        }
    }

    /// <summary>
    ///     FinanceDepartment
    /// </summary>
    public class FinanceDepartment : Department
    {
        /// <summary>
        ///     Id
        /// </summary>
        public override int Id
        {
            get { return (int) DepartmentId.Finance; }
            set { throw new NotSupportedException(); }
        }

        /// <summary>
        ///     Description
        /// </summary>
        public override string Description
        {
            get { return "Finance"; }
            set { throw new NotSupportedException(); }
        }
    }

    /// <summary>
    ///     SecurityDepartment
    /// </summary>
    public class SecurityDepartment : Department
    {
        /// <summary>
        ///     Id
        /// </summary>
        public override int Id
        {
            get { return (int) DepartmentId.Security; }
            set { throw new NotSupportedException(); }
        }

        /// <summary>
        ///     Description
        /// </summary>
        public override string Description
        {
            get { return "Security"; }
            set { throw new NotSupportedException(); }
        }
    }

    /// <summary>
    ///     MiscDepartment
    /// </summary>
    public class MiscDepartment : Department
    {
        /// <summary>
        ///     Id
        /// </summary>
        public override int Id
        {
            get { return (int) DepartmentId.Misc; }
            set { throw new NotSupportedException(); }
        }

        /// <summary>
        ///     Description
        /// </summary>
        public override string Description
        {
            get { return "Misc"; }
            set { throw new NotSupportedException(); }
        }
    }

    /// <summary>
    ///     DepartmentSelector
    /// </summary>
    public class DepartmentSelector : ModelReadOnlySelector<Department>
    {
        /// <summary>
        ///     
        /// </summary>
        public DepartmentSelector()
            : base(new List<Department>
                {
                    new ManagementDepartment(),
                    new HRDepartment(),
                    new MarketDepartment(),
                    new ConsultDepartment(),
                    new DevelopmentDepartment(),
                    new FinanceDepartment(),
                    new SecurityDepartment(),
                    new MiscDepartment()
                })
        {

        }
    }
}
