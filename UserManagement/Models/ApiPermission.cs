namespace InsuranceUserManagement.Models
{
    public class ApiPermission
    {
        public Guid Id { get; set; }
        public string ApiName { get; set; } = default!;
        public string Controller { get; set; } = default!;
        public string Action { get; set; } = default!;
        public bool Selected { get; internal set; }

        public override string ToString()
        {
            return $"{ApiName}.{Controller}.{Action}";
        }
    }
}
