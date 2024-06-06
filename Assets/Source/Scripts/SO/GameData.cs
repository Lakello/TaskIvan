using TaskIvan.Player;
using UnityEngine;

namespace TaskIvan.SO
{
	[CreateAssetMenu(menuName = "Data/Player", fileName = "PlayerData")]
	public class GameData : ScriptableObject
	{
		[Header("General")]
		[SerializeField] private PlayerEntity _playerPrefab;
		[Space(10)]
		[Header("Control")]
		[SerializeField] [Range(0, 100)] private float _moveSpeed;
		[SerializeField] [Range(0, 100)] private float _jumpForce;
 		[SerializeField] [Range(0, 100)] private float _jumpRotateSpeed;
		[Space(10)]
		[Header("Camera")]
		[SerializeField] [Range(0, 100)] private float _mouseHorizontalSensitivity;
		[SerializeField] [Range(0, 100)] private float _mouseVerticalSensitivity;
		[SerializeField] private float _cameraHeight;
		[SerializeField] private float _cameraDistance;
		[SerializeField] private Vector2 _minMaxVerticalCameraAngle;
		
		public PlayerEntity PlayerPrefab => _playerPrefab;
		public float MoveSpeed => _moveSpeed;
		public float JumpRotateSpeed => _jumpRotateSpeed;
		public float MouseHorizontalSensitivity => _mouseHorizontalSensitivity;
		public float MouseVerticalSensitivity => _mouseVerticalSensitivity;
		public float CameraHeight => _cameraHeight;
		public Vector2 MinMaxVerticalCameraAngle => _minMaxVerticalCameraAngle;
		public float CameraDistance => _cameraDistance;
		public float JumpForce => _jumpForce;
	}
}