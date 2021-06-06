using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace RandomDomain.Api.Extensions
{
    public static class EnumerableExtensions
    {
        public static T SingleRandomOrDefault<T>(this IEnumerable<T> collection)
        {
            var list = collection.ToImmutableList();

            return !list.Any() ? default : list.OrderBy(x => Guid.NewGuid()).Take(1).First();
        }
    }
}