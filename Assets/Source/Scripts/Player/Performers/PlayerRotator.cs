using TaskIvan.SO;
using UnityEngine;

namespace TaskIvan.Player
{
	public class PlayerRotator
	{
		private readonly PlayerEntity _playerEntity;
		private readonly PlayerData _data;

		public PlayerRotator(PlayerEntity playerEntity, PlayerData data)
		{
			_playerEntity = playerEntity;
			_data = data;
		}

		public void Rotate(float mouseDeltaX)
		{
			_playerEntity.transform.rotation *= 
				Quaternion.Euler(0, mouseDeltaX * _data.RotateSpeed * _data.MouseHorizontalSensitivity, 0);
		}
	}
}