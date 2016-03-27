using UnityEngine;
using UnityEditor;
using Tactical.Core;
using Tactical.Battle;

namespace Tactical.Editor {

	public class BattleWindow : EditorWindow {

		private BattleManager battleManager;
		private TurnManager turnManager;

		[MenuItem("Window/Tactical/Battle")]
    private static void ShowWindow () {
      EditorWindow.GetWindow(typeof(BattleWindow), false, "Battle");
    }

    private void OnInspectorUpdate () {
			UpdateManagers();
			Repaint();
    }

		private void OnGUI () {
			CreateBattleUI();
		}

		private void UpdateManagers () {
			if (
				GameManager.instance == null ||
				GameManager.instance.battleManager == null ||
				GameManager.instance.battleManager.turnManager == null
			) {
				return;
			}

			battleManager = GameManager.instance.battleManager;
			turnManager = GameManager.instance.battleManager.turnManager;
		}

		private void CreateBattleUI () {
			if (battleManager == null) {
				GUILayout.Label("BattleManager not initialized.");
				return;
			}
			if(turnManager == null) {
				GUILayout.Label("TurnManager not initialized.");
				return;
			}

			CreateBattleSection();
			if (battleManager.inProgress && !battleManager.playerTurnInProgress) {
				CreateTurnSection();
			}
		}

		private void CreateBattleSection () {
			GUILayout.Label("Battle");
			GUILayout.Label("Turns: " + battleManager.currentTurn + "/" + battleManager.maxTurns);
			CreateStartEndBattleButton();
		}

		private void CreateTurnSection () {
			GUILayout.Label("Turn #" + battleManager.currentTurn);
			CreateNextTurnButton();
		}

		private void CreateStartEndBattleButton () {
			var text = battleManager.inProgress ? "Stop" : "Start";

			var button = GUILayout.Button(text);
			if (button) {
				if (battleManager.inProgress) {
					battleManager.StopBattle();
				} else {
					battleManager.StartBattle();
				}
			}
		}

		private void CreateNextTurnButton () {
			var nextTurnButton = GUILayout.Button("NextTurn");

			if (nextTurnButton) {
				turnManager.NextTurn(battleManager.unitManager.units);
			}
		}

	}

}
