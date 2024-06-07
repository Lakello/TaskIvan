using System;
using TaskIvan.CameraSystem.Performers;
using TaskIvan.InputSystem;
using TaskIvan.Level.Entities;
using TaskIvan.Player;
using TaskIvan.SO;
using UnityEngine;

namespace TaskIvan.CameraSystem.Services
{
	public class CameraControlService : IDisposable
	{
		private readonly IInputService _inputService;
		private readonly CameraMover _cameraMover;
		private readonly CameraRotator _rotator;

		public CameraControlService(
			IInputService inputService,
			PlayerEntity playerEntity,
			GameData data,
			CameraPoint cameraPoint)
		{
			_inputService = inputService;
			_cameraMover = new CameraMover(playerEntity, cameraPoint);
			_rotator = new CameraRotator(data, cameraPoint);
			_inputService.Moving += OnMoving;
			_inputService.MouseMoving += OnMouseMoving;
		}

		public void Dispose()
		{
			_inputService.Moving -= OnMoving;
			_inputService.MouseMoving -= OnMouseMoving;
		}

		private void OnMouseMoving(Vector2 delta) =>
			_rotator.Rotate(delta);

		private void OnMoving(Vector2 _) =>
			_cameraMover.Move();
	}
}