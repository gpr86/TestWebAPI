using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace TestWebAPI.Model
{
    public abstract class Entity
    {
        [JsonPropertyName("_id")]
        public virtual string Id { get; protected set; }
    }
}
