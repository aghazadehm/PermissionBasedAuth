using InsuranceUserManagement.Models;

namespace InsuranceUserManagement.Data
{
    public class PermissionsRepository : IPermissionsRepository
    {
        private ApplicationDbContext _dbContext;

        public PermissionsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ApiPermission> GetApiPermissions()
        {
            return _dbContext.ApiPermissions.ToList();
        }
    }
}
