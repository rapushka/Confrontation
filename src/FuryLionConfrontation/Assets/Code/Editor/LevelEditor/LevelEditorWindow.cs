using UnityEditor;
using UnityEngine;
using Zenject;

// ReSharper disable Unity.PerformanceCriticalCodeInvocation
namespace Confrontation.Editor
{
	public class LevelEditorWindow : ZenjectEditorWindow
	{
		// ReSharper disable FieldCanBeMadeReadOnly.Local - https://github.com/modesttree/Zenject#creating-unity-editorwindows-with-zenject
		private ConfigurableField.State _fieldState = new();
		private PlayersConfigurator.State _playersState = new();
		private RegionsConfigurator.State _regionsState = new();
		private Vector2 _scroll;

		[MenuItem("Tools/" + nameof(Confrontation) + "/Level Editor")]
		private static void ShowWindow()
		{
			var window = GetWindow<LevelEditorWindow>();
			window.titleContent = new GUIContent(nameof(LevelEditorWindow));
			window.Show();
		}

		public override void OnGUI()
		{
			_scroll = EditorGUILayout.BeginScrollView(_scroll);
			base.OnGUI();
			EditorGUILayout.EndScrollView();

			GUILayout.Button(nameof(SaveAll).Pretty()).OnClick(SaveAll);
		}

		private static void SaveAll() => FindObjectsOfType<MonoBehaviour>().ForEach(EditorUtility.SetDirty);

		public override void InstallBindings()
		{
			var resourcesService = Resources.Load<ResourcesService>(Constants.ResourcePath.ResourcesService);

			Container.Bind<IResourcesService>().FromInstance(resourcesService).AsSingle();
			Container.Bind<FieldGenerator>().AsSingle();
			Container.BindInterfacesAndSelfTo<LevelEditorAssetsService>().AsSingle();

			Container.BindInterfacesTo<LevelEditor>().AsSingle();
			Container.BindInterfacesTo<ConfigurableField>().AsSingle();
			Container.BindInterfacesAndSelfTo<PlayersConfigurator>().AsSingle();
			Container.BindInterfacesAndSelfTo<BuildingsCreator>().AsSingle();
			Container.BindInterfacesTo<RegionsConfigurator>().AsSingle();

			Container.BindInterfacesTo<CellsPlayerColorSphereGizmoDrawer>().AsSingle();
			Container.BindInterfacesTo<CellsRegionVillageLineGizmoDrawer>().AsSingle();

			Container.BindFactory<Cell, Cell.Factory>();

			Container.BindInstance(_fieldState);
			Container.BindInstance(_playersState);
			Container.BindInstance(_regionsState);
		}
	}
}