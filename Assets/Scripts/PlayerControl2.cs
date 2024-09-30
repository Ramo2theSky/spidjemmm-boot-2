using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Trash
{
    public class PlayerControl2 : MonoBehaviour
    {

        public InputActionAsset asset;
        public Rigidbody2D body;

        public float speed = 5;
        public float jumpVelocity = 10;
        public float downGravityModifier = 1;
        public LayerMask groundLayer;
        public Transform groundCheck; 
        public float groundCheckRadius = 0.2f;

        private float moveDirection;
        private float faceDirection = 1;
        private bool jumpPressed;
        private bool jumpCanceled;
        private bool downPressed;
        private bool isGrounded;
        private bool canDoubleJump;

        public Transform holdPoint;
        public SpriteRenderer handSprite;
        public Vector2 throwVelocity = new Vector2(5, 2);
        public float throwBodyVelocityFactor = 0.2f;
        private IPickable currentPickableItem;
        private IPickable currentHeldItem;

        private InputAction jumpAction;
        private InputAction moveAction;
        private InputAction downAction;
        private InputAction throwAction;

        public AudioSource PlayerSound;
        public AudioClip jumpSound, throwSound;

        private void Awake()
        {
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


        private void FixedUpdate()
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

            if (isGrounded)
            {
                canDoubleJump = true;
            }

            CalculateVelocity();
        }

        private void CalculateVelocity()
        {
            var bodyVelocity = body.velocity;

            bodyVelocity.x = moveDirection * speed;

            if (jumpPressed)
            {
                if (isGrounded)
                {
                    bodyVelocity.y = jumpVelocity;
                    PlayerSound.PlayOneShot(jumpSound);
                }
                else if (canDoubleJump)
                {
                    bodyVelocity.y = jumpVelocity;
                    PlayerSound.PlayOneShot(jumpSound);
                    canDoubleJump = false;
                }
                jumpPressed = false;
            }

            if (jumpCanceled)
            {
                if (bodyVelocity.y > 0)
                {
                    bodyVelocity.y /= 2;
                }
                jumpCanceled = false;
            }

            body.velocity = bodyVelocity;
        }

        private void OnDestroy()
        {
            moveAction.performed -= OnMove_Performed;
            moveAction.canceled -= OnMove_Canceled;
            jumpAction.performed -= OnJump_Started;
            downAction.started -= OnDown_Started;
            downAction.canceled -= OnDown_Canceled;
            throwAction.performed -= OnThrow_Performed;
        }

        private void OnMove_Performed(InputAction.CallbackContext ctx)
        {
            moveDirection = ctx.ReadValue<float>();
            faceDirection = moveDirection;
        }
        private void OnMove_Canceled(InputAction.CallbackContext obj)
        {
            moveDirection = 0;
        }

        private void OnJump_Started(InputAction.CallbackContext ctx)
        {
            jumpPressed = true;
        }
        private void OnJump_Canceled(InputAction.CallbackContext ctx)
        {
            jumpCanceled = true;
        }

        private void OnThrow_Performed(InputAction.CallbackContext ctx)
        {

            if (currentHeldItem != null)
            {
                currentHeldItem.OnThrow(CalculateThrowVelocity());

                PlayerSound.clip = throwSound;
                PlayerSound.Play();

                handSprite.enabled = false;
                currentHeldItem = null;
                return;
            }

            if (currentPickableItem != null)
            {
                currentHeldItem = currentPickableItem;
                currentHeldItem.OnPick(holdPoint);
                handSprite.enabled = true;
            }

            Vector2 CalculateThrowVelocity()
            {
                var vel = throwVelocity;
                vel.x *= faceDirection;
                if (downPressed)
                {
                    vel.x = 0;
                    vel.y = -vel.y;
                }
                else
                {
                    vel += throwBodyVelocityFactor * body.velocity;
                }
                return vel;
            }
        }

        private void OnDown_Started(InputAction.CallbackContext ctx)
        {
            downPressed = true;
            body.gravityScale += downGravityModifier;
        }

        private void OnDown_Canceled(InputAction.CallbackContext ctx)
        {
            downPressed = false;
            body.gravityScale -= downGravityModifier;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.TryGetComponent(out currentPickableItem);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.TryGetComponent<IPickable>(out var pickable)) { return; }
            if (pickable != currentPickableItem) { return; }
            currentPickableItem = null;
        }

    }



}