namespace RandomDomainFunction
{
    using System.Threading.Tasks;

    using global::RandomDomainFunction.Models;
    using global::RandomDomainFunction.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.AspNetCore.Http;

    public static class RandomDomainFunction
    {
        [FunctionName("RandomDomain")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            try
            {
                var randomDomain = await new RandomDomainService().GetRandomDomain();

                return new OkObjectResult(ApiResponse.Create(RandomDomainResponse.Create(randomDomain)));
            }
            catch (FailedToRetrieveRandomDomainException)
            {
                return new BadRequestObjectResult(ApiResponse.CreateWithError("Failed to retrieve random domain"));
            }
        }
    }
}
