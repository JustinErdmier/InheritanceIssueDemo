# Inheritance Issue Demo

## Context

In some of my recent projects, I have modeled my domain layer following the design of that used
in [Amichai Mantinband's](https://x.com/amantinband) [Buber Dinner](https://youtube.com/playlist?list=PLzYkqgWkHPKBcDIP5gzLfASkQyTdy0t4k&si=YlTOUo812NxGpUCP)
course. To get the base models to work with EF Core, the `AggregateRoot<TId, TIdType>` class defines a new `Id` property of type `AggregateRootId<TId>` which
hides the base `Entity<TId>.Id` property which is generically typed `TId`. While this solves the issue with EF Core, it introduces a new issue related to
reflection.

When using instances derived from `AggregateRoot<TId, TIdType>` with certain libraries, such as **[Radzen Blazor Components](https://blazor.radzen.com/docs/)**
and **[Mapster](https://github.com/MapsterMapper/Mapster)**, a `NullReferenceException` is thrown because they are invoking `Entity<TId>.GetHashCode()`
which in turn invokes the `GetHashCode()` method on `Entity<TId>.Id`. However, `Entity<TId>.Id` is never set.

![image](https://github.com/JustinErdmier/InheritanceIssueDemo/assets/48148443/63ec5452-9e5a-45e6-be38-57997849bab7)

## The Issue

The primary issue is that I don't really understand why `Entity<TId>.Id` is being referenced. I understand that the override of `GetHashCode()` is
implemented in `Entity<TId>`, but the runtime should know that the underlying type of the object is `AggregateRoot<TId, TIdType>`, which hides the base `Id`
property. I don't understand why the appropriate property is not being referenced. I could also define the `GetHashCode()` and other methods in the
`AggregateRoot<TId, TIdType>` class, but that would defeat the entire purpose of inheriting from `Entity<TId>`.

I also fail to understand an issue related to one of the solutions. The solution being to modify the constructor of `AggregateRoot<TId, TIdType>` so that is
passes the value down to the base constructor. As I'll explain in the next section, I was actually able to successfully solve this issue with this solution
in this demo. However, when I tried this in one of my more complex projects, the value of the underlying `Entity<TId>.Id` property was still `null` at
runtime. I am currently trying to figure out why it's not working in my other project and attempt to reproduce in this demo.

## Solutions

### 1 | _branch_ `ResolveWithDtoAndGuid`

My first solution is to create DTOs which use the underlying type for the aggregate ids. If all I was doing was creating an API to expose publicly, this
would be fine. However, since I am using these types in the presentation layers (e.g., Blazor, MAUI, etc.), using DTOs for this reason defeats the purpose of
using strongly typed ids.

### 2 | _branch_ `PassIdToBaseCtor`

Update the constructor for `AggregateRoot<TId, TIdType>` so that it passes the value down to the base constructor as well.

```csharp
protected AggregateRoot(TId id)
    : base(id) =>
    Id = id;
```

While this does appear to resolve the issue in this demo project, I have not been so lucky with one of my more complex projects. I will update soon with
more information. To try and reproduce this solution not working, I will also be testing this with the **Mapster** library as well as EF Core. What I
suspect is happening is that, because EF Core doesn't use the constructor, it _only_ sets `AggregateRoot<TId, TIdType>.Id` when loading the entities from
the database, thus leaving `Entity<TId>.Id` set to `null`.
