using UnityEngine;

namespace Trash {
	public class LevelProgressTracker : MonoBehaviour {

        [SerializeField] private GameTimer timer;
        [SerializeField] private int totalTrashLeft;

        public static LevelProgressTracker Instance { get; private set; }

        private void Awake() {
            Instance = this;
        }

        public void OnTrashDumped() {
            totalTrashLeft--;
            if (totalTrashLeft == 0) {
                OnWin();
            }
        }

        private void OnWin() {
            timer.enabled = false;
        }

    }

}