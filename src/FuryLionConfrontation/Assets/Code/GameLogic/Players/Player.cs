using System;
using UnityEngine;

namespace Confrontation
{
	public class Player : MonoBehaviour
	{
		[field: SerializeField] public Data Value { get; private set; }

		[Serializable]
		public class Data
		{
			public string Name { get; set; }
		}
	}
}