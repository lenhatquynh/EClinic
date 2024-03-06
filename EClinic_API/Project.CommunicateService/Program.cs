using Microsoft.EntityFrameworkCore;
using Project.CommunicateService.Data.Configurations;
using Project.CommunicateService.Hubs;
using Project.CommunicateService.Repository.ChatMessageRepositories;
using Project.CommunicateService.Repository.RoomRepositories;
using Project.CommunicateService.Repository.RoomTypeRepositories;
using Project.CommunicateService.Service;
using Project.Core.Authentication;
using Project.Core.AWS;
using Project.Core.Cors;
using Project.Core.Mapper;
using Project.Core.MediatR;
using Project.Core.Swagger;
using Project.Core.Versioning;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("EClinicDBConnection"))
);
builder.Services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();
builder.Services.AddGrpc();
builder.Services.AddMyVersioning();
var CorsName = "Eclinic";
builder.Services.AddMyCors(CorsName);
builder.Services.AddMyAuthentication(builder.Configuration.GetJWTOptions());
builder.Services.AddMyMediatR();
builder.Services.AddAWSS3Bucket(builder.Configuration);
builder.Services.AddMyMapper();
builder.Services.AddMySwagger();
builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMySwagger();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(CorsName);
app.UseEndpoints(endpoints => {
    endpoints.MapHub<MessageHub>("/message");
    endpoints.MapHub<CallHub>("/call");
    endpoints.MapHub<MessageNotificationHub>("/notification");
    endpoints.MapGrpcService<CommunicationDataService>();
});
app.MapControllers();
app.Run();
