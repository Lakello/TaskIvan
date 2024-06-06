using TaskIvan.SO;
using UnityEngine;

namespace TaskIvan.Player
{
	public class PlayerMover
	{
		private readonly PlayerEntity _playerEntity;
		private readonly PlayerData _data;

		public PlayerMover(PlayerEntity playerEntity, PlayerData data)
		{
			_playerEntity = playerEntity;
			_data = data;
		}

		public void Move(Vector2 direction)
		{
			var playerTransform = _playerEntity.transform;
			var moveDirection = (playerTransform.forward * direction.y) + (playerTransform.right * direction.x);
			
			_playerEntity.SelfRigidbody.MovePosition(
				_playerEntity.SelfRigidbody.position + (moveDirection * _data.MoveSpeed * Time.fixedDeltaTime));
		}
	}
}