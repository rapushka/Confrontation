using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Confrontation.Editor
{
	public static class RegionsConfigurationExtensions
	{
		private static float HorizontalStep => 50;
		private static float VerticalStep   => EditorGUIUtility.singleLineHeight;

		public static void Draw(this Rect rect, Village village)
		{
			rect.DrawVillagePosition(village);
			rect.DrawPlayerOwner(village);
			rect.DrawCellsHeader();
			DrawCellsElements();
			rect.DrawSelectedIndicator(village);
		}

		private static void DrawVillagePosition(this Rect rect, Village village)
		{
			var position = village.RelatedCell.transform.position;
			EditorGUI.LabelField(rect, $"{village.GetType().Name.Pretty()} ({position.x:F2}; {position.z:F2})");
		}

		private static void DrawPlayerOwner(this Rect rect, Village village)
		{
			rect.y += VerticalStep;
			rect.x += HorizontalStep;

			EditorGUI.LabelField(rect, "Player Owner: ");
			rect.x += HorizontalStep * 2;
			rect.width = HorizontalStep;

			village.RelatedCell.OwnerPlayerId = EditorGUI.IntField(rect, village.RelatedCell.OwnerPlayerId);
		}

		private static void DrawCellsHeader(this Rect rect)
		{
			rect.y += VerticalStep * 2;
			rect.x += HorizontalStep;

			EditorGUI.LabelField(rect, "CellsInRegion: ");

			rect.width = HorizontalStep;
			rect.x += HorizontalStep * 2;
		}

		private static void DrawCellsElements() { }

		private static void DrawSelectedIndicator(this Rect rect, Village village)
		{
			if (Selection.gameObjects.WithComponent<Cell>().Contains(village.RelatedCell)
			    || Selection.gameObjects.WithComponent<Building>().Contains(village))
			{
				rect.x += HorizontalStep * 4;
				EditorGUI.LabelField(rect, "<- Selected");
			}
		}
	}
}