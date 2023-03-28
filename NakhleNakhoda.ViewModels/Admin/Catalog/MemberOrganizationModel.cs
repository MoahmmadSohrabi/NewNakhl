using NakhleNakhoda.ViewModels.Admin.Identity;

namespace NakhleNakhoda.ViewModels.Admin.Catalog
{
    public class MemberOrganizationModel : BaseEntityModel
    {
        public long OrganizationId { get; set; }
        public OrganizationModel Organization { get; set; }
        public long UserId { get; set; }
        public MemberModel User { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
    }
}
