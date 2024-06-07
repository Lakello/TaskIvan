using System;
using System.Collections;
using TaskIvan.BonusSystem.Entities;
using TaskIvan.SO;
using TaskIvan.Utils;
using UnityEngine;

namespace TaskIvan.Player
{
	public class PlayerJumper : IDisposable
	{
		private readonly PlayerEntity _playerEntity;
		private readonly GameData _data;
		private readonly GroundChecker _groundChecker;
		private readonly CapsuleCollider _playerCollider;

		private Coroutine _jumpCoroutine;
		private Coroutine _somersaultCoroutine;
		private int _jumpCount;

		public PlayerJumper(PlayerEntity playerEntity, GameData data)
		{
			_playerEntity = playerEntity;
			_data = data;
			_playerCollider = playerEntity.GetComponentInChildren<CapsuleCollider>();
			_groundChecker = new GroundChecker(_playerCollider);
		}

		public void Jump(JumpBonus bonus)
		{
			if (_jumpCoroutine != null)
			{
				if (bonus == null)
					return;

				TryUseBonus();
			}

			if (_jumpCount >= _data.MaxJumpCount)
				return;

			if (_groundChecker.IsGrounded() == false)
				return;

			Jump();
		}

		public void Dispose()
		{
			CoroutineHolder.Instance.Stop(_jumpCoroutine);
			CoroutineHolder.Instance.Stop(_somersaultCoroutine);
		}

		private void Jump()
		{
			_jumpCount++;
			_jumpCoroutine = CoroutineHolder.Instance.Play(Start());
		}

		private void TryUseBonus()
		{
			if (_jumpCount < _data.MaxJumpCount && _groundChecker.IsGrounded() == false && _somersaultCoroutine == null)
			{
				CoroutineHolder.Instance.Stop(_jumpCoroutine);
				Jump();
			}
		}

		private IEnumerator Start()
		{
			_playerEntity.SelfRigidbody.AddForce(Vector3.up * _data.JumpForce, ForceMode.Impulse);

			_somersaultCoroutine = CoroutineHolder.Instance.Play(Somersault());

			yield return new WaitUntil(() => _groundChecker.IsGrounded() == false);
			yield return new WaitUntil(() => _groundChecker.IsGrounded());

			_jumpCount = 0;
			_jumpCoroutine = null;
		}

		private IEnumerator Somersault()
		{
			var wait = new WaitForFixedUpdate();

			var currentTime = 0f;

			while (currentTime <= _data.SomersaultDuration)
			{
				var angle = Mathf.Lerp(0, 360, currentTime / _data.SomersaultDuration);

				_playerCollider.transform.Rotate(new Vector3(angle, 0, 0));

				currentTime += Time.fixedDeltaTime;

				yield return wait;
			}

			_playerCollider.transform.Rotate(new Vector3(0, 0, 0));

			_somersaultCoroutine = null;
		}
	}
}