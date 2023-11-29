namespace BigO.Cqrs;

internal static class ReflectionExtensions
{
    public static bool IsBasedOn(this Type type, Type otherType)
    {
        return otherType.IsGenericTypeDefinition
            ? type.IsAssignableToGenericTypeDefinition(otherType)
            : otherType.IsAssignableFrom(type);
    }

    private static bool IsAssignableToGenericTypeDefinition(this Type type, Type genericType)
    {
        // ReSharper disable once LoopCanBeConvertedToQuery
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

        if (type.IsGenericType)
        {
            var genericTypeDefinition = type.GetGenericTypeDefinition();
            if (genericTypeDefinition == genericType)
            {
                return true;
            }
        }

        var baseType = type.BaseType;
        return baseType is not null && baseType.IsAssignableToGenericTypeDefinition(genericType);
    }
}