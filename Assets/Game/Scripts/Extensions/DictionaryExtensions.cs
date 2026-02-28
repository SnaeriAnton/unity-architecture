using System;
using System.Collections.Generic;

namespace ExtensionSystems
{
    public static class DictionaryExtensions
    {
        public static void ForEach<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Action<KeyValuePair<TKey, TValue>> action)
        {
            foreach (KeyValuePair<TKey, TValue> kv in dictionary)
                action(kv);
        }
        
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T item in source) action(item);
        }
    }
}
