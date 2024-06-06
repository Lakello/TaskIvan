using System;
using UnityEngine;

namespace TaskIvan.Extensions
{
	public static class GameObjectExtension
	{
		public static T GetComponentElseThrow<T>(this GameObject origin) =>
			GetComponentElseThrow(origin, out T _);

		public static T GetComponentElseThrow<T>(this GameObject origin, out T component)
		{
			if (origin.TryGetComponent(out component) is false)
			{
				Debug.LogException(new NullReferenceException(), origin);
			}

			return component;
		}
	}
}