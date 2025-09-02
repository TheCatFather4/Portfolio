using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Data
{
    public class IdentityCafeContext : IdentityDbContext
    {
        public IdentityCafeContext(DbContextOptions<IdentityCafeContext> options) 
            : base(options) 
        {
        }
    }
}
