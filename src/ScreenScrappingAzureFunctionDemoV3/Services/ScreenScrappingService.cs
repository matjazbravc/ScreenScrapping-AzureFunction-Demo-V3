using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ScreenScrappingAzureFunctionDemoV3.Services.Settings;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System;
using System.Net.Http.Headers;

namespace ScreenScrappingAzureFunctionDemoV3.Services
{
    /// <summary>
    ///     This represents the function that screen scrap certain web page.
    /// </summary>
    public class ScreenScrappingService : IScreenScrappingService
    {
        private readonly ILogger _logger;
        private readonly FunctionSettings _funcSettings;

        public ScreenScrappingService(ILogger logger, FunctionSettings funcSettings)
        {
            _logger = logger;
            _funcSettings = funcSettings;
        }

        /// <summary>
        ///     Invokes the function.
        /// </summary>
        /// <returns>Returns the <see cref="HttpResponseMessage" /> instance.</returns>
        public async Task<string> GetResultAsJsonAsync()
        {
            string result = string.Empty;
            try
            {
                _logger.LogInformation("Scrapping titles has started");

                // Decompress response
                var handler = new HttpClientHandler();
                if (handler.SupportsAutomaticDecompression)
                {
                    handler.AutomaticDecompression = DecompressionMethods.GZip |
                                                     DecompressionMethods.Deflate;
                }

                string html;
                using (var client = new HttpClient(handler))
                {
                    client.Timeout = TimeSpan.FromSeconds(30);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    html = await client.GetStringAsync(_funcSettings.Url).ConfigureAwait(false);
                }

                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                var postTitles = doc.DocumentNode
                    .Descendants("td")
                    .Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("title"))
                    .Select(x => x.InnerText).ToList();

                result = JsonConvert.SerializeObject(postTitles);

                _logger.LogInformation($"Scrapping {postTitles.Count} titles has ended");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return result;
        }
    }
}
