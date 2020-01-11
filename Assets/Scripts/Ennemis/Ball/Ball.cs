using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Classe mère des balls.
 * Vérifie qu'elle soit correctement instantié.
 * 
 * Classe gardant une référence sur toute les boule enfant instanciées.
 *
 */
public abstract class Ball : MonoBehaviour
{
    // Static

    private static List<Ball> BALL = new List<Ball>();

    public static List<Ball> GetAllBall()
    {
        // On renvoie une copie pour ne pas donner accès à la list de base.
        return new List<Ball>(BALL);
    }


    // Méthodes

    protected virtual void Start()
    {
        BALL.Add(this);
    }

    // Méthode permettant d'initialiser la ball, doit appelé SetInitiateObject(true) dans le corps.
    public abstract void InitiateObject(Direction.DirectionValue direction, int nbrSplit);

    // Permet de tuer la ball.
    public virtual void Kill()
    {
        BALL.Remove(this);
        Destroy(this.gameObject);
    }
}
