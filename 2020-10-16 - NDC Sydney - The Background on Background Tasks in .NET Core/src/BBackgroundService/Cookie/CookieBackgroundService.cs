using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared;

namespace BBackgroundService.Cookie
{
    public class CookieBackgroundService : BackgroundService
    {
        private readonly ILogger<CookieBackgroundService> _logger;
        private readonly ICacheService _cacheService;

        public CookieBackgroundService(ILogger<CookieBackgroundService> logger, ICacheService cacheService)
        {
            _logger = logger;
            _cacheService = cacheService;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Job starts
            _logger.LogInformation("Starting {jobName}", nameof(CookieBackgroundService));

            // Continue until the app shuts down
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await _cacheService.RefreshCookieCacheAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Job {jobName} threw an exception", nameof(CookieBackgroundService));
                }

                await Task.Delay(5000);
            }
            
            _logger.LogInformation("Stopping {jobName}", nameof(CookieBackgroundService));
            _cacheService.RemoveCookieCache();
        }
    }
}