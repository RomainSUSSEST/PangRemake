using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    // Constante

    private readonly float COEFF_NEW_SIZE = 1.5f;
    private readonly float DISTANCE_REPOP_X = 1 / 2f; // En nombre de fois la corpulance de la boule
    private readonly float DISTANCE_REPOP_Y = 1 / 2f; // En nombre de fois la corpulance de la boule
    private readonly float BOOST = 10; // Coups de boost donné à la balle si celle-ci est en dessous de la ligne sur laquel elle devrait être


    // Attributs

    [SerializeField] private int FORCE_X_AXIS = 180; // Force ajoutée au spawn
    [SerializeField] private int FORCE_Y_AXIS = 140; // Force ajoutée au spawn
    [SerializeField] private Direction.DirectionValue direction; // Direction de départ
    [SerializeField] private int RemainingSplit = 4; // Nombre de scission possible
    [SerializeField] private GameObject BallsStep;

    private Rigidbody2D rb;


    // "Constructeur"

    public void InitiateObject(Direction.DirectionValue direction, int nbrSplit)
    {
        this.direction = direction;
        this.RemainingSplit = nbrSplit;

        // On récupére le ball step associé aux nombres de split restant

        Transform ballStep = GetBallStepAssociate();

        // Si le ball step est au dessus de la ball, on lui donne un peu d'élan

        if (ballStep.position.y > this.transform.position.y)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -BOOST);
        }
    }


    // Requetes

    public int GetRemainingSplit()
    {
        return RemainingSplit;
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
            if (RemainingSplit > 1)
            {
                // CALCUL DES NOUVELLES POSITIONS DES BOULES

                // y = position de le boule + 3/4 de sa corpulence
                float y = this.transform.position.y + DISTANCE_REPOP_Y * this.transform.localScale.y;

                // On calcul le x pour la boule de gauche = position de la boule - 3 / 4 de sa corpulence
                float xLeft = this.transform.position.x - DISTANCE_REPOP_X * this.transform.localScale.x;

                // On calcul le x pour la boule de droite = position de la boule + 3 / 4 de sa corpulence
                float xRight = this.transform.position.x + DISTANCE_REPOP_X * this.transform.localScale.x;


                // CALCUL DES NOUVELLES TAILLES DES BOULES

                float scaleX = this.transform.localScale.x / COEFF_NEW_SIZE;
                float scaleY = this.transform.localScale.y / COEFF_NEW_SIZE;
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

                ballLeft.GetComponent<BallScript>().InitiateObject(Direction.DirectionValue.LEFT,
                    this.RemainingSplit - 1);
                ballRight.GetComponent<BallScript>().InitiateObject(Direction.DirectionValue.RIGHT,
                    this.RemainingSplit - 1);
            }
           

            // On détruit les éléments this & le projectile

            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }


    // Outils

    // Renvoie le ball step associé à la boule courante ou null.
    private Transform GetBallStepAssociate()
    {
        int children = BallsStep.transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            if (BallsStep.transform.GetChild(i).GetComponent<BallsStep>().GetRemainingSplitStep() == GetRemainingSplit())
            {
                return BallsStep.transform.GetChild(i);
            }
        }
        return null;
    }
}
