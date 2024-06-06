using System;
using TaskIvan.CameraSystem.Services;
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
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			
			var mainCamera = Camera.main;

			var inputService = new DesktopInputService(this);

			var playerFactory = new PlayerFactory(_playerData);
			var playerEntity = playerFactory.Create(new PlayerInit(_spawnPoint.transform.position, inputService), mainCamera);
			
			var cameraPoint = new GameObject(nameof(CameraPoint)).AddComponent<CameraPoint>();
			mainCamera.transform.SetParent(cameraPoint.transform);
			mainCamera.transform.position = 
				cameraPoint.transform.position + new Vector3(0, _playerData.CameraHeight, -_playerData.CameraDistance);
			cameraPoint.Init(playerEntity);

			var cameraControlService = new CameraControlService(inputService, _playerData, cameraPoint);
			
			_disposables = new IDisposable[]
			{
				inputService, playerFactory, cameraControlService
			};
		}

		private void OnDisable() =>
			_disposables.ForEach(disposable => disposable.Dispose());
	}
}