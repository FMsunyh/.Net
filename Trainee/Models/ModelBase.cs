using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Trainee.Models
{
    /// <summary>
    ///     ModelBase
    /// </summary>
    [JsonObject]
    public abstract class ModelBase
    {
        /// <summary>
        ///     Id
        /// </summary>
        [JsonProperty("id")]
        public abstract int Id { get; set; }

        /// <summary>
        ///     Description
        /// </summary>
        [JsonProperty("desc")]
        public abstract string Description { get; set; }
    }
}
