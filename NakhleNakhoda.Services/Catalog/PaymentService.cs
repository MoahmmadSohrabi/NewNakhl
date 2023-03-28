using NakhleNakhoda.Data.Context;
using NakhleNakhoda.Domain.Catalog;

namespace NakhleNakhoda.Services.Catalog
{
    public interface IPaymentService : IRepository<Payment>
    {
    }
    public class PaymentService : Repository<Payment>, IPaymentService
    {
        public PaymentService(IUnitOfWork uow) : base(uow)
        {
        }
    }
}
