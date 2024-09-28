using UnityEngine;

namespace Trash {
    public class TrashDestroy : MonoBehaviour {

        [SerializeField] private TrashCan trashCan;

        private void OnTriggerEnter2D(Collider2D other) {
            trashCan.OnInsert();
            Destroy(other.gameObject);
        }
    }

}