using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBulletScript : MonoBehaviour, IPooledObject
{
    [Header ("Bullet Settings")]

    [Range(0, 20)] public float Damage = 10f;

    [Range(.1f, 5)] public float DamageRadius = 2f;

    public GameObject BigExplosionPref;

    public string PoolTag;

    private float explosionForce = 2f;

    public void OnObjectSpawn()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player")) return;
        
        var colliders = Physics.OverlapSphere(col.contacts[0].point, DamageRadius);
        var s = col.gameObject;
        if (s.CompareTag("Spawner"))
        {
            Instantiate(BigExplosionPref, transform.position, Quaternion.identity);
            Destroy(s);
        }
        foreach (Collider hit in colliders)
        {
            var rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(Random.Range(0.9f * explosionForce, 1.1f * explosionForce), col.contacts[0].point, DamageRadius, 0, ForceMode.Impulse);
                var enemy = hit.GetComponent<EnemyAIScript>();
                if (enemy !=null)
                enemy.MarkDelayedForces();
            }
            var enHealth = hit.GetComponent<EnemyHealthController>();

            if (enHealth != null)
            {
                enHealth.ReceiveDamage((int)(Damage + Random.Range(-Damage / 10, Damage / 10) * 
                    1 - (Vector3.Distance(transform.position, hit.gameObject.transform.position)/DamageRadius)));
            }
        }
        ObjectPooler.instance.ReturnToPool(gameObject, PoolTag);
        ObjectPooler.instance.SpawnFromPool("SmallExplosionEffect", col.contacts[0].point, Quaternion.Euler(col.contacts[0].normal));
    }
}
