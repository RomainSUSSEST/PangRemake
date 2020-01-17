using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TightsGrapnelProjectiles : GrapnelProjectiles
{
    // Attributs

    [SerializeField] private float ImmobilityDelai;
    private bool IsGrapnelFixe;


    // Méthode

    public override void LimitHeightEnter()
    {
        FixeGrapnel();
    }


    // Outils

    // Arrete l'expansion du grapnel et le fait disparaitre au bout d'un certain temps.
    private void FixeGrapnel()
    {
        SetExpansionSpeed(0);
        StartCoroutine("KillProjectile");
    }

    private IEnumerator KillProjectile()
    {
        yield return new WaitForSeconds(ImmobilityDelai);

        if (!IsDestroyed())
        {
            Kill();
        }
    }
}
