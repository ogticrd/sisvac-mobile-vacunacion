using System;
using Akavache.Sqlite3.Internal;
using Newtonsoft.Json;

namespace SisVac.Framework.Domain
{
    public class BaseEntity
    {
        [PrimaryKey, AutoIncrement]
        public int? LocalId { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
