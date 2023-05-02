using UnityEngine;

namespace Confrontation
{
	public interface IAssetsService : IDestroyService
	{
		T Instantiate<T>(T original, Transform parent) where T : Object;

		T Instantiate<T>(T original, InstantiateGroup group = InstantiateGroup.Common) where T : Object;

		void ToGroup(Transform transform, InstantiateGroup group = InstantiateGroup.Common);
	}
}