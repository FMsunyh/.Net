using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Data;
using Newtonsoft.Json;

namespace Trainee.Models
{
    /// <summary>
    ///     StaffStore
    /// </summary>
    public class StaffStore
    {
        #region Fields

        private static readonly string _filePath = string.Format(@"{0}/{1}.json", System.Environment.CurrentDirectory, "Trainee");
        private readonly StaffManager _manager;
       
        #endregion

        #region Constructors

        /// <summary>
        ///     StaffFileLoader
        /// </summary>
        static StaffStore()
        {
            if (!File.Exists(_filePath))
            {
                using (var fileSteam = new FileStream(_filePath, FileMode.Create, FileAccess.Write))
                using (var writer = new StreamWriter(fileSteam))
                {
                    var json = new StaffSerializer();
                    writer.Write(json.Serialize());
                    writer.Close();
                    fileSteam.Close();
                }
            }
        }

        /// <summary>
        ///     
        /// </summary>
        public StaffStore()
        {
            var serialzier = GetSerializer();
            _manager = serialzier == null ||
                       serialzier.Data.Length == 0
                           ? new StaffManager()
                           : new StaffManager(serialzier.Data);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     GetSerializer
        /// </summary>
        protected virtual StaffSerializer GetSerializer()
        {
            string json;
            using (var fileSteam = new FileStream(_filePath, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(fileSteam))
            {
                json = reader.ReadToEnd();
                reader.Close();
                fileSteam.Close();
            }
            return StaffSerializer.Deserialize(json);
        }

        /// <summary>
        ///     Save
        /// </summary>
        public void Save()
        {
            var serializer = _manager.CreateSerializer();
            using (var fileSteam = new FileStream(_filePath, FileMode.Create, FileAccess.Write))
            using (var writer = new StreamWriter(fileSteam))
            {
                writer.Write(serializer.Serialize());
                writer.Close();
                fileSteam.Close();
            }
        }

        /// <summary>
        ///     GetManager
        /// </summary>
        /// <returns></returns>
        public StaffManager GetManager()
        {
            return _manager;
        }

        #endregion
    }
}