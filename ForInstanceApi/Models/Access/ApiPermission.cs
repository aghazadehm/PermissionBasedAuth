using Microsoft.EntityFrameworkCore;

namespace InsuranceApi.Models.Access
{
    public class ApiPermission
    {
        public Guid Id { get; set; }
        public string ApiName { get; set; } = default!;
        public string Controller { get; set; } = default!;
        public string Action { get; set; } = default!;
    }
}