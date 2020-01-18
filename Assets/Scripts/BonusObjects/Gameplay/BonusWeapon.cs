using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusWeapon : BonusObject
{
    // Attributs

    [SerializeField] private GameObject Weapon;
    [SerializeField] private string SoundChangeWeapon;


    // Méthode
    protected override void ItemIsPickUp(GameObject Player) {
        Player.GetComponent<PlayerWeaponManager>().SetWeapon(Weapon);

        if (SfxManager.Instance) SfxManager.Instance.PlaySfx2D(SoundChangeWeapon);
    }
}
