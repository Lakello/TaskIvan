using System;
using System.Collections;
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

		private Coroutine _jumpCoroutine;

		public PlayerJumper(PlayerEntity playerEntity, GameData data)
		{
			_playerEntity = playerEntity;
			_data = data;
			_groundChecker = new GroundChecker(playerEntity.GetComponentInChildren<CapsuleCollider>());
		}

		public void Jump()
		{
			if (_jumpCoroutine != null || _groundChecker.IsGrounded() == false)
				return;

			_jumpCoroutine = CoroutineHolder.Instance.Play(Start());
		}

		public void Dispose()
		{
			CoroutineHolder.Instance.Stop(_jumpCoroutine);
		}

		private IEnumerator Start()
		{
			_playerEntity.SelfRigidbody.AddForce(Vector3.up * _data.JumpForce, ForceMode.Impulse);

			yield return new WaitUntil(() => _groundChecker.IsGrounded());
			_jumpCoroutine = null;
		}
	}
}