using UnityEngine;
using UnityEditor;

namespace Confrontation
{
	public static class SaveLoader<T>
		where T : ScriptableObject
	{
		private const string FolderPath = "Assets/Saves";

		private static string ValidFolderPath
		{
			get
			{
				if (AssetDatabase.IsValidFolder(FolderPath) == false)
				{
					AssetDatabase.CreateFolder("Assets", $"Saves/{typeof(T).Name}");
				}

				return FolderPath;
			}
		}

		public static void Save(T data, string fileName)
		{
			var filePath = $"{ValidFolderPath}/{fileName}.asset";
			AssetDatabase.CreateAsset(data, filePath);
			AssetDatabase.SaveAssets();
		}

		public static T Load(string fileName)
		{
			var filePath = $"{ValidFolderPath}/{fileName}.asset";
			var data = AssetDatabase.LoadAssetAtPath<T>(filePath);
			if (data == null)
			{
				Debug.LogError($"Save file not found at {filePath}");
			}

			return data;
		}
	}
}