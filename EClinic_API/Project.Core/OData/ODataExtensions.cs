using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.NewtonsoftJson;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace Project.Core.OData
{
    public static class ODataExtensions
    {
        public static IServiceCollection AddMyOData(this IServiceCollection services)
        {
            services.AddControllers().AddOData(opt =>
                                              opt.Select().Filter().Expand().OrderBy().Count().SetMaxTop(100))
                        .AddODataNewtonsoftJson()
                        .AddNewtonsoftJson(options =>
                        {
                            options.SerializerSettings.ContractResolver =
                                new CamelCasePropertyNamesContractResolver();
                            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                        });
            return services;
        }
    }
}
