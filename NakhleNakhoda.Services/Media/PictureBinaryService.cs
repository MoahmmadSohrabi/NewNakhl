using NakhleNakhoda.Data.Context;
using NakhleNakhoda.Domain.Media;

namespace NakhleNakhoda.Services.Media
{
    public interface IPictureBinaryService : IRepository<PictureBinary>
    {

    }

    public class PictureBinaryService : Repository<PictureBinary>, IPictureBinaryService
    {
        public PictureBinaryService(IUnitOfWork uow) : base(uow)
        {
        }
    }
}