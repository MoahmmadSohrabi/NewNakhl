using Microsoft.EntityFrameworkCore;
using NakhleNakhoda.Data.Context;
using NakhleNakhoda.Domain.Catalog;

namespace NakhleNakhoda.Services.Catalog
{
    public interface IBookRoomCategoryService : IRepository<BookRoomCategory>
    {
        Task<List<long>> GetBookCountByRoom(DateTime FromDate, DateTime ToDate, long CategoryId);
    }
    public class BookRoomCategoryService : Repository<BookRoomCategory>, IBookRoomCategoryService
    {
        public BookRoomCategoryService(IUnitOfWork uow) : base(uow)
        {
        }

        public async Task<List<long>> GetBookCountByRoom(DateTime FromDate, DateTime ToDate, long CategoryId)
        {
            return await Table
                .Where(x => (x.BookDate >= FromDate && x.BookDate <= ToDate) && x.RoomCategoryId.Equals(CategoryId))
                .GroupBy(x => x.RoomId)
                .Select(g => g.Key)
                .ToListAsync();
        }
    }
}
