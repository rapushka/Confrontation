using Zenject;
using Object = UnityEngine.Object;

namespace Confrontation
{
	public class BuildWindow : WindowBase
	{
		[Inject] private readonly IResourcesService _resources;
		[Inject] private readonly BuildingButton.Factory _buildingButtonFactory;

		private void Start()
		{
			foreach (var buildButton in _resources.Buildings)
			{
				_buildingButtonFactory.Create(buildButton);
			}
		}

		public override WindowBase Accept(IWindowVisitor windowVisitor) => windowVisitor.Visit(this);

		public new class Factory : PlaceholderFactory<Object, BuildWindow> { }
	}
}