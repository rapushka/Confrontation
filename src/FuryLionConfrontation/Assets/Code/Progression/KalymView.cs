using TMPro;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class KalymView : MonoBehaviour
	{
		[Inject] private readonly IProgressionStorageService _progression;

		[SerializeField] private TextMeshProUGUI _textMesh;

		private PlayerProgress PlayerProgress => _progression.LoadProgress();

		private void Start() => _textMesh.text = PlayerProgress.KalymCount.ToString();
	}
}