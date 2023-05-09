using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class PauseWindow : GameplayWindowBase
	{
		public override GameplayWindowBase Accept(IGameplayWindowVisitor visitor) => visitor.Visit(this);

		public new class Factory : PlaceholderFactory<Object, PauseWindow> { }
	}
}