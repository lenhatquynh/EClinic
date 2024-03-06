using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Project.Core.AWS
{

    public static class AmazonS3Extensions
    {
        public static IServiceCollection AddAWSS3Bucket(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDefaultAWSOptions(configuration.GetAWSOptions());
            services.AddAWSService<IAmazonS3>();
            services.AddTransient<IAmazonS3Bucket,AmazonS3Bucket>();
            return services;
        }

    }
}
