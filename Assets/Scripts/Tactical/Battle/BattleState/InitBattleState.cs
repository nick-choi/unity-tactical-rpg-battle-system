using UnityEngine;
using System.Collections;
using Tactical.Core;
using Tactical.Actor.Component;
using Tactical.Grid.Model;

namespace Tactical.Battle.BattleState {

	public class InitBattleState : BattleState {

		public override void Enter () {
			base.Enter();
			StartCoroutine(Init());
		}

		private IEnumerator Init () {
			board.Load(levelData);
			var p = new Point((int)levelData.tiles[0].x, (int)levelData.tiles[0].z);
			SelectTile(p);
			SpawnTestUnits();
			yield return null;
			owner.ChangeState<CutSceneState>();
		}

		private void SpawnTestUnits () {
			string[] jobs = {"Rogue", "Warrior", "Wizard"};
			for (int i = 0; i < jobs.Length; ++i) {
				var instance = Instantiate(owner.heroPrefab);

				Stats s = instance.AddComponent<Stats>();
				s[StatType.LVL] = 1;

				GameObject jobPrefab = Resources.Load<GameObject>( "Jobs/" + jobs[i] );
				var jobInstance = Instantiate(jobPrefab);
				jobInstance.transform.SetParent(instance.transform);

				Job job = jobInstance.GetComponent<Job>();
				job.Employ();
				job.LoadDefaultStats();
				instance.name = string.Format("Character #{0}", i+1);

				var p = new Point((int)levelData.tiles[i].x, (int)levelData.tiles[i].z);

				Unit unit = instance.GetComponent<Unit>();
				unit.Place(board.GetTile(p));
				unit.Match();

				instance.AddComponent<WalkMovement>();

				units.Add(unit);

				ExperienceRank rank = instance.AddComponent<ExperienceRank>();
				rank.Init(10);
			}
		}
	}

}
