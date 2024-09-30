using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trash {
    public class LeaderBoard : MonoBehaviour {
		
		[SerializeField] private LeaderBoardEntryDisplay[] displays;
		[SerializeField] private List<LeaderBoardEntry> entries = new List<LeaderBoardEntry>(10);

		private string SavePath => Path.Combine(Application.persistentDataPath, "leaderboard.sav");

        public bool TryInsertEntry(LeaderBoardEntry entry) {
			for (int i = entries.Count - 1; i >= 0; i--) {
				if (entry.CompareTo(entries[i]) < 0) {
					entries.Insert(i, entry);
					return true;
				}
			}
			return false;
		}

		[ContextMenu("Update Display")]
		private void UpdateDisplay() {
			int i = 0;
			for (; i < Mathf.Min(entries.Count, displays.Length); i++) {
				displays[i].SetDisplay(i + 1, entries[i]);
			}
			for (; i < displays.Length; i++) {
				displays[i].SetDisplayEmpty();
			}
		}

		[ContextMenu("Sort Entry")]
		private void SortEntry() {
			entries.Sort();
		}

		public void SaveLeaderBoard() {
			File.WriteAllText(SavePath, JsonUtility.ToJson(entries));
		}

        public void LoadLeaderBoard() {
			if (!File.Exists(SavePath)) { return; }
			entries = JsonUtility.FromJson<List<LeaderBoardEntry>>(File.ReadAllText(SavePath));
        }

    }
}