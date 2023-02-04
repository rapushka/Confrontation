using UnityEditor;
using UnityEngine;
using Zenject;

namespace Confrontation.Editor
{
	public class LevelEditorWindow : ZenjectEditorWindow
	{
		[SerializeField] private LevelEditor.State _state;

		private GameObject _rootForLevel;

		[MenuItem("Tools/" + nameof(Confrontation) + "/Level Editor")]
		private static void ShowWindow()
		{
			var window = GetWindow<LevelEditorWindow>();
			window.titleContent = new GUIContent(nameof(LevelEditorWindow));
			window.Show();
		}

		// ReSharper disable Unity.PerformanceAnalysis
		public override void InstallBindings()
		{
			var resourcesService = Resources.Load<ResourcesService>("ScriptableObjects/Resources");

			Container.Bind<FieldGenerator>().AsSingle();
			Container.Bind<IResourcesService>().FromInstance(resourcesService).AsSingle();
			Container.Bind<IAssetsService>().To<AssetsService>().AsSingle();
			Container.Bind<IField>().FromSubstitute().AsSingle();

			Container.BindInstance(_state);

			Container.BindInterfacesTo<LevelEditor>().AsSingle();
		}
	}
}