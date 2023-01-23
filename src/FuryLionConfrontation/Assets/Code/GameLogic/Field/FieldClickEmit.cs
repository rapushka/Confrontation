using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class FieldClickEmit : IInitializable
	{
		[Inject] private readonly Field _field;
		[Inject] private readonly User _user;

		public void Initialize() => _field.Cells.ForEach((c) => c.MouseClick += OnCellMouseClick);

		private void OnCellMouseClick(Cell cell)
		{
			Debug.Log($"isBelongToCurrentPlayer = {cell.IsBelongTo(_user.Player.Id)}");
		}
	}
}