using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuranGuide.Maui.Core.Services;
using QuranGuide.Maui.Infrastructure.ExternalServices;
using QuranGuide.Maui.Infrastructure.Repositories;

namespace QuranGuide.Maui.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddQuranGuideInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();

            services.AddHttpClient<IQuranService, QuranService>();
            services.AddHttpClient<IAudioService, AudioService>();
            services.AddHttpClient<IHadithService, HadithService>();
            services.AddHttpClient<ISearchService, QuranSearchService>();
            services.AddHttpClient<IWebScrapingService, WebScrapingService>();
            services.AddHttpClient<IScriptScrapingService, ScriptScrapingService>();
            services.AddScoped<IScrapeIngestionService, ScrapeIngestionService>();

            services.AddScoped<ISurahRepository, SurahRepository>();

            return services;
        }
    }
}


