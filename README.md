# Autoregister

Autoregister dependencies (usually services and repositories) on Microsoft.Extensions.DependencyInjection services container.

## Usage

### Package installation:

Either download the package from NuGet.org or clone and compile this repository and add the "using" reference to your Startup.cs file. 
AutoregisterServices can then be called as an extension of IServiceCollection:

```csharp
services.AutoregisterServices<TInterface,TPointer>(ServiceLifetime);
```

### Params:


- TInterface: Interface used to "mark" any services that implement it to be picked up for autoregistration.

- TPointer: Class used to get the assembly containing the services.

- ServiceLifetime: Transient / Scoped / Singleton (keep in mind that this param applies to all the services marked with TInterface)

(Both classes should be implemented by the user)

(see screenshot below for more details)


### Example:


![](https://raw.githubusercontent.com/jmolla31/Autoregister/master/readmeShot.png)


The "interface hierarchy" goes: SampleRepository implements ISampleRepository, as with any normal project, then ISampleRepository implements IRepositoryPointer.

RepositoryPointer is used only as a method to get the services assembly.


*Files contents:*

```csharp
public class SampleRepository : ISampleRepository 
{
    //Repository logic
}
```

```csharp
public interface ISampleRepository : IRepositoryPointer 
{
    //Interface methods
}
```

```csharp
public class RepositoryPointer 
{
    //EMPTY
}
```

```csharp
public interface IRepositoryPointer 
{
    //EMPTY
}
```





