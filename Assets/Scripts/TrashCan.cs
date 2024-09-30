using System;
using UnityEngine;

namespace Trash {
    public class TrashCan : MonoBehaviour {

        [SerializeField] private int maxCapacity = 10;
        [SerializeField] private int currentFilled;

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite emptySprite;
        [SerializeField] private Sprite fullSprite;   

        [SerializeField] private Collider2D fillableCollider;
        [SerializeField] private Collider2D fullCollider;

        [SerializeField] AudioSource trashCanSound;
        [SerializeField] AudioClip hitSound;


        public event Action<int, int> OnFillCountChanged = delegate { };

        public void OnInsert() {
            currentFilled++;
            if (currentFilled == maxCapacity) {
                Full();
            }
            OnFillCountChanged(currentFilled, maxCapacity);

            void Full() {
                fillableCollider.enabled = false;
                fullCollider.enabled = true;
                spriteRenderer.sprite = fullSprite;
            }
        }

        public void Empty() {
            currentFilled = 0;
            fillableCollider.enabled = true;
            fullCollider.enabled = false;
            spriteRenderer.sprite = emptySprite;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            trashCanSound.clip = hitSound;
            trashCanSound.Play();
        }

    }

}