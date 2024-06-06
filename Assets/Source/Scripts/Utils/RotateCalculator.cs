using UnityEngine;

namespace TaskIvan.Utils
{
	public static class RotateCalculator
	{
		public static Quaternion CalculateRotationToTarget(Vector3 from, Vector3 to, Vector3 direction)
		{
			var offset = to - from;
			offset.Set(offset.x, 0, offset.z);
			
			return Quaternion.Euler(0f, Vector3.SignedAngle(direction, offset, Vector3.up), 0f);
		}
	}
}