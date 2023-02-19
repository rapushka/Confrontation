using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class Purchase
	{
		[Inject] private readonly User _user;
		[Inject] private readonly GameplayUiMediator _uiMediator;

		public void BuyBuilding(Building building)
		{
			if (_user.Player.Stats.IsEnoughGoldFor(building.BalanceData.Price))
			{
				_uiMediator.Build(building);
				_uiMediator.CloseCurrentWindow();
			}
			else
			{
				Debug.Log("You don't have enough gold!");
			}
		}
	}
}