using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RIB.Visual.Workshop.BP.Core.Models
{
    /// <summary>
    ///     BusinessPartner
    /// </summary>
    public class BusinessPartner:AddressBase
    {
        /// <summary>
        ///     Id
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        ///     Name
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///     Title
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        ///     CompanyName
        /// </summary>
        public virtual string CompanyName { get; set; }

        /// <summary>
        ///     CompanyCode
        /// </summary>
        public virtual string CompanyCode { get; set; }

        /// <summary>
        ///     Email
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        ///     BusinessPartner
        /// </summary>
        public BusinessPartner()
            : base()
        { }
    }
}
