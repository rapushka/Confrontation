using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Confrontation
{
	public interface IAssetsService
	{
		GameObject Instantiate(string name);

		T Instantiate<T>(T original, Transform parent) where T : Object;

		T Instantiate<T>(T original, InstantiateGroup group = InstantiateGroup.Common) where T : Object;
	}

	public class AssetsService : IAssetsService
	{
		[Inject] private readonly IResourcesService _resources;

		private readonly Dictionary<InstantiateGroup, Transform> _roots = new();

		public GameObject Instantiate(string name) => new(name);

		public T Instantiate<T>(T original, Transform parent) where T : Object => Object.Instantiate(original, parent);

		public T Instantiate<T>(T original, InstantiateGroup group = InstantiateGroup.Common) where T : Object
			=> Object.Instantiate(original, GetTransformFor(group));

		private Transform GetTransformFor(InstantiateGroup group)
		{
			if (_roots.ContainsKey(group) == false
			    || _roots[group] == false)
			{
				_roots.Remove(group);
				_roots.Add(group, Instantiate(group));
			}

			return _roots[group];
		}

		private Transform Instantiate(InstantiateGroup group)
		{
			var gameObject = Instantiate(ToName(group));
			return group is InstantiateGroup.Windows
				? Instantiate(_resources.Canvas)
				: gameObject.transform;
		}

		private static string ToName(InstantiateGroup group) => $"{group.ToString()} Root";
	}
}