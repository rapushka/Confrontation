using System.Collections.Generic;
using Zenject;

namespace Confrontation
{
	public abstract class ConditionalInfluencer : InfluencerBase { }

	public class OnMovingUnitsInfluencer : ConditionalInfluencer
	{
	}
}