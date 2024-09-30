using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trash {
    public class SceneSelector : MonoBehaviour {

		[SerializeField] private GameState currentState;

        public static SceneSelector Instance { get; private set; }

        private void Awake() {
            Instance = this;
        }

        private void Start() {
            currentState.gameObject.SetActive(true);
        }

        public void ChangeState(GameState newState) {
            currentState.gameObject.SetActive(false);
			currentState = newState;
            currentState.gameObject.SetActive(true);
        }
    }

}