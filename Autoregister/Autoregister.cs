using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Autoregister
{
    public static class Autoregister
    {
        public static void AutoregisterScoped<TInterface, TPointer>(this IServiceCollection serviceColletion)
            where TPointer : class
            where TInterface : class
        {
            var pointerType = typeof(TPointer);
            var interfacePointerType = typeof(TInterface);

            var repoAssembly = pointerType.Assembly;

            var repoList = repoAssembly.ExportedTypes.Where(t => interfacePointerType.IsAssignableFrom(t));

            foreach (var repo in repoList)
            {
                var @interface = repo.GetInterfaces().FirstOrDefault(i => interfacePointerType.IsAssignableFrom(i) && i != interfacePointerType);

                if (@interface != null) serviceColletion.AddScoped(@interface, repo);
            }
        }

        public static void AutoregisterTransient<TInterface, TPointer>(this IServiceCollection serviceColletion)
            where TPointer : class
            where TInterface : class
        {
            var pointerType = typeof(TPointer);
            var interfacePointerType = typeof(TInterface);

            var repoAssembly = pointerType.Assembly;

            var repoList = repoAssembly.ExportedTypes.Where(t => interfacePointerType.IsAssignableFrom(t));

            foreach (var repo in repoList)
            {
                var @interface = repo.GetInterfaces().FirstOrDefault(i => interfacePointerType.IsAssignableFrom(i) && i != interfacePointerType);

                if (@interface != null) serviceColletion.AddTransient(@interface, repo);
            }
        }

        public static void AutoregisterSingleton<TInterface, TPointer>(this IServiceCollection serviceColletion)
            where TPointer : class
            where TInterface : class
        {
            var pointerType = typeof(TPointer);
            var interfacePointerType = typeof(TInterface);

            var repoAssembly = pointerType.Assembly;

            var repoList = repoAssembly.ExportedTypes.Where(t => interfacePointerType.IsAssignableFrom(t));

            foreach (var repo in repoList)
            {
                var @interface = repo.GetInterfaces().FirstOrDefault(i => interfacePointerType.IsAssignableFrom(i) && i != interfacePointerType);

                if (@interface != null) serviceColletion.AddSingleton(@interface, repo);
            }
        }
    }
}
