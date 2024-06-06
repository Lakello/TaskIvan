using UnityEngine;

namespace TaskIvan.Player
{
	public class GroundChecker
	{
		private const float CheckDistance = 0.01f;

		private readonly CapsuleCollider _playerCollider;
		private readonly RaycastHit[] _hits;

		public GroundChecker(CapsuleCollider playerCollider)
		{
			_playerCollider = playerCollider;
			_hits = new RaycastHit[2];
		}
		
		public bool IsGrounded()
		{
			var count = Physics.CapsuleCastNonAlloc(
				_playerCollider.bounds.max,
				_playerCollider.bounds.min,
				_playerCollider.radius,
				Vector3.down,
				_hits,
				CheckDistance);

			return count > 1;
		}
	}
}