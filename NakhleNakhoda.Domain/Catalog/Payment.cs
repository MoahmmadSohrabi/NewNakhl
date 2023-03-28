using NakhleNakhoda.Domain.Auditable;

namespace NakhleNakhoda.Domain.Catalog
{
    public class Payment : BaseEntity, IAuditable
    {
        public int Price { get; set; }
        public string Key { get; set; } = "";
        public string RefId { get; set; } = "";
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
    }
}
