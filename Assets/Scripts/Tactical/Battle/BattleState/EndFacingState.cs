using UnityEngine;
using Tactical.Core.Enums;
using Tactical.Core.Extensions;
using Tactical.Core.EventArgs;
using Tactical.Grid.Model;

namespace Tactical.Battle.BattleState {

	public class EndFacingState : BattleState {

		private Direction startDir;

		public override void Enter () {
			base.Enter();

			startDir = turn.actor.dir;
			SelectTile(turn.actor.tile.pos);

			// Move and show the unit direction indicator.
			MoveUnitDirectionIndicator(turn.actor.tile.pos);
			unitDirectionController.Show(startDir);
		}

		public override void Exit () {
			base.Exit();
			unitDirectionController.Hide();
		}

		protected override void OnMove (object sender, InfoEventArgs<Point> e) {
			turn.actor.dir = e.info.GetDirection();
			unitDirectionController.SetDirection(turn.actor.dir);
			turn.actor.Match();
		}

		protected override void OnFire (object sender, InfoEventArgs<int> e) {
			switch (e.info) {
				case 0:
					owner.ChangeState<SelectUnitState>();
					break;
				case 1:
					turn.actor.dir = startDir;
					turn.actor.Match();
					owner.ChangeState<CommandSelectionState>();
					break;
				}
		}

		private void MoveUnitDirectionIndicator (Point p) {
			if (!board.tiles.ContainsKey(p)) {
				return;
			}

			unitDirectionController.SetPosition(board.tiles[p].center);
		}

	}

}
