using Zenject;

namespace Confrontation
{
	public class BuildingButton : ButtonBase
	{
		[Inject] private readonly Building _building;
		[Inject] private readonly UiMediator _uiMediator;

		protected override void OnButtonClick() => _uiMediator.Build(_building);

		public class Factory : PlaceholderFactory<Building, BuildingButton> { }
	}
}