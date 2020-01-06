using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapnelScript : MonoBehaviour
{
    // Attributs

    [SerializeField] private float ExpansionSpeed;
    [SerializeField] private float RotationSpeed;


    // Méthode

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + Time.deltaTime * ExpansionSpeed,
                transform.localScale.z);
        transform.localRotation = new Quaternion(0, transform.localRotation.y + Time.deltaTime * RotationSpeed, 0, 0);
    }
}
