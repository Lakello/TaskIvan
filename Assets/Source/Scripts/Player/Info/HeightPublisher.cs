using System;
using System.Collections;
using TaskIvan.Messages;
using UniRx;

namespace TaskIvan.Player.Info
{
	public class HeightPublisher : IDisposable
	{
		private readonly PlayerEntity _playerEntity;
		private readonly IDisposable _publishDisposable;

		public HeightPublisher(PlayerEntity playerEntity)
		{
			_playerEntity = playerEntity;
			_publishDisposable = Observable.FromCoroutine(PublishHeight).Subscribe();
		}

		public void Dispose() =>
			_publishDisposable?.Dispose();

		private IEnumerator PublishHeight()
		{
			while (_playerEntity.enabled)
			{
				MessageBroker.Default
					.Publish(new Message<float>(
						MessageId.HeightChanged,
						_playerEntity.SelfRigidbody.position.y));

				yield return null;
			}
		}
	}
}