using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    [Header("BulletPrefs")]
    public float Damage = 3f;

    public string PoolTag = "EnemyBullets";

    void OnCollisionEnter (Collision c)
    {
        var obj = c.gameObject;

        if (obj.CompareTag("Player"))
            if (!PlayerStatsController.instance.Forced && !obj.GetComponent<TimeBody>().IsRewinding())
            obj.GetComponent<PlayerStatsController>().ChangeHP((int)-Damage);

        ObjectPooler.instance.ReturnToPool(gameObject, PoolTag);
        ObjectPooler.instance.SpawnFromPool("SmallExplosionEffect", c.contacts[0].point + c.contacts[0].point.normalized * 1f, Quaternion.Euler(c.contacts[0].normal));
    }
}
