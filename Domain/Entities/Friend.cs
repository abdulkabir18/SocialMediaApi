using Domain.Enums;

namespace Domain.Entities
{
    public class Friend : AuditableEntity
    {
        public Guid RequesterId { get; set; }
        public MediaUser Requester { get; set; } = default!;
        public Guid AddresseeId { get; set; }
        public MediaUser Addressee { get; set; } = default!;
        public FriendStatus Status { get; set; } = FriendStatus.Pending;
    }
}