using System.Collections;
using System.Collections.Generic;
using SDD.Events;
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

    // Permet de tuer proprement le projectile

    // Méthode

    // Permet de tuer proprement le projectile et qu'il ne tue pas 2 ennemi en meme temps si sur
    // 2 frame les objets sont proches.
    public virtual void Kill()
    {
        Destroy(this.gameObject);
        Destroyed = true;
    }

    public abstract void LimitHeightEnter();

    // Que faire si le projectile rentre en collision avec un mur indestructible ?
    public abstract void UndestructibleWallCollision();
}
