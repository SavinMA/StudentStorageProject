using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

using System;

namespace PresentationLayer.Helpers
{
    public static class Helpers
    {
        public static bool TryGetGuidFromCollection(this IFormCollection collection, string key, out Guid id)
        {
            id = default(Guid);

            if (collection.TryGetValue(key, out StringValues values))
            {
                return Guid.TryParse(values.ToString(), out id);
            }

            return false;
        }
    }
}
