using NakhleNakhoda.Data.Core.Mvc;
using NakhleNakhoda.Domain.Identity;

namespace NakhleNakhoda.Services.Security
{
    public class DynamicRoleClaimsManagerViewModel
    {
        public string[] ActionIds { set; get; } = new string[0];

        public int RoleId { set; get; }

        public Role? RoleIncludeRoleClaims { set; get; }

        public ICollection<MvcControllerViewModel> SecuredControllerActions { set; get; } = new List<MvcControllerViewModel>();
    }
}