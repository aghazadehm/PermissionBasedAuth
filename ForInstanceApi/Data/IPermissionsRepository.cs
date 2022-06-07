using InsuranceApi.Models.Access;

namespace InsuranceApi.Data
{
    public interface IPermissionsRepository
    {
        Task AddRange(IEnumerable<ApiPermission> permissions, bool collectPermissions = false, bool overwrite = false);
    }
}