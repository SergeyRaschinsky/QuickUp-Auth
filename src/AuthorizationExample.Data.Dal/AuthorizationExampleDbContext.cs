using AuthorizationExample.Data.Dal.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationExample.Data.Dal
{
    public class AuthorizationExampleDbContext : IdentityDbContext<AspNetUser>
    {
        public AuthorizationExampleDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
