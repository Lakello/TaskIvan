using System;
using System.Collections;
using System.Collections.Generic;
using TaskIvan.BonusSystem.Entities;
using TaskIvan.Player;
using UniRx;
using UnityEngine;

namespace TaskIvan.BonusSystem.Services
{
	public class BonusService : IDisposable
	{
		private readonly Dictionary<Type, (Bonus, IDisposable)> _bonuses = new Dictionary<Type, (Bonus, IDisposable)>();
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
			if (_bonuses.ContainsKey(bonus.GetType()))
			{
				var effectDisposable = _bonuses[bonus.GetType()].Item2;
				effectDisposable.Dispose();
				_bonuses[bonus.GetType()] = (bonus, Observable.FromCoroutine(() => PlayEffect(bonus)).Subscribe());
			}
			else
			{
				_bonuses.Add(bonus.GetType(), (bonus, Observable.FromCoroutine(() => PlayEffect(bonus)).Subscribe()));
			}
		}

		private IEnumerator PlayEffect(Bonus bonus)
		{
			yield return new WaitForSeconds(bonus.Duration);
			_bonuses.Remove(bonus.GetType());
		}
	}
}