using TaskIvan.InputSystem;
using UnityEngine;

namespace TaskIvan.Initializations.Player
{
	public class PlayerInit
	{
		public PlayerInit(Vector3 spawnPosition, IInputService inputService)
		{
			SpawnPosition = spawnPosition;
			InputService = inputService;
		}

		public Vector3 SpawnPosition { get; }
		public IInputService InputService { get; }
	}
}