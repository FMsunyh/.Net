using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RIB.Visual.Workshop.BP.Core.Models
{
    /// <summary>
    ///     Contact
    /// </summary>
    public class Contact
    {
        /// <summary>
        ///     Id
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        ///     BpId
        /// </summary>
        public virtual int BpId { get; set; }

        /// <summary>
        ///     SubsidraryId
        /// </summary>
        public virtual int SubsidiaryId { get; set; }

        /// <summary>
        ///     Name
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///     Title
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        ///     Role
        /// </summary>
        public virtual string Role { get; set; }

        /// <summary>
        ///     CompanyName
        /// </summary>
        public virtual string CompanyName { get; set; }

        /// <summary>
        ///     CompanyCode
        /// </summary>
        public virtual string CompanyCode { get; set; }

        /// <summary>
        ///     Telephone
        /// </summary>
        public virtual string Telephone { get; set; }

        /// <summary>
        ///     Email
        /// </summary>
        public virtual string Email { get; set; }
    }
}
