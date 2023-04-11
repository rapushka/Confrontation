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

		[SerializeField] private List<SpellScriptableObject> _spells;
		[SerializeField] private Transform _buttonsRoot;
		[SerializeField] private ToolTip _toolTip;

		private void Start()
			=> _spells
			   .Select((s) => _spellButtonFactory.Create(s, _toolTip))
			   .ForEach((sb) => sb.transform.SetParent(_buttonsRoot));

		public override GameplayWindowBase Accept(IGameplayWindowVisitor visitor) => visitor.Visit(this);

		public new class Factory : PlaceholderFactory<Object, SpellBookWindow> { }
	}
}