using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RIB.Visual.Workshop.BP.Core.Models;

namespace RIB.Visual.Workshop.BP.Core.Service
{
    /// <summary>
    ///     interface IContactService
    /// </summary>
    public interface IContactService
    {
        /// <summary>
        ///     GetContact
        /// </summary>
        /// <param name="bpId"></param>
        /// <returns></returns>
        IEnumerable<Contact> GetContact(int bpId);

        /// <summary>
        ///     Add
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        int Add(Contact contact);

        /// <summary>
        ///     Delete
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        int Delete(int contactId);

        /// <summary>
        ///     Update
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        int Update(Contact contact);
    }
}
