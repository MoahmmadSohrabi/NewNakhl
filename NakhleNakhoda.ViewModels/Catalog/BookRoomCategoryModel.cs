using NakhleNakhoda.Domain.Catalog;

namespace NakhleNakhoda.ViewModels.Catalog
{
    public class BookRoomCategoryModel
    {
        public static BookRoomCategoryModel FromCatalog(BookRoomCategory model)
        {
            return new BookRoomCategoryModel
            {
                BookDate = model.BookDate,
                CreatedOnUtc = model.CreatedOnUtc,
                RoomId = model.RoomId,
                RoomName = model.Room?.Name ?? "",
                RoomPrice = model.RoomPrice,
                UpdatedOnUtc = model.UpdatedOnUtc,
                RoomCategoryId = model.RoomCategoryId,
                RoomCategoryName = model.RoomCategory?.Name ?? "",
            };
        }

        public long RoomCategoryId { get; set; }
        public string RoomCategoryName { get; set; } = "";
        public long RoomId { get; set; }
        public string RoomName { get; set; } = "";
        public int RoomPrice { get; set; }
        public DateTime BookDate { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
    }
}
