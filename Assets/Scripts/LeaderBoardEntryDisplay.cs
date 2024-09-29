using UnityEngine;
using TMPro;

namespace Trash {
    public class LeaderBoardEntryDisplay : MonoBehaviour {
		[SerializeField] TMP_Text rankDisplay;
		[SerializeField] TMP_Text nameDisplay;
		[SerializeField] TMP_Text timeDisplay;

		public void SetDisplay(int rank, LeaderBoardEntry entry) {
            if (string.IsNullOrEmpty(entry.Name)) {
				SetDisplayEmpty();
				return;
			}
			gameObject.SetActive(true);
			rankDisplay.text = $"{rank})";
			nameDisplay.text = entry.Name;
			timeDisplay.text = string.Format("{0:0.00}", entry.Time);
        }

		public void SetDisplayEmpty() {
            gameObject.SetActive(false);
        }
	}
}