using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class FieldClickEmit : IInitializable
	{
		[Inject] private readonly Field _field;

		public void Initialize() => _field.Cells.ForEach((c) => c.MouseClick += OnCellMouseClick);

		private void OnCellMouseClick(Coordinates coordinates) => Debug.Log("coordinates = " + coordinates);
	}
}