using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Confrontation.Editor
{
	public static class RegionsConfigurationExtensions
	{
		private static float HorizontalStep => 50;
		private static float VerticalStep   => EditorGUIUtility.singleLineHeight;

		public static void Draw(this Rect rect, Settlement settlement)
		{
			rect.DrawVillagePosition(settlement);
			rect.DrawPlayerOwner(settlement);
			rect.DrawCellsHeader();
			DrawCellsElements();
			rect.DrawSelectedIndicator(settlement);
		}

		private static void DrawVillagePosition(this Rect rect, Settlement settlement)
		{
			var position = settlement.RelatedCell.transform.position;
			EditorGUI.LabelField(rect, $"{settlement.GetType().Name.Pretty()} ({position.x:F2}; {position.z:F2})");
		}

		private static void DrawPlayerOwner(this Rect rect, Settlement settlement)
		{
			rect.y += VerticalStep;
			rect.x += HorizontalStep;

			EditorGUI.LabelField(rect, "Player Owner: ");
			rect.x += HorizontalStep * 2;
			rect.width = HorizontalStep;

			settlement.RelatedCell.OwnerPlayerId = EditorGUI.IntField(rect, settlement.RelatedCell.OwnerPlayerId);
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

		private static void DrawSelectedIndicator(this Rect rect, Settlement settlement)
		{
			if (Selection.gameObjects.WithComponent<Cell>().Contains(settlement.RelatedCell)
			    || Selection.gameObjects.WithComponent<Building>().Contains(settlement))
			{
				rect.x += HorizontalStep * 4;
				EditorGUI.LabelField(rect, "<- Selected");
			}
		}
	}
}