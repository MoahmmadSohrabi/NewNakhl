using NakhleNakhoda.Domain.Auditable;

namespace NakhleNakhoda.Domain.Catalog
{
    public class BookRoomCategory : BaseEntity, IAuditable
    {
        public long RoomCategoryId { get; set; }
        public virtual RoomCategory? RoomCategory { get; set; }
        public long RoomId { get; set; }
        public virtual Room? Room { get; set; }
        public long UserBookId { get; set; }
        public virtual UserBook? UserBook { get; set; }
        public int RoomPrice { get; set; }
        public DateTime BookDate { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
    }
}
