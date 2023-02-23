using Zenject;

namespace Confrontation
{
	public class Purchase
	{
		[Inject] private readonly User _user;
		[Inject] private readonly GameplayUiMediator _uiMediator;
		[Inject] private readonly IBalanceTable _balanceTable;


		public void BuyBuilding(Building building)
		{
			var buildingPrice = _balanceTable.PriceFor(building);

			if (_user.Player.Stats.IsEnoughGoldFor(buildingPrice))
			{
				_uiMediator.Build(building);
				_uiMediator.CloseCurrentWindow();
				_user.Player.Stats.Spend(buildingPrice);
			}
			else
			{
				_uiMediator.OpenWindow<NotEnoughGoldWindow>();
			}
		}
	}
}