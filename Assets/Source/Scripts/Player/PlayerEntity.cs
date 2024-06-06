using TaskIvan.Extensions;
using UnityEngine;

namespace TaskIvan.Player
{
	[RequireComponent(typeof(Rigidbody))]
	public class PlayerEntity : MonoBehaviour
	{
		public Rigidbody SelfRigidbody { get; private set; }

		private void Awake()
		{
			SelfRigidbody = gameObject.GetComponentElseThrow<Rigidbody>();
		}
	}
}