using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Dungecto.Common.Utils
{
    /// <summary>Parentless extensions with no place to go</summary>
    public static class AdoptExtensionMethods
    {
        /// <summary>Removes all items matching condition</summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="collection">Collection</param>
        /// <param name="condition">Condition</param>
        /// <returns>Amount of removed items</returns>
        public static int Remove<T>(this Collection<T> collection, Func<T, bool> condition)
        {
            var toRemove = collection.Where(condition).ToList();

            foreach (var itemToRemove in toRemove)
            {
                collection.Remove(itemToRemove);
            }

            return toRemove.Count;
        }
    }
}
