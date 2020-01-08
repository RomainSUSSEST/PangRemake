using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameplayBonusObjects : MonoBehaviour
{
    // Attributs

    private bool m_Destroyed;

    // Méthodes
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.Instance.IsPlaying && !m_Destroyed && collision.gameObject.CompareTag(Tags.PLAYER))
        {
            m_Destroyed = true;
            Destroy(this.gameObject);

            ItemIsPickUp(collision.gameObject);
        }
    }

    // Fonction à implémenter, permettant de réaliser l'action de l'objet bonus.
    protected abstract void ItemIsPickUp(GameObject Player);
}
