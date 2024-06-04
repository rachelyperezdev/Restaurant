using Restaurant.Core.Domain.Common;

namespace Restaurant.Core.Domain.Entities
{
    public class Table : AuditableBaseEntity
    {
        public int SeatingCapacity { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public ICollection<Order> Orders { get; set; } // creo que es nullable
    }
}
