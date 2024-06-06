using System.Collections;
using UnityEngine;

namespace TaskIvan.Utils
{
	public class CoroutineHolder : MonoBehaviour, ICoroutineHolder
	{
		public static ICoroutineHolder Instance { get; private set; }

		private void Awake()
		{
			if (Instance == null)
				Instance = this;
			else
				Destroy(this);
		}

		public Coroutine Play(IEnumerator coroutine) =>
			StartCoroutine(coroutine);

		public void Stop(Coroutine coroutine) =>
			StopCoroutine(coroutine);
	}
}