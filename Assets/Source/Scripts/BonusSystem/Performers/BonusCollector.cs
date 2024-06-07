using System;
using TaskIvan.BonusSystem.Entities;
using UnityEngine;

namespace TaskIvan.BonusSystem
{
	public class BonusCollector : MonoBehaviour
	{
		public event Action<Bonus> Collected;

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out Bonus bonus))
			{
				bonus.Collect();
				Collected?.Invoke(bonus);
			}
		}
	}
}