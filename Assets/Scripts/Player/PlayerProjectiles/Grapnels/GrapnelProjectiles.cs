using System.Collections;
using System.Collections.Generic;
using SDD.Events;
using UnityEngine;

public abstract class GrapnelProjectiles : PlayerProjectiles
{
    // Attributs

    [SerializeField] private float ExpansionSpeed;
    [SerializeField] private float RotationSpeed;

    private System.Action KillFunction;


    // Méthode

    public void SetKillFunction(System.Action KillFunction)
    {
        this.KillFunction = KillFunction;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + Time.deltaTime * ExpansionSpeed,
                transform.localScale.z);
        transform.localRotation = new Quaternion(0, transform.localRotation.y + Time.deltaTime * RotationSpeed, 0, 0);
    }

    // Permet de détruire proprement le Grapnel
    private void OnDestroy()
    {
        if (KillFunction == null)
        {
            Debug.LogError("Erreur, le projectile ne possède pas de fonction kill");
            return;
        }

        KillFunction();
    }

    protected void SetExpansionSpeed(float speed)
    {
        ExpansionSpeed = speed;
    }

    protected void SetRotationSpeed(float speed)
    {
        RotationSpeed = speed;
    }
}
