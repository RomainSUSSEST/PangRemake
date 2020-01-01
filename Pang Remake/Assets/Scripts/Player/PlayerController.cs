using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Attributs

    [SerializeField] private float speed;

    private Rigidbody2D rb;
    private Vector2 inputs = Vector2.zero;

    // Méthodes

    // Start is called before the first frame update
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        // On récupére le mouvement souhaité

        inputs = new Vector2(Input.GetAxis("Horizontal"), 0);

        // On oriente le personnage en fonction

        if (inputs != Vector2.zero)
        {
            transform.forward = new Vector3(0, 0, -inputs.x);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + inputs * speed * Time.fixedDeltaTime);
    }
}
