using Microsoft.EntityFrameworkCore;
using NakhleNakhoda.Data.Context;
using NakhleNakhoda.Domain.Catalog;
using NakhleNakhoda.ViewModels.Public;

namespace NakhleNakhoda.Services.Catalog
{
    public interface IRoomService : IRepository<Room>
    {
    }
    public class RoomService : Repository<Room>, IRoomService
    {
        public RoomService(IUnitOfWork uow) : base(uow)
        {
        }
    }
}
