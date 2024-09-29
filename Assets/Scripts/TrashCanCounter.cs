using UnityEngine;
using TMPro;

namespace Trash {
    public class TrashCanCounter : MonoBehaviour {

        [SerializeField] private TrashCan trashCan;
        [SerializeField] private TMP_Text text;

        private void Start() {
            trashCan.OnFillCountChanged += OnFillCountChanged;
        }

        private void OnFillCountChanged(int count, int capacity) {
            text.SetText($"{count}/{capacity}");
        }
    }

}