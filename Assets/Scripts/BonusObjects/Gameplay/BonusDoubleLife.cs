using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusDoubleLife : BonusObject
{
    // Constante

    private static readonly int BONUS_HP = 2;


    // Attributs

    [SerializeField] private int RotationSpeed;


    // Méthode
    protected override void ItemIsPickUp(GameObject Player)
    {
        Player.GetComponent<PlayerHPAndDefeat>().SetNbrHP(BONUS_HP);
    }

    // On fait tourner l'objet.
    private void Update()
    {
        GetGfx().transform.Rotate(transform.rotation.x, transform.rotation.y + RotationSpeed * Time.deltaTime, transform.rotation.z);
    }
}
