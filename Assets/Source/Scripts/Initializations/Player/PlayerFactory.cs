using System;
using TaskIvan.BonusSystem.Services;
using TaskIvan.Player;
using TaskIvan.SO;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TaskIvan.Initializations.Player
{
	public class PlayerFactory : IDisposable
	{
		private readonly GameData _data;

		private PlayerControlService _controlService;

		public PlayerFactory(GameData data)
		{
			_data = data;
		}

		public PlayerEntity Create(PlayerInit init, Camera mainCamera)
		{
			var playerEntity = Object.Instantiate(_data.PlayerPrefab, init.SpawnPosition, Quaternion.identity);

			var bonusService = new BonusService(playerEntity);
			_controlService = new PlayerControlService(init.InputService, playerEntity, _data, mainCamera, bonusService);

			return playerEntity;
		}

		public void Dispose()
		{
			_controlService.Dispose();
		}
	}
}