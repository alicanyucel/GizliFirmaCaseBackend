using Gizli.Domain.Abstractions;

namespace Gizli.Domain.Entities
{
    public sealed class AlarmLog : Entity
    {
        public double Temperature { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
