using NakhleNakhoda.Domain.Identity;

namespace NakhleNakhoda.Services.Security
{
    public interface IJwtAuth
    {
        public string Generate(Member user, IList<string> role);
    }
}
