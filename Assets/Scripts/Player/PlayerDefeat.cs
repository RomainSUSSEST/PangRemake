using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefeat : MonoBehaviour
{

    // Attributs

    [SerializeField] private CapsuleCollider2D CapsuleCollider2DZAxis;

    private Animator AnimPlayer;


    // Méthode

    private void Start()
    {
        AnimPlayer = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.Instance.IsPlaying && collision.gameObject.CompareTag(Tags.BALL))
        {
            AnimPlayer.applyRootMotion = true;
            AnimPlayer.SetBool("IsDead", true);

            // On détruit les capsuleCollider et le Rigidbody

            Destroy(GetComponent<PlayerController>());
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(GetComponent<CapsuleCollider2D>());
            Destroy(CapsuleCollider2DZAxis);
        }
    }
}
