using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Project.Data.Model;
using Project.Data.Repository.MongoDB;

namespace Project.Data.Extensions
{
    public static class MongoDBExtensions
    {
        public static IServiceCollection AddMongoDB(this IServiceCollection services, string connectionURI, string databaseName)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

            services.AddSingleton(serviceProvider =>
            {
                var mongoClient = new MongoClient(connectionURI);
                return mongoClient.GetDatabase(databaseName);
            });

            return services;
        }

        public static IServiceCollection AddMongoDBRepository<T>(this IServiceCollection services, string collectionName)
            where T : MongoDBEntity
        {
            services.AddSingleton<IMongoDBRepository<T>>(serviceProvider =>
            {
                var database = serviceProvider.GetService<IMongoDatabase>();
                return new MongoDBRepository<T>(database, collectionName);
            });

            return services;
        }
    }
}

