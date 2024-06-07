using TaskIvan.Messages;
using TMPro;
using UniRx;
using UnityEngine;

namespace TaskIvan.UI.Player
{
	public class SpeedView : MonoBehaviour
	{
		[SerializeField] private TMP_Text _output;

		private void Awake()
		{
			MessageBroker.Default
				.Receive<Message<float>>()
				.Where(message => message.Id == MessageId.SpeedChanged)
				.Subscribe(message => Show(message.Data))
				.AddTo(this);
		}

		private void Show(float height) =>
			_output.text = height.ToString("0.0");
	}
}