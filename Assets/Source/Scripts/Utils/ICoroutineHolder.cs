using System.Collections;
using UnityEngine;

namespace TaskIvan.Utils
{
	public interface ICoroutineHolder
	{
		public Coroutine Play(IEnumerator coroutine);

		public void Stop(Coroutine coroutine);
	}
}