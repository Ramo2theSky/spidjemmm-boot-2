using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Trash {
	public class PlayerController : MonoBehaviour {

		public InputActionAsset asset;
        public Rigidbody2D body;

        public float speed = 5;

        private float moveDirection;

        private InputAction jumpAction;
        private InputAction moveAction;

        private void Awake() {
            asset.Enable();
            var map = asset.FindActionMap("Player");
            moveAction = map.FindAction("Move");
            jumpAction = map.FindAction("Jump");

            jumpAction.performed += OnJump;
            moveAction.performed += OnMove;
            moveAction.canceled += OnMoveCanceled;
        }


        private void Update() {
            body.velocity = new Vector2(speed * moveDirection, 0);
        }

        private void OnDestroy() {
            jumpAction.performed -= OnJump;
            moveAction.performed -= OnMove;
            moveAction.canceled -= OnMoveCanceled;
        }

        private void OnMove(InputAction.CallbackContext ctx) {
            moveDirection = ctx.ReadValue<float>();
        }
        private void OnMoveCanceled(InputAction.CallbackContext obj) {
            moveDirection = 0;
        }

        private void OnJump(InputAction.CallbackContext ctx) {
            body.velocity = new Vector2(0, 10);
        }


    }



}