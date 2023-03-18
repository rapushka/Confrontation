using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Confrontation
{
	public class FieldSizesPanel : MonoBehaviour
	{
		[Inject] private readonly ConfigurableField _configurableField;

		[SerializeField] private TMP_InputField _widthInputField;
		[SerializeField] private TMP_InputField _heightInputField;
		[SerializeField] private Button _regenerateButton;

		private int Width => int.Parse(_widthInputField.text);

		private int Height => int.Parse(_heightInputField.text);

		private void OnEnable() => _regenerateButton.onClick.AddListener(RegenerateField);

		private void OnDisable() => _regenerateButton.onClick.RemoveListener(RegenerateField);

		private void Start()
		{
			_widthInputField.text = _configurableField.Sizes.Width.ToString();
			_heightInputField.text = _configurableField.Sizes.Height.ToString();
		}

		private void RegenerateField()
		{
			_configurableField.Sizes = new Sizes(Width, Height);
		}
	}
}