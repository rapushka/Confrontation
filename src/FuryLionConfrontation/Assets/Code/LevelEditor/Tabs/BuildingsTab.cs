using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class BuildingsTab : LevelEditorPage
	{
		[Inject] private readonly BuildingButton.Factory _buildingButtonFactory;

		
		[SerializeField] private Transform _buildingButtonsRoot;

		public override void Handle(Cell clickedCell) { }
	}
}