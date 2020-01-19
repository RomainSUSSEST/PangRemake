using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnDestructibleWall : MonoBehaviour
{
    // Méthode

    // Lorsqu'une collision est détecté...
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si l'on rentre en collision avec un player projectile, on le détruit
        if (GameManager.Instance.IsPlaying && collision.gameObject.CompareTag(Tags.PLAYER_PROJECTILES) && !collision.gameObject.GetComponent<PlayerProjectiles>().IsDestroyed())
        {
            collision.gameObject.GetComponent<PlayerProjectiles>().UndestructibleWallCollision();
        }
    }
}
