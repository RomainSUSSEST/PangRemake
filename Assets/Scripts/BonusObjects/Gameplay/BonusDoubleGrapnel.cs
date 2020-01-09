using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusDoubleGrapnel : GameplayBonusObjects
{
    // Méthode

    protected override void ItemIsPickUp(GameObject Player) {
        Player.GetComponent<Shooting>().SetMaxNbrGrapnel(2);
    }
}
