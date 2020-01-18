using SDD.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringBonusObject : BonusObject
{
    // Attributs

    [SerializeField] private int ScoringValue;


    // Méthode

    // Implémentation de BonusObject

    protected override void ItemIsPickUp(GameObject Player)
    {
        EventManager.Instance.Raise(new ScoreItemEvent { eScore = ScoringValue });
    }
}
