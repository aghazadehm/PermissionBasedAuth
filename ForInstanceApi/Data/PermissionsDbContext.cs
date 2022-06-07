using InsuranceApi.Models.Access;
using Microsoft.EntityFrameworkCore;

namespace InsuranceApi.Data
{
    public class PermissionsDbContext : DbContext
    {
        public PermissionsDbContext(DbContextOptions<PermissionsDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApiPermission> ApiPermissions { get; set; }
    }
}
