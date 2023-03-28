using Microsoft.EntityFrameworkCore;
using NakhleNakhoda.Data.Context;
using NakhleNakhoda.Domain.Catalog;
using System.Security.Cryptography.X509Certificates;

namespace NakhleNakhoda.Services.Catalog
{
    public interface IUserBookService : IRepository<UserBook>
    {
        Task<UserBook?> GetIncludeById(long Id);
        Task<UserBook?> GetPaymentById(long Id);
    }
    public class UserBookService : Repository<UserBook>, IUserBookService
    {
        public UserBookService(IUnitOfWork uow) : base(uow)
        {
        }

        public async Task<UserBook?> GetIncludeById(long Id)
        {
            return await Table
                .Include(x => x.BookRoomCategories)
                .ThenInclude(x => x.Room)
                .Include(x => x.BookRoomCategories)
                .ThenInclude(x => x.RoomCategory)
                .Include(x => x.RoomFacilities)
                .ThenInclude(x => x.RoomFacility)
                .FirstOrDefaultAsync(x => x.Id.Equals(Id));
        }
        public async Task<UserBook?> GetPaymentById(long Id)
        {
            return await Table
                .Include(x => x.Payment)
                .FirstOrDefaultAsync(x => x.Id.Equals(Id));
        }
    }
}
