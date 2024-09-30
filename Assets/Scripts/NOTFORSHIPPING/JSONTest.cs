using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trash {
    public partial class JSONTest : MonoBehaviour {

		public List<LeaderBoardEntry> entry;
		public int[] aaa;

		[ContextMenu("TEST")]
		public void Test() {
			Debug.Log(JsonUtility.ToJson(new LeaderBoardEntries { entry = entry }));
		}

		
	}
}