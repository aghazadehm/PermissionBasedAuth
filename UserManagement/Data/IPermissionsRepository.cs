using InsuranceUserManagement.Models;

namespace InsuranceUserManagement.Data
{
    public interface IPermissionsRepository
    {
        List<ApiPermission> GetApiPermissions();
    }
}