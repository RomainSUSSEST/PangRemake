using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{

    // Attributs

    [SerializeField] private GameObject PlayerHandForWeapon;
    [SerializeField] private GameObject DefaultWeapon;
    private GameObject CurrentWeaponGameObject;
    private Weapon CurrentWeaponScript;

    private Animator AnimController;


    // Méthode

    private void Start()
    {
        AnimController = GetComponent<Animator>();
        SetWeapon(DefaultWeapon);
    }

    private void Update()
    {
        bool isShooting = Input.GetButtonDown("Fire1");

        if (isShooting && CurrentWeaponScript.CanShoot() && !AnimController.GetBool("IsShooting"))
        {
            AnimController.SetTrigger("IsShooting");
        }
    }

    // Instancie une copie de l'objet weapon dans la main du player.
    public void SetWeapon(GameObject weapon)
    {
        if (CurrentWeaponGameObject != null)
        {
            Destroy(CurrentWeaponGameObject);
        }

        CurrentWeaponGameObject = Instantiate(weapon, PlayerHandForWeapon.transform);
        CurrentWeaponScript = CurrentWeaponGameObject.GetComponent<Weapon>();
    }

    
    // Outils

        // Appelé par Animator.
    private void Shoot()
    {
        CurrentWeaponScript.Shoot();
    }
}
