using UnityEngine;

namespace TaskIvan.Player
{
	public class PlayerRotator
	{
		private readonly PlayerEntity _playerEntity;
		private readonly Camera _mainCamera;

		public PlayerRotator(PlayerEntity playerEntity, Camera mainCamera)
		{
			_playerEntity = playerEntity;
			_mainCamera = mainCamera;
		}

		public void Rotate()
		{
			var offset = _playerEntity.SelfRigidbody.position - _mainCamera.transform.position;
			offset.Set(offset.x, 0, offset.z);

			var targetRotation =
				Quaternion.Euler(0f, Vector3.SignedAngle(Vector3.forward, offset, Vector3.up), 0f);

			_playerEntity.SelfRigidbody.MoveRotation(targetRotation);
		}
	}
}