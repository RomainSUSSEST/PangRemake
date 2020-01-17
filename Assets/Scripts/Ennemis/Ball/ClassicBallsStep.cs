using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicBallsStep : MonoBehaviour
{
    // Attributs

    [SerializeField] private int RemainingSplitStep;


    // Requete

    public int GetRemainingSplitStep()
    {
        return RemainingSplitStep;
    }


    // Méthode

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.BALL) && collision.GetComponent<ClassicBall>().GetRemainingSplit() == GetRemainingSplitStep())
        {
            if (collision.GetComponent<Rigidbody2D>().velocity.y >= 0) // Si la boule monte
            {
                // On reset la velocité en Y pour ne pas que la boule aille plus haut.
                Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
                rb.velocity = new Vector2(rb.velocity.x, 0);

                // On désactive le boost de la ball pour son atterrissage futur.
                collision.GetComponent<Ball>().DisableNextBoost();

            } else // Si la boule descend
            {
                // On désactive le boost de la ball pour son atterrissage futur.
                collision.GetComponent<Ball>().DisableNextBoost();
            }

        }
    }
}
