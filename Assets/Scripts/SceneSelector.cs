using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trash {
	public class SceneSelector : MonoBehaviour {

		[SerializeField] private GameState currentState;

        public SceneSelector Instance { get; private set; }

        private void Awake() {
            Instance = this;
        }

        public void ChangeState(GameState newState) {
            currentState.OnStateExit();
			currentState = newState;
            currentState.OnStateEnter();
        }
    }

    public abstract class GameState : MonoBehaviour {

        public abstract void OnStateEnter();
        public abstract void OnStateExit();
    }

    public class GameState_StartMenu : GameState {

        [SerializeField] private GameObject startMenuCanvas;

        public override void OnStateEnter() {
            throw new NotImplementedException();
        }

        public override void OnStateExit() {
            throw new NotImplementedException();
        }
    }

    public class GameState_Gameplay : GameState {

        [SerializeField] private GameObject startMenuCanvas;

        public override void OnStateEnter() {
            throw new NotImplementedException();
        }

        public override void OnStateExit() {
            throw new NotImplementedException();
        }
    }

    public class GameState_Result : GameState {

        [SerializeField] private GameObject startMenuCanvas;

        public override void OnStateEnter() {
            throw new NotImplementedException();
        }

        public override void OnStateExit() {
            throw new NotImplementedException();
        }
    }


}