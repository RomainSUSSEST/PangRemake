using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeapon : Weapon
{
    // Attributs

    [SerializeField] private float SecondeShootingRecast; // En seconde le delai entre chaque tir possible.
    private float CmptShootingRecast;


    // Méthodes

    // Implémentation de Weapon
    public override bool CanShoot()
    {
        return CmptShootingRecast >= SecondeShootingRecast;
    }

    public override void Shoot()
    {
        CmptShootingRecast = 0;
        GameObject projectile = Instantiate(GetProjectilesPrefab(), GetSpawnProjectiles().position, Quaternion.identity, LevelManager.Instance.GetCurrentLevel());
        projectile.GetComponent<GunProjectile>().SetDirection(Direction.DirectionValue.LEFT);

        projectile = Instantiate(GetProjectilesPrefab(), GetSpawnProjectiles().position, Quaternion.identity, LevelManager.Instance.GetCurrentLevel());
        projectile.GetComponent<GunProjectile>().SetDirection(Direction.DirectionValue.RIGHT);


        if (SfxManager.Instance) SfxManager.Instance.PlaySfx2D(GetSoundOnFire());
    }

    private void Start()
    {
        CmptShootingRecast = SecondeShootingRecast;
    }

    // Update is called once per frame
    void Update()
    {
        if (CmptShootingRecast < SecondeShootingRecast)
        {
            CmptShootingRecast += Time.deltaTime;
        }
    }
}
