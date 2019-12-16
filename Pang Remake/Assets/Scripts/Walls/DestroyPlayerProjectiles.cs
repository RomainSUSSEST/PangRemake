using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlayerProjectiles : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.PLAYER_PROJECTILES))
        {
            Destroy(collision.gameObject);
        }
    }
}
