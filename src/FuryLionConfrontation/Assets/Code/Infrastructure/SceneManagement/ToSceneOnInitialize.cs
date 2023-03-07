using Zenject;

namespace Confrontation
{
	public abstract class ToSceneOnInitialize : ToSceneBase, IInitializable
	{
		public async void Initialize() => await Transfer();
	}
}