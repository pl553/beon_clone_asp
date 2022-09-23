public static class ServiceCollectionExtensions
{
    public static void AddFactory<TService, TImplementation>(this IServiceCollection services) 
        where TService : class
        where TImplementation : class, TService
    {
        services.AddScoped<Func<TService>>(provider => () => provider.GetRequiredService<TService>());
    }
}