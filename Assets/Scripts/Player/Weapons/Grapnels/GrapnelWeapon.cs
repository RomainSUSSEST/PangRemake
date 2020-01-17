using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapnelWeapon : Weapon
{
    // Attributs

    [SerializeField] private int MaxNbrGrapnel = 1;
    private int CurrentNbrGrapnel;
    private Transform SpawnProjectiles;


    // Requête

    public int GetMaxNbrGrapnel()
    {
        return MaxNbrGrapnel;
    }


    // Méthodes

    protected override void Start()
    {
        base.Start();

        // On récupére le transform du player auquel on est attaché.
        SpawnProjectiles = transform.root;
    }

    // Implentation de Weapon
    public override void Shoot()
    {
        GameObject Grapnel = Instantiate(GetProjectilesPrefab(), SpawnProjectiles.position, Quaternion.identity, LevelManager.Instance.GetCurrentLevel());
        Grapnel.GetComponent<GrapnelProjectile>().Initiate(KillGrapnel);

        if (SfxManager.Instance) SfxManager.Instance.PlaySfx2D(GetSoundOnFire());

        CurrentNbrGrapnel += 1;
    }

    public override bool CanShoot()
    {
        return CurrentNbrGrapnel < GetMaxNbrGrapnel();
    }

 
    // Outils

    private void KillGrapnel()
    {
        CurrentNbrGrapnel -= 1;
    }
}
