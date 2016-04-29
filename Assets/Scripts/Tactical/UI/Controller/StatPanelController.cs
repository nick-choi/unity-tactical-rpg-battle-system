using UnityEngine;
using Tactical.UI.Component;

namespace Tactical.UI.Controller {

	public class StatPanelController : MonoBehaviour {

		private const string ShowKey = "Show";
		private const string HideKey = "Hide";

		[SerializeField] private StatPanel primaryPanel;
		[SerializeField] private StatPanel secondaryPanel;

		private Tweener primaryTransition;
		private Tweener secondaryTransition;

		private void Start () {
			if (primaryPanel.panel.CurrentPosition == null) {
				primaryPanel.panel.SetPosition(HideKey, false);
			}
			if (secondaryPanel.panel.CurrentPosition == null) {
				secondaryPanel.panel.SetPosition(HideKey, false);
			}
		}

		public void ShowPrimary (GameObject obj) {
			Debug.Log("ShowPrimary");
			primaryPanel.Display(obj);
			MovePanel(primaryPanel, ShowKey, ref primaryTransition);
		}

		public void HidePrimary () {
			Debug.Log("HidePrimary");
			MovePanel(primaryPanel, HideKey, ref primaryTransition);
		}

		public void ShowSecondary (GameObject obj) {
			Debug.Log("ShowSecondary");
			secondaryPanel.Display(obj);
			MovePanel(secondaryPanel, ShowKey, ref secondaryTransition);
		}

		public void HideSecondary () {
			Debug.Log("HideSecondary");
			MovePanel(secondaryPanel, HideKey, ref secondaryTransition);
		}

		private void MovePanel (StatPanel obj, string pos, ref Tweener t) {
			Panel.Position target = obj.panel[pos];
			if (obj.panel.CurrentPosition != target) {
				if (t != null && t.easingControl != null) {
					t.easingControl.Stop();
				}
				t = obj.panel.SetPosition(pos, true);
				t.easingControl.duration = 0.5f;
				t.easingControl.equation = EasingEquations.EaseOutQuad;
			}
		}

	}

}