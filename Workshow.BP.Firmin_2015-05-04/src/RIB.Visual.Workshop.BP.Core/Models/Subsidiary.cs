using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RIB.Visual.Workshop.BP.Core.Models
{
    /// <summary>
    ///     Subsidiary
    /// </summary>
    public class Subsidiary:AddressBase
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
        ///     IsMain
        /// </summary>
        public virtual bool IsMain { get; set; }

        /// <summary>
        ///     Description
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        ///     Email
        /// </summary>
        public virtual string Email { get; set; }
    }
}
