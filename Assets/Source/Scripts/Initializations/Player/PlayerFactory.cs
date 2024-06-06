using System;
using TaskIvan.Player;
using TaskIvan.SO;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TaskIvan.Initializations.Player
{
	public class PlayerFactory : IDisposable
	{
		private readonly PlayerData _data;

		private PlayerControlService _controlService;

		public PlayerFactory(PlayerData data)
		{
			_data = data;
		}

		public PlayerEntity Create(PlayerInit init)
		{
			var playerEntity = Object.Instantiate(_data.PlayerPrefab, init.SpawnPosition, Quaternion.identity);

			_controlService = new PlayerControlService(init.InputService, playerEntity, _data);
			
			return playerEntity;
		}

		public void Dispose()
		{
			_controlService.Dispose();
		}
	}
}