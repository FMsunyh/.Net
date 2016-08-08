using RIB.Visual.Workshop.BP.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIB.Visual.Workshop.BP.Libraries
{
    /// <summary>
    ///     ServiceGateway
    /// </summary>
    public class ServiceGateway
    {
        /// <summary>
        ///     _serviceGateway
        /// </summary>
        protected static readonly Lazy<ServiceGateway> _serviceGateway = new Lazy<ServiceGateway>(
            () => new ServiceGateway(),
            true
            );

        /// <summary>
        ///     BPService
        /// </summary>
        public static IBusinessPartnerService BPService
        {
            get
            {
                return _serviceGateway.Value.BusinessPartner;
            }
        }

        /// <summary>
        ///     CTService
        /// </summary>
        public static IContactService CTService
        {
            get 
            {
                return _serviceGateway.Value.Contact;
            }
        }

        /// <summary>
        ///     SBService
        /// </summary>
        public static ISubsidiaryService SBService
        {
            get
            {
                return _serviceGateway.Value.Subsidiary;
            }
        }

        /// <summary>
        ///     BusinessPartner
        /// </summary>
        public IBusinessPartnerService BusinessPartner
        {
            get;
            set;
        }

        /// <summary>
        ///     Contact
        /// </summary>
        public IContactService Contact
        {
            get;
            set;
        }

        /// <summary>
        ///     Subsidiary
        /// </summary>
        public ISubsidiaryService Subsidiary
        {
            get;
            set;
        }
    }
}
