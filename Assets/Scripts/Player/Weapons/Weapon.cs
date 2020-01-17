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


    // Méthode

    private void Start()
    {
        HudManager.Instance.SetWeaponIcon(Icone);
    }

    // Envoie un projectile
    public abstract void Shoot();

    // Indique si l'on peut tirer.
    public abstract bool CanShoot();
}
