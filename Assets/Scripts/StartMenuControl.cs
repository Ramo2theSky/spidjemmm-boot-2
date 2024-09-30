using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace Trash {
    public class StartMenuControl : MonoBehaviour {

        [SerializeField] private InputAction anyKey;
        [SerializeField] private GameState gameplayState;

        [SerializeField] AudioSource backgroundAudio;
        [SerializeField] AudioClip changeMusic;


        private void OnEnable() {
            anyKey.Enable();
            anyKey.performed += Click;
        }
        private void OnDisable() {
            anyKey.Disable();
            anyKey.performed -= Click;
        }

        private void Click(InputAction.CallbackContext obj) {
            SceneSelector.Instance.ChangeState(gameplayState);

            backgroundAudio.clip = changeMusic;
            backgroundAudio.Play();
        }
    }

}