using SDD.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPAndDefeat : MonoBehaviour
{

    // Attributs

    [SerializeField] private CapsuleCollider2D CapsuleCollider2DZAxis;

    [Header("Health Settings")]
    [SerializeField] private int DefaultHP = 1;
    [SerializeField] private GameObject EffectBonusHP; // Effet qui s'active ou se désactive selon les HP du joueurs.
    [SerializeField] private float InvincibilityTime;

    private Animator AnimPlayer;
    private int NbrHP;
    private int NewHP; // variable tampon permettant de stocker les nouveaux hp pour la coroutine.
    private bool IsInvincible; // Indique si le joueur est invincible.


    // Requete

        public int GetNbrHP()
        {
            return NbrHP;
        }


    // Méthode

    private void Start()
    {
        NbrHP = DefaultHP;
        AnimPlayer = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.Instance.IsPlaying && collision.gameObject.CompareTag(Tags.BALL) && !IsInvincible) // Si le joueur est invincible, on ne descend pas les hp.
        {
            SetNbrHP(GetNbrHP() - 1);
        }

        if (GameManager.Instance.IsPlaying && collision.gameObject.CompareTag(Tags.BALL) && NbrHP == 0)
        {
            AnimPlayer.applyRootMotion = true;
            AnimPlayer.SetBool("IsDead", true);

            // On détruit les capsuleCollider et le Rigidbody

            Destroy(GetComponent<PlayerController>());
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(GetComponent<CapsuleCollider2D>());
            Destroy(CapsuleCollider2DZAxis);

            EventManager.Instance.Raise(new APlayerIsDeadEvent());
        }
    }
    
    // Méthode permettant de fixer les hp du joueurs
    // Si ceci sont au dessus de 1, ajoute un effet visuel indiquant la double vie.
    // Gére l'invincibilité.
    public void SetNbrHP(int NewHP)
    {
        this.NewHP = NewHP;

        // Si les HP descendent, on fait pop l'invincivilité
        if (NbrHP > NewHP && NewHP > 0)
        {
            IsInvincible = true; // On indique que le joueur est invincible.

            // Pour ne pas faire n'importe quoi avec les boules ennemi, on change le layer pour désactiver les collision avec celle-ci.
            this.gameObject.layer = 13; // PlayerWithOutCollision
            CapsuleCollider2DZAxis.gameObject.layer = 13;

            StartCoroutine("Invincibility");
        } else if (NbrHP < NewHP && NewHP > 0) // Sinon, on active l'effet et on actualise les hp.
        {
            EffectBonusHP.SetActive(true);
            NbrHP = this.NewHP;
        } else
        {
            NbrHP = NewHP;
        }
    }

    
    // Outils

    IEnumerator Invincibility()
    {
        // rend le joueur invincible pour InvincivilityTime secondes.
        float tampon = InvincibilityTime / 5; // Il y aura 5 alertes (clignotement)
        float cmpt = 0;

        while (cmpt < InvincibilityTime)
        {
            if (EffectBonusHP.activeSelf)
            {
                EffectBonusHP.SetActive(false);
            } else
            {
                EffectBonusHP.SetActive(true);
            }

            yield return new WaitForSeconds(tampon);
            cmpt += tampon;
        }

        // Une fois le temps d'invincibilité passé, on passe les hp à 1.

        NbrHP = this.NewHP;

        // Si les hp sont supérieurs à 1, on active l'effet
        if (NbrHP > 1)
        {
            EffectBonusHP.SetActive(true);
        } else // Sinon on le désactive.
        {
            EffectBonusHP.SetActive(false);
        }

        IsInvincible = false; // L'invincibilité est terminé.
        // On réactive les collisions avec les ennemis.
        this.gameObject.layer = 8; // PlayerWithCollision
        CapsuleCollider2DZAxis.gameObject.layer = 8;
    }
}
