using System;
using UnityEngine;
using UnityEngine.UI;

namespace Confrontation
{
	public class EnsureWindow : WindowBase
	{
		[SerializeField] private Button _yesButton;
		[SerializeField] private Button _noButton;

		private Answer _answer = Answer.None;

		public event Action<Answer> WindowClosed;

		private void OnEnable()
		{
			_yesButton.onClick.AddListener(AnswerYes);
			_noButton.onClick.AddListener(AnswerNo);
		}

		private void OnDisable()
		{
			_yesButton.onClick.RemoveListener(AnswerYes);
			_noButton.onClick.RemoveListener(AnswerNo);
		}

		public override void Open()
		{
			_answer = Answer.None;
			base.Open();
		}

		public override void Close()
		{
			base.Close();
			WindowClosed?.Invoke(_answer);
		}

		private void AnswerYes()
		{
			_answer = Answer.Yes;
			Close();
		}

		private void AnswerNo()
		{
			_answer = Answer.No;
			Close();
		}
	}
}