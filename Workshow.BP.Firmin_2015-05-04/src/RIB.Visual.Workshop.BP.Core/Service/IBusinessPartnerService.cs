using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RIB.Visual.Workshop.BP.Core.Models;

namespace RIB.Visual.Workshop.BP.Core.Service
{
    /// <summary>
    ///     IBusinessPartnerService
    /// </summary>
    public interface IBusinessPartnerService
    {
        /// <summary>
        ///     Load
        /// </summary>
        /// <returns></returns>
        IEnumerable<BusinessPartner> Load();

        /// <summary>
        ///     Add
        /// </summary>
        /// <param name="bp"></param>
        /// <returns></returns>
        int Add(BusinessPartner bp);

        /// <summary>
        ///     Delete
        /// </summary>
        /// <param name="bpId"></param>
        /// <returns></returns>
        int Delete(int bpId);

        /// <summary>
        ///     Update
        /// </summary>
        /// <param name="bp"></param>
        /// <returns></returns>
        int Update(BusinessPartner bp);
    }
}
