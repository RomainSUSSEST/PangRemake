using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public class BonusObjects : MonoBehaviour
{
    [SerializeField] int m_ObjectValue;
    [SerializeField] int m_LifeTime;
    protected bool m_Destroyed = false;

    private void Start()
    {
        Destroy(gameObject, m_LifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collison)
    {
        Debug.Log("On trigger");
        if (GameManager.Instance.IsPlaying && !m_Destroyed && collison.gameObject.CompareTag("Player"))
        {
            m_Destroyed = true;

            EventManager.Instance.Raise(new ScoreItemEvent() { eScore = m_ObjectValue });
            Destroy(this.gameObject);
        }

        /*if (collision.gameObject.CompareTag("BonusObject"))
        {
            Destroy(this.gameObject);
        }*/
    }
}
