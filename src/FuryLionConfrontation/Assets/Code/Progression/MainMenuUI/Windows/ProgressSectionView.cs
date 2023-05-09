using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Confrontation
{
	public class ProgressSectionView : MonoBehaviour
	{
		[Inject] private readonly ProgressionManipulator _progressionManipulator;
		[Inject] private readonly ISceneTransferService _sceneTransfer;

		private const string Code = "gimmeall";

		[SerializeField] private Button _resetProgressionButton;
		[SerializeField] private Button _enterCodeButton;
		[SerializeField] private TMP_InputField _codeField;
		[FormerlySerializedAs("_ensureWindo")] [FormerlySerializedAs("_areYouSureWindow")] [SerializeField] private EnsureWindow _ensureWindow;

		private void OnEnable()
		{
			_resetProgressionButton.onClick.AddListener(ResetProgression);
			_enterCodeButton.onClick.AddListener(EnterCode);
			_ensureWindow.WindowClosed += OnEnsureWindowClosed;
		}

		private void OnDisable()
		{
			_resetProgressionButton.onClick.RemoveListener(ResetProgression);
			_enterCodeButton.onClick.RemoveListener(EnterCode);
			_ensureWindow.WindowClosed -= OnEnsureWindowClosed;
		}

		private void ResetProgression() => _ensureWindow.Open();

		private void OnEnsureWindowClosed(Answer answer)
		{
			if (answer is Answer.Yes)
			{
				_progressionManipulator.Reset();
				ToBootstrapScene();
			}
		}

		private void EnterCode()
		{
			if (_codeField.text.Equals(Code, StringComparison.OrdinalIgnoreCase))
			{
				_progressionManipulator.UnlockAll();
				ToBootstrapScene();
			}
		}

		private void ToBootstrapScene() => _sceneTransfer.ToScene(Constants.SceneName.BootstrapScene);
	}
}