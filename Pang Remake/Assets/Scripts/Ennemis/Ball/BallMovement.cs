using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    // Attributs

    [SerializeField] private int FORCE_X_AXIS = 180; // Force ajoutée au spawn
    [SerializeField] private int FORCE_Y_AXIS = 140; // Force ajoutée au spawn
    [SerializeField] private Direction.DirectionValue direction; // Direction de départ
    [SerializeField] private int NumberSplitPossible = 4; // Nombre de scission possible

    private Rigidbody2D rb;


    // "Constructeur"

    public void InitiateObject(Direction.DirectionValue direction, int nbrSplit)
    {
        this.direction = direction;
        this.NumberSplitPossible = nbrSplit;
    }


    // Méthodes

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Vector2 movement = new Vector2(Direction.GetValue(direction), 0) * FORCE_X_AXIS;
        rb.AddForce(movement);

        // On ajoute une force vers le haut pour plus de smooth

        Vector2 up = new Vector2(0, 1) * FORCE_Y_AXIS;
        rb.AddForce(up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.PLAYER_PROJECTILES))
        {
            if (NumberSplitPossible > 1)
            {
                // CALCUL DES NOUVELLES POSITIONS DES BOULES

                // y = position de le boule + 3/4 de sa corpulence
                float y = this.transform.position.y + 3 / 4 * this.transform.localScale.y;

                // On calcul le x pour la boule de gauche = position de la boule - 3 / 4 de sa corpulence
                float xLeft = this.transform.position.x - 3 / 4 * this.transform.localScale.x;

                // On calcul le x pour la boule de droite = position de la boule + 3 / 4 de sa corpulence
                float xRight = this.transform.position.x + 3 / 4 * this.transform.localScale.x;


                // CALCUL DES NOUVELLES TAILLES DES BOULES

                float scaleX = this.transform.localScale.x / 2;
                float scaleY = this.transform.localScale.y / 2;
                float scaleZ = this.transform.localScale.z;


                // INSTANTIATION DES NOUVELLES BOULES
                Vector3 positionLeft = new Vector3(xLeft, y, this.transform.position.z);
                Vector3 positionRight = new Vector3(xRight, y, this.transform.position.z);

                GameObject ballLeft = Instantiate(this.gameObject, positionLeft, Quaternion.identity);
                GameObject ballRight = Instantiate(this.gameObject, positionRight, Quaternion.identity);

                // On redimensionne les 2 boules

                ballLeft.GetComponent<Transform>().localScale = new Vector3(scaleX, scaleY, scaleZ);
                ballRight.GetComponent<Transform>().localScale = new Vector3(scaleX, scaleY, scaleZ);

                // On initialise les objets

                ballLeft.GetComponent<BallMovement>().InitiateObject(Direction.DirectionValue.LEFT,
                    this.NumberSplitPossible - 1);
                ballRight.GetComponent<BallMovement>().InitiateObject(Direction.DirectionValue.RIGHT,
                    this.NumberSplitPossible - 1);
            }
           

            // On détruit les éléments this & le projectile

            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
