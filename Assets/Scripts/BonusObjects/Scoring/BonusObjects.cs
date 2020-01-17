using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public class BonusObjects : MonoBehaviour
{
    [SerializeField] float m_ObjectValue;
    [SerializeField] int m_LifeTime;
    protected bool m_Destroyed = false;

    private void Start()
    {
        Destroy(gameObject, m_LifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collison)
    {
        if (GameManager.Instance.IsPlaying && !m_Destroyed && collison.gameObject.CompareTag(Tags.PLAYER))
        {
            m_Destroyed = true;
            Destroy(this.gameObject);

            // Ajout de points à la destruction d'un objet, ici une balle
            EventManager.Instance.Raise(new ScoreItemEvent() { eScore = m_ObjectValue });
        }
    }
}
