using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceApi.Models.Access
{
    [Table("ApiPermissions", Schema = "Identity")]

    public class ApiPermission
    {
        public Guid Id { get; set; }
        public string ApiName { get; set; } = default!;
        public string Controller { get; set; } = default!;
        public string Action { get; set; } = default!;
    }
}