using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleParser
{
    public static class MyExtensions
    {
        public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> source,
            int count)
        {
            Queue<T> saveList = new Queue<T>();
            int saved = 0;
            foreach (T item in source)
            {
                if (saved < count)
                {
                    saveList.Enqueue(item);
                    ++saved;
                    continue;
                }
                saveList.Enqueue(item);
                yield return saveList.Dequeue();
            }
            yield break;
        }

        public static string StringConcatenate(this IEnumerable<string> source)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in source)
                sb.Append(s);
            return sb.ToString();
        }

        public static string StringConcatenate<T>(
            this IEnumerable<T> source,
            Func<T, string> projectionFunc)
        {
            return source.Aggregate(new StringBuilder(),
                (s, i) => s.Append(projectionFunc(i)),
                s => s.ToString());
        }
    }
}
