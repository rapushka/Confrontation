using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class KalymHud : MonoBehaviour
	{
		[Inject] private readonly IProgressionStorageService _progressionStorage;
		[Inject] private readonly Progression _progression;

		[SerializeField] private IntPrefixView _kalymView;

		private PlayerProgress PlayerProgress => _progressionStorage.LoadProgress();

		private void OnEnable()  => _progression.KalymValueChanged += UpdateView;
		private void OnDisable() => _progression.KalymValueChanged -= UpdateView;

		private void Start() => UpdateView();

		private void UpdateView() => _kalymView.Value = PlayerProgress.KalymCount;
	}
}