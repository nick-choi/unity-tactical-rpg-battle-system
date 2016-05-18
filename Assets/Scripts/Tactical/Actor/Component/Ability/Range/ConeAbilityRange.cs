using UnityEngine;
using System.Collections.Generic;
using Tactical.Core.Enums;
using Tactical.Grid.Model;
using Tactical.Grid.Component;
using Tactical.Battle.Component;

namespace Tactical.Actor.Component {

	public class ConeAbilityRange : AbilityRange {

		public override bool directionOriented { get { return true; }}

		public override List<Tile> GetTilesInRange (Board board) {
			Point pos = unit.tile.pos;
			List<Tile> retValue = new List<Tile>();
			int dir = (unit.dir == Direction.North || unit.dir == Direction.East) ? 1 : -1;
			int lateral = 1;

			if (unit.dir == Direction.North || unit.dir == Direction.South) {
				for (int y = 1; y <= horizontal; ++y) {
					int min = -(lateral / 2);
					int max = (lateral / 2);
					for (int x = min; x <= max; ++x) {
						Point next = new Point(pos.x + x, pos.y + (y * dir));
						Tile tile = board.GetTile(next);
						if (ValidTile(tile)) {
							retValue.Add(tile);
						}
					}
					lateral += 2;
				}
			} else {
				for (int x = 1; x <= horizontal; ++x) {
					int min = -(lateral / 2);
					int max = (lateral / 2);
					for (int y = min; y <= max; ++y) {
						Point next = new Point(pos.x + (x * dir), pos.y + y);
						Tile tile = board.GetTile(next);
						if (ValidTile(tile)) {
							retValue.Add(tile);
						}
					}
					lateral += 2;
				}
			}

			return retValue;
		}

		private bool ValidTile (Tile t) {
			return t != null && Mathf.Abs(t.height - unit.tile.height) <= vertical;
		}

	}

}
