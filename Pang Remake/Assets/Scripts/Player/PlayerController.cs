using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Attributs

    [SerializeField] private float speed;

    private Rigidbody2D rigidbody;
    private Vector2 inputs = Vector2.zero;

    // Méthodes

    // Start is called before the first frame update
    private void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        // On récupére le mouvement souhaité

        inputs = new Vector2(Input.GetAxis("Horizontal"), 0);

        // On oriente le personnage en fonction

        if (inputs != Vector2.zero)
        {
            transform.forward = new Vector3(0, 0, inputs.x);
        }
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + inputs * speed * Time.fixedDeltaTime);
    }
}
