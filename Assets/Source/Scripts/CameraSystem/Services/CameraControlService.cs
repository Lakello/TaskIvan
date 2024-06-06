using System;
using TaskIvan.InputSystem;
using TaskIvan.Player;
using TaskIvan.SO;
using UnityEngine;

namespace TaskIvan.CameraSystem.Services
{
	public class CameraControlService : IDisposable
	{
		private readonly IInputService _inputService;
		private readonly PlayerEntity _playerEntity;
		private readonly PlayerData _data;
		private readonly Camera _mainCamera;

		public CameraControlService(IInputService inputService, PlayerEntity playerEntity, PlayerData data)
		{
			_inputService = inputService;
			_playerEntity = playerEntity;
			_data = data;
			_mainCamera = Camera.main;

			_inputService.Moving += OnMoving;
		}

		public void Dispose()
		{
			_inputService.MouseMoving -= OnMoving;
		}

		private void OnMoving(Vector2 _)
		{
			var newPosition = _playerEntity.SelfRigidbody.position + _data.CameraOffset;
			_mainCamera.transform.position = 
				Vector3.Lerp(_mainCamera.transform.position, newPosition, _data.CameraMoveLag * Time.fixedDeltaTime);
		}
	}
}