using TaskIvan.Messages;
using UniRx;

namespace TaskIvan.Player.Info
{
	public class SpeedPublisher
	{
		private readonly PlayerEntity _playerEntity;

		public SpeedPublisher(PlayerEntity playerEntity)
		{
			_playerEntity = playerEntity;
		}

		public void Publish(float speed)
		{
			MessageBroker.Default
				.Publish(new Message<float>(MessageId.SpeedChanged, speed));
		}
	}
}