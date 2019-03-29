using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Autoregister
{
    public static class Autoregister
    {
        public static void AutoregisterServices<TInterface, TPointer>(this IServiceCollection serviceColletion, ServiceLifetime serviceLifetime)
            where TPointer : class
            where TInterface : class
        {
            var pointerType = typeof(TPointer);
            var interfacePointerType = typeof(TInterface);

            var servicesAssembly = pointerType.Assembly;

            var services = servicesAssembly.ExportedTypes.Where(t => interfacePointerType.IsAssignableFrom(t)  && !t.IsAbstract);

            foreach (var service in services)
            {
                var @interface = service.GetInterfaces().FirstOrDefault(i => interfacePointerType.IsAssignableFrom(i) && i != interfacePointerType);

                if (@interface != null) serviceColletion.Add(new ServiceDescriptor(@interface, service, serviceLifetime));
            }
        }
    }
}
