using MD.PersianDateTime.Standard;
using NakhleNakhoda.Common;
using NakhleNakhoda.Common.Extensions;
using NakhleNakhoda.Domain.Catalog;

namespace NakhleNakhoda.ViewModels.Catalog
{
    public class UserBookModel : BaseEntityModel
    {
        public static UserBookModel FromCatalog(UserBook model)
        {
            return new UserBookModel
            {
                Id = model.Id,
                Price = model.Price,
                FromDate = new PersianDateTime(model.FromDate),
                ToDate = new PersianDateTime(model.ToDate),
                IsPaid = model.IsPaid,
                Payment = PaymentModel.FromCatalog(model.Payment),
                Status = model.Status,
                StatusText = model.Status.GetDisplayName(),
                RoomFacilities = model.RoomFacilities.Select(x => RoomFacilityModel.FromCatalog(x.RoomFacility!)).ToList(),
                BookRoomCategories = model.BookRoomCategories.Select(x => BookRoomCategoryModel.FromCatalog(x)).ToList(),
                QtyChild = model.QtyChild,
                QtyBaby = model.QtyBaby,
                QtyAdult = model.QtyAdult,
                CreatedOnUtc = model.CreatedOnUtc,
                UpdatedOnUtc = model.UpdatedOnUtc,
            };
        }
        public PersianDateTime FromDate { get; set; }
        public PersianDateTime ToDate { get; set; }
        public int QtyAdult { get; set; }
        public int QtyChild { get; set; }
        public int QtyBaby { get; set; }
        public int Guests => QtyAdult + QtyBaby + QtyChild;
        public int Price { get; set; }
        public bool IsPaid { get; set; }
        public BookStatus Status { get; set; }
        public string StatusText { get; set; } = "";
        public PaymentModel? Payment { get; set; }
        public List<BookRoomCategoryModel> BookRoomCategories { get; set; } = new List<BookRoomCategoryModel>();
        public List<RoomFacilityModel> RoomFacilities { get; set; } = new List<RoomFacilityModel>();
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
    }
}