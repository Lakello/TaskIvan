using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = TaskIvan.InputSystem.Actions.PlayerInput;

namespace TaskIvan.InputSystem
{
	public class DesktopInputService : IDisposable, IInputService
	{
		private readonly MonoBehaviour _mono;
		private readonly PlayerInput _playerInput;
		private readonly Coroutine _updateInputCoroutine;

		public event Action<Vector2> Moving;
		public event Action Jumping;
			
		public DesktopInputService(MonoBehaviour mono)
		{
			_mono = mono;

			_playerInput = new PlayerInput();
			_playerInput.Enable();

			_playerInput.Player.Jump.performed += OnJumpPerformed;

			_updateInputCoroutine = _mono.StartCoroutine(UpdateInput());
		}

		private void OnJumpPerformed(InputAction.CallbackContext _) =>
			Jumping?.Invoke();

		public void Dispose()
		{
			if (_playerInput != null)
			{
				_playerInput.Dispose();
				_playerInput.Player.Jump.performed -= OnJumpPerformed;
			}
			
			_mono.StopCoroutine(_updateInputCoroutine);
		}

		private IEnumerator UpdateInput()
		{
			float horizontal;
			float vertical;

			while (_mono.enabled)
			{
				horizontal = _playerInput.Player.Horizontal.ReadValue<float>();
				vertical = _playerInput.Player.Vertical.ReadValue<float>();
				
				Moving?.Invoke(new Vector2(horizontal, vertical));

				yield return null;
			}
		}
	}
}