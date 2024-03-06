using Microsoft.EntityFrameworkCore;
using Project.Common.Constants;
using Project.Core.Authentication;
using Project.Core.AWS;
using Project.Core.Cors;
using Project.Core.Filters;
using Project.Core.Mapper;
using Project.Core.MediatR;
using Project.Core.RabbitMQ;
using Project.Core.Swagger;
using Project.Core.Versioning;
using Project.ProfileService.Consumer;
using Project.ProfileService.Data.Configurations;
using Project.ProfileService.Events;
using Project.ProfileService.Repository.DoctorProfileRepository;
using Project.ProfileService.Repository.EmployeeProfileRepository;
using Project.ProfileService.Repository.HealthProfileRepository;
using Project.ProfileService.Repository.ProfileRepository;
using Project.ProfileService.Repository.RelationshipRepository;
using Project.ProfileService.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("EClinicDBConnection"))
);
builder.Services.AddMassTransitWithRabbitMQ((config, context) =>
{
    config.AddReceiveEndpoint<DeleteProfileResultConsumer>(ExchangeConstants.ProfileService + "Delete", context);
    config.AddReceiveEndpoint<UpdateProfileResultConsumer>(ExchangeConstants.ProfileService, context);
});
builder.Services.AddMyVersioning();
var CorsName = "Eclinic";
builder.Services.AddMyCors(CorsName);
//builder.Services.AddRedisCache(builder.Configuration);
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IHealthProfileRepository, HealthProfileRepository>();
builder.Services.AddScoped<IDoctorProfileRepository, DoctorProfileRepository>();
builder.Services.AddScoped<IEmployeeProfilesRepository, EmployeeProfilesRepository>();
builder.Services.AddScoped<IRelationshipRepository, RelationshipRepository>();
builder.Services.AddScoped(typeof(NotFoundIdFilter<IProfileRepository, Project.ProfileService.Data.Profile>));
builder.Services.AddMyAuthentication(builder.Configuration.GetJWTOptions());
builder.Services.AddControllers();
builder.Services.AddMyMediatR();
builder.Services.AddAWSS3Bucket(builder.Configuration);
builder.Services.AddMyMapper();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMySwagger();
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
app.UseCors(CorsName);
app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<ProfileDataService>();
});
app.MapControllers();

app.Run();
