using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Confrontation
{
	public class SpellBookWindow : GameplayWindowBase
	{
		[Inject] private readonly SpellButton.Factory _spellButtonFactory;
		[Inject] private readonly TimeStopService _timeStopService;
		[Inject] private readonly IProgressionStorageService _progression;

		[SerializeField] private List<SpellScriptableObject> _spells;
		[SerializeField] private Transform _buttonsRoot;
		[SerializeField] private ToolTip _toolTip;

		private float _spellsCounter;

		private int LearnedSpellsCount => _progression.LoadProgress().LearnedSpellsCount;

		private void Start()
			=> _spells
			   .Select((s) => _spellButtonFactory.Create(s, _toolTip, LearnedSpellsCount > _spellsCounter++))
			   .ForEach((sb) => sb.transform.SetParent(_buttonsRoot));

		public override void Open()
		{
			_toolTip.Hide();

			_timeStopService.Stop();
			base.Open();
		}

		public override void Close()
		{
			base.Close();
			_timeStopService.Resume();
		}

		public override GameplayWindowBase Accept(IGameplayWindowVisitor visitor) => visitor.Visit(this);

		public new class Factory : PlaceholderFactory<Object, SpellBookWindow> { }
	}
}