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
        if (!IsGrapnelFixe)
        {
            FixeGrapnel();
        }
    }

    public override void UndestructibleWallCollision()
    {
        if (!IsGrapnelFixe)
        {
            BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
            // On descend un peu le box collider pour ne pas qu'il reste en collision avec le mur est ainsi éviter un bug.
            boxCollider2D.offset = new Vector2(boxCollider2D.offset.x, boxCollider2D.offset.y - 0.2f);
            boxCollider2D.size = new Vector2(boxCollider2D.size.x, boxCollider2D.size.y - 0.2f);
            FixeGrapnel();
        }
    }


    // Outils

    // Arrete l'expansion du grapnel et le fait disparaitre au bout d'un certain temps.
    private void FixeGrapnel()
    {
        IsGrapnelFixe = true;
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
