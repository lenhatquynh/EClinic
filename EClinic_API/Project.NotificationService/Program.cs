using Project.Common.Constants;
using Project.Core.Authentication;
using Project.Core.Caching;
using Project.Core.Cors;
using Project.Core.MediatR;
using Project.Core.RabbitMQ;
using Project.Core.Swagger;
using Project.Core.Versioning;
using Project.NotificationService.Consumer;
using Project.NotificationService.Service;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMassTransitWithRabbitMQ((config, context) =>
{
    config.AddReceiveEndpoint<SendMailConsumer>(ExchangeConstants.NotificationService, context);
    config.AddReceiveEndpoint<SendBillConsumer>(ExchangeConstants.NotificationService+"SendBill", context);
    config.AddReceiveEndpoint<SendAccountConsumer>(ExchangeConstants.NotificationService + "SendAccount", context);
});
builder.Services.AddMyVersioning();
var CorsName = "Eclinic";
builder.Services.AddMyCors(CorsName);
builder.Services.AddRedisCache(builder.Configuration);
builder.Services.AddMyAuthentication(builder.Configuration.GetJWTOptions());
builder.Services.AddControllers();
builder.Services.AddTransient<IMailService,MailService>();
builder.Services.AddMyMediatR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMySwagger();

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
app.MapControllers();

app.Run();