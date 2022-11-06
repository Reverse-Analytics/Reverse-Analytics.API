using ReverseAnalytics.Domain.Common;

namespace ReverseAnalytics.Domain.Entities
{
    public class User : BaseAuditableEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public User()
        {
            UserName = string.Empty;
            Password = string.Empty;
        }
    }
}
