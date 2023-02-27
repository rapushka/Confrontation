using UnityEngine;
using Zenject;

namespace Confrontation.Editor
{
	public class LevelEditorAssetsService : IInitializable, IAssetsService
	{
		private Transform _root;

		public void Initialize()
		{
			_root = GameObject.Find(nameof(_root))?.transform;
			if (_root == false)
			{
				CreateRoot();
			}
		}

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
		public void ToGroup(Transform transform, InstantiateGroup group = InstantiateGroup.Common) 
			=> transform.SetParent(_root);


		public void CleanRoot()
		{
			Destroy(_root.gameObject);
			CreateRoot();
		}

		private void CreateRoot() => _root = new GameObject(nameof(_root)).transform;
	}
}