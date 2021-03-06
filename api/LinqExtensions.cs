﻿namespace RandomDomainFunction
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;

    public static class LinqExtensions
    {
        public static T SingleRandomOrDefault<T>(this IEnumerable<T> collection)
        {
            var list = collection.ToImmutableList();

            return !list.Any() ? default : list.OrderBy(x => Guid.NewGuid()).Take(1).First();
        }
    }
}
