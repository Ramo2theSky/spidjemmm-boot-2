using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trash {
    public class TrashObject : MonoBehaviour, IPickable {

        [SerializeField] private Rigidbody2D body;

        public void OnPick(Transform holdPoint) {
            body.position = holdPoint.position;
            body.transform.parent = holdPoint;
            body.bodyType = RigidbodyType2D.Kinematic;
        }

        public void OnThrow(Vector2 velocity) {
            body.transform.parent = null;
            body.bodyType = RigidbodyType2D.Dynamic;
            body.velocity = velocity;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            Debug.Log("Hello");    
        }

    }

}