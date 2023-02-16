using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Confrontation
{
	public class BuildWindow : WindowBase
	{
		[Inject] private readonly IResourcesService _resources;
		[Inject] private readonly BuildingButton.Factory _buildingButtonFactory;

		[SerializeField] private Transform _buttonsRoot;

		private void Start()
		{
			foreach (var building in _resources.Buildings)
			{
				_buildingButtonFactory.Create(building, _buttonsRoot);
			}
		}

		public override WindowBase Accept(IWindowVisitor windowVisitor) => windowVisitor.Visit(this);

		public new class Factory : PlaceholderFactory<Object, BuildWindow> { }
	}
}