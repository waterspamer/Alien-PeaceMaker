using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public GameObject Gun;

    public GameObject ShootEffect;

    [Header("Attributes")]

    [Range(0, 50f)]

    public float range = 15f;

    public int RocketEnergyCost = 50;

    public float fireRate = 10f;

    public float BulletSpeed = 3f;

    private float fireCountdown = 0f;

    [Range(0, 20)]

    public int gunRotSpeed = 15;

    public Transform partToRotate;

    public GameObject bulletPref;

    public GameObject MarkDecal;

    public Transform firePoint;

    private Vector3 target;

    public Camera cam;

    private Vector3 hitPoint;

    public GameObject RocketPrefab;

    private bool _canUseRocket = true;

    [Header("Unity Setups")]

    private PlayerStatsController inst;

    public float rotSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        hitPoint = new Vector3();
        inst = PlayerStatsController.instance;
    }

    void Shoot()
    {
        var bulletGO = ObjectPooler.instance.SpawnFromPool("BasicBullets", firePoint.position, Quaternion.identity);
        var bul = bulletGO.GetComponent<BasicBulletScript>();
        if (bul != null)
        {
            bulletGO.GetComponent<Rigidbody>().AddForce( (target - firePoint.position).normalized * BulletSpeed * 100);
        }

    }

    IEnumerator RocketCoolDown()
    {
        _canUseRocket = false;
        yield return new WaitForSeconds(10f);
        _canUseRocket = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inst.IsEnoughEnergy(RocketEnergyCost) && _canUseRocket)
            {
                inst.ChangeEnergy(-RocketEnergyCost);
                Instantiate(RocketPrefab, MarkDecal.transform.position + new Vector3(0, 5, 0), Quaternion.Euler(0, 0, 90));
                StartCoroutine(RocketCoolDown());
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            AudioManager.instance.PlayMGSound();
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            AudioManager.instance.StopMGSound();
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Gun.transform.Rotate(new Vector3(0, 0, 1), gunRotSpeed, Space.Self);
            if (fireCountdown <= 0f)
            {
                Shoot();
                if (fireRate >= 0) fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }

        partToRotate.transform.localPosition = new Vector3(0, partToRotate.transform.localPosition.y, 0);
        if (!partToRotate.GetComponent<TimeBody>().IsRewinding())
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            var hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                hitPoint = hit.point;
                target = hit.point;
                MarkDecal.transform.position = target + target.normalized * .1f ;
                MarkDecal.transform.rotation = Quaternion.LookRotation(-hit.normal);
                var dir = target - transform.position;
                var lookRot = Quaternion.LookRotation(dir);
                var rotation = Quaternion.Lerp(partToRotate.rotation, lookRot, Time.deltaTime * rotSpeed).eulerAngles;
                partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            }
        }
    }
}
