using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleWall : MonoBehaviour
{
    // Attributs

    private bool IsDestroyed;


    // Méthode

    // Lorsqu'une collision est détecté...
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si l'on rentre en collision avec un player projectile, on le détruit this et le projectile
        if (GameManager.Instance.IsPlaying && !IsDestroyed && collision.gameObject.CompareTag(Tags.PLAYER_PROJECTILES) && !collision.gameObject.GetComponent<PlayerProjectiles>().IsDestroyed())
        {
            IsDestroyed = true;
            Destroy(this.gameObject);
            collision.gameObject.GetComponent<PlayerProjectiles>().Kill();
        } else if (IsDestroyed)
        {
            // Tous les objets entrant en collision alors que l'objet est déjà détruit, on leur redonne leur élan passé.

            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = collision.relativeVelocity;
            }
        }
    }
}
