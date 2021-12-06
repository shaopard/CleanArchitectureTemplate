using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Entities
{
    public class Order : AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid UserIId { get; set; }
        public int OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }
        public bool OrderPaid { get; set; }
    }
}
