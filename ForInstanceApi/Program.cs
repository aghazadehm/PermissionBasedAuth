using InsuranceApi.Data;
using InsuranceApi.Identity;
using InsuranceApi.PermissionHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("PermissionConnection");
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    options.AddSecurityDefinition(name: "Bearer",
        securityScheme: new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
});
var tokenValidationOption = new TokenValidationOptions
{
    IssuerSigningKey = builder.Configuration["Jwt:IssuerSigningKey"],
    ValidAudience = builder.Configuration["Jwt:ValidAudience"],
    ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
    ValidateAudience = true,
    ValidateIssuer = true,
    ValidateIssuerSigningKey = true,
    ValidateLifetime = true
};
builder.Services.Configure<TokenValidationOptions>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddBuiltInAuthorization(tokenValidationOption);

builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();
builder.Services.AddDbContextFactory<PermissionsDbContext>(options =>
    options.UseSqlServer(connectionString), ServiceLifetime.Singleton);
builder.Services.AddSingleton<IPermissionsRepository, PermissionsRepository>();
builder.Services.AddHostedService<PermissionsHost>();
builder.Services.Configure<PermissionConfig>(builder.Configuration.GetSection(PermissionConfig.ConfigName));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
