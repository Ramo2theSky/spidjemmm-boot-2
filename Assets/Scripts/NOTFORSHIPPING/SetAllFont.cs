using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trash {
	public class SetAllFont : MonoBehaviour {

        public TMPro.TMP_FontAsset fontAsset;

        [ContextMenu("yey")]
        private void yey() {
            foreach (var t in FindObjectsOfType<TMPro.TMP_Text>(true)) {
                t.font = fontAsset;
            }
        }
    }
}