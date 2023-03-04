using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Confrontation
{
	public class CoolDownActionsHandler : ITickable
	{
		[Inject] private readonly ITimeService _timeService;
		[Inject] private readonly IField _field;
		[Inject] private readonly ArtificialIntelligence _ai;

		private IEnumerable<IActorWithCoolDown> ActorsWithCoolDown
			=> _field.Buildings.OfType<IActorWithCoolDown>()
			         .Union(_ai.Enemies)
			         .Union(Capitals.SelectMany((c) => c.StashedBuildings));

		private IEnumerable<Capital> Capitals => _field.Buildings.OfType<Capital>();

		public void Tick()
		{
			var deltaTime = _timeService.DeltaTime;
			foreach (var actor in ActorsWithCoolDown)
			{
				if (actor.CoolDownDuration < 0f)
				{
					continue;
				}
				
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