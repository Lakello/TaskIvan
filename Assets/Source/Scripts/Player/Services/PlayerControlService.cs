using System;
using TaskIvan.BonusSystem.Entities;
using TaskIvan.BonusSystem.Services;
using TaskIvan.InputSystem;
using TaskIvan.Player.Info;
using TaskIvan.SO;
using UnityEngine;

namespace TaskIvan.Player
{
	public class PlayerControlService : IDisposable
	{
		private readonly IInputService _inputService;
		private readonly BonusService _bonusService;
		private readonly PlayerMover _mover;
		private readonly PlayerRotator _rotator;
		private readonly PlayerJumper _jumper;
		private readonly SpeedPublisher _speedPublisher;

		public PlayerControlService(
			IInputService inputService,
			PlayerEntity playerEntity,
			GameData data,
			Camera mainCamera,
			BonusService bonusService)
		{
			_inputService = inputService;
			_bonusService = bonusService;

			_mover = new PlayerMover(playerEntity, data);
			_rotator = new PlayerRotator(playerEntity, mainCamera);
			_jumper = new PlayerJumper(playerEntity, data);
			_speedPublisher = new SpeedPublisher(playerEntity);
			_ = new HeightPublisher(playerEntity);

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
			{
				_speedPublisher.Publish(0);
				return;
			}
			
			_mover.Move(direction, _bonusService.TryGetBonus<SpeedBonus>());
			_rotator.Rotate();
			_speedPublisher.Publish(_mover.CurrentSpeed);
		}

		private void OnJumping() =>
			_jumper.Jump(_bonusService.TryGetBonus<JumpBonus>());
	}
}