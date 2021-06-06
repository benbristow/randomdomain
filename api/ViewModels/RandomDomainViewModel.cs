using System;
using Newtonsoft.Json;

namespace RandomDomain.Api.ViewModels
{
    public class RandomDomainViewModel
    {
        public RandomDomainViewModel(Uri domain)
        {
            Host = domain.Host;
            Uri = domain;
        }

        [JsonProperty] public string Host { get; private set; }

        [JsonProperty] public Uri Uri { get; private set; }
    }
}