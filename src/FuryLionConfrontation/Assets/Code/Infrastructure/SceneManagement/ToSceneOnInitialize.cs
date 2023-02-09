using Zenject;

namespace Confrontation
{
	public abstract class ToSceneOnInitialize : ToSceneBase, IInitializable
	{
		public void Initialize() => Transfer();
	}
}