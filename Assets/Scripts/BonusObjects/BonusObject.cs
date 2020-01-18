using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BonusObject : MonoBehaviour
{
    // Attributs

    [SerializeField] int m_LifeTime;
    [SerializeField] private GameObject gfx;
    private bool m_Destroyed;


    // Requete

    protected GameObject GetGfx()
    {
        return gfx;
    }


    // Méthodes

    private void Start()
    {
        StartCoroutine("KillBonusObject");
    }

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


    // Outils

    IEnumerator KillBonusObject()
    {
        yield return new WaitForSeconds(m_LifeTime * 7 / 10); // On Attend la majeur partie du temps.

        if (!m_Destroyed) // Si l'objet n'est pas détruit.
        {
            float tampon = m_LifeTime * 0.25f / 10;
            int cmpt = 12; // On fait clignoter 6 fois
            while (cmpt > 0 && !m_Destroyed) // Tans que cmpt > 0
            {
                if (gfx.activeSelf) // Si le gfx est activé, on le désactive et inversement
                {
                    gfx.SetActive(false);
                }
                else
                {
                    gfx.SetActive(true);
                }

                yield return new WaitForSeconds(tampon); // On attend un peu

                --cmpt;
            }

            if (!m_Destroyed)
            {
                m_Destroyed = true;
                Destroy(this.gameObject);
            }
        }
    }
}
