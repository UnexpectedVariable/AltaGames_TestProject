using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public void Subscribe(Bullet bullet)
    {
        bullet.CollisionEvent += HandleBulletCollision;
    }

    public void HandleBulletCollision(object sender, CollisionEventArgs args)
    {
        Debug.Log("Bullet collision event invoked");
        //make an explosion
        //destroy bullet
        //destroy obstacles
    }
}
