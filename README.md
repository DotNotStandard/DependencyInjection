# Summary
Dependency injection utilities, including automated DI registration using convention over configuration.

# Auto Discovery
The initial offering in this library is auto discovery and DI registration. If you are tired of manually 
updating code files to register types into your .NET DI container, then this might be for you.

The functonality is intentionally limited, but more on that in a moment.

# Why Another One?
Auto discovery and registration isn't new in the .NET space, even for the default DI container that comes 
with .NET Core/5/6. Why would I create another one, rather than using one of the others - for example 
Scrutor?

Well, in a word, security. However, a longer word might be specificity.

I didn't want to write my own; I'd have much preferred to use an existing one, but they didn't seem to 
offer the ability to be specific enough about what I wanted to do. It is quite important that you are 
specific about what gets registered and from where, because *bad things* might happen otherwise.

I want to be quite specific about what gets registered into my DI container, and most of the other 
offerings don't seem to have the knobs that I need to register only the types I want, against the 
service types I need, without the potential for them also registering other things.

This library offers DI registration using convention over configuration by default. With this 
library I can specify that all of the types in a specific assembly whose names start with or end 
with a specific prefix get registered, usually using a service type that is an interface that 
matches the type name. They don't register things against any old interface that is implemented, 
and it's not all of the types in the assembly - or worse, all of the loaded assemblies, which could 
include plenty of code that I didn't write.

This is beneficial in several regards:
1. The number of registrations is kept to a minimum, keeping the container as performant as possible. 
Performance _might_ vary by the number of registrations - but to be fair, I haven't checked.
2. The registrations that are performed are based on naming conventions; code that doesn't meet those 
naming conventions is not registered, which helps encourage a consistency in naming within the system.
3. The risk of types unintentionally getting registered in a way that might affect the security or 
general behaviour of the system is minimised.

Register things against security-related interfaces of ASP.NET without my requesting it? No thanks!

# What Functionality Is Provided?
The main functionality is exposed via the `DiscoverTypes()` and `DiscoveryTypesIn()` extension methods
on the IServiceCollection type. These are the starting point of the configuration, discovery and 
registration functionality. Everything else is method chained off these two methods, using a fluent 
configuration style.

The following configurations are available:

## Type Filtering
The `Where()` and `WhereNot()` methods are used to specify what types are to be included or excluded, 
respectively. Two overloads of each exist; the first takes a lambda expression (a `Func<Type, bool>`) 
in which you can specify the rule for the match. The second might be more useful for you if you are 
intending to match by name. These overloads accept any type implementing the `IFilter` interface. If 
filtering by name then you might want to make careful choices about case sensitivity, and the IFilter 
implementations that already exist in the library offer case-insensitive matching by default, if that is 
what you prefer.

`Where()` is used to specify types that are to be included. Calling it multiple times adds multiple 
include rules; a type matching any single include rule is included.

`WhereNot()` is used to specify types that are to be excluded. Calling it multiple times adds multiple 
include rules; a type matching any single exclude rule is excluded.

`ClearDefaultExclusions()` can be used in very advanced scenarios. By default, two exclusions are 
applied that will be appropriate in almost all circumstances - these assume that you don't want to 
discover/register abstract classes or interfaces as implementing types. If, in your scenario, that isn't 
true, then call this method to remove those default exclusions. Note that abstract types and interfaces 
can be used as service types, just not as implementing types. Since neither of these are instantiable, 
they don't implement much, so it is extremely unlikely that this would ever be necessary - but it's there 
in case you need it for a reason I haven't thought of.

If you want very complex type matching rules - inclusions or exclusions - then you can create your own 
implementations of the `IFilter` interface and use those instead of the simpler options.

## Service Type Discovery 
Next come a set of methods that specify how the service type is selected - this is the type against 
which your discovered types are registered. This can be their own type (so you ask for the types by 
their own name in the constructor of a class to have them injected) or another type, such as an 
interface.

The following built in service type discovery methods are supported:

`AsThemselves()` registers types as themselves, useful when you ask for them by their type
directly, rather than as implementers of another type. I tend to use this for ViewModels, where I 
have chosen that I don't need to use an interface to abstract the code from the UI. `AppViewModel`
would be an example; in the constructor of my UI I ask for a type of `AppViewModel` directly.

`AsSimilarlyNamedInterface()` tries to register a type by an interface whose name matches the type 
(but for the fact that it is prefixed with an I.) I use this for repository implementations, where 
I want to abstract out the data access for testing, and occasionally to support multiple data access 
implementations. `PersonRepository` would be registered against an interface of `IPersonRepository`
(assuming it implements it.)

`AsSimilarlyNamedInterfaceLessPrefix()` tries to register a type by an interface whose name matches 
the type (but for the fact that the implementation is prefixed with something to be ignored, and the 
interface is prefixed with an I.) I use this for fakes in unit tests; `FakePersonRepository` would 
be registered against an interface of `IPersonRepository` (assuming it implements it.)

`AsServiceType<T>()` exists if I want to specify that multiple implementing types all get registered 
against a single service type. We could perhaps use this to register all of the types that implement 
an interface named `ICalculator`, for example.

`WithCustomServiceTypeSelector()` exists to support custom selection scenarios of your own. You might 
need this in order to implement custom naming conventions that are not included out of the box. You 
use this to pass an instance of a class implementing the IServiceTypeSelector interface.

If multiple methods in this group are called then the last one wins.

## Service Lifetime
The `AsTransient()`, `AsScoped()` and `AsSingleton()` methods allow you to specify the lifetime with 
which types will be registered. Lifetime defaults to Transient and so if that's what you want you 
don't need to call anything at all, but the `AsTransient()` method exists for completeness, and in 
case you want to be specific in your code.

If multiple methods in this group are called then the last one wins.

## Exception Behaviour
You can choose what happens if a service type cannot be discovered for an implementing type.

`ThrowingExceptionIfNoServiceTypeIsDiscovered()` exists to request that an exception be thrown 
(and discovery halted) if a service type cannot be found. This is the default behaviour so you do not 
need to call this if that is your choice, but it exists for completeness and to allow you to be 
specific in your intention if that is preferred.

`IgnoringTypeIfNoServiceTypeIsDiscovered()` This option ignores any matching types for which the 
service type selector was unable to identify a matching service type - they are not registered and 
the discovery process continues.

If multiple methods in this group are called then the last one wins.

## Registration
Call the `Register()` method to complete the configuration and perform registration as requested.

If you want to do multiple types of discovery and registration - implementing multiple separate 
conventions for example, then do each separately.

# Sample Registrations
Here are a couple of common configurations I use:

```csharp
// Register all of the ViewModels from the current assembly
// This will register them with a lifetime of Transient
// This is a case-sensitive match unless we specify extra parameters
services.DiscoverTypes()
	.Where(t => t.Name.EndsWith("ViewModel"))
	.AsThemselves()
	.Register();
	
// Register all of the Repository implementations in the current assembly
// This will register them with a lifetime of Transient, and will throw an 
// exception if any of them do not implement an interface that matches their name
// This is a case-insensitive match, as the built in Filter classes apply this by default
services.DiscoverTypes()
	.Where(new NameEndsWithFilter("Repository"))
	.AsSimilarlyNamedInterface()
	.Register();
	
```