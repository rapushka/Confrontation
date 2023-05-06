using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class TutorialWindow : GameplayWindowBase
	{
		[Inject] private readonly TimeStopService _timeStopService;
		[Inject] private readonly ILevelSelector _levelSelector;

		[SerializeField] private RectTransform _pageRoot;

		public override void Open()
		{
			_timeStopService.Stop();
			LoadTutorialPages();
			base.Open();
		}

		private void LoadTutorialPages()
		{
			foreach (var pagePrefab in _levelSelector.SelectedLevel.TutorialPages)
			{
				Instantiate(pagePrefab, _pageRoot.transform);
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