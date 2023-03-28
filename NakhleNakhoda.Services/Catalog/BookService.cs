using MD.PersianDateTime.Standard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NakhleNakhoda.Common;
using NakhleNakhoda.Domain.Catalog;
using NakhleNakhoda.Domain.Identity;
using NakhleNakhoda.Services.Pay;
using NakhleNakhoda.ViewModels.Catalog;
using NakhleNakhoda.ViewModels.Public;

namespace NakhleNakhoda.Services.Catalog
{
    public interface IBookService
    {
        Task<ResponseModel<List<string>>> AddBook(BookModel model);
        Task<UserBookModel?> GetPayment(long id);
        Task<BookModel> InitBook(BookModel? model);
        Task<string?> RequestPay(string domain, long Id);
        Task<UserBookModel?> ValidatePay(long Id);
    }
    public class BookService : IBookService
    {
        private readonly UserManager<Member> _memberManager;
        private readonly HttpContext _httpContext;
        private readonly IRoomCategoryService _roomCategoryService;
        private readonly IRoomService _roomService;
        private readonly IRoomFacilityService _roomFacilityService;
        private readonly IUserBookService _userBookService;
        private readonly IBookRoomCategoryService _bookRoomCategoryService;
        private readonly IPaymentService _paymentService;
        private readonly IPayService _payService;

        public BookService(UserManager<Member> memberManager,
            IHttpContextAccessor httpContextAccessor,
            IRoomCategoryService roomCategoryService,
            IRoomService roomService,
            IRoomFacilityService roomFacilityService,
            IUserBookService userBookService,
            IBookRoomCategoryService bookRoomCategoryService,
            IPaymentService paymentService,
            IPayService payService)
        {
            _memberManager = memberManager;
            _httpContext = httpContextAccessor.HttpContext!;
            _roomCategoryService = roomCategoryService;
            _roomService = roomService;
            _roomFacilityService = roomFacilityService;
            _userBookService = userBookService;
            _bookRoomCategoryService = bookRoomCategoryService;
            _paymentService = paymentService;
            _payService = payService;
        }

        public async Task<BookModel> InitBook(BookModel? model)
        {
            var user = _memberManager.GetUserAsync(_httpContext.User).Result!;
            var now = new PersianDateTime(DateTime.Now);
            return new BookModel()
            {
                Name = user.DisplayName,
                Email = user.Email ?? "",
                FromDate = string.IsNullOrEmpty(model?.FromDate) ? now.ToShortDateString() : model?.FromDate!,
                ToDate = string.IsNullOrEmpty(model?.ToDate) ? now.AddDays(2).ToShortDateString() : model?.ToDate!,
                RoomCategories = await _roomCategoryService.GetAllIncludeRooms(),
                RoomFacilities = await _roomFacilityService.GetAll(),
                AdultQty = model?.AdultQty ?? 1,
                BabyQty = model?.BabyQty ?? 0,
                ChildQty = model?.ChildQty ?? 0,
            };
        }

