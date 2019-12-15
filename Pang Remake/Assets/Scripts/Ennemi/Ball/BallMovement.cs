using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    // Attributs

    [SerializeField] private int FORCE_X_AXIS = 180;
    [SerializeField] private Direction.DirectionValue direction;

    private Rigidbody2D rb;


    // Méthodes

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 movement = new Vector2(Direction.GetValue(direction), 0);
        rb.AddForce(movement * FORCE_X_AXIS);
    }
}
