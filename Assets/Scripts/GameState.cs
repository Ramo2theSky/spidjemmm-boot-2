using UnityEngine;

namespace Trash {
    public class GameState : MonoBehaviour {

        [SerializeField] private GameObject[] objects;

        private void OnEnable() {
            foreach (GameObject obj in objects) {
                obj.SetActive(true);
            }
        }

        private void OnDisable() {
            foreach (GameObject obj in objects) {
                obj.SetActive(false);
            }
        }
    }

}