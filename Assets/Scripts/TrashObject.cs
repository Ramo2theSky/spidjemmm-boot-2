using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trash {
    public class TrashObject : MonoBehaviour {


        private void OnTriggerEnter2D(Collider2D other) {
            Debug.Log("Hello");    
        }

    }


    public interface IPickable {
        void OnPick();
        void OnThrow(Vector2 velocity);
    }

}