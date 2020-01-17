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

    // Permet de tuer proprement le projectile
    public void Kill()
    {
        Destroy(this.gameObject);
        Destroyed = true;
    }
}
