using hangfire_jobs_service.Dependency;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace hangfire_jobs_tests.Dependency
{
    public static class GetServiceInjection
    {
        public static T GetService<T>()
        {
            var serviceCollection = new ServiceCollection();

            IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile(".\\appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            serviceCollection.AddSingleton(configuration);

            ServiceContainer.RegisterServiceContainer(serviceCollection, configuration); 

            var serviceProvider = serviceCollection.BuildServiceProvider();

            T? getService = serviceProvider.GetRequiredService<T>();

            return getService;
        }
    }
}
