using System;
using System.Collections;
using UnityEngine;
using TMPro;

namespace Trash {
    public class LevelProgressTracker : MonoBehaviour {

        [SerializeField] private GameTimer timer;
        [SerializeField] private int totalTrash = 6;
        [SerializeField] private int trashCollected = 0;

        [SerializeField] private TMP_Text trashCountText;

        [SerializeField] private GameState resultState;

        public static LevelProgressTracker Instance { get; private set; }

        private void Awake() {
            Instance = this;
            trashCountText.SetText($"{trashCollected}/{totalTrash}");
        }

        public void OnTrashDumped() {
            trashCollected++;
            trashCountText.SetText(trashCollected == totalTrash ? "DONE" : $"{trashCollected}/{totalTrash}");
            if (trashCollected == totalTrash) {
                OnWin();
            }
        }

        private void OnWin() {
            timer.enabled = false;
            StartCoroutine(WinCoroutine());
        }

        private IEnumerator WinCoroutine() {
            yield return new WaitForSeconds(0.5f);
            OpenLeaderBoard();
        }

        private void OpenLeaderBoard() {
            SceneSelector.Instance.ChangeState(resultState);
        }

        [ContextMenu("FORCE WIN")]
        private void ForceWin() {
            OpenLeaderBoard();
        }


    }

}