        public async Task<ResponseModel<List<string>>> AddBook(BookModel model)
        {
            ResponseModel<List<string>> response = new()
            {
                Content = new List<string>()
            };

            List<string> Messages = new();

            if (model.GuestQty == 0)
            {
                Messages.Add("تعداد مسافرین را وارد کنید");
            }
            if (model.RoomCount == 0)
            {
                Messages.Add("اتاق را انتخاب کنید");
            }

            model.FromDateUtc = PersianDateTime.Parse(model.FromDate).ToDateTime().Date;
            model.ToDateUtc = PersianDateTime.Parse(model.ToDate).ToDateTime().Date;
            var now = DateTime.Now.Date;
            var difInDays = (model.ToDateUtc - model.FromDateUtc).TotalDays;
            if (difInDays < 0)
            {
                Messages.Add("تاریخ ورود را نمی توان بعد از تاریخ خروج انتخاب کرد.");
            }

            if ((model.FromDateUtc - now).TotalDays < 0)
            {
                Messages.Add("تاریخ ورود باید بیشتر از تاریخ امروز باشد.");
            }

            //Input Errors
            if (Messages.Any())
            {
                response.IsSuccess = false;
                response.Message = "بروز خطا";
                response.Content = Messages;
                return response;
            }

            var roomCategories = await _roomCategoryService.GetAllIncludeRooms();
            var selectedCategories = model.RoomCategories.Where(x => x.Qty > 0).ToList();
            //Refill Categories
            selectedCategories.ForEach(x =>
            {
                x.Price = roomCategories.First(y => y.Id.Equals(x.Id)).Price;
                x.Rooms = roomCategories.First(y => y.Id.Equals(x.Id)).Rooms;
            });

            var price = 0;
            List<BookRoomCategory> bookRoomCategories = new();

            foreach (var x in selectedCategories)
            {
                var roomsTaken = await _bookRoomCategoryService.GetBookCountByRoom(model.FromDateUtc, model.ToDateUtc, x.Id);

                var availableRoom = x.Rooms.Where(y => !roomsTaken.Contains(y.Id)).Select(y => y.Id).ToList();

                if (availableRoom.Count < x.Qty)
                {
                    Messages.Add("امکان رزرو در این تاریخ وجود ندارد.");
                }
                else
                {
                    int d = (int)(difInDays + 1);
                    price += x.Qty * d * x.Price;
                    for (int i = 0; i < d; i++)
                    {
                        for (int j = 0; j < x.Qty; j++)
                        {
                            bookRoomCategories.Add(new BookRoomCategory()
                            {
                                BookDate = model.FromDateUtc.AddDays(i),
                                RoomCategoryId = x.Id,
                                RoomId = availableRoom[j],
                                RoomPrice = x.Price,
                            });
                        }
                    }
                }
            }
            //Date Error
            if (Messages.Any())
            {
                response.IsSuccess = false;
                response.Message = "بروز خطا";
                response.Content = Messages;
                response.StatusCode = 201;
                return response;
            }

            var user = _memberManager.GetUserAsync(_httpContext.User).Result;
            if (user != null)
            {
                var facilities = await _roomFacilityService.GetByIds(model.RoomFacilities.Where(x => x.Checked).Select(x => x.Id).ToList());
                price += facilities.Select(x => x.Price).Sum();
                var bookFacilities = facilities.Select(x => new BookFacility() { RoomFacilityId = x.Id }).ToList();

                var payment = new Payment()
                {
                    Price = price,
                };

                //TODO: Add Book
                var book = new UserBook()
                {
                    FromDate = model.FromDateUtc,
                    ToDate = model.ToDateUtc,
                    IsDeleted = false,
                    IsPaid = false,
                    MemberId = user.Id,
                    QtyAdult = model.AdultQty,
                    QtyBaby = model.BabyQty,
                    QtyChild = model.ChildQty,
                    Status = BookStatus.Submit,
                    Price = price,
                    BookRoomCategories = bookRoomCategories,
                    RoomFacilities = bookFacilities,
                    Payment = payment,
                    Name = model.Name,
                    Email = model.Email,
                };

                await _userBookService.InsertAsync(book);

                //bookRoomCategories.ForEach(x => x.UserBookId = book.Id);
                //await _bookRoomCategoryService.InsertAsync(bookRoomCategories);

                response.IsSuccess = true;
                response.Message = "ثبت با موفقیت انجام شد";
                response.Content = null;
                response.StatusCode = book.Id;
                return response;
            }

            response.IsSuccess = false;
            response.Message = "بروز خطا";
            response.Content = Messages;
            response.StatusCode = 401;
            return response;
        }

        public async Task<UserBookModel?> GetPayment(long Id)
        {
            var model = await _userBookService.GetIncludeById(Id);

            if (model == null) return null;
            if (model.IsDeleted) return null;

            var user = _memberManager.GetUserAsync(_httpContext.User).Result;

            if (user == null) return null;
            if (!model.MemberId.Equals(user.Id)) return null;

            model.BookRoomCategories = model.BookRoomCategories.GroupBy(item => item.RoomId).Select(group => group.First()).ToList();
            if (model.IsPaid)
            {
                return UserBookModel.FromCatalog(model);
            }

            if (model.PaymentId == 0)
            {
                var payment = new Payment()
                {
                    Price = model.Price,
                };
                await _paymentService.InsertAsync(payment);
                model.Payment = payment;
                await _userBookService.UpdateAsync(model);
            }

            return UserBookModel.FromCatalog(model);
        }

        public async Task<string?> RequestPay(string domain, long Id)
        {
            var user = _memberManager.GetUserAsync(_httpContext.User).Result;
            if (user == null) return null;

            var model = await _userBookService.GetPaymentById(Id);
            if (model == null) return null;
            if (model.IsDeleted) return null;
            if (model.IsPaid) return null;
            if (!model.MemberId.Equals(user.Id)) return null;

            if (model.Payment == null)
            {
                var payment = new Payment()
                {
                    Price = model.Price,
                };
                await _paymentService.InsertAsync(payment);
                model.Payment = payment;
                await _userBookService.UpdateAsync(model);
            }

            var request = await _payService.Request(domain,user.PhoneNumber!, "رزرو اتاق، بوم گردی نخل ناخدا", user.Email ?? "", model.Price, Id);

            if (request == null) return null;
            if (string.IsNullOrEmpty(request.Url)) return null;
            if (string.IsNullOrEmpty(request.Authority)) return null;

            model.Payment.Key = request.Authority;
            await _paymentService.UpdateAsync(model.Payment);
            return $"{request.Url}{request.Authority}";
        }

        public async Task<UserBookModel?> ValidatePay(long Id)
        {

            var model = await _userBookService.GetPaymentById(Id);
            if (model == null) return null;
            if (model.IsDeleted) return null;
            if (model.IsPaid) return null;
            if (model.Payment == null) return null;

            var request = await _payService.Validate(model.Payment!.Key, model.Price);

            if (request == null) return null;

            if (request.Status == 100)
            {
                model.Payment.RefId = request.RefId.ToString();
                await _paymentService.UpdateAsync(model.Payment);

                model.IsPaid = true;
                //var p = model.Payment;
                await _userBookService.UpdateAsync(model);
                //model.Payment = p;
            }
            return UserBookModel.FromCatalog(model);
        }
    }
}
