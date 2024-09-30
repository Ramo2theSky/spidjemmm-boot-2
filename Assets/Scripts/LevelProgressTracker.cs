using System;
using System.Collections;
using UnityEngine;
using TMPro;

namespace Trash {
    public class LevelProgressTracker : MonoBehaviour {

        [SerializeField] private GameTimer timer;
        [SerializeField] private int totalTrash = 5;
        [SerializeField] private int trashCollected = 0;

        [SerializeField] private TMP_Text text;
        [SerializeField] private GameState resultState;

        public static LevelProgressTracker Instance { get; private set; }

        private void Awake() {
            Instance = this;
            totalTrash = FindObjectsByType<TrashObject>(FindObjectsSortMode.None).Length;
            text.SetText($"{trashCollected}/{totalTrash}");
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
            yield return new WaitForSeconds(1f);
            OpenLeaderBoard();
        }

        private void OpenLeaderBoard() {
            SceneSelector.Instance.ChangeState(resultState);
        }
    }

}