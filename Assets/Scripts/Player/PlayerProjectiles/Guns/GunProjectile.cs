using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunProjectile : PlayerProjectiles
{
    // Attributs

    [SerializeField] private Vector2 ProjectileSpeed;
    private Direction.DirectionValue direction = Direction.DirectionValue.NO;


    // Méthode

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(ProjectileSpeed.x * Direction.GetValue(direction), ProjectileSpeed.y);
    }

    public void SetDirection(Direction.DirectionValue direction)
    {
        this.direction = direction;
    }
}
