using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusDoubleGrapnel : GameplayBonusObjects
{
    // Attributs

    [SerializeField] private GameObject Weapon;


    #region
    [SerializeField] Texture m_TextureWeaponIcon;
    #endregion

    // Méthode
    protected override void ItemIsPickUp(GameObject Player) {
        Player.GetComponent<PlayerWeaponManager>().SetWeapon(Weapon);
        HudManager.Instance.m_WeaponIcon.texture = m_TextureWeaponIcon;
    }
}
