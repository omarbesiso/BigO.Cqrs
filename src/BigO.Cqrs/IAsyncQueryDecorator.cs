﻿namespace BigO.Cqrs;

/// <summary>
///     Defines a decorator for async query handlers. Each decorator provides the ability to add additional functionality
///     to
///     query handlers while not violating the single responsibility principle or the open/closed principle for the
///     S.O.L.I.D design patterns.
/// </summary>
/// <typeparam name="TQuery">The type of the query. Must be a reference type.</typeparam>
/// <typeparam name="TResult">The type of the response.</typeparam>
public interface IAsyncQueryDecorator<in TQuery, TResult> : IAsyncQueryHandler<TQuery, TResult>
    where TQuery : class
{
}