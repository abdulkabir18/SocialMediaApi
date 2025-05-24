using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.Dtos
{
    public record FriendDto
    {
        public Guid Id { get; set; }
        public Guid RequesterId { get; set; }
        public Guid AddresseeId { get; set; }
        public FriendStatus Status { get; set; }
        public DateTime DateCreated { get; set; }
    }

    public record AddFriendRequestModel
    {
        [Required]
        public required Guid RequesterId { get; set; }
        [Required]
        public required Guid AddresseeId { get; set; }
    }

    public record CancelFriendRequestModel
    {
        [Required]
        public required Guid FriendId { get; set; }

    }
}
