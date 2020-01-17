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

    private static readonly float BOOST = 15; // Coups de boost donné à la balle si celle-ci est en dessous de la ligne sur laquel elle devrait être


    // Static

    private static List<Ball> BALL = new List<Ball>();

    public static List<Ball> GetAllBall()
    {
        // On renvoie une copie pour ne pas donner accès à la list de base.
        return new List<Ball>(BALL);
    }

    // Attributs

    [SerializeField] string SoundPlayOnDead; // Son joué à la mort de l'objet.
    [SerializeField] private float XVelocity;
    [SerializeField] private int FORCE_Y_AXIS = 140; // Force ajoutée au spawn
    [SerializeField] private GameObject BallsStep; // Les palier de hauteur selon la taille de la balle.
    [SerializeField] private Direction.DirectionValue direction; // Direction de départ
    [SerializeField] private List<GameObject> BonusObjectList; // Liste des objets pouvant apparaitre à la destruction de la balle.

    private bool IsGameOver;
    private bool BoostIsActivate;
    private Rigidbody2D rb;
    private bool IsDestroyed;


    // Méthodes

    public void SetDirection(Direction.DirectionValue direction)
    {
        this.direction = direction;
    }

    public void DisableNextBoost()
    {
        BoostIsActivate = false;
    }

    protected virtual void Start()
    {
        BALL.Add(this);

        rb = GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(XVelocity * Direction.GetValue(direction), 0);

        // On ajoute une force vers le haut pour plus de smooth

        Vector2 up = new Vector2(0, 1) * FORCE_Y_AXIS;
        rb.AddForce(up);

        BoostIsActivate = true;
    }

    // Lorsque la balle meurt, on la retire des balls restantes.
    protected override void OnDestroy()
    {
        base.OnDestroy();
        BALL.Remove(this);

        // On envoie un event pour signifier qu'une ball a été détruite.
        EventManager.Instance.Raise(new BallHasBeenDestroyedEvent());

        if (!IsGameOver && GameManager.Instance.IsPlaying)
        {
            PlayDeathSound();
            AddScore();
        }

    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.PLAYER_PROJECTILES) && !IsDestroyed && !collision.gameObject.GetComponent<PlayerProjectiles>().IsDestroyed())
        {
            IsDestroyed = true; // On indique que la ball est détruite

            // On détruit les éléments this & le projectile

            this.Kill();
            collision.gameObject.GetComponent<PlayerProjectiles>().Kill(); // On détruit le player projectile
        } else
        {
            // GESTION DU BOOST EN Y
            if (collision.gameObject.CompareTag(Tags.GROUND)) // Si la collision vient du sol.
            { // Si oui, le boost compte.
                if (BoostIsActivate) // Si la boule a besoin d'un coups de boost
                {
                    rb.velocity = new Vector2(rb.velocity.x, BOOST);
                }
                else // Sinon, on se contente de réactiver le boost.
                {
                    BoostIsActivate = true; // On active toujours le boost.
                }
            }

            // GESTION DE LA VITESSE CONSTANTE EN X

            if (rb.velocity.x > 0)
            {
                direction = Direction.DirectionValue.RIGHT;
            }
            else if (rb.velocity.x < 0)
            {
                direction = Direction.DirectionValue.LEFT;
            }

            rb.velocity = new Vector2(XVelocity * Direction.GetValue(direction), rb.velocity.y);
        }
    }

    protected List<GameObject> GetBonusObjectList()
    {
        return BonusObjectList;
    }

    protected GameObject GetBallsStep()
    {
        return BallsStep;
    }

    protected override void GameOver(GameOverEvent e)
    {
        base.GameOver(e);

        IsGameOver = true;
    }


    // Outils

    // Méthode a appelé pour détruire correctement une Ball. (Pour qu'elle puisse appliquer son avant mort).
    public abstract void Kill();

    protected abstract Transform GetBallStepAssociate();

    // Joue le son de mort de la balle.
    private void PlayDeathSound()
    {
        if (SfxManager.Instance) SfxManager.Instance.PlaySfx2D(SoundPlayOnDead);
    }

    private void AddScore()
    {
        // Ajout de point à la destruction du projectile
        EventManager.Instance.Raise(new ScoreItemEvent() { eScore = 20 });
    }
}
