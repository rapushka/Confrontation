using TMPro;
using UnityEngine;

namespace Confrontation
{
	public abstract class View<T> : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _textMesh;

		private T _value;

		public T Value
		{
			get => _value;
			set
			{
				_value = value;
				_textMesh.text = ValueToString;
			}
		}

		private void OnValidate() => Value = default;

		protected virtual string ValueToString => _value.ToString();
	}
}