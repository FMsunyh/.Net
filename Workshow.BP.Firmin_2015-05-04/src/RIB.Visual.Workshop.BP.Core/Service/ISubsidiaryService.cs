using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RIB.Visual.Workshop.BP.Core.Models;


namespace RIB.Visual.Workshop.BP.Core.Service
{
    /// <summary>
    ///     interface ISubsidiaryService
    /// </summary>
    public interface ISubsidiaryService
    {
        /// <summary>
        ///     GetSubsidiary
        /// </summary>
        /// <param name="bpId"></param>
        /// <returns></returns>
        IEnumerable<Subsidiary> GetSubsidiary(int bpId);
       
        /// <summary>
        ///     Add
        /// </summary>
        /// <param name="subsidiary"></param>
        /// <returns></returns>
        int Add(Subsidiary subsidiary);

        /// <summary>
        ///     Delete
        /// </summary>
        /// <param name="subsidiaryId"></param>
        /// <returns></returns>
        int Delete(int subsidiaryId);

        /// <summary>
        ///     Update
        /// </summary>
        /// <param name="subsidiary"></param>
        /// <returns></returns>
        int Update(Subsidiary subsidiary);
    }
}
