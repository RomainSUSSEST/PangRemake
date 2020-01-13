using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusDoubleGrapnel : GameplayBonusObjects
{
    #region
    [SerializeField] Texture m_TextureWeaponIcon;
    #endregion

    // Méthode
    protected override void ItemIsPickUp(GameObject Player) {
        Player.GetComponent<Shooting>().SetMaxNbrGrapnel(2);
        HudManager.Instance.m_WeaponIcon.texture = m_TextureWeaponIcon;
    }
}
