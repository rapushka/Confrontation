using UnityEngine;

namespace Confrontation.Editor
{
	public class LevelEditorAssetsService : IAssetsService
	{
		public GameObject Instantiate(string name) { throw new System.NotImplementedException();}

		public T Instantiate<T>(T original, Transform parent)
			where T : Object { throw new System.NotImplementedException();}

		public T Instantiate<T>(T original, Vector3 position, InstantiateGroup group = InstantiateGroup.Common)
			where T : Object { throw new System.NotImplementedException();}

		public T Instantiate<T>(T original, InstantiateGroup group = InstantiateGroup.Common)
			where T : Object { throw new System.NotImplementedException();}

		public void Destroy(GameObject target)
		{
			throw new System.NotImplementedException();
		}
	}
}