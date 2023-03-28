using NakhleNakhoda.Domain.Catalog;

namespace NakhleNakhoda.ViewModels.Catalog
{
    public class RoomFacilityModel : BaseEntityModel
    {
        public static RoomFacilityModel FromCatalog(RoomFacility model)
        {
            return new RoomFacilityModel
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Qty = 0,
                Checked = false,
                Max = 1,
            };
        }

        public string Name { get; set; } = "";
        public int Price { get; set; } = 0;
        public string Description { get; set; } = "";
        public int Qty { get; set; } = 0;
        public int Max { get; set; } = 0;
        public bool Checked { get; set; } = false;
        public string ClientId => "RoomFacility" + Id.ToString();
    }
}