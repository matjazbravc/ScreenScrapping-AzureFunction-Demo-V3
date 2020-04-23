using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using ScreenScrappingAzureFunctionDemoV3.Services;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System;

namespace ScreenScrappingAzureFunctionDemoV3.Functions
{
    public class ScreenScrappingFunction
    {
        private readonly ILogger _logger;
        private readonly IScreenScrappingService _screenScrappingService;

        public ScreenScrappingFunction(ILogger logger, IScreenScrappingService screenScrappingService)
        {
            _logger = logger;
            _screenScrappingService = screenScrappingService;
        }

        [FunctionName(nameof(ScreenScrappingFunction))]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req)
        {
            try
            {
                _logger.LogInformation($"Starting {nameof(ScreenScrappingFunction)}");
                var result = await _screenScrappingService.GetResultAsJsonAsync().ConfigureAwait(false);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                var exMessage = $"And error occured processing your request: {ex.Message}";
                _logger.LogError(exMessage);
                var result = new ObjectResult(exMessage)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
                return result;
            }
        }
    }
}
