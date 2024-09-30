using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineScript : MonoBehaviour
{
    [SerializeField] float bounce = 5f;
    [SerializeField] AudioSource trampolineSound;
    [SerializeField] AudioClip bounceSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);

        trampolineSound.clip = bounceSound;
        trampolineSound.Play();
    }
}
