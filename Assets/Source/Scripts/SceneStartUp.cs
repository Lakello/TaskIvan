using System;
using TaskIvan.Extensions;
using TaskIvan.Initializations.Player;
using TaskIvan.InputSystem;
using TaskIvan.Level.Entities;
using TaskIvan.SO;
using UnityEngine;

namespace TaskIvan
{
	public class SceneStartUp : MonoBehaviour
	{
		[SerializeField] private SpawnPoint _spawnPoint;
		[SerializeField] private PlayerData _playerData;
		
		private IDisposable[] _disposables;
		
		private void Awake()
		{
			var inputService = new DesktopInputService(this);

			var playerFactory = new PlayerFactory(_playerData);
			var playerEntity = playerFactory.Create(new PlayerInit(_spawnPoint.transform.position, inputService));
			
			_disposables = new IDisposable[]
			{
				inputService, playerFactory
			};
		}

		private void OnDisable() =>
			_disposables.ForEach(disposable => disposable.Dispose());
	}
}