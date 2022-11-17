using Newtonsoft.Json;

namespace RandomDomain.Api.ViewModels;

public class ApiResponseViewModel
{
    [JsonProperty]
    public object Data { get; private set; }

    [JsonProperty]
    public string Error { get; private set; }

    public static ApiResponseViewModel Create<T>(T data)
    {
        return new ApiResponseViewModel
        {
            Data = data
        };
    }

    public static ApiResponseViewModel CreateWithError(string error)
    {
        return new ApiResponseViewModel
        {
            Error = error
        };
    }
}
