using UnityEngine;

namespace TaskIvan.BonusSystem.Entities
{
	public class SpeedBonus : Bonus
	{
		[SerializeField] private float _multiplier = 1.2f;

		public float Multiplier => _multiplier;
	}
}