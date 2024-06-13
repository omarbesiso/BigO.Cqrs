namespace BigO.Cqrs;

/// <summary>
///     Provides extension methods for reflection-based type checking.
/// </summary>
internal static class ReflectionExtensions
{
    /// <summary>
    ///     Determines whether the specified type is based on another type, including handling generic type definitions.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <param name="otherType">The type to compare against.</param>
    /// <returns><c>true</c> if the specified type is based on the other type; otherwise, <c>false</c>.</returns>
    public static bool IsBasedOn(this Type type, Type otherType)
    {
        return otherType.IsGenericTypeDefinition
            ? type.IsAssignableToGenericTypeDefinition(otherType)
            : otherType.IsAssignableFrom(type);
    }

    /// <summary>
    ///     Determines whether the specified type is assignable to a generic type definition.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <param name="genericType">The generic type definition to compare against.</param>
    /// <returns><c>true</c> if the specified type is assignable to the generic type definition; otherwise, <c>false</c>.</returns>
    private static bool IsAssignableToGenericTypeDefinition(this Type type, Type genericType)
    {
        // Check all interfaces implemented by the type
        foreach (var interfaceType in type.GetInterfaces())
        {
            if (!interfaceType.IsGenericType)
            {
                continue;
            }

            var genericTypeDefinition = interfaceType.GetGenericTypeDefinition();
            if (genericTypeDefinition == genericType)
            {
                return true;
            }
        }

        // Check if the type itself is a generic type and matches the generic type definition
        if (type.IsGenericType)
        {
            var genericTypeDefinition = type.GetGenericTypeDefinition();
            if (genericTypeDefinition == genericType)
            {
                return true;
            }
        }

        // Recursively check the base types
        var baseType = type.BaseType;
        return baseType is not null && baseType.IsAssignableToGenericTypeDefinition(genericType);
    }
}