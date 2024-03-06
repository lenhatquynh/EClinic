using Microsoft.EntityFrameworkCore;
using Project.Core.Authentication;
using Project.Core.AWS;
using Project.Core.Mapper;
using Project.Core.MediatR;
using Project.Core.Swagger;
using Project.Core.Versioning;
using Project.ServiceInformationService.Data.Configurations;
using Project.ServiceInformationService.Repository.ServicePackageItemRepository;
using Project.ServiceInformationService.Repository.ServicePackageRepository;
using Project.ServiceInformationService.Repository.ServiceRepository;
using Project.ServiceInformationService.Repository.SpecializationRepository;
using Project.ServiceInformationService.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(
    options => {
        options.UseSqlServer(builder.Configuration.GetConnectionString("EClinicDBConnection"));
        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
);
// add token jwt
builder.Services.AddMyAuthentication(builder.Configuration.GetJWTOptions());

builder.Services.AddScoped<ISpecializationRepository, SpecializationRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IServicePackageRepository, ServicePackageRepository>();
builder.Services.AddScoped<IServicePackageItemRepository, ServicePackageItemRepository>();
// Add auto mapper
builder.Services.AddMyMapper();
builder.Services.AddAWSS3Bucket(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddMyMediatR();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMySwagger();
builder.Services.AddMyVersioning();
builder.Services.AddGrpc();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMySwagger();
}
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<DataService>();
});
app.MapControllers();

app.Run();
