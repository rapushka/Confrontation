using Zenject;

namespace Confrontation
{
	public class InjectedLevelSelector : ILevelSelector
	{
		[Inject] private readonly ILevel _level;

		public ILevel SelectedLevel => _level;
	}
}