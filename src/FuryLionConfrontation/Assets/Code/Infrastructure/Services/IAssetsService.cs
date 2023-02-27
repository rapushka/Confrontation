using UnityEngine;

namespace Confrontation
{
	public interface IAssetsService
	{
		GameObject Instantiate(string name);

		T Instantiate<T>(T original, Transform parent) where T : Object;

		T Instantiate<T>(T original, Vector3 position, InstantiateGroup group = InstantiateGroup.Common)
			where T : Object;

		T Instantiate<T>(T original, InstantiateGroup group = InstantiateGroup.Common) where T : Object;

		void Destroy(GameObject target);

		void ToGroup(Transform transform, InstantiateGroup group = InstantiateGroup.Common);
	}
}