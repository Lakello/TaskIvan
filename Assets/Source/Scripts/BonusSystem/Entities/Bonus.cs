using System;
using UnityEngine;

namespace TaskIvan.BonusSystem.Entities
{
	public abstract class Bonus : MonoBehaviour
	{
		[SerializeField] private float _duration = 10f;
		[SerializeField] private float _cooldown = 5;
		[SerializeField] private float _rotateSpeed = 5f;

		public float Duration => _duration;
		public float Cooldown => _cooldown;

		private void Update()
		{
			transform.rotation = 
				Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y + _rotateSpeed * Time.deltaTime, 0));
		}
	}
}