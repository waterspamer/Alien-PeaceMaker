using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIScript : MonoBehaviour
{
    private NavMeshAgent _agent;
    private GameObject Player;
    private Rigidbody rb;

    [Header("Attributes")]

    [Range(0, 50f)]

    public float Range = 15f;

    public float FireRate = 10f;

    public float BulletSpeed = 3f;

    private float fireCountdown = 0f;

    public GameObject bulletPref;

    public Transform[] FirePoints;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Player = GameObject.FindGameObjectWithTag("Player");
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(Player.transform.position);
        _agent.Move(_agent.destination);
    }


    IEnumerator NullForces()
    {
        yield return new WaitForSeconds(.2f);
        rb.Sleep();
        StopCoroutine(NullForces());
    }

    void Shoot(GameObject obj)
    {
        foreach (var firePoint in FirePoints)
        {
            var bulletGO = ObjectPooler.instance.SpawnFromPool("EnemyBullets", firePoint.position, Quaternion.identity);
            var bul = bulletGO.GetComponent<EnemyBulletScript>();
            if (bul != null)
            {
                bulletGO.GetComponent<Rigidbody>().AddForce((Player.transform.position - firePoint.position).normalized * BulletSpeed * 100);
            }
        }  
    }

    private bool ContainsWithTag(RaycastHit[] rh, string tag)
    {
        foreach (var item in rh)
        {
            if (item.transform.CompareTag(tag)) return true;
        }
        return false;
    }

    private bool CheckForShootingObstacle()
    {
        Debug.DrawLine(transform.position, Player.transform.position);
        return ContainsWithTag(Physics.RaycastAll(transform.position, Player.transform.position, 
            Vector3.Distance(transform.position, Player.transform.position)),("Obstacle"));
    }

    public void MarkDelayedForces()
    {
        StartCoroutine(NullForces());
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) < 8f)
        {
            if (fireCountdown <= 0f)
            {
                if (!CheckForShootingObstacle()) Shoot(Player);
                if (FireRate >= 0) fireCountdown = 1f / FireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
        _agent.SetDestination(Player.transform.position);
    }
}
