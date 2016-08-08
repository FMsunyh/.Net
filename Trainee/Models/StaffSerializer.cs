using System;
using System.ComponentModel;
using System.Windows.Data;
using Newtonsoft.Json;

namespace Trainee.Models
{
    /// <summary>
    ///     StaffSerializer
    /// </summary>
    [JsonObject]
    public class StaffSerializer
    {
        #region Fields

        private Staff[] _data;

        #endregion

        #region Constructors

        /// <summary>
        ///     
        /// </summary>
        public StaffSerializer()
        {
            Data = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public StaffSerializer(Staff[] data)
        {
            Data = data;
        }

        #endregion

        #region Data Property

        /// <summary>
        ///     Data
        /// </summary>
        [JsonProperty]
        public Staff[] Data
        {
            get { return _data; }
            set { _data = value ?? new Staff[0]; }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Serialize
        /// </summary>
        /// <returns></returns>
        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        ///     Deserialize
        /// </summary>
        /// <param name="json"></param>
        public static StaffSerializer Deserialize(string json)
        {
            return string.IsNullOrWhiteSpace(json)
                       ? null
                       : JsonConvert.DeserializeObject<StaffSerializer>(json);
        }

        #endregion
    }
}