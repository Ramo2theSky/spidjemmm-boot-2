using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Trash.JSONTest;
using static UnityEngine.EventSystems.EventTrigger;

namespace Trash {
    public class LeaderBoard : MonoBehaviour {
		
		[SerializeField] private LeaderBoardEntryDisplay[] displays;
		[SerializeField] private List<LeaderBoardEntry> entries = new List<LeaderBoardEntry>(10);

		private string SavePath => Path.Combine(Application.persistentDataPath, "leaderboard.sav");

        public bool TryInsertEntry(LeaderBoardEntry entry) {
			entries.Add(entry);
			SortEntry();
			Debug.Log(entries.Count);
			return true;
		}

		[ContextMenu("Update Display")]
		public void UpdateDisplay() {
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
			File.WriteAllText(SavePath, JsonUtility.ToJson(new LeaderBoardEntries { entry = entries }));
		}

        public void LoadLeaderBoard() {
			if (!File.Exists(SavePath)) { return; }
			entries = JsonUtility.FromJson<LeaderBoardEntries>(File.ReadAllText(SavePath)).entry;
        }

    }
}