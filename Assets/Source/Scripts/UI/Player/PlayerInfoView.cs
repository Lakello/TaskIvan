using TaskIvan.Messages;
using TMPro;
using UniRx;
using UnityEngine;

namespace TaskIvan.UI.Player
{
	public class PlayerInfoView : MonoBehaviour
	{
		[SerializeField] private TMP_Text _output;
		[SerializeField] private MessageId _targetMessageId;

		private void Awake()
		{
			MessageBroker.Default
				.Receive<Message<float>>()
				.Where(message => message.Id == _targetMessageId)
				.Subscribe(message => Show(message.Data))
				.AddTo(this);
		}

		private void Show(float height) =>
			_output.text = height.ToString("0.0");
	}
}