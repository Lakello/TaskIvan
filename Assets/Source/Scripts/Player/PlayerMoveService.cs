using System;
using TaskIvan.InputSystem;
using UnityEngine;

namespace TaskIvan.Player
{
	public class PlayerMoveService : IDisposable
	{
		private readonly IInputService _inputService;
		private readonly PlayerEntity _playerEntity;

		public PlayerMoveService(IInputService inputService, PlayerEntity playerEntity)
		{
			_inputService = inputService;
			_playerEntity = playerEntity;

			_inputService.Moving += OnMoving;
		}

		public void Dispose() =>
			_inputService.Moving -= OnMoving;

		private void OnMoving(Vector3 direction)
		{
			
		}
	}
}