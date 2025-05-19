namespace Domain.Entities
{
    public abstract class AuditableEntity
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public DateTime DateCreated { get; init; } = DateTime.Now;
        public DateTime? DateModified { get; set; }
        public required string CreatedBy {get;set;}
        public string? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
