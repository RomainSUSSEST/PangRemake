using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    // Attributs

    [SerializeField] private GameObject ProjectilesPrefab;
    [SerializeField] private Transform SpawnProjectiles;

    private int MaxNbrGrapnel;
    private int CurrentNbrGrapnel;

    private Animator AnimPlayer;


    // Requête

    public int GetMaxNbrGrapnel()
    {
        return MaxNbrGrapnel;
    }


    // Méthode

    private void Start()
    {
        AnimPlayer = GetComponent<Animator>();

        // On initialise le nombre max de grappin à 1

        MaxNbrGrapnel = 1;
    }

    private void Update()
    {
        bool isShooting = Input.GetButtonDown("Fire1");

        if (isShooting && CurrentNbrGrapnel < MaxNbrGrapnel && !AnimPlayer.GetBool("IsShooting"))
        {
            AnimPlayer.SetTrigger("IsShooting");
        }
    }

    public void SetMaxNbrGrapnel(int max)
    {
        if (max > 0)
        {
            MaxNbrGrapnel = max;
        } else
        {
            Debug.LogError("SetMaxNbrGrapnel valeur incorrecte donnée.");
        }
    }


    // Appelé par evenement
    private void Shoot()
    {
        GameObject Grapnel = Instantiate(ProjectilesPrefab, SpawnProjectiles.transform.position, Quaternion.identity, null);
        Grapnel.GetComponent<GrapnelScript>().Initiate(KillGrapnel);

        CurrentNbrGrapnel += 1;
    }


    // Outils

    private void KillGrapnel()
    {
        CurrentNbrGrapnel -= 1;
    }
}
