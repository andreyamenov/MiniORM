# 01.ORM-Fundamentals-Exercise-MiniORM-Skeleton-6.0 (1)



# MiniORM Core Framework

A custom, lightweight Object-Relational Mapping (ORM) framework built from scratch in C# using .NET. Designed with a clean architectural separation, isolating the core database mapping logic into a dedicated Class Library project (`MiniORM`), and utilizing an executable Console Application (`MiniORM.App`) for integration testing and staging verification.

## Key Architectural Features
- **Clean Decoupling**: Architected using a separate Class Library for core framework logic and an executable console project for implementation testing.
- **Custom DbSet Implementation**: Created a custom `DbSet<T>` generic repository by implementing the `ICollection<T>` interface to handle in-memory database entities.
- **State Auditing & Change Tracking**: Developed an advanced `ChangeTracker<T>` using Reflection to dynamically snapshot original entity states and capture runtime data mutations.
- **Automated Metadata Scanning**: Built an abstract `DbContext` base configuration layer that leverages structural Reflection to discover, map, and automatically initialize relational sets at runtime.
- **SQL Execution Simulators**: Engineered an automated transaction mechanism within `SaveChanges()` that safely intercepts memory mutations to generate corresponding database transaction operations.

## Technology Stack & OOP Concepts Used
- **Language**: C# / .NET
- **Concepts**: Generic Constraints (`where T : class, new()`), Custom Iterators (`IEnumerator`), Interface Implementation, Advanced Reflection, and Encapsulation via Explicit Access Modifiers (`internal`, `private readonly`).
