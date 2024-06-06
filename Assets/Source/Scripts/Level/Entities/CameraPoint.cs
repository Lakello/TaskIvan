using TaskIvan.Player;
using UnityEngine;

namespace TaskIvan.Level.Entities
{
	public class CameraPoint : MonoBehaviour
	{
		private PlayerEntity _playerEntity;

		public void Init(PlayerEntity playerEntity)
		{
			_playerEntity = playerEntity;
		}

		public void Move() =>
			transform.position = _playerEntity.transform.position;
	}
}