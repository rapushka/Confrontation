using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Confrontation.Editor
{
	public static class RegionsConfigurationExtensions
	{
		public static void Draw(this Rect rect, Village village)
		{
			rect.DrawVillagePosition(village);
			rect.DrawPlayerOwner(village);
			rect.DrawCellsHeader(village);
			rect.DrawCellsElements(village);
			rect.DrawSelectedIndicator(village);
		}

		private static void DrawVillagePosition(this Rect rect, Village village)
		{
			var position = village.RelatedCell.transform.position;
			EditorGUI.LabelField(rect, $"{village.GetType().Name.Pretty()} ({position.x:F2}; {position.z:F2})");
		}

		private static void DrawPlayerOwner(this Rect rect, Village village)
		{
			rect.y += EditorGUIUtility.singleLineHeight;
			rect.x += 50;

			EditorGUI.LabelField(rect, "Player Owner: ");
			rect.x += 100;
			rect.width = 50;

			village.OwnerPlayerId = EditorGUI.IntField(rect, village.OwnerPlayerId);
		}

		private static void DrawCellsHeader(this Rect rect, Village village)
		{
			rect.y += EditorGUIUtility.singleLineHeight;
			rect.y += EditorGUIUtility.singleLineHeight;
			rect.x += 50;

			EditorGUI.LabelField(rect, $"{nameof(village.CellsInRegion)}: ");

			rect.width = 50;
			rect.x += 100;

			var newLength = EditorGUI.IntField(rect, village.CellsInRegion.Count);
			village.CellsInRegion.Resize(newLength, null);
		}

		private static void DrawCellsElements(this Rect rect, Village village)
		{
			rect.x += 50;
			rect.y += EditorGUIUtility.singleLineHeight;
			rect.y += EditorGUIUtility.singleLineHeight;

			for (var i = 0; i < village.CellsInRegion.Count; i++)
			{
				var cell = village.CellsInRegion[i];
				rect.y += EditorGUIUtility.singleLineHeight;
				rect.width = 150;
				village.CellsInRegion[i] = cell.AsObjectField(rect);

				if (cell == true)
				{
					cell.RelatedRegion = village;
				}
			}
		}

		private static void DrawSelectedIndicator(this Rect rect, Village village)
		{
			if (Selection.gameObjects.WithComponent<Cell>().Contains(village.RelatedCell)
			    || Selection.gameObjects.WithComponent<Building>().Contains(village))
			{
				rect.x += 200;
				EditorGUI.LabelField(rect, "<- Selected");
			}
		}
	}
}