using TaskIvan.BonusSystem.Entities;
using TaskIvan.SO;
using UnityEngine;

namespace TaskIvan.Player
{
	public class PlayerMover
	{
		private readonly PlayerEntity _playerEntity;
		private readonly GameData _data;

		public PlayerMover(PlayerEntity playerEntity, GameData data)
		{
			_playerEntity = playerEntity;
			_data = data;
		}

		public void Move(Vector2 direction, SpeedBonus speedBonus)
		{
			var playerTransform = _playerEntity.transform;
			var moveDirection = (playerTransform.forward * direction.y) + (playerTransform.right * direction.x);

			var multiplier = speedBonus == null ? 1 : speedBonus.Multiplier;
			
			_playerEntity.SelfRigidbody.MovePosition(
				_playerEntity.SelfRigidbody.position + 
				(moveDirection * _data.MoveSpeed * multiplier * Time.fixedDeltaTime));
		}
	}
}