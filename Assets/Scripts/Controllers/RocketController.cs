using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    private Rigidbody _rb;
    [Header("MoveParams")]

    public float Speed;
    private int _cursorPosX;
    private int _cursorPosY;

    public float Damage = 80;
    public float DamageRadius = 6;

    public GameObject ExplosionFX;

    private TimeController _timeCtrl;

    void Start()
    {
        _timeCtrl = TimeController.instance;
        _timeCtrl.EnableBulletTime();
        CameraController.instance.RocketObserve(gameObject);
        _rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision col)
    {
        var colliders = Physics.OverlapSphere(transform.position, DamageRadius, ~14);
        foreach (var hit in colliders)
        {
            var enemy = hit.GetComponent<EnemyHealthController>();

            if (enemy != null)
            {
                enemy.ReceiveDamage((int)(Damage + Random.Range(-Damage / 10, Damage / 10) * 1 - (Vector3.Distance(transform.position, hit.gameObject.transform.position) / DamageRadius)));
            }
        }
        Instantiate(ExplosionFX, transform.position, Quaternion.identity);
        CameraController.instance.DefaultObserve();
        Destroy(gameObject, .5f);
        OnDeath();
    }

    void OnDeath()
    {
        _timeCtrl.DisableBulletTime();
        CameraController.instance.DefaultObserve();
    }

    void FixedUpdate()
    {
        _rb.AddForce(new Vector3(0, -2 * Speed * 400, 0) * Time.fixedDeltaTime);
    }
}
