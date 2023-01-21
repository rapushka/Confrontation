using UnityEngine;

namespace Confrontation
{
	public interface IAssetsService
	{
		T Instantiate<T>(T original, Transform parent) where T : Object;
	}

	public class AssetsService : IAssetsService
	{
		public T Instantiate<T>(T original, Transform parent)
			where T : Object
			=> Object.Instantiate(original, parent);
	}
}