using STUDENT_NAME;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : Ball
{
    // Constante

    private static readonly float COEFF_NEW_SIZE = 1.5f;
    private static readonly float DISTANCE_REPOP_X = 1 / 2f; // En nombre de fois la corpulance de la boule
    private static readonly float DISTANCE_REPOP_Y = 1 / 2f; // En nombre de fois la corpulance de la boule
    private static readonly float BOOST = 10; // Coups de boost donné à la balle si celle-ci est en dessous de la ligne sur laquel elle devrait être
    private static readonly System.Random random = new System.Random();


    // Attributs

    [SerializeField] private int FORCE_X_AXIS = 180; // Force ajoutée au spawn
    [SerializeField] private int FORCE_Y_AXIS = 140; // Force ajoutée au spawn
    [SerializeField] private Direction.DirectionValue direction; // Direction de départ
    [SerializeField] private int RemainingSplit = 4; // Nombre de scission possible
    [SerializeField] private GameObject BallsStep;
    [SerializeField] private List<GameObject> BonusObjectList;

    private Rigidbody2D rb;
    private bool IsDestroyed;


    // "Constructeur"

    public override void InitiateObject(Direction.DirectionValue direction, int nbrSplit)
    {
        this.direction = direction;
        this.RemainingSplit = nbrSplit;

        // On récupére le ball step associé aux nombres de split restant (Le ball step corresponds à des étapes de hauteur sur la carte)

        Transform ballStep = GetBallStepAssociate();

        // Si le bas du ball step est au dessus du haut de la ball, on lui donne un peu d'élan
        // Position du haut de la ball
        float BallTopPosition = this.transform.position.y + this.transform.localScale.y / 2; // Car le pivot est au centre.
        float BallSptepBottomPosition = ballStep.position.y - ballStep.localScale.y / 2; // Car le pivot est au centre.
        if (BallSptepBottomPosition > BallTopPosition)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, BOOST);
        }
    }


    // Requetes

    public int GetRemainingSplit()
    {
        return RemainingSplit;
    }


    // Méthodes

    protected override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody2D>();

        Vector2 movement = new Vector2(Direction.GetValue(direction), 0) * FORCE_X_AXIS;
        rb.AddForce(movement);

        // On ajoute une force vers le haut pour plus de smooth

        Vector2 up = new Vector2(0, 1) * FORCE_Y_AXIS;
        rb.AddForce(up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.PLAYER_PROJECTILES) && !IsDestroyed && !collision.gameObject.GetComponent<PlayerProjectiles>().IsDestroyed())
        {
            IsDestroyed = true; // On indique que la ball est détruite

            // On détruit les éléments this & le projectile

            this.Kill();
            collision.gameObject.GetComponent<PlayerProjectiles>().Kill(); // On détruit le player projectile
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

    public override void Kill()
    {
        if (GetRemainingSplit() > 1)
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

            GameObject ballLeft = Instantiate(this.gameObject, positionLeft, Quaternion.identity, LevelManager.Instance.GetCurrentLevel());
            GameObject ballRight = Instantiate(this.gameObject, positionRight, Quaternion.identity, LevelManager.Instance.GetCurrentLevel());


            // On redimensionne les 2 boules

            ballLeft.GetComponent<Transform>().localScale = new Vector3(scaleX, scaleY, scaleZ);
            ballRight.GetComponent<Transform>().localScale = new Vector3(scaleX, scaleY, scaleZ);

            // On initialise les objets

            ballLeft.GetComponent<BallScript>().InitiateObject(Direction.DirectionValue.LEFT,
                this.RemainingSplit - 1);
            ballRight.GetComponent<BallScript>().InitiateObject(Direction.DirectionValue.RIGHT,
                this.RemainingSplit - 1);

            // On fait fait un random pour savoir si l'on fait pop ou non un objet bonus.

            int maxValue = GetRemainingSplit() * GetRemainingSplit() - 9 * GetRemainingSplit() + 22; // Par interpolation de Lagrange sur f(2)=8, f(3)=4, f(4)=2
            int randomValue = random.Next(1, maxValue + 1);

            if (randomValue == 1 && BonusObjectList.Count != 0) // Représente pour GetRemainingSplit() = 4, 1/2 chance.
                                                                //                 GetRemainingSplit() = 3, 1/4 chance
                                                                //                 GetRemainingSplit() = 2, 1/8 chance                              
            {

                // Si la chance sourit, on spawn un objet bonus.
                int indexObject = random.Next(BonusObjectList.Count);
                Instantiate(BonusObjectList[indexObject], transform.position, BonusObjectList[indexObject].transform.rotation, LevelManager.Instance.GetCurrentLevel());
            }
        }

        Destroy(this.gameObject);
    }
}
