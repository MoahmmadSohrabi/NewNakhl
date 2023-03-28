namespace NakhleNakhoda.Domain.Catalog
{
    public class BookFacility : BaseEntity
    {
        public long UserBookId { get; set; }
        public UserBook? UserBook { get; set; }
        public long RoomFacilityId { get; set; }
        public RoomFacility? RoomFacility { get; set; }
    }
}
