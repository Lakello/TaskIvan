using System;
using System.Collections;
using System.Collections.Generic;
using TaskIvan.BonusSystem.Entities;
using TaskIvan.Player;
using TaskIvan.Utils;
using UnityEngine;

namespace TaskIvan.BonusSystem.Services
{
	public class BonusService : IDisposable
	{
		private readonly Dictionary<Type, (Bonus, Coroutine)> _bonuses = new Dictionary<Type, (Bonus, Coroutine)>();
		private readonly BonusCollector _bonusCollector;

		public BonusService(PlayerEntity playerEntity)
		{
			_bonusCollector = playerEntity.GetComponentInChildren<BonusCollector>();

			_bonusCollector.Collected += OnCollected;
		}

		public TBonus TryGetBonus<TBonus>()
			where TBonus : Bonus
		{
			if (_bonuses.TryGetValue(typeof(TBonus), out var bonusData))
			{
				return (TBonus)bonusData.Item1;
			}

			return null;
		}

		public void Dispose()
		{
			_bonusCollector.Collected -= OnCollected;
		}

		private void OnCollected(Bonus bonus)
		{
			bonus.gameObject.SetActive(false);

			if (_bonuses.ContainsKey(bonus.GetType()))
			{
				var effectCoroutine = _bonuses[bonus.GetType()].Item2;
				CoroutineHolder.Instance.Stop(effectCoroutine);
				_bonuses[bonus.GetType()] = (bonus, CoroutineHolder.Instance.Play(PlayEffect(bonus)));
			}
			else
			{
				_bonuses.Add(bonus.GetType(), (bonus, CoroutineHolder.Instance.Play(PlayEffect(bonus))));
			}

			CoroutineHolder.Instance.Play(BonusCooldown(bonus));
		}

		private IEnumerator PlayEffect(Bonus bonus)
		{
			yield return new WaitForSeconds(bonus.Duration);
			_bonuses.Remove(bonus.GetType());
		}

		private IEnumerator BonusCooldown(Bonus bonus)
		{
			yield return new WaitForSeconds(bonus.Cooldown);

			bonus.gameObject.SetActive(true);
		}
	}
}