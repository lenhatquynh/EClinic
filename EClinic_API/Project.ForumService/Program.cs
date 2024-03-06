using Project.Common.Constants;
using Project.Core.Authentication;
using Project.Core.AWS;
using Project.Core.Cors;
using Project.Core.Mapper;
using Project.Core.MediatR;
using Project.Core.RabbitMQ;
using Project.Core.Swagger;
using Project.Core.Versioning;
using Project.Data.Extensions;
using Project.ForumService.Consumer;
using Project.ForumService.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMyVersioning();
var CorsName = "Eclinic";
builder.Services.AddMassTransitWithRabbitMQ((config, context) =>
{
    config.AddReceiveEndpoint<DeleteProfileConsumer>(ExchangeConstants.ForumService + "Delete", context);
    config.AddReceiveEndpoint<UpdateProfileConsumer>(ExchangeConstants.ForumService, context);
});
builder.Services.AddMyCors(CorsName);
var collectionNames = builder.Configuration.GetSection("EClinicDB:CollectionNames").Get<List<string>>();
var serviceSettings = builder.Configuration.GetSection("EClinicDB:ConnectionURI").Get<string>();
var mongoDbSettings = builder.Configuration.GetSection("EClinicDB:DatabaseName").Get<string>();
builder.Services.AddMongoDB(serviceSettings, mongoDbSettings)
    .AddMongoDBRepository<Post>(collectionNames[0])
    .AddMongoDBRepository<Answer>(collectionNames[1])
    .AddMongoDBRepository<Comment>(collectionNames[2])
    .AddMongoDBRepository<Hashtag>(collectionNames[3]);
builder.Services.AddMyAuthentication(builder.Configuration.GetJWTOptions());
builder.Services.AddControllers();
builder.Services.AddMyMediatR();
builder.Services.AddMyMapper();
builder.Services.AddAWSS3Bucket(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMySwagger();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMySwagger();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(CorsName);
app.MapControllers();

app.Run();
