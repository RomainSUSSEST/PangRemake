using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusWeapon : GameplayBonusObjects
{
    // Attributs

    [SerializeField] private GameObject Weapon;


    // Méthode
    protected override void ItemIsPickUp(GameObject Player) {
        Player.GetComponent<PlayerWeaponManager>().SetWeapon(Weapon);
    }
}
