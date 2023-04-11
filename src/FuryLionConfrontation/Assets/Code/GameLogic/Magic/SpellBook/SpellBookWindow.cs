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

		private void Start()
			=> _spells
			   .Select(_spellButtonFactory.Create)
			   .ForEach((sb) => sb.transform.SetParent(_buttonsRoot));

		public override GameplayWindowBase Accept(IGameplayWindowVisitor visitor) => visitor.Visit(this);

		public new class Factory : PlaceholderFactory<Object, SpellBookWindow> { }
	}
}