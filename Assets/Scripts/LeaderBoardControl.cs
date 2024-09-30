using Furari;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Trash {
    public class LeaderBoardControl : MonoBehaviour {

        [SerializeField] private SceneReference scene;
        [SerializeField] private InputAction anyKey;
        
        private void OnEnable() {
            StartCoroutine(Wait());
        }

        private void OnDisable() {
            anyKey.Disable();
            anyKey.performed -= Click;
        }

        private void Click(InputAction.CallbackContext obj) {
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene.Name);
        }

        private IEnumerator Wait() {
            yield return new WaitForSeconds(1.0f);
            anyKey.Enable();
            anyKey.performed += Click;
        }

    }

}