using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using RandomDomain.Api;
using RandomDomain.Api.Services;

[assembly: FunctionsStartup(typeof(Startup))]
namespace RandomDomain.Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<IRandomDomainService, RandomDomainService>();
        }
    }
}