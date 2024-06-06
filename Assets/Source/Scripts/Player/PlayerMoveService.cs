using System;
using TaskIvan.InputSystem;
using TaskIvan.SO;
using UnityEngine;

namespace TaskIvan.Player
{
	public class PlayerMoveService : IDisposable
	{
		private readonly IInputService _inputService;
		private readonly PlayerEntity _playerEntity;
		private readonly PlayerData _data;
		private readonly PlayerMover _mover;
		private readonly PlayerRotator _rotator;

		public PlayerMoveService(IInputService inputService, PlayerEntity playerEntity, PlayerData data)
		{
			_inputService = inputService;
			_playerEntity = playerEntity;
			_data = data;

			_mover = new PlayerMover(playerEntity, data.MoveSpeed);
			_rotator = new PlayerRotator();
			
			_inputService.Moving += OnMoving;
		}

		public void Dispose() =>
			_inputService.Moving -= OnMoving;

		private void OnMoving(Vector2 direction)
		{
			if (direction == Vector2.zero)
				return;
			
			_mover.Move(direction);
			
		}
	}
}