using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusDynamite : GameplayBonusObjects
{
    // Méthode
    protected override void ItemIsPickUp(GameObject Player)
    {
        Ball.GetAllBall().ForEach(x =>
        {
            Debug.Log(x);
            x.Kill();
        });
    }
}
