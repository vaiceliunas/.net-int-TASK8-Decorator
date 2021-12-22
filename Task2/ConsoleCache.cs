using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator.Task2
{
    public class ConsoleCache
    {
        private static Dictionary<string, decimal?> MemoryCache = new Dictionary<string, decimal?>();
        private ConsoleCache()
        {
        }

        public static decimal? GetItem(string key)
        {
            var result = MemoryCache.DefaultIfEmpty(new KeyValuePair<string, decimal?>(null,null)).FirstOrDefault(t => t.Key == key);
            if (result.Key == null)
                return null;
            return result.Value;
        }

        public static void AddItem(KeyValuePair<string, decimal> kvPair)
        {
            if (MemoryCache.ContainsKey(kvPair.Key))
                return;

            MemoryCache.Add(kvPair.Key, kvPair.Value);
        }
    }
}
