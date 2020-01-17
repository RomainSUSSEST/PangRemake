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
        if (collision.gameObject.CompareTag(Tags.BALL) && collision.GetComponent<ClassicBall>().GetRemainingSplit() == RemainingSplitStep
            && collision.gameObject.transform.position.y < transform.position.y)
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }
}
