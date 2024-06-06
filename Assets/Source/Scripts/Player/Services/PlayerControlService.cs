using System;
using TaskIvan.InputSystem;
using TaskIvan.SO;
using UnityEngine;

namespace TaskIvan.Player
{
	public class PlayerControlService : IDisposable
	{
		private readonly IInputService _inputService;
		private readonly PlayerEntity _playerEntity;
		private readonly PlayerData _data;
		private readonly PlayerMover _mover;
		private readonly PlayerRotator _rotator;

		public PlayerControlService(IInputService inputService, PlayerEntity playerEntity, PlayerData data)
		{
			_inputService = inputService;
			_playerEntity = playerEntity;
			_data = data;

			_mover = new PlayerMover(playerEntity, data);
			_rotator = new PlayerRotator(playerEntity, data);
			
			_inputService.Moving += OnMoving;
			_inputService.MouseMoving += OnMouseMoving;
		}

		public void Dispose()
		{
			_inputService.Moving -= OnMoving;
			_inputService.MouseMoving -= OnMouseMoving;
		}

		private void OnMoving(Vector2 direction)
		{
			if (direction == Vector2.zero)
				return;
			
			_mover.Move(direction);
		}

		private void OnMouseMoving(Vector2 delta) =>
			_rotator.Rotate(delta.x);
	}
}