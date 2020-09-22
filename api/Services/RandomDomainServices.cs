namespace RandomDomainFunction.Services
{
    using System;
    using System.Collections.Immutable;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Reflection;
    using System.Threading.Tasks;

    using global::RandomDomainFunction.Models;
    using Polly;

    public class RandomDomainService
    {
        private readonly ImmutableList<string> _words;

        private readonly ImmutableList<string> _extensions;

        private readonly HttpClient _httpClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(1) };

        private const int BatchSize = 5;

        private const int BatchesToTry = 10;

        public RandomDomainService()
        {
            _words = LoadWordListFromEmbeddedFile("words.txt");
            _extensions = LoadWordListFromEmbeddedFile("extensions.txt");
        }

        public async Task<Uri> GetRandomDomain()
        {
            return await
                Policy
                    .Handle<FailedToRetrieveRandomDomainException>()
                    .RetryAsync(BatchesToTry)
                    .ExecuteAsync(TryGetRandomDomainFromParallelBatch);
        }

        private async Task<Uri> TryGetRandomDomainFromParallelBatch()
        {
            var tasks =
                Enumerable.Range(1, BatchSize)
                    .Select(_ => TryGetRandomAliveDomain())
                    .AsParallel();

            var results = await Task.WhenAll(tasks);

            return results.Where(r => r != null).SingleRandomOrDefault() ?? throw new FailedToRetrieveRandomDomainException();
        }

        private async Task<Uri> TryGetRandomAliveDomain()
        {
            var uri = new Uri($"http://{_words.SingleRandomOrDefault()}.{_extensions.SingleRandomOrDefault()}");

            if (!await TestUriAlive(uri))
            {
                return null;
            }

            return uri;

            async Task<bool> TestUriAlive(Uri testUri)
            {
                try
                {
                    var checkingResponse = await _httpClient.GetAsync(testUri);

                    checkingResponse.EnsureSuccessStatusCode();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        private static ImmutableList<string> LoadWordListFromEmbeddedFile(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var wordListResourceName = assembly.GetManifestResourceNames().Single(r => r.EndsWith(fileName));

            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(wordListResourceName);
            using var reader = new StreamReader(stream!);

            return reader
                .ReadToEnd()
                .Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .ToImmutableList();
        }
    }
}