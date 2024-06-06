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
		public event Action<Vector2> MouseMoving;
		public event Action Jumping;
			
		public DesktopInputService(MonoBehaviour mono)
		{
			_mono = mono;

			_playerInput = new PlayerInput();
			_playerInput.Enable();

			_playerInput.KeyboardAndMouse.Jump.performed += OnJumpPerformed;
			_playerInput.KeyboardAndMouse.MouseDelta.performed += OnMouseDeltaPerformed;

			_updateInputCoroutine = _mono.StartCoroutine(UpdateInput());
		}

		private void OnMouseDeltaPerformed(InputAction.CallbackContext context) =>
			MouseMoving?.Invoke(context.ReadValue<Vector2>());

		private void OnJumpPerformed(InputAction.CallbackContext _) =>
			Jumping?.Invoke();

		public void Dispose()
		{
			if (_playerInput != null)
			{
				_playerInput.Dispose();
				_playerInput.KeyboardAndMouse.Jump.performed -= OnJumpPerformed;
			}
			
			_mono.StopCoroutine(_updateInputCoroutine);
		}

		private IEnumerator UpdateInput()
		{
			var wait = new WaitForFixedUpdate();
			
			while (_mono.enabled)
			{
				Moving?.Invoke(_playerInput.KeyboardAndMouse.Move.ReadValue<Vector2>());

				yield return wait;
			}
		}
	}
}