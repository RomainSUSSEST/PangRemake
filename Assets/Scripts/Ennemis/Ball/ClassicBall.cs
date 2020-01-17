using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicBall : Ball
{
    // Constante

    private static readonly float COEFF_NEW_SIZE = 1.5f;
    private static readonly float DISTANCE_REPOP_X = 1 / 2f; // En nombre de fois la corpulance de la boule
    private static readonly float DISTANCE_REPOP_Y = 1 / 2f; // En nombre de fois la corpulance de la boule
    private static readonly System.Random random = new System.Random();


    // Attributs

    [SerializeField] private int RemainingSplit = 4; // Nombre de scission possible


    // Requetes

    public int GetRemainingSplit()
    {
        return RemainingSplit;
    }

    // Renvoie le ball step associé à la boule courante ou null.
    protected override Transform GetBallStepAssociate()
    {
        int children = GetBallsStep().transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            if (GetBallsStep().transform.GetChild(i).GetComponent<ClassicBallsStep>().GetRemainingSplitStep() == GetRemainingSplit())
            {
                return GetBallsStep().transform.GetChild(i);
            }
        }
        return null;
    }


    // Méthodes

    protected void SetRemainingSplit(int remainingSplit)
    {
        this.RemainingSplit = remainingSplit;
    }


    // Outils

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

            int newRemainingSplit = this.RemainingSplit - 1;

            ClassicBall tamp = ballLeft.GetComponent<ClassicBall>();
            tamp.SetDirection(Direction.DirectionValue.LEFT);
            tamp.SetRemainingSplit(newRemainingSplit);

            tamp = ballRight.GetComponent<ClassicBall>();
            tamp.SetDirection(Direction.DirectionValue.RIGHT);
            tamp.SetRemainingSplit(newRemainingSplit);

            // On fait fait un random pour savoir si l'on fait pop ou non un objet bonus.

            int maxValue = GetRemainingSplit() * GetRemainingSplit() - 9 * GetRemainingSplit() + 22; // Par interpolation de Lagrange sur f(2)=8, f(3)=4, f(4)=2
            int randomValue = random.Next(1, maxValue + 1);

            if (randomValue == 1 && GetBonusObjectList().Count != 0) // Représente pour GetRemainingSplit() = 4, 1/2 chance.
                                                                //                 GetRemainingSplit() = 3, 1/4 chance
                                                                //                 GetRemainingSplit() = 2, 1/8 chance                              
            {

                // Si la chance sourit, on spawn un objet bonus.
                int indexObject = random.Next(GetBonusObjectList().Count);
                Instantiate(GetBonusObjectList()[indexObject], transform.position, GetBonusObjectList()[indexObject].transform.rotation, LevelManager.Instance.GetCurrentLevel());
            }
        }

        Destroy(this.gameObject);
    }
}