using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Trash {
	public class GameTimer : MonoBehaviour {

        [SerializeField] TMP_Text text;
		[SerializeField] private float elapsed;

        public float Elapsed => elapsed;

        private void Update() {
            elapsed += Time.deltaTime;
            text.SetText(string.Format("{0:0.00}", elapsed));
            
        }

        public void ResetTimer() {
            elapsed = 0;
        }



    }
}