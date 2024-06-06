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
		private readonly Coroutine _updateInputMoveCoroutine;
		private readonly Coroutine _updateInputMouseCoroutine;

		public event Action<Vector2> Moving;
		public event Action<Vector2> MouseMoving;
		public event Action Jumping;
			
		public DesktopInputService(MonoBehaviour mono)
		{
			_mono = mono;

			_playerInput = new PlayerInput();
			_playerInput.Enable();

			_playerInput.KeyboardAndMouse.Jump.performed += OnJumpPerformed;
			
			var wait = new WaitForFixedUpdate();
			_updateInputMoveCoroutine = _mono.StartCoroutine(UpdateInput(
				() => Moving?.Invoke(_playerInput.KeyboardAndMouse.Move.ReadValue<Vector2>()),
				wait));

			_updateInputMouseCoroutine = _mono.StartCoroutine(UpdateInput(
				() => MouseMoving?.Invoke(_playerInput.KeyboardAndMouse.MouseDelta.ReadValue<Vector2>())));
		}

		private void OnJumpPerformed(InputAction.CallbackContext _) =>
			Jumping?.Invoke();

		public void Dispose()
		{
			if (_playerInput != null)
			{
				_playerInput.Dispose();
				_playerInput.KeyboardAndMouse.Jump.performed -= OnJumpPerformed;
			}
			
			_mono.StopCoroutine(_updateInputMoveCoroutine);
			_mono.StopCoroutine(_updateInputMouseCoroutine);
		}
		
		private IEnumerator UpdateInput(Action action, YieldInstruction instruction = null)
		{
			while (_mono.enabled)
			{
				action.Invoke();
				
				yield return instruction;
			}
		}
	}
}