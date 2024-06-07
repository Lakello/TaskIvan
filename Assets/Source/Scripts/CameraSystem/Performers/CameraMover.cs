using TaskIvan.Level.Entities;
using TaskIvan.Player;

namespace TaskIvan.CameraSystem.Performers
{
	public class CameraMover
	{
		private readonly PlayerEntity _playerEntity;
		private readonly CameraPoint _cameraPoint;

		public CameraMover(PlayerEntity playerEntity, CameraPoint cameraPoint)
		{
			_playerEntity = playerEntity;
			_cameraPoint = cameraPoint;
		}

		public void Move() =>
			_cameraPoint.transform.position = _playerEntity.transform.position;
	}
}