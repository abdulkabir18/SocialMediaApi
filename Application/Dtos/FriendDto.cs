using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.Dtos
{
    public class FriendDto
    {
        public Guid Id { get; set; }
        public Guid RequesterId { get; set; }
        public Guid AddresseeId { get; set; }
        public FriendStatus Status { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class AddFriendRequestModel
    {
        [Required]
        public Guid RequesterId { get; set; }
        [Required]
        public Guid AddresseeId { get; set; }
    }
}
