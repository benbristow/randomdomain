namespace RandomDomainFunction.Models
{
    using System;

    using Newtonsoft.Json;

    public class RandomDomainResponse
    {
        private RandomDomainResponse()
        {
        }

        [JsonProperty]
        public string Host { get; private set; }

        [JsonProperty]
        public Uri Uri { get; private set; }

        public static RandomDomainResponse Create(Uri domain)
        {
            return new RandomDomainResponse()
            {
                Host = domain.Host,
                Uri = domain
            };
        }
    }
}
