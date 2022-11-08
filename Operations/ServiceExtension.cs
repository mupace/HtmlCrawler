using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Operations.Helpers;

namespace Operations
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddOperationServices(this IServiceCollection services)
        {
            services.TryAddScoped<IHtmlDocumentHelper, HtmlDocumentHelper>();

            return services;
        }
    }
}
