using Project.Common.Constants;
using Project.Core.AWS;
using Project.Core.Caching;
using Project.Core.RabbitMQ;
using Project.ProfileService.Consumer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMassTransitWithRabbitMQ((config, context) =>
{
    config.AddReceiveEndpoint<DeleteProfileConsumer>(ExchangeConstants.IdentityService, context);
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAWSS3Bucket(builder.Configuration);
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
