using TaskIvan.Player;
using UnityEngine;

namespace TaskIvan.SO
{
	[CreateAssetMenu(menuName = "Data/Player", fileName = "PlayerData")]
	public class PlayerData : ScriptableObject
	{
		[SerializeField] private PlayerEntity _playerPrefab;
		[SerializeField] [Range(0, 100)] private float _moveSpeed;
		[SerializeField] [Range(0, 100)] private float _rotateSpeed;
		[SerializeField] [Range(0, 100)] private float _jumpForce;
		[SerializeField] [Range(0, 100)] private float _jumpRotateSpeed;
		[SerializeField] [Range(0, 100)] private float _mouseHorizontalSensitivity;
		[SerializeField] [Range(0, 100)] private float _mouseVerticalSensitivity;
		
		public PlayerEntity PlayerPrefab => _playerPrefab;
		public float MoveSpeed => _moveSpeed;
		public float RotateSpeed => _rotateSpeed;
		public float JumpForce => _jumpForce;
		public float JumpRotateSpeed => _jumpRotateSpeed;
		public float MouseHorizontalSensitivity => _mouseHorizontalSensitivity;
		public float MouseVerticalSensitivity => _mouseVerticalSensitivity;
	}
}