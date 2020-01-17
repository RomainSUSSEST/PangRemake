using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerProjectiles : MonoBehaviour
{
    // Attributs

    private bool Destroyed;


    // Requete

    public bool IsDestroyed()
    {
        return Destroyed;
    }


    // Méthode

    // Permet de tuer proprement le projectile et qu'il ne tue pas 2 ennemi en meme temps si sur
    // 2 frame les objets sont proches.
    public void Kill()
    {
        Destroy(this.gameObject);
        Destroyed = true;
    }
}
