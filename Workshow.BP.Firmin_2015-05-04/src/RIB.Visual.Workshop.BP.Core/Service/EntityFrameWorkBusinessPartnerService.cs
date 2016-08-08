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
    ///     class EntityFrameWorkBusinessPartnerService
    /// </summary>
    public class EntityFrameWorkBusinessPartnerService : IBusinessPartnerService
    {
        /// <summary>
        ///     _strDatabasePath
        /// </summary>
        private string _strDatabasePath = string.Empty;

        /// <summary>
        ///     EntityFrameWorkBusinessPartnerService
        /// </summary>
        public EntityFrameWorkBusinessPartnerService()
        {
            _strDatabasePath = "../../Database/BusinessPartners.xml";
        }

        /// <summary>
        ///     Load
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Models.BusinessPartner> Load()
        {
            XDocument doc = XDocument.Load(_strDatabasePath);
            
            return (
                from e in doc.Descendants("BusinessPartner")
                select new BusinessPartner()
                {
                    Id = Convert.ToInt32(e.Attribute("Id").Value),
                    Name = e.Element("Name").Value,
                    Title = e.Element("Title").Value,
                    CompanyName = e.Element("CompanyName").Value,
                    CompanyCode = e.Element("CompanyCode").Value,
                    Address = e.Element("Address").Value,
                    Street = e.Element("Street").Value,
                    City = e.Element("City").Value,
                    ZipCode = e.Element("ZipCode").Value,
                    Telephone = e.Element("Telephone").Value,
                    Email = e.Element("Email").Value
                }
                  );

        }

        /// <summary>
        ///     Add
        /// </summary>
        /// <param name="bp"></param>
        /// <returns></returns>
        public int Add(Models.BusinessPartner bp)
        {
            XDocument doc = XDocument.Load(_strDatabasePath);

            XElement businessPartner = new XElement("BusinessPartner",
                     new XAttribute("Id", bp.Id),
                     new XElement("Name", bp.Name),
                     new XElement("Title", bp.Title),
                     new XElement("CompanyName", bp.CompanyName),
                     new XElement("CompanyCode", bp.CompanyCode),
                     new XElement("Address", bp.Address),
                     new XElement("Street", bp.Street),
                     new XElement("ZipCode", bp.ZipCode),
                     new XElement("City", bp.City),
                     new XElement("Telephone", bp.Telephone),
                     new XElement("Email", bp.Email)
                     );
            doc.Element("BusinessPartners").Add(businessPartner);
            doc.Save(_strDatabasePath);

            return 1;
        }

        /// <summary>
        ///     Delete
        /// </summary>
        /// <param name="bpId"></param>
        /// <returns></returns>
        public int Delete(int bpId)
        {
            return 1;
            //throw new NotImplementedException();
        }

        /// <summary>
        ///     Update
        /// </summary>
        /// <param name="bp"></param>
        /// <returns></returns>
        public int Update(Models.BusinessPartner bp)
        {
            return 1;
            //throw new NotImplementedException();
        }
    }
}
