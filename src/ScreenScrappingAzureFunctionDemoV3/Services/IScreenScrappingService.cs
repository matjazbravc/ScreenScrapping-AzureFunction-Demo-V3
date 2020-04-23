using System.Threading.Tasks;

namespace ScreenScrappingAzureFunctionDemoV3.Services
{
    /// <summary>
    ///     This provides interfaces to the <see cref="ScreenScrappingService" /> class.
    /// </summary>
    public interface IScreenScrappingService
    {
        Task<string> GetResultAsJsonAsync();
    }
}
