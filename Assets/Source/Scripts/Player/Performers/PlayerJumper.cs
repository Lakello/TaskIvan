using System;
using System.Collections;
using DG.Tweening;
using TaskIvan.BonusSystem.Entities;
using TaskIvan.SO;
using UniRx;
using UnityEngine;

namespace TaskIvan.Player
{
	public class PlayerJumper : IDisposable
	{
		private readonly PlayerEntity _playerEntity;
		private readonly GameData _data;
		private readonly GroundChecker _groundChecker;
		private readonly CapsuleCollider _playerCollider;

		private IDisposable _jumpDisposable;
		private Tweener _somersaultTweener;
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
			if (_jumpDisposable != null)
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
			_jumpDisposable?.Dispose();
			_somersaultTweener?.Kill();
		}

		private void Jump()
		{
			_jumpCount++;
			_jumpDisposable = Observable.FromCoroutine(Start).Subscribe();
		}

		private void TryUseBonus()
		{
			if (_jumpCount < _data.MaxJumpCount && _groundChecker.IsGrounded() == false)
			{
				if (_somersaultTweener != null && _somersaultTweener.IsPlaying())
					return;

				_jumpDisposable?.Dispose();

				Jump();
			}
		}

		private IEnumerator Start()
		{
			_playerEntity.SelfRigidbody.AddForce(Vector3.up * _data.JumpForce, ForceMode.Impulse);

			_somersaultTweener = _playerCollider.transform
				.DOLocalRotate(new Vector3(360, 0, 0),
					_data.SomersaultDuration,
					RotateMode.FastBeyond360)
				.OnKill(() => _playerCollider.transform.Rotate(new Vector3(0, 0, 0)));

			yield return new WaitUntil(() => _groundChecker.IsGrounded() == false);
			yield return new WaitUntil(() => _groundChecker.IsGrounded());

			_jumpCount = 0;
			_jumpDisposable = null;
		}
	}
}