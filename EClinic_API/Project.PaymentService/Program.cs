using Microsoft.EntityFrameworkCore;
using Project.Common.Constants;
using Project.Core.Authentication;
using Project.Core.Caching;
using Project.Core.Cors;
using Project.Core.Mapper;
using Project.Core.MediatR;
using Project.Core.RabbitMQ;
using Project.Core.Swagger;
using Project.Core.Versioning;
using Project.PaymentService.Consumer;
using Project.PaymentService.Data.Configurations;
using Project.PaymentService.MomoPayment;
using Project.PaymentService.Repository.PaymentRepositories;
using Project.PaymentService.Repository.RefundRepositories;
using Project.PaymentService.VNPayPayment;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransitWithRabbitMQ((config, context) =>
{
    config.AddReceiveEndpoint<PaymentResult>(ExchangeConstants.NotificationService, context);
    config.AddReceiveEndpoint<RefundConsumer>(ExchangeConstants.PaymentService, context);
});
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("EClinicDBConnection"))
);
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IRefundRepository, RefundRepository>();
builder.Services.AddMyVersioning();
var CorsName = "Eclinic";
builder.Services.AddMyCors(CorsName);
builder.Services.AddRedisCache(builder.Configuration);
builder.Services.AddMyAuthentication(builder.Configuration.GetJWTOptions());
builder.Services.AddControllers();
builder.Services.AddMyMediatR();
builder.Services.AddMyMapper();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMySwagger();
builder.Services.AddTransient<IMomoPayment, MomoPayment>();
builder.Services.AddTransient<IVNPayPayment, VNPayPayment>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMySwagger();
}
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(CorsName);
app.MapControllers();

app.Run();