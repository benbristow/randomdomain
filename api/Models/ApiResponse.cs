namespace RandomDomainFunction.Models
{
    using Newtonsoft.Json;

    public class ApiResponse
    {
        private ApiResponse()
        {
        }

        [JsonProperty]
        public object Data { get; private set; }

        [JsonProperty]
        public string Error { get; private set; }

        public static ApiResponse Create<T>(T data)
        {
            return new ApiResponse()
            {
                Data = data
            };
        }

        public static ApiResponse CreateWithError(string error)
        {
            return new ApiResponse()
            {
                Error = error
            };
        }
    }
}