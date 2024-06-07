using System.Collections;
using DG.Tweening;
using TaskIvan.Extensions;
using TaskIvan.Messages;
using UniRx;
using UnityEngine;

namespace TaskIvan.BonusSystem.Entities
{
	[RequireComponent(typeof(MeshRenderer))]
	public abstract class Bonus : MonoBehaviour
	{
		[SerializeField] private float _duration = 10f;
		[SerializeField] private float _cooldown = 5;
		[SerializeField] private float _rotateSpeed = 5f;
		[SerializeField] private MessageId _publishMessageId;
		[SerializeField] private float _collectedMaxHeight = 5f;
		[SerializeField] private float _collectedDuration = 1f;

		private float _startHeight;
		private Color _startColor;
		private Color _targetColor;
		private MeshRenderer _meshRenderer;

		public float Duration => _duration;
		public float Cooldown => _cooldown;

		private void Awake()
		{
			_startHeight = transform.position.y;
			gameObject.GetComponentElseThrow(out _meshRenderer);
			_startColor = _meshRenderer.material.color;
			_targetColor = _startColor;
			_targetColor.a = 0;
		}

		private void Update()
		{
			transform.rotation =
				Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y + _rotateSpeed * Time.deltaTime, 0));
		}

		private void OnEnable()
		{
			_meshRenderer.material.SetColor("_Color", _startColor);
			var position = new Vector3(transform.position.x, _startHeight, transform.position.z);
			transform.position = position;
		}

		public void Collect()
		{
			MessageBroker.Default.Publish(new Message<float>(_publishMessageId, _duration));

			DOTween.Sequence()
				.Append(transform.DOMoveY(
					_startHeight + _collectedMaxHeight,
					_collectedDuration))
				.Join(_meshRenderer.material.DOColor(_targetColor, _collectedDuration))
				.OnKill(() => gameObject.SetActive(false));

			Observable.FromCoroutine(BonusCooldown);
		}

		private IEnumerator BonusCooldown()
		{
			yield return new WaitForSeconds(Cooldown);

			gameObject.SetActive(true);
		}
	}
}