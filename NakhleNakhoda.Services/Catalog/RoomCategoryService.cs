using Microsoft.EntityFrameworkCore;
using NakhleNakhoda.Data.Context;
using NakhleNakhoda.Domain.Catalog;
using NakhleNakhoda.Migrations;
using NakhleNakhoda.ViewModels.Catalog;

namespace NakhleNakhoda.Services.Catalog
{
    public interface IRoomCategoryService : IRepository<RoomCategory>
    {
        Task<List<RoomCategoryModel>> GetAllIncludeRooms();
    }
    public class RoomCategoryService : Repository<RoomCategory>, IRoomCategoryService
    {
        public RoomCategoryService(IUnitOfWork uow) : base(uow)
        {
        }

        public async Task<List<RoomCategoryModel>> GetAllIncludeRooms()
        {
            return await Table
                .Include(x => x.Rooms)
                .Select(x => new RoomCategoryModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Qty = 0,
                    Rooms = x.Rooms.Select(r => new RoomModel()
                    {
                        Id = r.Id,
                        Name = r.Name,
                    }).ToList(),
                })
                .ToListAsync();
        }

        public async Task InitRoomCategory()
        {

            if (!(await GetAllByEnumerableAsync()).Any())
            {
                await InsertAsync(new RoomCategory()
                {
                    Name = "اتاق یک خوابه دو نفره",
                    Price = 1000000,
                    CreatedOnUtc = DateTime.UtcNow,
                    UpdatedOnUtc = DateTime.UtcNow,
                });

                await InsertAsync(new RoomCategory()
                {
                    Name = "اتاق دو خوابه دو نفره",
                    Price = 1200000,
                    CreatedOnUtc = DateTime.UtcNow,
                    UpdatedOnUtc = DateTime.UtcNow,
                });
                await InsertAsync(new RoomCategory()
                {
                    Name = "اتاق دو خوابه چهار نفره",
                    Price = 1500000,
                    CreatedOnUtc = DateTime.UtcNow,
                    UpdatedOnUtc = DateTime.UtcNow,
                });
                await InsertAsync(new RoomCategory()
                {
                    Name = "اتاق VIP شش نفره",
                    Price = 2000000,
                    CreatedOnUtc = DateTime.UtcNow,
                    UpdatedOnUtc = DateTime.UtcNow,
                });
                await SaveAsync();
            }
        }
    }
}
