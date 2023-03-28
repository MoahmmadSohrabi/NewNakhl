using Microsoft.EntityFrameworkCore;
using NakhleNakhoda.Data.Context;
using NakhleNakhoda.Domain.Catalog;
using NakhleNakhoda.ViewModels.Catalog;

namespace NakhleNakhoda.Services.Catalog
{
    public interface IRoomFacilityService : IRepository<RoomFacility>
    {
        Task<List<RoomFacilityModel>> GetAll();
        Task<List<RoomFacility>> GetByIds(List<long> Ids);
    }
    public class RoomFacilityService : Repository<RoomFacility>, IRoomFacilityService
    {
        public RoomFacilityService(IUnitOfWork uow) : base(uow)
        {
        }

        public async Task<List<RoomFacilityModel>> GetAll()
        {
            return await Table
                .Select(x => new RoomFacilityModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Description = x.Description
                })
                .ToListAsync();
        }
        public async Task<List<RoomFacility>> GetByIds(List<long> Ids)
        {
            return await Table
                .Where(x => Ids.Contains(x.Id))
                //.Select(x => new RoomFacilityModel()
                //{
                //    Id = x.Id,
                //    Name = x.Name,
                //    Price = x.Price,
                //    Description = x.Description
                //})
                .ToListAsync();
        }
        //model.RoomFacilities.Where(x => x.Checked).Select(x => new BookFacility() { Id = x.Id }).ToList()
    }
}
