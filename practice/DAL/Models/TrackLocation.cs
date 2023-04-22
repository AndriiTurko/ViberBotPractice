using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace practice.DAL.Models
{
    public class TrackLocation : IEquatable<TrackLocation>
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public int Id { get; set; }

        [DataMember(Name = "imei", EmitDefaultValue = false)]
        public string? Imei { get; set; }

        [DataMember(Name = "latitude", EmitDefaultValue = false)]
        public decimal Latitude { get; set; }

        [DataMember(Name = "longtitude", EmitDefaultValue = false)]
        public decimal Longitude { get; set; }

        [DataMember(Name = "dateEvent", EmitDefaultValue = false)]
        public DateTime DateEvent { get; set; }

        [DataMember(Name = "dateTrack", EmitDefaultValue = false)]
        public DateTime date_track { get; set; }

        [DataMember(Name = "type", EmitDefaultValue = false)]
        public int TypeSource { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append("class TrackLocation {\n");

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
            if (obj == null || obj is not TrackLocation)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            return Equals((TrackLocation)obj);
        }

        public bool Equals(TrackLocation? other)
        {
            if (ReferenceEquals(this, other))
                return true;

            return base.Equals(other);
        }

        public static bool operator ==(TrackLocation? left, TrackLocation? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TrackLocation? left, TrackLocation? right)
        {
            return !Equals(left, right);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
