using RIB.Visual.Workshop.BP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RIB.Visual.Workshop.BP.Core.Service
{
    /// <summary>
    ///     class EntityFrameWorkContactService
    /// </summary>
    public class EntityFrameWorkContactService:IContactService
    {
        /// <summary>
        ///     _strDatabasePath
        /// </summary>
        private string _strDatabasePath = string.Empty;

        /// <summary>
        ///     EntityFrameWorkContactService
        /// </summary>
        public EntityFrameWorkContactService()
        {
            _strDatabasePath = "../../Database/Contacts.xml";
        }

        /// <summary>
        ///     GetContact
        /// </summary>
        /// <param name="bpId"></param>
        /// <returns></returns>
        public IEnumerable<Contact> GetContact(int bpId)
        {
            XDocument doc = XDocument.Load(_strDatabasePath);

            return (
                from e in doc.Descendants("Contact")
                where Convert.ToInt32(e.Element("BpId").Value) == bpId
                select new Contact()
                {
                    Id = Convert.ToInt32(e.Attribute("Id").Value),
                    Name = e.Element("Name").Value,
                    SubsidiaryId = Convert.ToInt32(e.Element("SubsidiaryId").Value),
                    Title = e.Element("Title").Value,
                    Role = e.Element("Role").Value,
                    CompanyName = e.Element("CompanyName").Value,
                    CompanyCode = e.Element("CompanyCode").Value,
                    Telephone = e.Element("Telephone").Value,
                    Email = e.Element("Email").Value,
                }
                  );
        }

        /// <summary>
        ///     Add
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public int Add(Contact contact)
        {
            return 1;
            //throw new NotImplementedException();
        }

        /// <summary>
        ///     Delete
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        public int Delete(int contactId)
        {
            return 1;
            //throw new NotImplementedException();
        }

        /// <summary>
        ///     Update
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public int Update(Contact contact)
        {
            return 1;
            //throw new NotImplementedException();
        }
    }
}
