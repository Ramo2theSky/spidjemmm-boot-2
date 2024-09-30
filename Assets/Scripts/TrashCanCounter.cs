using UnityEngine;
using TMPro;

namespace Trash {
    public class TrashCanCounter : MonoBehaviour {

        [SerializeField] private TrashCan trashCan;
        [SerializeField] private TMP_Text text;

        [SerializeField] AudioSource trashCanSound;
        [SerializeField] AudioClip filledSound;

        private void Start() {
            trashCan.OnFillCountChanged += OnFillCountChanged;
        }

        private void OnFillCountChanged(int count, int capacity) {
            text.SetText(count == capacity ? "FULL" : $"{count}/{capacity}");

            trashCanSound.clip = filledSound;
            trashCanSound.Play();
        }
    }

}