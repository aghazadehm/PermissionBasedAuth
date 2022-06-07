using InsuranceApi.Data;
using InsuranceApi.Helpers;
using Microsoft.Extensions.Options;

internal class PermissionsHost : IHostedService
{
    private readonly IPermissionsRepository _permissiionsRepository;
    private readonly bool _CollectPermissions;
    private readonly bool _overwrite;

    public PermissionsHost(IPermissionsRepository permissiionsRepository, IOptions<PermissionConfig> options)
    {
        _permissiionsRepository = permissiionsRepository;
        _CollectPermissions = options.Value.CollectPermissions;
        _overwrite = options.Value.Overwrite;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var permissions = ControllerHelper.ReflectControllersAndActions();
        await _permissiionsRepository.AddRange(permissions, _CollectPermissions, _overwrite);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}