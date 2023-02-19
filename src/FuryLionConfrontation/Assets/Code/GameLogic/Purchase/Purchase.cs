using System;
using UnityEngine;
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
			var buildingPrice = building switch
			{
				Barracks   => _balanceTable.Barrack.Price,
				GoldenMine => _balanceTable.GoldenMine.Price,
				var _      => throw new ArgumentException(),
			};

			if (_user.Player.Stats.IsEnoughGoldFor(buildingPrice))
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