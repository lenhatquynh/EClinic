using Microsoft.EntityFrameworkCore;
using Project.BookingService.Data.Configurations;
using Project.BookingService.Repository.BookingDoctorRepository;
using Project.BookingService.Repository.BookingPackageRepository;
using Project.BookingService.Repository.DoctorCalendarRepository;
using Project.BookingService.Repository.DoctorScheduleRepository;
using Project.BookingService.Service;
using Project.Core.Authentication;
using Project.Core.Mapper;
using Project.Core.MediatR;
using Project.Core.RabbitMQ;
using Project.Core.Swagger;
using Project.Core.Versioning;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(
    options => {
        options.UseSqlServer(builder.Configuration.GetConnectionString("EClinicDBConnection"));
        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
);
builder.Services.AddMassTransitWithRabbitMQ((config, context) =>
{
});
// add token jwt
builder.Services.AddMyAuthentication(builder.Configuration.GetJWTOptions());

builder.Services.AddScoped<IBookingPackageRepository, BookingPackageRepository>();
builder.Services.AddScoped<IBookingDoctorRepository, BookingDoctorRepository>();
builder.Services.AddScoped<IDoctorCalendarRepository, DoctorCalendarRepository>();
builder.Services.AddScoped<IDoctorScheduleRepository, DoctorScheduleRepository>();

// Add auto mapper
builder.Services.AddMyMapper();
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
    endpoints.MapGrpcService<BookingDataService>();
});
app.MapControllers();

app.Run();
