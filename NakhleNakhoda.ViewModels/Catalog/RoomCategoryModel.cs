using Microsoft.AspNetCore.Mvc.Rendering;
using NakhleNakhoda.Domain.Catalog;

namespace NakhleNakhoda.ViewModels.Catalog
{
    public class RoomCategoryModel : BaseEntityModel
    {
        public string Name { get; set; } = "";
        public List<RoomModel> Rooms { get; set; } = new List<RoomModel>();
        public int Price { get; set; } = 0;
        public string ClientId => "RoomCategory" + Id.ToString();
        public int Qty { get; set; } = 0;
        public IList<SelectListItem> AvailableRoom { get; set; } = new List<SelectListItem>();

        public static RoomCategoryModel FromCatalog(RoomCategory model)
        {
            return new RoomCategoryModel
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Rooms = model.Rooms.Select(x => RoomModel.FromCatalog(x)).ToList(),
            };
        }

        public RoomCategory ToCatalog()
        {
            return new RoomCategory
            {
                Id = Id,
                Name = Name,
                Price = Price,
                Rooms = Rooms.Select(x => x.ToCatalog()).ToList(),
            };
        }
    }
}