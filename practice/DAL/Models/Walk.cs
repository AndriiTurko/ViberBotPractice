using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace practice.DAL.Models
{
    public class Walk : IEquatable<Walk>
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public int Id { get; set; }

        [DataMember(Name = "imei", EmitDefaultValue = false)]
        public string? Imei { get; set; }

        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string? Name { get; set; }

        [DataMember(Name = "duration", EmitDefaultValue = false)]
        public DateTime Duration { get; set; }

        [DataMember(Name = "distance", EmitDefaultValue = false)]
        public decimal Distance { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append("class Walk {\n");

            sb.Append($"  Id: {Id}");

            sb.Append("}\n");

            return sb.ToString();
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj is not Walk)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            return Equals((Walk)obj);
        }

        public bool Equals(Walk? other)
        {
            if (ReferenceEquals(this, other))
                return true;

            return base.Equals(other);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
