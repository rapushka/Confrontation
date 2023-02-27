using UnityEngine;

namespace Confrontation
{
	public interface IAssetsService
	{
		T Instantiate<T>(T original, Transform parent) where T : Object;

		T Instantiate<T>(T original, InstantiateGroup group = InstantiateGroup.Common) where T : Object;

		void Destroy(GameObject target);

		void ToGroup(Transform transform, InstantiateGroup group = InstantiateGroup.Common);
	}
}