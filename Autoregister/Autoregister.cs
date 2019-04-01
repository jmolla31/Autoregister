using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Autoregister
{
    public static class Autoregister
    {
        public static void AutoregisterServices<TInterface, TPointer>(this IServiceCollection serviceColletion, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
            where TPointer : class
            where TInterface : class
        => AutoregisterCore(serviceColletion, typeof(TPointer), typeof(TInterface), serviceLifetime);


        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void AutoregisterServices(this IServiceCollection serviceColletion, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            var assembly = Assembly.GetCallingAssembly();

            var dualPointerType = assembly.ExportedTypes.First(typ => typ.Name.Equals("IAutoPointer"));

            AutoregisterCore(serviceColletion, dualPointerType, dualPointerType, serviceLifetime);
        }

        private static void AutoregisterCore(IServiceCollection serviceColletion, Type pointer, Type iPointer, ServiceLifetime lifetime)
        {
            var servicesAssembly = pointer.Assembly;

            var services = servicesAssembly.ExportedTypes.Where(t => iPointer.IsAssignableFrom(t) && !t.IsAbstract);

            foreach (var service in services)
            {
                var @interface = service.GetInterfaces().FirstOrDefault(i => iPointer.IsAssignableFrom(i) && i != iPointer);

                if (@interface != null) serviceColletion.Add(new ServiceDescriptor(@interface, service, lifetime));
            }
        }
    }
}
