using UnityEngine;

namespace Confrontation
{
	[CreateAssetMenu(fileName = "Player", menuName = nameof(Confrontation) + "/Player", order = 0)]
	public class User : ScriptableObject
	{
		[field: SerializeField] public Player Player        { get; private set; }
		[field: SerializeField] public Level  SelectedLevel { get; private set; }
	}
}