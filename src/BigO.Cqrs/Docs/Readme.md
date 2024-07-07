
# BigO.Cqrs

[![NuGet version](https://badge.fury.io/nu/BigO.Cqrs.svg)](https://badge.fury.io/nu/BigO.Cqrs)

BigO.Cqrs provides a comprehensive framework for implementing the Command Query Responsibility Segregation (CQRS) pattern in .NET applications. This package simplifies the separation of read and write operations, enhancing the scalability and maintainability of your projects.

## Features

- **Commands and Queries**: Define and handle commands and queries with ease.
- **Handlers**: Implement command and query handlers following a clear and structured approach.
- **Buses\Mediators**: Use pub/sub utilities to dispatch commands and queries, promoting decoupling.
- **Pipeline Behaviors**: Add custom behaviors to the processing pipeline.

## Installation

Install via NuGet Package Manager Console:

```bash
Install-Package BigO.Cqrs
```

Or via .NET CLI:

```bash
dotnet add package BigO.Cqrs
```

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
