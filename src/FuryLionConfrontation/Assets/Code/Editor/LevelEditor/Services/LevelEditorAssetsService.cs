using UnityEngine;

namespace Confrontation.Editor
{
	public class LevelEditorAssetsService : IAssetsService
	{
		private Transform _root;

		public LevelEditorAssetsService() => CreateRoot();

		public GameObject Instantiate(string name)
		{
			var gameObject = new GameObject(name);
			gameObject.transform.SetParent(_root);
			return gameObject;
		}

		public T Instantiate<T>(T original, Transform parent)
			where T : Object
			=> Object.Instantiate(original, parent);

		public T Instantiate<T>(T original, Vector3 position, InstantiateGroup group = InstantiateGroup.Common)
			where T : Object
			=> Object.Instantiate(original, position, Quaternion.identity, _root);

		public T Instantiate<T>(T original, InstantiateGroup group = InstantiateGroup.Common)
			where T : Object
			=> Object.Instantiate(original, _root);

		public void Destroy(GameObject target) => Object.DestroyImmediate(target);

		public void CleanRoot()
		{
			Destroy(_root.gameObject);
			CreateRoot();
		}

		private void CreateRoot() => _root = new GameObject("Root").transform;
	}
}