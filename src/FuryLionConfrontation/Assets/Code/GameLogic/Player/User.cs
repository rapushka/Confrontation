using UnityEngine;

namespace Confrontation
{
	[CreateAssetMenu(fileName = "User", menuName = nameof(Confrontation) + "/User", order = 0)]
	public class User : ScriptableObject
	{
		[field: SerializeField] public Player Player { get; private set; }
	}
}