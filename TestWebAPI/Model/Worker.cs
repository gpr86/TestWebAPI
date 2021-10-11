using System;
using System.Text.Json.Serialization;

namespace TestWebAPI.Model
{
    public sealed class Worker //: Entity
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public string JobTitle { get; set; }
        public string JobDescriptor { get; set; }
        public string JobArea { get; set; }
        public string JobType { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfEmployment { get; set; }
    }
}
