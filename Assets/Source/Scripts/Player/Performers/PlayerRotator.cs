using TaskIvan.Utils;
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
			_playerEntity.SelfRigidbody.MoveRotation(RotateCalculator.CalculateRotationToTarget(
				_playerEntity.SelfRigidbody.position,
				_mainCamera.transform.position,
				Vector3.back));
		}
	}
}