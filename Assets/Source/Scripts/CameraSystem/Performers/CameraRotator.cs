using TaskIvan.Level.Entities;
using TaskIvan.SO;
using UnityEngine;

namespace TaskIvan.CameraSystem.Performers
{
	public class CameraRotator
	{
		private readonly GameData _data;
		private readonly CameraPoint _cameraPoint;
		private readonly float _rotateAngle;

		private float _vertical;
		private float _horizontal;

		public CameraRotator(GameData data, CameraPoint cameraPoint)
		{
			_data = data;
			_cameraPoint = cameraPoint;
		}

		public void Rotate(Vector2 delta)
		{
			_vertical -= _data.MouseVerticalSensitivity * delta.y * Time.deltaTime;
			_horizontal += _data.MouseHorizontalSensitivity * delta.x * Time.deltaTime;

			_vertical = Mathf.Clamp(_vertical, _data.MinMaxVerticalCameraAngle.x, _data.MinMaxVerticalCameraAngle.y);
			_cameraPoint.transform.eulerAngles = new Vector3(_vertical, _horizontal, 0);
		}
	}
}