using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    // Attributs

    [SerializeField] private GameObject ProjectilesPrefab;
    [SerializeField] private Transform SpawnProjectiles;
    [SerializeField] private float ShootingRecast; // En seconde
    [SerializeField] private float ProjectilesInitialSpeed;

    private float cmptRecast;


    // Méthode

    private void Update()
    {
        // On incrémente cmpt recast de Time.DeltaTime pour
        // avoir un recast indépendant de la puissance de la machine et en seconde.
        cmptRecast += Time.deltaTime;
    }
    private void FixedUpdate()
    {
        bool isShooting = Input.GetAxis("Fire1") == 1;

        if (isShooting && cmptRecast >= ShootingRecast)
        {
            Shoot();
        }
    }


    // Outils

    private void Shoot()
    {
        GameObject Projectile = Instantiate(ProjectilesPrefab, SpawnProjectiles.transform.position, Quaternion.identity, null);
        Rigidbody2D ProjectileRb = Projectile.GetComponent<Rigidbody2D>();
        ProjectileRb.velocity = ProjectilesInitialSpeed * SpawnProjectiles.up;

        // On réinitialise le recast
        cmptRecast = 0;
    }
}
