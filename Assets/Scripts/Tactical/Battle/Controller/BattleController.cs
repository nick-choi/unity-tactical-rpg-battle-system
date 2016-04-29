using UnityEngine;
using System.Collections.Generic;
using Tactical.Core.StateMachine;
using Tactical.Grid.Model;
using Tactical.Grid.Component;
using Tactical.Unit.Component;
using Tactical.Battle.Model;
using Tactical.Battle.Component;
using Tactical.Battle.BattleState;
using Tactical.UI.Controller;

namespace Tactical.Battle.Controller {

	public class BattleController : StateMachine {

		public CameraRig cameraRig;
		public Board board;
		public LevelData levelData;
		public Transform tileSelectionIndicator;
		public Point pos;
		public GameObject heroPrefab;
		public Tile currentTile { get { return board.GetTile(pos); }}
		public ActionMenuPanelController actionMenuPanelController;
		public Turn turn = new Turn();
		public List<UnitCore> units = new List<UnitCore>();
		public StatPanelController statPanelController;

		private void Start () {
			ChangeState<InitBattleState>();
		}
	}

}
