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
    ///     class EntityFrameWorkSubsidiaryService
    /// </summary>
    public class EntityFrameWorkSubsidiaryService : ISubsidiaryService
    {
        /// <summary>
        ///     _strDatabasePath
        /// </summary>
        private string _strDatabasePath = string.Empty;

        /// <summary>
        ///     EntityFrameWorkSubsidiaryService
        /// </summary>
        public EntityFrameWorkSubsidiaryService()
        {
            _strDatabasePath = "../../Database/Subsidiaries.xml";
        }

        /// <summary>
        ///     GetSubsidiary
        /// </summary>
        /// <param name="bpId"></param>
        /// <returns></returns>
        public IEnumerable<Models.Subsidiary> GetSubsidiary(int bpId)
        {
            XDocument doc = XDocument.Load(_strDatabasePath);

            return (
                from e in doc.Descendants("Subsidiary")
                where Convert.ToInt32(e.Element("BpId").Value) == bpId
                select new Subsidiary()
                {
                    Id = Convert.ToInt32(e.Attribute("Id").Value),
                    IsMain = Convert.ToBoolean(e.Element("IsMainAddress").Value),
                    Description = e.Element("Description").Value,
                    Address = e.Element("Address").Value,
                    Street = e.Element("Street").Value,
                    City = e.Element("City").Value,
                    ZipCode = e.Element("ZipCode").Value,
                    Telephone = e.Element("Telephone").Value,
                    Email = e.Element("Email").Value,
                }
                  );
        }

        /// <summary>
        ///     Add
        /// </summary>
        /// <param name="subsidiary"></param>
        /// <returns></returns>
        public int Add(Models.Subsidiary subsidiary)
        {
            return 1;
            //throw new NotImplementedException();
        }

        /// <summary>
        ///     Delete
        /// </summary>
        /// <param name="subsidiaryId"></param>
        /// <returns></returns>
        public int Delete(int subsidiaryId)
        {
            return 1;
            //throw new NotImplementedException();
        }

        /// <summary>
        ///     Update
        /// </summary>
        /// <param name="subsidiary"></param>
        /// <returns></returns>
        public int Update(Models.Subsidiary subsidiary)
        {
            return 1;
            //throw new NotImplementedException();
        }
    }
}
