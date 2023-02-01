using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Confrontation
{
	public class CoolDownActionsHandler : ITickable
	{
		[Inject] private readonly BuildingsGenerator _buildingsGenerator;
		[Inject] private readonly ITimeService _timeService;

		private IEnumerable<IActorWithCoolDown> ActorsWithCoolDown
			=> _buildingsGenerator.Buildings.OfType<IActorWithCoolDown>();

		public void Tick()
		{
			var deltaTime = _timeService.DeltaTime;
			foreach (var actor in ActorsWithCoolDown)
			{
				actor.PassedDuration += deltaTime;

				if (actor.PassedDuration >= actor.CoolDownDuration)
				{
					actor.Action();
					actor.PassedDuration = 0f;
				}
			}
		}
	}
}