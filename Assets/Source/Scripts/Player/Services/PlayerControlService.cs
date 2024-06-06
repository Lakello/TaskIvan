using System;
using TaskIvan.InputSystem;
using TaskIvan.SO;
using UnityEngine;

namespace TaskIvan.Player
{
	public class PlayerControlService : IDisposable
	{
		private readonly IInputService _inputService;
		private readonly PlayerMover _mover;
		private readonly PlayerRotator _rotator;
		private readonly PlayerJumper _jumper;

		public PlayerControlService(IInputService inputService, PlayerEntity playerEntity, GameData data, Camera mainCamera)
		{
			_inputService = inputService;

			_mover = new PlayerMover(playerEntity, data);
			_rotator = new PlayerRotator(playerEntity, mainCamera);
			_jumper = new PlayerJumper(playerEntity, data);

			_inputService.Moving += OnMoving;
			_inputService.Jumping += OnJumping;
		}

		public void Dispose()
		{
			_inputService.Moving -= OnMoving;
			_inputService.Jumping -= OnJumping;
		}

		private void OnMoving(Vector2 direction)
		{
			if (direction == Vector2.zero)
				return;
			
			_mover.Move(direction);
			_rotator.Rotate();
		}

		private void OnJumping()
		{
			_jumper.Jump();
		}
	}
}