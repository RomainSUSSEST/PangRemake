using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    // Attributs

    [SerializeField] private GameObject ProjectilesPrefab;
    [SerializeField] private Transform SpawnProjectiles;
    [SerializeField] private string SoundOnFire;
    [SerializeField] private Texture Icone;

    private GameObject WeaponBearer;


    // Requete

    protected GameObject GetProjectilesPrefab()
    {
        return ProjectilesPrefab;
    }

    protected Transform GetSpawnProjectiles()
    {
        return SpawnProjectiles;
    }

    protected string GetSoundOnFire()
    {
        return SoundOnFire;
    }

    protected GameObject GetWeaponBearer()
    {
        return WeaponBearer;
    }


    // Méthode

    protected virtual void Start()
    {
        HudManager.Instance.SetWeaponIcon(Icone);
    }

    protected virtual void SetSpawnProjectile(Transform SpawnProjectiles)
    {
        this.SpawnProjectiles = SpawnProjectiles;
    }

    // Envoie un projectile
    public abstract void Shoot();

    // Indique si l'on peut tirer.
    public abstract bool CanShoot();

    public void SetWeaponBearer(GameObject WeaponBearer)
    {
        this.WeaponBearer = WeaponBearer;
    }
}
