using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class FieldClickEmit : IInitializable
	{
		[Inject] private readonly Field _field;

		public void Initialize() => _field.Cells.ForEach((c) => c.MouseClick += OnCellMouseClick);

		private void OnCellMouseClick(Cell cell)
		{
			const int currentPlayerId = 1;

			Debug.Log($"isBelongToCurrentPlayer = {cell.IsBelongTo(currentPlayerId)}");
		}
	}
}