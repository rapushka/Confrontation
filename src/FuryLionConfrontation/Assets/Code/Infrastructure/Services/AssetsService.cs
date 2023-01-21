using UnityEngine;

namespace Confrontation
{
	public interface IAssetsService
	{
		T Instantiate<T>(T original, Transform parent) where T : Object;

		GameObject Instantiate(string name);
	}

	public class AssetsService : IAssetsService
	{
		public T Instantiate<T>(T original, Transform parent)
			where T : Object
			=> Object.Instantiate(original, parent);

		public GameObject Instantiate(string name) => new(name);
	}
}