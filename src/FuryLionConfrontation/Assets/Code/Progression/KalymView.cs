using TMPro;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class KalymView : MonoBehaviour
	{
		[Inject] private readonly IProgressionStorageService _progressionStorage;
		[Inject] private readonly Progression _progression;

		[SerializeField] private TextMeshProUGUI _textMesh;

		private PlayerProgress PlayerProgress => _progressionStorage.LoadProgress();

		private void OnEnable()  => _progression.KalymValueChanged += UpdateView;
		private void OnDisable() => _progression.KalymValueChanged -= UpdateView;

		private void Start() => UpdateView();

		private void UpdateView() => _textMesh.text = PlayerProgress.KalymCount.ToString();
	}
}