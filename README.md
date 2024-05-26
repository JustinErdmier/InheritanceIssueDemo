I have two branches: `main` and `ResolveWithDtoAndGuid`. The issue is that the code in the `main` branch is the what should work and is the desired solution.
However, due to the inheritance of my base entity models, there is an issue when libraries try to use reflection.

![image](https://github.com/JustinErdmier/InheritanceIssueDemo/assets/48148443/63ec5452-9e5a-45e6-be38-57997849bab7)

Despite the fact that `AggregateRoot` inherits from `Entity`, it says the value for `Id` is null. At first, this made sense because `AggregateRoot` hides the
base `Entity.Id` property with its own. However, if you modify the `AggregateRoot` constructor such that it passes the value down to the base, the null
reference error still happens.

This is bizarre to me. Since `AggregateRootId` inherits from `EntityId` and the value gets passed during construction, both `Id` properties should have a value,
or so I would think.
