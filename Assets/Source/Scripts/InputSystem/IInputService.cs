using System;
using UnityEngine;

namespace TaskIvan.InputSystem
{
	public interface IInputService
	{
		public event Action<Vector3> Moving;
		public event Action Jumping;
	}
}