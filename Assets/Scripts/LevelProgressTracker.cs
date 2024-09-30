using System;
using System.Collections;
using UnityEngine;
using TMPro;

namespace Trash {
    public class LevelProgressTracker : MonoBehaviour {

        [SerializeField] private GameTimer timer;
        [SerializeField] private int totalTrash = 6;
        [SerializeField] private int trashCollected = 0;

        [SerializeField] private TMP_Text text;

        public static LevelProgressTracker Instance { get; private set; }

        private void Awake() {
            Instance = this;
        }

        public void OnTrashDumped() {
            trashCollected++;
            text.SetText(trashCollected == totalTrash ? "DONE" : $"{trashCollected}/{totalTrash}");
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
            OpenLeaderBoard(timer);
        }

        private void OpenLeaderBoard(GameTimer timer) {
            //throw new NotImplementedException();
        }
    }

}