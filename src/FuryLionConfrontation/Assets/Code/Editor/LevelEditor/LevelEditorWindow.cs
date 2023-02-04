using UnityEditor;
using UnityEngine;
using Zenject;

namespace Confrontation.Editor
{
	public class LevelEditorWindow : ZenjectEditorWindow
	{
		[SerializeField] private ConfigurableField.State _fieldState;
		[SerializeField] private PlayersConfigurator.State _playersState;

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

			Container.Bind<IResourcesService>().FromInstance(resourcesService).AsSingle();

			Container.Bind<FieldGenerator>().AsSingle();
			Container.BindInterfacesTo<LevelEditor>().AsSingle();
			Container.BindInterfacesAndSelfTo<LevelEditorAssetsService>().AsSingle();
			Container.BindInterfacesTo<ConfigurableField>().AsSingle();
			Container.BindInterfacesAndSelfTo<PlayersConfigurator>().AsSingle();
			Container.BindInterfacesTo<BuildingsCreator>().AsSingle();
			Container.BindInterfacesTo<CellsRegionGizmoDrawer>().AsSingle();

			Container.BindInstance(_fieldState);
			Container.BindInstance(_playersState);
		}
	}
}