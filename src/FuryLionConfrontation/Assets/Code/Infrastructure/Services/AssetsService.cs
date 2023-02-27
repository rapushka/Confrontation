using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Confrontation
{
	public class AssetsService : IAssetsService
	{
		private readonly Dictionary<InstantiateGroup, Transform> _roots = new();

		public T Instantiate<T>(T original, Transform parent) where T : Object => Object.Instantiate(original, parent);

		public T Instantiate<T>(T original, InstantiateGroup group = InstantiateGroup.Common)
			where T : Object
			=> Object.Instantiate(original, GetTransformFor(group));

		public void Destroy(GameObject target) => Object.Destroy(target);

		public void ToGroup(Transform transform, InstantiateGroup group = InstantiateGroup.Common)
			=> transform.SetParent(GetTransformFor(group));

		private Transform GetTransformFor(InstantiateGroup group)
		{
			if (_roots.ContainsKey(group) == false
			    || _roots[group] == false)
			{
				_roots.Remove(group);
				_roots.Add(group, new GameObject(ToName(group)).transform);
			}

			return _roots[group];
		}

		private static string ToName(InstantiateGroup group) => $"{group.ToString()} Root";
	}
}