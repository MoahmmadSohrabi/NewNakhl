using NakhleNakhoda.Common;
using NakhleNakhoda.Domain.Auditable;
using NakhleNakhoda.Domain.Identity;

namespace NakhleNakhoda.Domain.Catalog
{
    public class UserBook : BaseEntity, IAuditable
    {
        public long MemberId { get; set; }
        public virtual Member? Member { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int QtyAdult { get; set; }
        public int QtyChild { get; set; }
        public int QtyBaby { get; set; }
        public int Price { get; set; }
        public bool IsPaid { get; set; }
        public bool IsDeleted { get; set; }
        public BookStatus Status { get; set; }
        public long PaymentId { get; set; }
        public virtual Payment? Payment { get; set; }
        public List<BookRoomCategory> BookRoomCategories { get; set; } = new List<BookRoomCategory>();
        public List<BookFacility> RoomFacilities { get; set; } = new List<BookFacility>();
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }

    }
}
