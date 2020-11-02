using System;
using System.Collections.Generic;
using System.Linq;

namespace Dates
{
    internal static class Guard
    {
        internal static void AgainstUnsupportedValues<T>(T argumentValue, string argumentName, IEnumerable<T> supportedValues, string message = null)
        {
            if ((supportedValues == null) || !supportedValues.Contains(argumentValue))
                throw new ArgumentException(message ?? $"Argument value not supported. Supported values are {string.Join(", ", supportedValues)}", argumentName);
        }
    }
}
