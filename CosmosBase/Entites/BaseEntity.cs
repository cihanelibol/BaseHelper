namespace CosmosBase.Entites
{
    public class BaseEntity
    {
        public Guid Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }
        public Guid CreatedBy { get; protected set; }
        public Guid? UpdatedBy { get; protected set; }
        public bool IsDeleted { get; protected set; }

        public void SetCreatedAt(DateTime createdAt)
        {
            CreatedAt = createdAt;
        }

        public void SetUpdatedAt(DateTime updatedAt)
        {
            UpdatedAt = updatedAt;
        }

        public void SetCreatedBy(Guid createdBy)
        {
            CreatedBy = createdBy;
        }

        public void SetUpdatedBy(Guid updatedBy)
        {
            UpdatedBy = updatedBy;
        }

        public void SetIsDeleted(bool isDeleted)
        {
            IsDeleted = isDeleted;
        }
    }
}
