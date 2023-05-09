using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class TutorialWindow : GameplayWindowBase
	{
		[Inject] private readonly TimeStopService _timeStopService;
		[Inject] private readonly ILevelSelector _levelSelector;

		[SerializeField] private RectTransform _pageRoot;
		[SerializeField] private RectTransform _buttonsRoot;
		[SerializeField] private TabsSystem _tabsSystem;
		[SerializeField] private PageButton _buttonPrefab;

		private List<TutorialPage> TutorialPages => _levelSelector.SelectedLevel.TutorialPages;

		public override void Open()
		{
			if (TutorialPages.Any())
			{
				_timeStopService.Stop();
				LoadTutorialPages();
				base.Open();
			}
		}

		private void LoadTutorialPages()
		{
			var pageCounter = 1;
			foreach (var pagePrefab in TutorialPages)
			{
				var page = Instantiate(pagePrefab, _pageRoot);
				var pageButton = Instantiate(_buttonPrefab, _buttonsRoot);

				pageButton.PageNumber = pageCounter++;
				var tab = new Tab(pageButton.Button, page);

				_tabsSystem.AddTab(tab);
			}
		}

		public override void Close()
		{
			base.Close();
			_timeStopService.Resume();
		}

		public override GameplayWindowBase Accept(IGameplayWindowVisitor visitor) => visitor.Visit(this);

		public new class Factory : PlaceholderFactory<Object, TutorialWindow> { }
	}
}