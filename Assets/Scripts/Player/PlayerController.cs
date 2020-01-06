using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Attributs

    [SerializeField] private float speed;

    private Rigidbody2D rb;
    private Animator animPlayer;
    private Vector2 inputs = Vector2.zero;
    private bool isShooting; // Variable indiquant si oui ou non le player est entrain de tirer.


    // Méthodes

    // Start is called before the first frame update
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        animPlayer = GetComponent<Animator>();
}

    // Update is called once per frame
    private void Update()
    {
        // On ne continue que si nous ne sommes pas entrain de tirer.

        if (isShooting)
        {
            inputs = Vector2.zero;
            transform.forward = new Vector3(0, 0, 1); // On oriente le joueur vers le décor
        } else
        {

            // On récupére le mouvement souhaité

            inputs = new Vector2(Input.GetAxis("Horizontal"), 0);

            // On oriente le personnage en fonction et on anime le player (isWalking = true)

            if (inputs != Vector2.zero)
            {
                animPlayer.SetBool("IsWalking", true);
                transform.forward = new Vector3(inputs.x, 0, 0);
            }
            else
            {
                animPlayer.SetBool("IsWalking", false);
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + inputs * speed * Time.fixedDeltaTime);
    }

    
    // Fonction appelé par évenement
    private void shootStart()
    {
        isShooting = true;
    }

    private void shootEnd()
    {
        isShooting = false;
        animPlayer.SetBool("IsShooting", false);
    }
}
