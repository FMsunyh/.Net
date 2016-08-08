using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIB.Visual.Workshop.BP.Core
{
    /// <summary>
    ///     AddressBase
    /// </summary>
    public abstract class AddressBase
    {
        /// <summary>
        ///     Address
        /// </summary>
        public virtual string Address { get; set; }

        /// <summary>
        ///     City
        /// </summary>
        public virtual string City { get; set; }

        /// <summary>
        ///     Street
        /// </summary>
        public virtual string Street { get; set; }

        /// <summary>
        ///     ZipCode
        /// </summary>
        public virtual string ZipCode { get; set; }

        /// <summary>
        ///     Telephone
        /// </summary>
        public virtual string Telephone { get; set; }
    }
}
