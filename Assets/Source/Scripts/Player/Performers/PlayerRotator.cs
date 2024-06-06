using TaskIvan.SO;
using UnityEngine;

namespace TaskIvan.Player
{
	public class PlayerRotator
	{
		private readonly PlayerEntity _playerEntity;
		private readonly PlayerData _data;
		private readonly Camera _mainCamera;

		public PlayerRotator(PlayerEntity playerEntity, PlayerData data)
		{
			_playerEntity = playerEntity;
			_data = data;
			_mainCamera = Camera.main;
		}

		public void Rotate()
		{
			var offset = _mainCamera.transform.position - _playerEntity.SelfRigidbody.position;
			offset.Set(offset.x, 0, offset.z);
			_playerEntity.SelfRigidbody.MoveRotation(
				Quaternion.Euler(0f, Vector3.SignedAngle(Vector3.back, offset, Vector3.up), 0f));
		}
	}
}