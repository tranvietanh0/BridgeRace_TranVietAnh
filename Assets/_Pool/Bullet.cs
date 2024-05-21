using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameUnit
{
    public Rigidbody rb;

    public void OnInit()
    {
        rb.velocity = Vector3.forward * 5f;
    }

    public void OnDespawn()
    {
        SimplePool.Despawn(this);
    }


    private void OnTriggerEnter(Collider other)
    {
        ParticlePool.Play(ParticleType.Hit, transform.position, Quaternion.identity);
        OnDespawn();
    }
}
