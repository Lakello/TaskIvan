using System;
using TaskIvan.CameraSystem.Performers;
using TaskIvan.InputSystem;
using TaskIvan.Level.Entities;
using TaskIvan.SO;
using UnityEngine;

namespace TaskIvan.CameraSystem.Services
{
	public class CameraControlService : IDisposable
	{
		private readonly IInputService _inputService;
		private readonly CameraPoint _cameraPoint;
		private readonly CameraRotator _rotator;

		public CameraControlService(
			IInputService inputService,
			PlayerData data,
			CameraPoint cameraPoint)
		{
			_inputService = inputService;
			_cameraPoint = cameraPoint;
			_rotator = new CameraRotator(data, cameraPoint);
			_inputService.Moving += OnMoving;
			_inputService.MouseMoving += OnMouseMoving;
		}

		public void Dispose() =>
			_inputService.MouseMoving -= OnMouseMoving;

		private void OnMouseMoving(Vector2 delta) =>
			_rotator.Rotate(delta);

		private void OnMoving(Vector2 _) =>
			_cameraPoint.Move();
	}
}