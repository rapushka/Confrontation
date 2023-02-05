using UnityEditor;
using UnityEngine;
using Zenject;

namespace Confrontation.Editor
{
	public class LevelEditorWindow : ZenjectEditorWindow
	{
		[SerializeField] private ConfigurableField.State _fieldState;
		[SerializeField] private PlayersConfigurator.State _playersState;
		[SerializeField] private RegionsConfigurator.State _regionsState;

		[MenuItem("Tools/" + nameof(Confrontation) + "/Level Editor")]
		private static void ShowWindow()
		{
			var window = GetWindow<LevelEditorWindow>();
			window.titleContent = new GUIContent(nameof(LevelEditorWindow));
			window.Show();
		}

		// ReSharper disable Unity.PerformanceAnalysis
		public override void OnGUI()
		{
			base.OnGUI();

			GUILayout.Button(nameof(SaveAll).Pretty()).OnClick(SaveAll);
		}

		private static void SaveAll() => FindObjectsOfType<MonoBehaviour>().ForEach(EditorUtility.SetDirty);

		// ReSharper disable Unity.PerformanceAnalysis
		public override void InstallBindings()
		{
			var resourcesService = Resources.Load<ResourcesService>("ScriptableObjects/Resources");

			Container.Bind<IResourcesService>().FromInstance(resourcesService).AsSingle();
			Container.Bind<FieldGenerator>().AsSingle();
			Container.BindInterfacesAndSelfTo<LevelEditorAssetsService>().AsSingle();

			Container.BindInterfacesTo<LevelEditor>().AsSingle();
			Container.BindInterfacesTo<ConfigurableField>().AsSingle();
			Container.BindInterfacesAndSelfTo<PlayersConfigurator>().AsSingle();
			Container.BindInterfacesTo<BuildingsCreator>().AsSingle();
			Container.BindInterfacesTo<RegionsConfigurator>().AsSingle();

			Container.BindInterfacesTo<CellsPlayerGizmoDrawer>().AsSingle();

			Container.BindInstance(_fieldState);
			Container.BindInstance(_playersState);
			Container.BindInstance(_regionsState);
		}
	}
}