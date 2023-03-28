using NakhleNakhoda.Domain.Catalog;

namespace NakhleNakhoda.ViewModels.Catalog
{
    public class RoomModel : BaseEntityModel
    {
        public string Name { get; set; } = "";
        public long RoomCategoryId { get; set; }
        public RoomCategoryModel? RoomCategory { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }

        public static RoomModel FromCatalog(Room model)
        {
            return new RoomModel
            {
                Id = model.Id,
                Name = model.Name,
            };
        }

        internal Room ToCatalog()
        {
            return new Room
            {
                Id = Id,
                Name = Name,
            };
        }
    }
}
