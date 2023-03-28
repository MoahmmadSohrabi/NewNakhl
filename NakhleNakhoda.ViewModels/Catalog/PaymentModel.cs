using NakhleNakhoda.Domain.Catalog;

namespace NakhleNakhoda.ViewModels.Catalog
{
    public class PaymentModel : BaseEntityModel
    {
        public static PaymentModel? FromCatalog(Payment? model)
        {
            if (model == null) { return null; }
            return new PaymentModel
            {
                Id = model.Id,
                Price = model.Price,
                Key = model.Key,
                RefId = model.RefId,
                CreatedOnUtc = model.CreatedOnUtc,
                UpdatedOnUtc = model.UpdatedOnUtc
            };
        }
        public int Price { get; set; }
        public string Key { get; set; } = "";
        public string RefId { get; set; } = "";
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
    }
}
