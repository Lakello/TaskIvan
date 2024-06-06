using TaskIvan.Player;
using UnityEngine;

namespace TaskIvan.SO
{
	[CreateAssetMenu(menuName = "Data/Player", fileName = "PlayerData")]
	public class PlayerData : ScriptableObject
	{
		[SerializeField] private PlayerEntity _playerPrefab;
		[SerializeField] [Range(0, 100)] private float _moveSpeed;
		[SerializeField] [Range(0, 100)] private float _jumpForce;
		[SerializeField] [Range(0, 100)] private float _jumpRotateSpeed;
		[SerializeField] [Range(0, 100)] private float _mouseHorizontalSensitivity;
		[SerializeField] [Range(0, 100)] private float _mouseVerticalSensitivity;
		[SerializeField] private float _cameraHeight;
		[SerializeField] private float _cameraDistance;
		[SerializeField] private float _cameraMoveLag;
		[SerializeField] private float _cameraRotateSpeed;
		[SerializeField] private Vector2 _minMaxVerticalCameraAngle;
		
		public PlayerEntity PlayerPrefab => _playerPrefab;
		public float MoveSpeed => _moveSpeed;
		public float JumpForce => _jumpForce;
		public float JumpRotateSpeed => _jumpRotateSpeed;
		public float MouseHorizontalSensitivity => _mouseHorizontalSensitivity;
		public float MouseVerticalSensitivity => _mouseVerticalSensitivity;
		public float CameraHeight => _cameraHeight;
		public float CameraMoveLag => _cameraMoveLag;
		public Vector2 MinMaxVerticalCameraAngle => _minMaxVerticalCameraAngle;
		public float CameraRotateSpeed => _cameraRotateSpeed;
		public float CameraDistance => _cameraDistance;
	}
}