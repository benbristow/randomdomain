using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using RandomDomain.Api.Exceptions;
using RandomDomain.Api.Services;
using RandomDomain.Api.ViewModels;

namespace RandomDomain.Api.Functions;

public class RandomDomain
{
    private readonly IRandomDomainService _randomDomainService;

    public RandomDomain(IRandomDomainService randomDomainService)
    {
        _randomDomainService = randomDomainService;
    }

    [FunctionName(nameof(RandomDomain))]
    public async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
    {
        try
        {
            var randomDomain = await _randomDomainService.GetRandomDomain();

            return new OkObjectResult(ApiResponseViewModel.Create(
                new RandomDomainViewModel(randomDomain)));
        }
        catch (FailedToRetrieveRandomDomainException)
        {
            return new BadRequestObjectResult(
                ApiResponseViewModel.CreateWithError("Failed to retrieve random domain"));
        }
    }
}