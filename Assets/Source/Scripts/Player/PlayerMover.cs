using UnityEngine;

namespace TaskIvan.Player
{
	public class PlayerMover
	{
		private readonly PlayerEntity _playerEntity;
		private readonly float _speed;

		public PlayerMover(PlayerEntity playerEntity, float speed)
		{
			_playerEntity = playerEntity;
			_speed = speed;
		}

		public void Move(Vector2 direction)
		{
			var playerTransform = _playerEntity.transform;
			var moveDirection = (playerTransform.forward * direction.y) + (playerTransform.right * direction.x);
			
			_playerEntity.SelfRigidbody.MovePosition(
				_playerEntity.SelfRigidbody.position + (moveDirection * _speed * Time.deltaTime));
		}
	}
}