using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultGrapnelProjectiles : GrapnelProjectiles
{
    public override void LimitHeightEnter()
    {
        Destroy(this.gameObject);
    }
}
