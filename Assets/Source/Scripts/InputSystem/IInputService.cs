using System;
using UnityEngine;

namespace TaskIvan.InputSystem
{
	public interface IInputService
	{
		public event Action<Vector2> Moving;
		public event Action Jumping;
	}
}