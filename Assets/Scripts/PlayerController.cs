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
        public float jumpVelocity = 10;
        public float downGravityModifier = 1;

        private float moveDirection;
        private bool jumpPressed;
        private bool jumpCanceled;
        private bool downPressed;

        private InputAction jumpAction;
        private InputAction moveAction;
        private InputAction downAction;
        private InputAction throwAction;

        private void Awake() {
            asset.Enable();
            var map = asset.FindActionMap("Player");
            moveAction = map.FindAction("Move");
            jumpAction = map.FindAction("Jump");
            downAction = map.FindAction("Down");
            throwAction = map.FindAction("Throw");

            moveAction.performed += OnMove_Performed;
            moveAction.canceled += OnMove_Canceled;
            jumpAction.started += OnJump_Started;
            jumpAction.canceled += OnJump_Canceled;
            downAction.started += OnDown_Started;
            downAction.canceled += OnDown_Canceled;
            throwAction.performed += OnThrow_Performed;
        }


        private void FixedUpdate() {
            CalculateVelocity();
        }

        private void CalculateVelocity() {
            var bodyVelocity = body.velocity;

            bodyVelocity.x = moveDirection * speed;

            if (jumpPressed) {
                bodyVelocity.y = jumpVelocity;
                jumpPressed = false;
            }

            if (jumpCanceled) {
                if (bodyVelocity.y > 0) {
                    bodyVelocity.y /= 2;
                }
                jumpCanceled = false;
            }

            body.velocity = bodyVelocity;
        }

        private void OnDestroy() {
            moveAction.performed -= OnMove_Performed;
            moveAction.canceled -= OnMove_Canceled;
            jumpAction.performed -= OnJump_Started;
            downAction.started -= OnDown_Started;
            downAction.canceled -= OnDown_Canceled;
            throwAction.performed -= OnThrow_Performed;
        }

        private void OnMove_Performed(InputAction.CallbackContext ctx) {
            moveDirection = ctx.ReadValue<float>();
        }
        private void OnMove_Canceled(InputAction.CallbackContext obj) {
            moveDirection = 0;
        }

        private void OnJump_Started(InputAction.CallbackContext ctx) {
            jumpPressed = true;
        }
        private void OnJump_Canceled(InputAction.CallbackContext ctx) {
            jumpCanceled = true;
        }

        private void OnThrow_Performed(InputAction.CallbackContext ctx) {
            throw new NotImplementedException();
        }

        private void OnDown_Started(InputAction.CallbackContext ctx) {
            downPressed = true;
            body.gravityScale += downGravityModifier;
        }

        private void OnDown_Canceled(InputAction.CallbackContext ctx) {
            downPressed = false;
            body.gravityScale -= downGravityModifier;
        }
    }



}