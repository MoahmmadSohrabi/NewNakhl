using NakhleNakhoda.Domain.Auditable;

namespace NakhleNakhoda.Domain.Catalog
{
    public class RoomCategory : BaseEntity, IAuditable
    {
        public string Name { get; set; } = "";
        public int Price { get; set; }
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
    }
}
