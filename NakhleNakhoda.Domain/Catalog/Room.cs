using NakhleNakhoda.Domain.Auditable;

namespace NakhleNakhoda.Domain.Catalog
{
    public class Room : BaseEntity, IAuditable
    {
        public string Name { get; set; } = "";
        public long RoomCategoryId { get; set; }
        public virtual RoomCategory? RoomCategory { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }

    }
}
