using InsuranceApi.Models.Access;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace InsuranceApi.Helpers
{
    public static class ControllerHelper
    {
        public static List<ApiPermission> ReflectControllersAndActions()
        {
            var controllers = Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(type => typeof(ControllerBase).IsAssignableFrom(type))
                    .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                    .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                    .GroupBy(x => x.DeclaringType?.Name)
                    .Select(x=>x);                
                
            var permissions = from controller in controllers
                       from action in controller
                       select new ApiPermission
                       {
                           Id = new Guid(),
                           ApiName = "InsuranceApi",
                           Controller = controller.Key,
                           Action = action.Name
                       };

            return permissions.ToList();
        }
    }
}
