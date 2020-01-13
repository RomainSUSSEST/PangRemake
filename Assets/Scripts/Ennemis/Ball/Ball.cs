using SDD.Events;
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
public abstract class Ball : SimpleGameStateObserver
{
    // Constante

    private static readonly float COEFF_NEW_SIZE = 1.5f;
    private static readonly float DISTANCE_REPOP_X = 1 / 2f; // En nombre de fois la corpulance de la boule
    private static readonly float DISTANCE_REPOP_Y = 1 / 2f; // En nombre de fois la corpulance de la boule
    private static readonly float BOOST = 10; // Coups de boost donné à la balle si celle-ci est en dessous de la ligne sur laquel elle devrait être
    private static readonly System.Random random = new System.Random();


    // Static

    private static List<Ball> BALL = new List<Ball>();

    public static List<Ball> GetAllBall()
    {
        // On renvoie une copie pour ne pas donner accès à la list de base.
        return new List<Ball>(BALL);
    }

    // Attributs

    [SerializeField] string SoundPlayOnDead; // Son joué à la mort de l'objet.


    // Méthodes

    protected virtual void Start()
    {
        BALL.Add(this);
    }

    // Méthode permettant d'initialiser la ball, doit appelé SetInitiateObject(true) dans le corps.
    public abstract void InitiateObject(Direction.DirectionValue direction, int nbrSplit);

    // Lorsque la balle meurt, on la retire des balls restantes.
    protected override void OnDestroy()
    {
        base.OnDestroy();
        BALL.Remove(this);

        // On envoie un event pour signifier qu'une ball a été détruite.
        EventManager.Instance.Raise(new BallHasBeenDestroyedEvent());
        PlaySound();
    }

    // Outils

     // Méthode a appelé pour détruire correctement une Ball. (Pour qu'elle puisse appliquer son avant mort).
    public abstract void Kill();

    void PlaySound()
    {
        if (SfxManager.Instance) SfxManager.Instance.PlaySfx2D(SoundPlayOnDead);
    }
}
