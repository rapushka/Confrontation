using System;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class Purchase
	{
		[Inject] private readonly User _user;
		[Inject] private readonly GameplayUiMediator _uiMediator;
		[Inject] private readonly BalanceTable _balanceTable;

		public void BuyBuilding(Building building)
		{
			throw new NotImplementedException();
			if (_user.Player.Stats.IsEnoughGoldFor(0))
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