using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class NotEnoughManaWindow : GameplayWindowBase
	{
		[Inject] private readonly ISoundService _playSound;

		public override void Open()
		{
			_playSound.UiError();
			base.Open();
		}

		public override GameplayWindowBase Accept(IGameplayWindowVisitor visitor) => visitor.Visit(this);

		public new class Factory : PlaceholderFactory<Object, NotEnoughManaWindow> { }
	}
}