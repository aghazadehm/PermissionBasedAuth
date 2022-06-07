using InsuranceApi.Helpers;
using InsuranceApi.Models.Access;
using Microsoft.EntityFrameworkCore;

namespace InsuranceApi.Data
{
    public class PermissionsRepository : IPermissionsRepository
    {
        private readonly PermissionsDbContext _permissionsDbContext;

        public PermissionsRepository(IDbContextFactory<PermissionsDbContext> contextFactory)
        {
            _permissionsDbContext = contextFactory.CreateDbContext();
        }
        public async Task AddRange(IEnumerable<ApiPermission> permissions, bool collectPermissions = false, bool overwrite = false)
        {

            if (collectPermissions)
            {
                if (overwrite)
                {
                    _permissionsDbContext.ApiPermissions.TrancateByName("ApiPermissions");
                }
                _permissionsDbContext.ApiPermissions.AddRange(permissions);
                await _permissionsDbContext.SaveChangesAsync();
            }
        }
    }
}
