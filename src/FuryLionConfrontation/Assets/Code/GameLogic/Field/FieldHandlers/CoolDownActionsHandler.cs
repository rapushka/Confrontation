using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class CoolDownActionsHandler : ITickable
	{
		[Inject] private readonly BuildingsGenerator _buildingsGenerator;

		private IEnumerable<IActorWithCoolDown> ActorsWithCoolDown
			=> _buildingsGenerator.Buildings.OfType<IActorWithCoolDown>();

		public void Tick()
		{
			var deltaTime = Time.deltaTime;
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