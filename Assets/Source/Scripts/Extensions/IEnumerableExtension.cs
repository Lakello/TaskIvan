using System;
using System.Collections.Generic;

namespace TaskIvan.Extensions
{
	public static class IEnumerableExtension
	{
		public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			foreach (T obj in source)
				action(obj);

			return source;
		}
	}
}