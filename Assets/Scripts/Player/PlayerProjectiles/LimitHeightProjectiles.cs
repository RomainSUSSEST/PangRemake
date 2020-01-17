using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitHeightProjectiles : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance.IsPlaying && collision.gameObject.CompareTag(Tags.PLAYER_PROJECTILES))
        {
            Destroy(collision.gameObject);
        }
    }
}
