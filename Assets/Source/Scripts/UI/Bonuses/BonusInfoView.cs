using DG.Tweening;
using TaskIvan.Messages;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace TaskIvan.UI.Bonuses
{
	public class BonusInfoView : MonoBehaviour
	{
		[SerializeField] private Image _image;
		[SerializeField] private MessageId _targetMessageId;

		private Tweener _cooldownTweener;

		private void Awake()
		{
			_image.fillAmount = 0;

			MessageBroker.Default
				.Receive<Message<float>>()
				.Where(message => message.Id == _targetMessageId)
				.Subscribe(message => OnBonusCollected(message.Data))
				.AddTo(this);
		}

		private void OnBonusCollected(float duration)
		{
			_cooldownTweener?.Kill();
			_image.fillAmount = 1;

			_cooldownTweener = _image.DOFillAmount(0, duration);
		}
	}
}